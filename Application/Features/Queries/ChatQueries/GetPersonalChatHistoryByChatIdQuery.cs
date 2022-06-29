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

namespace Application.Features.Queries.ChatQueries
{
    public class GetPersonalChatHistoryByChatIdQuery : IRequest<ChatDetailsApiModel>
    {

        public int ChatParentTypeId { get; set; }
        public int ChatParentId { get; set; }
        public int SecondMemberId { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public class GetPersonalChatHistoryByChatIdHandler : IRequestHandler<GetPersonalChatHistoryByChatIdQuery, ChatDetailsApiModel>
        {
            private readonly IApplicationDbContext _context;
            public GetPersonalChatHistoryByChatIdHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<ChatDetailsApiModel> Handle(GetPersonalChatHistoryByChatIdQuery query, CancellationToken cancellationToken)
            {
                ChatDetails dbModel = new ChatDetails();
                ChatDetailsApiModel apiModel = new ChatDetailsApiModel();
                List<int> userIds = new List<int>();
                userIds.Add(query.CreatedBy);
                userIds.Add(query.SecondMemberId);
                int chatId = 0;
                var ctIds = _context.chatMembers.Join(_context.chatDetails,cm=>cm.ChatId,cd=>cd.Id,(cm,cd)=>new { cm,cd}).Where(y => y.cm.ChatMemberId == query.CreatedBy && y.cd.ChatTypeId==(int)ChatTypes.PersonalChat && y.cd.ChatParentTypeId==query.ChatParentTypeId && y.cd.ChatParentId==query.ChatParentId).Select(x => x.cm.ChatId).ToList();
                foreach (var ctId in ctIds)
                {
                    var chkchatId = _context.chatMembers.Join(_context.chatDetails, cm => cm.ChatId, cd => cd.Id, (cm, cd) => new { cm, cd }).Where(q => q.cm.ChatId == ctId && q.cm.ChatMemberId == query.SecondMemberId && q.cd.ChatTypeId == (int)ChatTypes.PersonalChat && q.cd.ChatParentTypeId == query.ChatParentTypeId && q.cd.ChatParentId == query.ChatParentId).Select(qx => qx.cm.ChatId).FirstOrDefault();
                    if (chkchatId == ctId)
                    {
                        chatId = chkchatId;
                    }
                }
                if (chatId > 0)
                {
                    apiModel = _context.chatDetails.Where(y => y.Id == chatId)
                        .Select(x => new ChatDetailsApiModel
                        {
                            Id = x.Id,
                            ChatParentId = x.ChatParentId,
                            ChatParentTypeId = x.ChatParentTypeId,
                            ChatTypeId = x.ChatTypeId,
                            CreatedBy = x.CreatedBy,
                            CreatedOn = x.CreatedOn,
                            UpdatedBy = x.UpdatedBy,
                            UpdatedOn = x.UpdatedOn
                        }).FirstOrDefault();
                  
                }
                else if(chatId == 0)
                {
                    dbModel.ChatTypeId = (int)ChatTypes.PersonalChat;
                    dbModel.ChatParentTypeId = query.ChatParentTypeId;
                    dbModel.ChatParentId = query.ChatParentId;
                    dbModel.CreatedBy = query.CreatedBy;
                    dbModel.UpdatedBy = query.UpdatedBy;
                    dbModel.ChatStatus = true;
                    _context.chatDetails.Add(dbModel);
                    await _context.SaveChanges();


                    apiModel.Id = dbModel.Id;
                    apiModel.ChatParentId = dbModel.ChatParentId;
                    apiModel.ChatParentTypeId = dbModel.ChatParentTypeId;
                    apiModel.ChatTypeId = dbModel.ChatTypeId;
                    apiModel.CreatedBy = dbModel.CreatedBy;
                    apiModel.UpdatedBy = dbModel.UpdatedBy;
                    apiModel.CreatedOn = dbModel.CreatedOn;
                    apiModel.UpdatedOn = dbModel.UpdatedOn;

                    foreach (var item in userIds)
                    {
                        if (_context.chatMembers.Where(x => x.ChatMemberId == item && x.ChatId == dbModel.Id).Count() == 0)
                        {
                            var member = new ChatMembers();
                            member.ChatId = dbModel.Id;
                            member.ChatMemberId = item;
                            member.ChatMemberStatus = true;
                            member.CreatedBy = query.CreatedBy;
                            member.UpdatedBy = query.UpdatedBy;
                            _context.chatMembers.Add(member);
                            // lstmembers.Add(member);
                            await _context.SaveChanges();

                            //NotificationsServices notificationsServices = new NotificationsServices(_context);
                            //await notificationsServices.SendNotification(notification.Title, notification.Id, dbModel.Id, item, command.CreatedBy, command.UpdatedBy, NotificationTypeChild.BubbleNotification);

                        }
                    }
                }
                apiModel.lstUserDetails = _context.userDetails
                                          .Join(_context.chatMembers, ud => ud.Id, chm => chm.ChatMemberId, (ud, chm) => new { ud, chm })
                                          .Where(y => y.ud.Id == y.chm.ChatMemberId && y.chm.ChatId == ((chatId==0)? dbModel.Id : chatId) && y.ud.IsActive == true)
                                          .Select(udx => new UserApiModels
                                          {
                                              Id = udx.ud.Id,
                                              Username = udx.ud.Username,
                                              PhoneNo = udx.ud.PhoneNo,
                                              ProfilePicUrl = udx.ud.ProfilePicUrl,
                                              County = udx.ud.County,
                                              CountyName = _context.counties.Where(y => y.Fips == udx.ud.County).Select(x => x.CountyName).FirstOrDefault(),
                                              CreatedBy = udx.ud.CreatedBy,
                                              UpdatedBy = udx.ud.UpdatedBy,
                                              CreatedOn = udx.ud.CreatedOn,
                                              UpdatedOn = udx.ud.UpdatedOn,
                                              countyDetails = _context.counties.Where(y => y.Fips == udx.ud.County).Select(x => new CountiesApiModel
                                              {
                                                  Id = x.Id,
                                                  Fips = x.Fips,
                                                  Country = x.Country,
                                                  CountyName = x.CountyName,
                                                  State = x.State,
                                                  CreatedBy = x.CreatedBy,
                                                  UpdatedBy = x.UpdatedBy,
                                                  CreatedOn = x.CreatedOn,
                                                  UpdatedOn = x.UpdatedOn

                                              }).FirstOrDefault()
                                          }).ToList();

                apiModel.lstChatHistory = _context.chatHistory
                                          .Where(y => y.ChatId == ((chatId == 0) ? dbModel.Id : chatId))
                                          .Select(x => new ChatHistoryApiModel
                                          {
                                              Id = x.Id,
                                              ChatId = x.ChatId,
                                              ChatMessageSenderId = x.ChatMessageSenderId,
                                              ChatMessage = x.ChatMessage,
                                              CreatedOnString = CommonHelperMethods.DateFormatterMethod(x.CreatedOn),
                                              UpdatedOnString = CommonHelperMethods.DateFormatterMethod(x.UpdatedOn)
                                          })
                                          .ToList();
                return apiModel;


            }


        }
    }
}