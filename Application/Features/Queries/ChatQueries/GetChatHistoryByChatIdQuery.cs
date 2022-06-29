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
using System.Globalization;

namespace Application.Features.Queries.ChatQueries
{
    public class GetChatHistoryByChatIdQuery : IRequest<ChatDetailsApiModel>
    {

        public int ChatTypeId { get; set; }
        public int ChatParentTypeId { get; set; }
        public int ChatParentId { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        //public List<int> lstRemovedChatMembers { get; set; }
        public class GetChatHistoryByChatIdHandler : IRequestHandler<GetChatHistoryByChatIdQuery, ChatDetailsApiModel>
        {
            private readonly IApplicationDbContext _context;
            public GetChatHistoryByChatIdHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<ChatDetailsApiModel> Handle(GetChatHistoryByChatIdQuery query, CancellationToken cancellationToken)
            {
                ChatDetails dbModel = new ChatDetails();
                ChatDetailsApiModel apiModel = new ChatDetailsApiModel();
                ChatMembersApiModel chatMemberDetails = new ChatMembersApiModel();

                if (query.ChatParentTypeId == (int)MeetType.Bubble)
                {
                    chatMemberDetails.UserList = _context.userDetails
                        .Join(_context.bubbleMembers, u => u.Id, bm => bm.UserId, (u, bm) => new { u, bm })
                        .Where(bmd => bmd.bm.BubbleId == query.ChatParentId
                                && bmd.u.IsActive == true)
                        .Select(x => new UserApiModels()
                        {
                            Id = x.u.Id,
                        }).ToList();
                }
                else if (query.ChatParentTypeId == (int)MeetType.BubbleMeet)
                {
                    chatMemberDetails.UserList = _context.userDetails
                            .Join(_context.bubbleMeetMembers, u => u.Id, bm => bm.UserId, (u, bm) => new { u, bm })
                            .Where(bmd => bmd.bm.BubbleMeetId == query.ChatParentId
                            && bmd.u.IsActive == true)
                            .Select(x => new UserApiModels()
                            {
                                Id = x.u.Id,
                            }).ToList();
                }
                else if (query.ChatParentTypeId == (int)MeetType.POD)
                {
                    chatMemberDetails.UserList = _context.userDetails
                                 .Join(_context.podBubbleMembers, u => u.Id, bm => bm.BubbleMemberId, (u, bm) => new { u, bm })
                                 .Where(bmd => bmd.bm.PODId == query.ChatParentId
                                && bmd.u.IsActive == true)
                                 .Select(x => new UserApiModels()
                                 {
                                     Id = x.u.Id,
                                 }).ToList();
                }
                else if (query.ChatParentTypeId == (int)MeetType.PODMeet)
                {
                    chatMemberDetails.UserList = _context.userDetails
                                    .Join(_context.podMeetMembers, u => u.Id, bm => bm.UserId, (u, bm) => new { u, bm })
                                    .Where(bmd => bmd.bm.PODMeetId == query.ChatParentId
                                && bmd.u.IsActive == true)
                                    .Select(x => new UserApiModels()
                                    {
                                        Id = x.u.Id,
                                    }).ToList();
                }

                if (_context.chatDetails.Where(y=>y.ChatParentTypeId==query.ChatParentTypeId && y.ChatParentId==query.ChatParentId && y.ChatTypeId==query.ChatTypeId).Count()==0)
                {
                    dbModel.ChatTypeId = query.ChatTypeId;
                    dbModel.ChatParentTypeId = query.ChatParentTypeId;
                    dbModel.ChatParentId = query.ChatParentId;
                    dbModel.CreatedBy = query.CreatedBy;
                    dbModel.UpdatedBy = query.UpdatedBy;
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
                            member.ChatMemberStatus = true;
                            member.CreatedBy = query.CreatedBy;
                            member.UpdatedBy = query.UpdatedBy;
                            _context.chatMembers.Add(member);
                            await _context.SaveChanges();

                            //NotificationsServices notificationsServices = new NotificationsServices(_context);
                            //await notificationsServices.SendNotification(notification.Title, notification.Id, dbModel.Id, item, query.CreatedBy, query.UpdatedBy, NotificationTypeChild.BubbleNotification);

                        }
                    }
                }
                else
                {
                    dbModel = _context.chatDetails.Where(y => y.ChatParentTypeId == query.ChatParentTypeId && y.ChatParentId == query.ChatParentId && y.ChatTypeId == query.ChatTypeId).FirstOrDefault();
                    dbModel.ChatParentId = query.ChatParentId;
                    dbModel.ChatParentTypeId = query.ChatParentTypeId;
                    dbModel.ChatTypeId = query.ChatTypeId;
                    dbModel.UpdatedBy = query.UpdatedBy;
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
                apiModel.lstChatMembers = _context.chatMembers
                                        .Where(y => y.ChatId == dbModel.Id)
                                        .Select(cm => new ChatMembersDataApiModel
                                        {
                                        Id=cm.Id,
                                        ChatId=cm.ChatId,
                                        ChatMemberId=cm.ChatMemberId,
                                        ChatMemberStatus =cm.ChatMemberStatus,
                                        }).ToList();

                apiModel.lstChatHistory = _context.chatHistory
                                          .Where(y => y.ChatId == dbModel.Id)
                                          .Select(x=>new ChatHistoryApiModel {
                                            Id=x.Id,
                                            ChatId=x.ChatId,
                                            ChatMessageSenderId=x.ChatMessageSenderId,
                                            ChatMessage=x.ChatMessage,
                                            CreatedOnString= CommonHelperMethods.DateFormatterMethod(x.CreatedOn),
                                            UpdatedOnString = CommonHelperMethods.DateFormatterMethod(x.UpdatedOn)

                                          })
                                          .ToList();
                return apiModel;

            }

        }


    }
}
