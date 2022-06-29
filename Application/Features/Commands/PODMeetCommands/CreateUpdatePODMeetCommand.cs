using MediatR;
using Domain.Entities;
using Application.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using static Domain.CommonCodes.CommonEnums;
using Application.ApiModels;
using System;
using System.Linq;
using System.Collections.Generic;
using Application.Services;

namespace Application.Features.Commands.PODMeetCommands
{

    public class CreateUpdatePODMeetCommand : IRequest<PODMeetDetailsApiModel>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string MeetDescription { get; set; }
        public DateTime MeetTiming { get; set; }
        public DateTime MeetDate { get; set; }
        public string MeetPlace { get; set; }
        public int County { get; set; }
        public bool IsChatAllowed { get; set; }
        public int UpdatedBy { get; set; }
        public int CreatedBy { get; set; }
        public List<int> PODIds { get; set; }

            
        public class CreateUpdatePODMeetCommandHandler : IRequestHandler<CreateUpdatePODMeetCommand, PODMeetDetailsApiModel>
        {
            private readonly IApplicationDbContext _context;
            public CreateUpdatePODMeetCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<PODMeetDetailsApiModel> Handle(CreateUpdatePODMeetCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    PODMeetDetails dbModel = new PODMeetDetails();
                    PODMeetDetailsApiModel apiModel = new PODMeetDetailsApiModel();

                    string notificationTitle = string.Empty;
                    string notificationDescription = string.Empty;
                    ///Notifiation for users
                    NotificationFCMApiModel notification = _context.notifications
                    .Where(y => y.NotificationTypeId == NotificationTypes.AddedTo)
                    .Select(x => new NotificationFCMApiModel
                    {
                        Title = x.Title,
                        Description = x.Description,
                        NotificationTypeId = x.NotificationTypeId,
                        Id = x.Id
                    }).FirstOrDefault();
                    ///Notifiation for admin
                    NotificationFCMApiModel notificationAdm = _context.notifications
                       .Where(y => y.NotificationTypeId == NotificationTypes.PODMeetCreated)
                       .Select(x => new NotificationFCMApiModel
                       {
                           Title = x.Title,
                           Description = x.Description,
                           NotificationTypeId = x.NotificationTypeId,
                           Id = x.Id
                       }).FirstOrDefault();
                    string username = _context.userDetails.Where(y => y.Id == command.CreatedBy).Select(x => x.Username).FirstOrDefault();
                    if (string.IsNullOrEmpty(Convert.ToString(command.Id)) || command.Id == 0)
                    {
                        dbModel.Title = command.Title;
                        dbModel.MeetDescription = command.MeetDescription;
                        dbModel.MeetPlace = command.MeetPlace;
                        dbModel.MeetDate = command.MeetDate;
                        dbModel.MeetTiming = command.MeetTiming;
                        dbModel.County = command.County;
                        dbModel.UpdatedBy = command.UpdatedBy;
                        dbModel.CreatedBy = command.CreatedBy;
                        dbModel.IsChatAllowed = command.IsChatAllowed;
                        _context.podMeetDetails.Add(dbModel);
                        await _context.SaveChanges();
                    }
                    else
                    {
                        dbModel = new PODMeetDetails();
                        dbModel = _context.podMeetDetails.Where(m => m.Id == command.Id).FirstOrDefault();
                        dbModel.Title = command.Title;
                        dbModel.MeetDescription = command.MeetDescription;
                        dbModel.MeetPlace = command.MeetPlace;
                        dbModel.MeetDate = command.MeetDate;
                        dbModel.MeetTiming = command.MeetTiming;
                        dbModel.County = command.County;
                        dbModel.UpdatedOn = DateTime.UtcNow;
                        dbModel.UpdatedBy = command.UpdatedBy;
                        dbModel.IsChatAllowed = command.IsChatAllowed;
                        await _context.SaveChanges();
                    }

                    apiModel.Id = dbModel.Id;
                    apiModel.Title = dbModel.Title;
                    apiModel.MeetDescription = dbModel.MeetDescription;
                    apiModel.MeetPlace = dbModel.MeetPlace;
                    apiModel.MeetTiming = dbModel.MeetTiming;
                    apiModel.MeetDate = dbModel.MeetDate;
                    apiModel.County = dbModel.County;
                    apiModel.CountyName = _context.counties.Where(y => y.Fips == dbModel.County).Select(x => x.CountyName).FirstOrDefault();
                    apiModel.IsChatAllowed = dbModel.IsChatAllowed;
                    apiModel.CreatedBy = dbModel.CreatedBy;
                    apiModel.CreatedOn = dbModel.CreatedOn;
                    apiModel.UpdatedBy = dbModel.UpdatedBy;
                    apiModel.UpdatedOn = dbModel.UpdatedOn;

                    if (command.PODIds != null)
                    {
                        int bubbleIdK = _context.bubbleMembers.Where(y => y.UserId == command.CreatedBy).Select(x => x.BubbleId).FirstOrDefault();
                            command.PODIds.Add(bubbleIdK);
                          
                        foreach (var bubbleId in command.PODIds)
                        {
                            BubbleApiModel bubbleApiModel = new BubbleApiModel();
                            int podId = _context.podMembers.Where(ya => ya.BubbleId == bubbleId).Select(xa => xa.PODId).FirstOrDefault();
                            var lstUserIds = _context.bubbleMembers.Where(y => y.BubbleId == bubbleId).Select(x => x.UserId).Distinct().ToList();
                            
                            foreach (var userId in lstUserIds)  
                            {
                                PODMeetMembers podMeetMembers = new PODMeetMembers();
                                podMeetMembers.PODMeetId = dbModel.Id;
                                podMeetMembers.PODId = podId;
                                podMeetMembers.BubbleId = bubbleId;
                                podMeetMembers.UserId = userId;
                                podMeetMembers.CreatedBy = command.CreatedBy;
                                   podMeetMembers.UpdatedBy = command.UpdatedBy;
                                _context.podMeetMembers.Add(podMeetMembers);
                                await _context.SaveChanges();
                                    
                                notificationTitle = (userId == command.CreatedBy) ? notificationAdm.Title + " " + command.Title : notification.Title + " " + command.Title;
                                notificationDescription = (userId == command.CreatedBy) ? notificationAdm.Description + " " + command.Title : notification.Description + " " + command.Title + " by " + username;

                                NotificationsServices notificationsServices = new NotificationsServices(_context);
                                await notificationsServices.SendNotification(notificationTitle, notificationDescription, notification.Id, dbModel.Id, userId, command.CreatedBy, command.UpdatedBy, NotificationTypeChild.PODMeetNotification, NotificationCategories.General);
                            }
                            bubbleApiModel = _context.bubbleDetails
                                         .Join(_context.bubbleMembers, bd => bd.Id, pmd => pmd.BubbleId, (bd, pmd) => new { bd, pmd })
                                         .Where(qy => qy.pmd.BubbleId == bubbleId)
                                         .Select(xpd => new BubbleApiModel
                                         {
                                             Id = xpd.bd.Id,
                                             BubbleName = xpd.bd.BubbleName,
                                             BubbleDescription = xpd.bd.BubbleDescription,
                                             BubbleSize = xpd.bd.BubbleSize,
                                             BubbleType = xpd.bd.BubbleType,
                                             BubbleValidity = xpd.bd.BubbleValidity,
                                             BubbleZipCode = xpd.bd.BubbleZipCode,
                                             IsOtherCountyMemberAllowed = xpd.bd.IsOtherCountyMemberAllowed,
                                             CreatedBy = xpd.bd.CreatedBy,
                                             UpdatedBy = xpd.bd.UpdatedBy,
                                             UpdatedOn = xpd.bd.UpdatedOn,
                                             CreatedOn = xpd.bd.CreatedOn,
                                             lstPodUser = _context.userDetails
                                             .Join(_context.bubbleMembers, udp => udp.Id, bmp => bmp.UserId, (udp, bmp) => new { udp, bmp })
                                             .Where(w => w.bmp.BubbleId == xpd.bd.Id && w.udp.IsActive == true)
                                                          .Select(uda => new UserApiModels
                                                          {
                                                              Id = uda.udp.Id,
                                                              Username = uda.udp.Username,
                                                              County = uda.udp.County,
                                                              CountyName = _context.counties.Where(y => y.Fips == uda.udp.County).Select(x => x.CountyName).FirstOrDefault(),
                                                              ProfilePicUrl = uda.udp.ProfilePicUrl,
                                                              PhoneNo = uda.udp.PhoneNo,
                                                              IsAdmin = (_context.bubbleMeetMemberPermissions
                                                                     .Where(p => p.BubbleMeetId == dbModel.Id && p.UserId == uda.udp.Id && p.UserPermissionTypeId == 0 && p.MeetTypeId == MeetType.PODMeet && p.UserPermissionStatus == true)
                                                                         .Select(x => x.UserPermissionStatus)
                                                                         .FirstOrDefault())
                                                          }).ToList()
                                         }).FirstOrDefault();
                            apiModel.lstbubbles.Add(bubbleApiModel);
                        }
                        //foreach (var bubbleIdP in command.PODIds)
                        //{
                            if (_context.bubbleMeetMemberPermissions.Where(yp => yp.BubbleMeetId == bubbleIdK && yp.MeetTypeId == MeetType.Bubble && yp.UserPermissionTypeId == UserPermission.IsAdmin && yp.UserPermissionStatus == true).Count() > 0)
                            {
                                List<int> lstAdmins = _context.bubbleMeetMemberPermissions.Where(ya => ya.BubbleMeetId == bubbleIdK && ya.UserPermissionTypeId == UserPermission.IsAdmin && ya.MeetTypeId == MeetType.Bubble && ya.UserPermissionStatus == true).Select(xa => xa.UserId).ToList();
                                foreach (var admin in lstAdmins)
                                {
                                    BubbleMeetMemberPermissions permission = new BubbleMeetMemberPermissions();
                                    permission.BubbleMeetId = dbModel.Id;
                                    permission.UserId = admin;
                                    permission.UserPermissionTypeId = UserPermission.IsAdmin;
                                    permission.UserPermissionStatus = true;
                                    permission.MeetTypeId = MeetType.PODMeet;
                                    permission.CreatedBy = command.CreatedBy;
                                    permission.UpdatedBy = command.UpdatedBy;
                                    _context.bubbleMeetMemberPermissions.Add(permission);
                                    await _context.SaveChanges();
                                }
                            }
                       // }
                    }
                    apiModel.IsAdmin = (_context.bubbleMeetMemberPermissions
                                                                 .Where(p => p.BubbleMeetId == dbModel.Id && p.UserId == command.CreatedBy && p.UserPermissionTypeId == 0 && p.MeetTypeId == MeetType.PODMeet && p.UserPermissionStatus == true)
                                                                     .Select(x => x.UserPermissionStatus)
                                                                     .FirstOrDefault());
                    return apiModel;
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
    }
}
