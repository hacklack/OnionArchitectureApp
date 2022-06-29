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
using FirebaseAdmin.Messaging;

namespace Application.Features.Commands.BubbleCommands
{
    public class CreateUpdateBubbleCommand : IRequest<BubbleApiModel>
    {

        public int Id { get; set; }
        public string BubbleName { get; set; }
        public string BubbleSize { get; set; }
        public string BubbleDescription { get; set; }
        public string BubbleZipCode { get; set; }
        public BubbleType BubbleType { get; set; }
        public DateTime BubbleValidity { get; set; }
        public bool IsOtherCountyMemberAllowed { get; set; }
        public List<int> UserIds { get; set; }
        public bool IsBubbleAdmin { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public class CreateUpdateBubbleCommandHandler : IRequestHandler<CreateUpdateBubbleCommand, BubbleApiModel>
        {
            private readonly IApplicationDbContext _context;
            public CreateUpdateBubbleCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<BubbleApiModel> Handle(CreateUpdateBubbleCommand command, CancellationToken cancellationToken)
            {
                string notificationTitle = string.Empty;
                string notificationDescription = string.Empty;

                BubbleDetails dbModel = new BubbleDetails();
                BubbleSafetyDetails dbSaftyModel = new BubbleSafetyDetails();
                BubbleApiModel apiModel = new BubbleApiModel();
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
                   .Where(y => y.NotificationTypeId == NotificationTypes.BubbleCreated)
                   .Select(x => new NotificationFCMApiModel
                   {
                       Title = x.Title,
                       Description = x.Description,
                       NotificationTypeId = x.NotificationTypeId,
                       Id = x.Id
                   }).FirstOrDefault();
                string username = _context.userDetails.Where(y => y.Id == command.CreatedBy).Select(x => x.Username).FirstOrDefault();

                var membersLst = _context.bubbleMembers.Select(bm => bm.UserId).ToList();
                if (_context.bubbleMembers.Where(bmc => bmc.UserId == command.CreatedBy).Count() == 0)
                {

                    if (string.IsNullOrEmpty(Convert.ToString(command.Id)) || command.Id == 0)
                    {
                        dbModel.BubbleName = command.BubbleName;
                        dbModel.BubbleDescription = command.BubbleDescription;
                        dbModel.BubbleSize = command.BubbleSize;
                        dbModel.BubbleValidity = command.BubbleValidity;
                        dbModel.BubbleZipCode = command.BubbleZipCode;
                        dbModel.BubbleType = command.BubbleType;
                        dbModel.CreatedBy = command.CreatedBy;
                        dbModel.UpdatedBy = command.UpdatedBy;
                        dbModel.IsOtherCountyMemberAllowed = command.IsOtherCountyMemberAllowed;

                        _context.bubbleDetails.Add(dbModel);
                        await _context.SaveChanges();


                        List<BubbleMembers> lstmembers = new List<BubbleMembers>();

                        foreach (var item in command.UserIds)
                        {
                            if (_context.bubbleMembers.Where(x => x.UserId == item && x.BubbleId == dbModel.Id).Count() == 0)
                            {
                                var member = new BubbleMembers();
                                member.BubbleId = dbModel.Id;
                                member.UserId = item;
                                member.MemberBubbleType = command.BubbleType;
                                member.IsBubbleAdmin = (item == command.CreatedBy) ? true : false;
                                member.CreatedBy = command.CreatedBy;
                                member.UpdatedBy = command.UpdatedBy;
                                _context.bubbleMembers.Add(member);
                                lstmembers.Add(member);
                                await _context.SaveChanges();

                                BubbleMeetMemberPermissions permission = new BubbleMeetMemberPermissions();
                                permission.BubbleMeetId = dbModel.Id;
                                permission.UserId = item;
                                permission.UserPermissionTypeId = UserPermission.IsAdmin;
                                permission.UserPermissionStatus = (item == command.CreatedBy) ? true : false;
                                permission.MeetTypeId = MeetType.Bubble;
                                permission.CreatedBy = command.CreatedBy;
                                permission.UpdatedBy = command.UpdatedBy;
                                _context.bubbleMeetMemberPermissions.Add(permission);
                                await _context.SaveChanges();

                                notificationTitle = (item == command.CreatedBy) ? notificationAdm.Title + " " + command.BubbleName : notification.Title + " " + command.BubbleName;
                                notificationDescription = (item == command.CreatedBy) ? notificationAdm.Description + " " + command.BubbleName : notification.Description + " " + command.BubbleName + " by " + username;
                                NotificationsServices notificationsServices = new NotificationsServices(_context);
                                await notificationsServices.SendNotification(notificationTitle, notificationDescription, notification.Id, dbModel.Id, item, command.CreatedBy, command.UpdatedBy, NotificationTypeChild.BubbleNotification, NotificationCategories.General);

                            }
                        }
                    }
                    else
                    {
                        dbModel = _context.bubbleDetails.Where(x => x.Id == command.Id).FirstOrDefault();
                        dbModel.BubbleName = command.BubbleName;
                        dbModel.BubbleDescription = command.BubbleDescription;
                        dbModel.BubbleSize = command.BubbleSize;
                        dbModel.BubbleType = command.BubbleType;
                        dbModel.BubbleValidity = command.BubbleValidity;
                        dbModel.BubbleZipCode = command.BubbleZipCode;
                        dbModel.IsOtherCountyMemberAllowed = command.IsOtherCountyMemberAllowed;
                        dbModel.UpdatedBy = command.UpdatedBy;
                        dbModel.UpdatedOn = DateTime.UtcNow;
                        await _context.SaveChanges();
                    }
                    apiModel.Id = dbModel.Id;
                    apiModel.IsOtherCountyMemberAllowed = dbModel.IsOtherCountyMemberAllowed;
                    apiModel.BubbleName = dbModel.BubbleName;
                    apiModel.BubbleDescription = dbModel.BubbleDescription;
                    apiModel.BubbleSize = dbModel.BubbleSize;
                    apiModel.BubbleType = dbModel.BubbleType;
                    apiModel.BubbleValidity = dbModel.BubbleValidity;
                    apiModel.BubbleZipCode = dbModel.BubbleZipCode;
                    apiModel.CreatedBy = dbModel.CreatedBy;
                    apiModel.CreatedOn = dbModel.CreatedOn;
                    apiModel.UpdatedBy = dbModel.UpdatedBy;
                    apiModel.UpdatedOn = dbModel.UpdatedOn;


                    if (command.BubbleType == BubbleType.Single)
                    {
                        dbSaftyModel.BubbleSaftyValue = CommonStaticStrings.BubbleSaftyLevel;
                    }
                    else if (command.BubbleType == BubbleType.Multi)
                    {
                        NotificationsServices notificationsServices = new NotificationsServices(_context);
                        dbSaftyModel.BubbleSaftyValue = await notificationsServices.BubbleSaftyCalculation(command.UserIds);
                    }

                    dbSaftyModel.BubblePODId = dbModel.Id;
                    dbSaftyModel.UpdatedBy = command.UpdatedBy;
                    dbSaftyModel.UpdatedOn = DateTime.UtcNow;
                    dbSaftyModel.BubbleSaftyTypeId = BubbleSaftyType.BubbleSaftyLevel;
                    if (_context.bubbleSafetyDetails.Where(y => y.BubblePODId == dbModel.Id).Count() == 0)
                    {
                        dbSaftyModel.CreatedBy = command.CreatedBy;
                        dbSaftyModel.CreatedOn = DateTime.UtcNow;
                        _context.bubbleSafetyDetails.Add(dbSaftyModel);
                    }
                    else
                    {
                        _context.bubbleSafetyDetails.Update(dbSaftyModel);
                    }
                    await _context.SaveChanges();
                }
                else
                {
                    apiModel.message = "You Cannot Create a bubble as you are already a member of a bubble.";
                }
                return apiModel;
            }

        }
    }
}
