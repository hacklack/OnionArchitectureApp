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

namespace Application.Features.Commands.PodCommands
{

    public class CreateUpdatePodCommand : IRequest<PodDetailsApiModel>
    {
        public int Id { get; set; }
        public string PODName { get; set; }
        public int PODBubbleType { get; set; }
        public int PODSize { get; set; }
        public string PODDescription { get; set; }
        public List<BubbleApiModel> lstPodBubbleApiModel { get; set; }
        public int UpdatedBy { get; set; }
        public int CreatedBy { get; set; }
        public List<int> DeletedBubbleIds { get; set; }

        public class CreateUpdatePodCommandHandler : IRequestHandler<CreateUpdatePodCommand, PodDetailsApiModel>
        {

            private readonly IApplicationDbContext _context;
            public CreateUpdatePodCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<PodDetailsApiModel> Handle(CreateUpdatePodCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    BubbleSafetyDetails dbSaftyModel = new BubbleSafetyDetails();

                    if (command.DeletedBubbleIds.Count > 0 && command.DeletedBubbleIds != null && command.Id > 0)
                    {
                        foreach (var bubbleId in command.DeletedBubbleIds)
                        {
                            var bubbleDel = _context.podMembers.Where(y => y.BubbleId == bubbleId && y.PODId == command.Id).FirstOrDefault();
                            _context.podMembers.Remove(bubbleDel);
                            var bubbleMemDel = _context.podBubbleMembers.Where(y => y.BubbleId == bubbleId && y.PODId == command.Id).FirstOrDefault();
                            _context.podBubbleMembers.Remove(bubbleMemDel);

                        }
                    }
                    //if (command.DeletedMemberIds.Count > 0 && command.DeletedMemberIds != null && command.Id > 0)
                    //{

                    //    foreach (var bubbleUserId in command.DeletedMemberIds)
                    //    {
                    //        var bubbleMemUserDel = _context.podBubbleMembers.Where(y => y.BubbleMemberId == bubbleUserId && y.PODId == command.Id).FirstOrDefault();
                    //        _context.podBubbleMembers.Remove(bubbleMemUserDel);

                    //    }
                    //}
                    string notificationTitle = string.Empty;
                    string notificationDescription = string.Empty;
                    PodDetailsApiModel returnPodApiModel = new PodDetailsApiModel();
                    PodDetails dbModel = new PodDetails();
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
                       .Where(y => y.NotificationTypeId == NotificationTypes.PODCreated)
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
                        dbModel.PODName = command.PODName;
                        dbModel.PODBubbleType = command.PODBubbleType;
                        dbModel.PODSize = command.PODSize;
                        dbModel.PODDescription = command.PODDescription;
                        dbModel.CreatedBy = command.CreatedBy;
                        dbModel.UpdatedBy = command.UpdatedBy;
                        _context.podDetails.Add(dbModel);
                        await _context.SaveChanges();
                    }
                    else
                    {
                        dbModel = _context.podDetails.Where(m => m.Id == command.Id).FirstOrDefault();
                        dbModel.PODName = command.PODName;
                        dbModel.PODBubbleType = command.PODBubbleType;
                        dbModel.PODSize = command.PODSize;
                        dbModel.PODDescription = command.PODDescription;
                        dbModel.CreatedBy = command.CreatedBy;
                        dbModel.UpdatedBy = command.UpdatedBy;
                        await _context.SaveChanges();
                    }
                    returnPodApiModel.Id = dbModel.Id;
                    returnPodApiModel.PODName = dbModel.PODName;
                    returnPodApiModel.PODSize = dbModel.PODSize;
                    returnPodApiModel.PODDescription = dbModel.PODDescription;

                    List<int> bubbleIds = new List<int>();
                    foreach (var item in command.lstPodBubbleApiModel)
                    {
                        bubbleIds.Add(item.Id);
                    }
                    NotificationsServices notificationsServices = new NotificationsServices(_context);
                    dbSaftyModel.BubbleSaftyValue =await notificationsServices.PODSaftyCalculation(bubbleIds);
                    dbSaftyModel.BubblePODId = dbModel.Id;
                    dbSaftyModel.UpdatedBy = command.UpdatedBy;
                    dbSaftyModel.UpdatedOn = DateTime.UtcNow;
                    dbSaftyModel.BubbleSaftyTypeId = BubbleSaftyType.PODSaftyLevel;
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

                    if (command.lstPodBubbleApiModel.Count() > 0 && command.lstPodBubbleApiModel != null)
                    {
                        foreach (var bubble in command.lstPodBubbleApiModel)
                        {
                            BubbleApiModel bubbleApiModel = new BubbleApiModel();
                            var pODbubble = new PODMembers();
                            pODbubble.BubbleId = bubble.Id;
                            pODbubble.PODId = dbModel.Id;
                            _context.podMembers.Add(pODbubble);
                            await _context.SaveChanges();

                            List<int> lstMemberIds = _context.bubbleMembers
                                 .Join(_context.userDetails, qbm => qbm.UserId, qud => qud.Id, (qbm, qud) => new { qbm, qud })
                                 .Where(qy => qy.qbm.BubbleId == bubble.Id && qy.qud.IsActive == true)
                                 .Select(qx => qx.qbm.UserId).ToList();
                            foreach (var memberId in lstMemberIds)
                            {
                                UserApiModels podMemberUsers = new UserApiModels();
                                if (_context.podBubbleMembers.Where(y => y.BubbleId == bubble.Id && y.PODId == dbModel.Id && y.BubbleMemberId == memberId).Count() == 0)
                                {
                                    var pODMemberIds = new PODBubbleMembers();
                                    pODMemberIds.PODId = dbModel.Id;
                                    pODMemberIds.BubbleId = bubble.Id;
                                    pODMemberIds.BubbleMemberId = memberId;
                                    _context.podBubbleMembers.Add(pODMemberIds);
                                    await _context.SaveChanges();

                                    BubbleMeetMemberPermissions permission = new BubbleMeetMemberPermissions();
                                    permission.BubbleMeetId = dbModel.Id;
                                    permission.UserId = memberId;
                                    permission.UserPermissionTypeId = UserPermission.IsAdmin;
                                    permission.UserPermissionStatus = (memberId == command.CreatedBy) ? true : false;
                                    permission.MeetTypeId = MeetType.POD;
                                    permission.CreatedBy = command.CreatedBy;
                                    permission.UpdatedBy = command.UpdatedBy;
                                    _context.bubbleMeetMemberPermissions.Add(permission);
                                    await _context.SaveChanges();


                                    notificationTitle = (memberId == command.CreatedBy) ? notificationAdm.Title + " " + command.PODName : notification.Title + " " + command.PODName;
                                    notificationDescription = (memberId == command.CreatedBy) ? notificationAdm.Description + " " + command.PODName : notification.Description + " " + command.PODName + " by " + username;
                                    await notificationsServices.SendNotification(notificationTitle, notificationDescription, notification.Id, dbModel.Id, memberId, command.CreatedBy, command.UpdatedBy, NotificationTypeChild.PODNotification, NotificationCategories.General);
                                }
                                podMemberUsers = _context.userDetails
                                 .Join(_context.podBubbleMembers, ud => ud.Id, pbm => pbm.BubbleMemberId, (ud, pbm) => new { ud, pbm })
                                 .Where(y => y.ud.Id == memberId && y.pbm.BubbleId == bubble.Id && y.ud.IsActive == true)
                                 .Select(x => new UserApiModels
                                 {
                                     Id = x.ud.Id,
                                     Username = x.ud.Username,
                                     PhoneNo = x.ud.PhoneNo,
                                     ProfilePicUrl = x.ud.ProfilePicUrl,
                                     CountyName = _context.counties.Where(y => y.Fips == x.ud.County).Select(x => x.CountyName).FirstOrDefault(),
                                     County = x.ud.County,
                                 }).FirstOrDefault();
                                bubbleApiModel.lstPodUser.Add(podMemberUsers);
                            }

                            bubbleApiModel = _context.bubbleDetails
                                  .Join(_context.podBubbleMembers, bd => bd.Id, pbm => pbm.BubbleId, (bd, pbm) => new { bd, pbm })
                                  .Where(y => y.bd.Id == bubble.Id && y.pbm.PODId == dbModel.Id)
                                  .Select(x => new BubbleApiModel
                                  {
                                      Id = x.bd.Id,
                                      BubbleName = x.bd.BubbleName,
                                      BubbleDescription = x.bd.BubbleDescription,
                                      BubbleSize = x.bd.BubbleSize,
                                      BubbleType = x.bd.BubbleType,
                                      IsOtherCountyMemberAllowed = x.bd.IsOtherCountyMemberAllowed,
                                      BubbleZipCode = x.bd.BubbleZipCode
                                  }).FirstOrDefault();
                            returnPodApiModel.lstPodBubbleApiModel.Add(bubbleApiModel);

                        }
                    }
                    return returnPodApiModel;
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
        }
    }
}
