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
    public class CreateUpdatePersonalChatCommand : IRequest<ChatDetailsApiModel>
    {

        public int Id { get; set; }
        public int ChatParentTypeId { get; set; }
        public int ChatParentId { get; set; }
        public int SecondMemberId { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        //public List<int> lstRemovedChatMembers { get; set; }
        public class CreateUpdatePersonalChatHandler : IRequestHandler<CreateUpdatePersonalChatCommand, ChatDetailsApiModel>
        {
            private readonly IApplicationDbContext _context;
            public CreateUpdatePersonalChatHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<ChatDetailsApiModel> Handle(CreateUpdatePersonalChatCommand command, CancellationToken cancellationToken)
            {
                ChatDetails dbModel = new ChatDetails();
                ChatDetailsApiModel apiModel = new ChatDetailsApiModel();
                List<int> userIds = new List<int>();
                int chatId = 0;
                userIds.Add(command.CreatedBy);
                userIds.Add(command.SecondMemberId);

                var ctIds = _context.chatMembers.Join(_context.chatDetails, cm => cm.ChatId, cd => cd.Id, (cm, cd) => new { cm, cd }).Where(y => y.cm.ChatMemberId == command.CreatedBy && y.cd.ChatTypeId == (int)ChatTypes.PersonalChat && y.cd.ChatParentTypeId == command.ChatParentTypeId && y.cd.ChatParentId == command.ChatParentId).Select(x => x.cm.ChatId).ToList();
                foreach (var ctId in ctIds)
                {
                    var chkchatId = _context.chatMembers.Join(_context.chatDetails, cm => cm.ChatId, cd => cd.Id, (cm, cd) => new { cm, cd }).Where(q => q.cm.ChatId == ctId && q.cm.ChatMemberId == command.SecondMemberId && q.cd.ChatTypeId == (int)ChatTypes.PersonalChat && q.cd.ChatParentTypeId == command.ChatParentTypeId && q.cd.ChatParentId == command.ChatParentId).Select(qx => qx.cm.ChatId).FirstOrDefault();
                    if (chkchatId == ctId)
                    {
                        chatId = chkchatId;
                    }
                }
                //( _context.chatDetails
                // .Join(_context.chatMembers,cdx=>cdx.Id,cmx=>cmx.ChatId,(cdx,cmx)=>new { cdx,cmx})
                // .Where(xx => userIds.Contains(xx.cmx.ChatMemberId)).Count()>0)
                //int chId= _context.chatDetails
                //   .Join(_context.chatMembers, cd => cd.Id, cm => cm.ChatId, (cd, cm) => new { cd, cm })
                //   .Where(y => userIds.Contains(y.cm.ChatMemberId) && y.cd.ChatTypeId == (int)ChatTypes.PersonalChat && y.cd.ChatParentId == command.ChatParentId).Select(x=>x.cd.Id).FirstOrDefault();

                if (chatId == 0 && command.Id == 0)
                {
                    dbModel.ChatTypeId = (int)ChatTypes.PersonalChat;
                    dbModel.ChatParentTypeId = command.ChatParentTypeId;
                    dbModel.ChatParentId = command.ChatParentId;
                    dbModel.CreatedBy = command.CreatedBy;
                    dbModel.UpdatedBy = command.UpdatedBy;
                    dbModel.ChatStatus = true;
                    _context.chatDetails.Add(dbModel);
                    await _context.SaveChanges();

                    foreach (var item in userIds)
                    {
                        if (_context.chatMembers.Where(x => x.ChatMemberId == item && x.ChatId == dbModel.Id).Count() == 0)
                        {
                            var member = new ChatMembers();
                            member.ChatId = dbModel.Id;
                            member.ChatMemberId = item;
                            member.ChatMemberStatus = true;
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
                    dbModel= _context.chatDetails.Where(y => y.Id == chatId).FirstOrDefault();
                    apiModel.Id = dbModel.Id;
                    apiModel.ChatTypeId = dbModel.ChatTypeId;
                    apiModel.ChatParentTypeId = dbModel.ChatParentTypeId;
                    apiModel.ChatParentId = dbModel.ChatParentId;
                    apiModel.CreatedBy = dbModel.CreatedBy;
                    apiModel.CreatedOn = dbModel.CreatedOn;
                    apiModel.UpdatedBy = dbModel.UpdatedBy;
                    apiModel.UpdatedOn = dbModel.UpdatedOn;
                    apiModel.lstUserDetails = _context.userDetails
                                            .Join(_context.chatMembers,ud=>ud.Id,chm=>chm.ChatMemberId,(ud,chm)=>new {ud,chm})
                                            .Where(y=>y.ud.Id==y.chm.ChatMemberId && y.chm.ChatId==dbModel.Id && y.ud.IsActive==true)
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
                                            }).ToList();

                    apiModel.lstChatHistory = _context.chatHistory
                                              .Where(y => y.ChatId == dbModel.Id)
                                              .Select(x => new ChatHistoryApiModel
                                              {
                                                  Id = x.Id,
                                                  ChatId = x.ChatId,
                                                  ChatMessageSenderId = x.ChatMessageSenderId,
                                                  ChatMessage = x.ChatMessage,
                                                  CreatedOn=x.CreatedOn,
                                                  UpdatedOn=x.UpdatedOn,
                                                  CreatedOnString = CommonHelperMethods.DateFormatterMethod(x.CreatedOn),
                                                  UpdatedOnString = CommonHelperMethods.DateFormatterMethod(x.UpdatedOn)
                                              })
                                              .ToList();
                }
                
                return apiModel;

            }

        }


    }
}
