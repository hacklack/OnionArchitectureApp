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

namespace Application.Features.Commands.BubbleMeetCommands
{

    public class CreateUpdateBubbleMeetCommand : IRequest<BubbleMeetDetailsApiModel>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        //public List<BubbleApiModel> ChooseBubble { get; set; }
        public string MeetDescription { get; set; }
        public DateTime MeetTiming { get; set; }
        public DateTime MeetDate { get; set; }
        public string MeetPlace { get; set; }
        public int County { get; set; }
        public bool IsChatAllowed { get; set; }
        public int UpdatedBy { get; set; }
        public int CreatedBy { get; set; }
        public List<int> UserIds { get; set; }
        public BubbleType BubbleType { get; set; }

        public class CreateUpdateBubbleMeetCommandHandler : IRequestHandler<CreateUpdateBubbleMeetCommand, BubbleMeetDetailsApiModel>
        {
            private readonly IApplicationDbContext _context;
            public CreateUpdateBubbleMeetCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<BubbleMeetDetailsApiModel> Handle(CreateUpdateBubbleMeetCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    BubbleSafetyDetails dbSaftyModel = new BubbleSafetyDetails();
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
                       .Where(y => y.NotificationTypeId == NotificationTypes.BubbleMeetCreated)
                       .Select(x => new NotificationFCMApiModel
                       {
                           Title = x.Title,
                           Description = x.Description,
                           NotificationTypeId = x.NotificationTypeId,
                           Id = x.Id
                       }).FirstOrDefault();

                    string username = _context.userDetails.Where(y => y.Id == command.CreatedBy).Select(x => x.Username).FirstOrDefault();
                    BubbleMeetDetails dbModel = new BubbleMeetDetails();
                    BubbleMeetDetailsApiModel apiModel = new BubbleMeetDetailsApiModel();
                    dbModel.Title = command.Title;
                    dbModel.MeetDescription = command.MeetDescription;
                    dbModel.MeetPlace = command.MeetPlace;
                    dbModel.MeetDate = command.MeetDate;
                    dbModel.MeetTiming = command.MeetTiming;
                    dbModel.County = command.County;
                    dbModel.UpdatedBy = command.UpdatedBy;
                    dbModel.CreatedBy = command.CreatedBy;
                    dbModel.IsChatAllowed = command.IsChatAllowed;
                    if (string.IsNullOrEmpty(Convert.ToString(command.Id)) || command.Id == 0)
                    {
                        _context.bubbleMeetDetails.Add(dbModel);
                        await _context.SaveChanges();
                        dbSaftyModel.BubblePODId = dbModel.Id;
                        dbSaftyModel.CreatedBy = command.CreatedBy;
                        dbSaftyModel.UpdatedBy = command.UpdatedBy;
                        dbSaftyModel.CreatedOn = DateTime.UtcNow;
                        dbSaftyModel.UpdatedOn = DateTime.UtcNow;
                        _context.bubbleSafetyDetails.Add(dbSaftyModel);
                        await _context.SaveChanges();

                        // List<BubbleMeetMembers> lstMeetmembers = new List<BubbleMeetMembers>();
                        List<BubbleMeetMemberPermissions> lstMeetMemberPermissions = new List<BubbleMeetMemberPermissions>();
                        command.UserIds.Add(command.CreatedBy);
                        foreach (var item in command.UserIds)
                        {
                            //if (_context.bubbleMeetMembers.Where(x => x.UserId == item).Count() == 0)
                            //{
                            BubbleMeetMembers member = new BubbleMeetMembers();
                            member.BubbleMeetId = dbModel.Id;
                            member.UserId = item;
                            member.CreatedBy = command.CreatedBy;
                            member.UpdatedBy = command.UpdatedBy;

                            _context.bubbleMeetMembers.Add(member);
                            await _context.SaveChanges();
                            // lstMeetmembers.Add(member);

                            BubbleMeetMemberPermissions permission = new BubbleMeetMemberPermissions();
                            permission.BubbleMeetId = dbModel.Id;
                            permission.UserId = item;
                            permission.UserPermissionTypeId = UserPermission.IsAdmin;
                            permission.UserPermissionStatus = (item == command.CreatedBy) ? true : false;
                            permission.MeetTypeId = MeetType.BubbleMeet;
                            permission.CreatedBy = command.CreatedBy;
                            permission.UpdatedBy = command.UpdatedBy;
                            _context.bubbleMeetMemberPermissions.Add(permission);
                            await _context.SaveChanges();

                            notificationTitle = (item == command.CreatedBy) ? notificationAdm.Title + " " + command.Title : notification.Title + " " + command.Title;
                            notificationDescription = (item == command.CreatedBy) ? notificationAdm.Description + " " + command.Title : notification.Description + " " + command.Title + " by " + username;

                            NotificationsServices notificationsServices = new NotificationsServices(_context);
                            await notificationsServices.SendNotification(notificationTitle, notificationDescription, notification.Id, dbModel.Id, item, command.CreatedBy, command.UpdatedBy, NotificationTypeChild.BubbleMeeetNotification, NotificationCategories.General);
                            //}
                        }

                    }
                    else
                    {
                        dbModel = new BubbleMeetDetails();
                        dbModel = _context.bubbleMeetDetails.Where(m => m.Id == command.Id).FirstOrDefault();
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

                    var lstOfMeetMembers = _context.bubbleMeetMembers
                        .Where(x => x.BubbleMeetId == dbModel.Id)
                        .Select(y => new BubbleMeetMembers
                        {
                            UserId = y.UserId,
                            BubbleMeetId = y.BubbleMeetId
                        }).ToList();
                    foreach (var item in lstOfMeetMembers)
                    {
                        var user = _context.userDetails
                                   .Where(u => u.Id == item.UserId && u.IsActive == true)
                                   .Select(ud => new UserApiModels
                                   {
                                       Id = ud.Id,
                                       Username = ud.Username,
                                       County = ud.County,
                                       CountyName = _context.counties.Where(y => y.Fips == ud.County).Select(x => x.CountyName).FirstOrDefault(),
                                       ProfilePicUrl = ud.ProfilePicUrl,
                                       IsAdmin = (_context.bubbleMeetMemberPermissions
                                       .Where(p => p.UserId == item.UserId && p.BubbleMeetId == item.BubbleMeetId
                                       && p.UserPermissionTypeId == 0 && p.MeetTypeId == MeetType.BubbleMeet)
                                       .Select(x => x.UserPermissionStatus)
                                       .FirstOrDefault()),
                                       PhoneNo = ud.PhoneNo
                                   }).FirstOrDefault();
                        apiModel.lstUsers.Add(user);
                    }
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
