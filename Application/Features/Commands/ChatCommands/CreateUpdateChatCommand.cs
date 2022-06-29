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

namespace Application.Features.Commands.ChatCommands
{
    public class CreateUpdateChatCommand : IRequest<ChatDetailsApiModel>
    {

        public int Id { get; set; }
        public ChatTypes ChatTypeId { get; set; }
        public MeetType ChatParentTypeId { get; set; }
        public int ChatParentId { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public List<int> lstRemovedChatMembers { get; set; }
        public class CreateUpdateChatHandler : IRequestHandler<CreateUpdateChatCommand, ChatDetailsApiModel>
        {
            private readonly IApplicationDbContext _context;
            public CreateUpdateChatHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<ChatDetailsApiModel> Handle(CreateUpdateChatCommand command, CancellationToken cancellationToken)
            {
                ChatDetails dbModel = new ChatDetails();
                ChatDetailsApiModel apiModel = new ChatDetailsApiModel();
                ChatMembersApiModel chatMemberDetails = new ChatMembersApiModel();
                if (command.lstRemovedChatMembers.Count > 0 && command.Id>0)
                {
                    foreach (var cm in command.lstRemovedChatMembers)
                    {
                        var cmDetails = _context.chatMembers.Where(y => y.ChatId == command.Id && y.Id == cm).FirstOrDefault();
                        cmDetails.ChatMemberStatus = false;
                        await _context.SaveChanges();
                    }
                }
                if (command.ChatParentTypeId == MeetType.Bubble)
                {
                    chatMemberDetails.UserList = _context.userDetails
                        .Join(_context.bubbleMembers, u => u.Id, bm => bm.UserId, (u, bm) => new { u, bm })
                        .Join(_context.bubbleMeetMemberPermissions, bma => bma.u.Id, p => p.UserId, (bma, p) => new { bma, p })
                        .Where(bmd => bmd.bma.bm.BubbleId == command.ChatParentId
                                && bmd.p.UserPermissionTypeId == UserPermission.IsChatAllowed
                                && bmd.p.UserPermissionStatus == true
                                && bmd.bma.u.IsActive == true)
                        .Select(x => new UserApiModels()
                        {
                            Id = x.bma.u.Id,
                        }).ToList();
                }
                else if (command.ChatParentTypeId == MeetType.BubbleMeet)
                {
                    chatMemberDetails.UserList = _context.userDetails
                            .Join(_context.bubbleMeetMembers, u => u.Id, bm => bm.UserId, (u, bm) => new { u, bm })
                            .Join(_context.bubbleMeetMemberPermissions, bma => bma.u.Id, p => p.UserId, (bma, p) => new { bma, p })
                            .Where(bmd => bmd.bma.bm.BubbleMeetId == command.ChatParentId
                             && bmd.p.UserPermissionTypeId == UserPermission.IsChatAllowed
                                && bmd.p.UserPermissionStatus == true
                                && bmd.bma.u.IsActive == true)
                            .Select(x => new UserApiModels()
                            {
                                Id = x.bma.u.Id,
                            }).ToList();
                }
                else if (command.ChatParentTypeId == MeetType.POD)
                {
                    chatMemberDetails.UserList = _context.userDetails
                                 .Join(_context.podBubbleMembers, u => u.Id, bm => bm.BubbleMemberId, (u, bm) => new { u, bm })
                                 .Join(_context.bubbleMeetMemberPermissions, bma => bma.u.Id, p => p.UserId, (bma, p) => new { bma, p })
                                 .Where(bmd => bmd.bma.bm.PODId == command.ChatParentId
                                && bmd.p.UserPermissionTypeId == UserPermission.IsChatAllowed
                                && bmd.p.UserPermissionStatus == true
                                && bmd.bma.u.IsActive == true)
                                 .Select(x => new UserApiModels()
                                 {
                                     Id = x.bma.u.Id,
                                 }).ToList();
                }
                else if (command.ChatParentTypeId == MeetType.PODMeet)
                {
                    chatMemberDetails.UserList = _context.userDetails
                                    .Join(_context.podMeetMembers, u => u.Id, bm => bm.UserId, (u, bm) => new { u, bm })
                                    .Join(_context.bubbleMeetMemberPermissions, bma => bma.u.Id, p => p.UserId, (bma, p) => new { bma, p })
                                    .Where(bmd => bmd.bma.bm.PODId == command.ChatParentId
                                && bmd.p.UserPermissionTypeId == UserPermission.IsChatAllowed
                                && bmd.p.UserPermissionStatus == true
                                && bmd.bma.u.IsActive == true)
                                    .Select(x => new UserApiModels()
                                    {
                                        Id = x.bma.u.Id,
                                    }).ToList();
                }

                if (string.IsNullOrEmpty(Convert.ToString(command.Id)) || command.Id == 0)
                {
                    dbModel.ChatTypeId = (int)command.ChatTypeId;
                    dbModel.ChatParentTypeId = (int)command.ChatParentTypeId;
                    dbModel.ChatParentId = command.ChatParentId;
                    dbModel.CreatedBy = command.CreatedBy;
                    dbModel.UpdatedBy = command.UpdatedBy;
                    dbModel.ChatStatus = true;
                    _context.chatDetails.Add(dbModel);
                    await _context.SaveChanges();

                    foreach (var item in chatMemberDetails.UserList)
                    {
                        if (_context.chatMembers.Where(x => x.ChatMemberId == item.Id && x.ChatId == dbModel.Id).Count() == 0)
                        {
                            var member = new ChatMembers();
                            member.ChatId = dbModel.Id;
                            member.ChatMemberId = item.Id;
                            member.CreatedBy = command.CreatedBy;
                            member.UpdatedBy = command.UpdatedBy;
                            _context.chatMembers.Add(member);
                            // lstmembers.Add(member);
                            await _context.SaveChanges();

                            //NotificationsServices notificationsServices = new NotificationsServices(_context);
                            //await notificationsServices.SendNotification(notification.Title, notification.Id, dbModel.Id, item, command.CreatedBy, command.UpdatedBy, NotificationTypeChild.BubbleNotification);

                        }
                    }
                }
                else
                {
                    dbModel = _context.chatDetails.Where(x => x.Id == command.Id).FirstOrDefault();
                    dbModel.ChatParentId = command.ChatParentId;
                    dbModel.ChatParentTypeId = (int)command.ChatParentTypeId;
                    dbModel.ChatTypeId = (int)command.ChatTypeId;
                    dbModel.UpdatedBy = command.UpdatedBy;
                    dbModel.UpdatedOn = DateTime.UtcNow;
                    await _context.SaveChanges();
                }
                apiModel.Id = dbModel.Id;
                apiModel.ChatTypeId = dbModel.ChatTypeId;
                apiModel.ChatParentTypeId = dbModel.ChatParentTypeId;
                apiModel.ChatParentId = dbModel.ChatParentId;
                apiModel.CreatedBy = dbModel.CreatedBy;
                apiModel.CreatedOn = dbModel.CreatedOn;
                apiModel.UpdatedBy = dbModel.UpdatedBy;
                apiModel.UpdatedOn = dbModel.UpdatedOn;
                return apiModel;

            }

        }


    }
}
