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
using Application.ChatComponents.Hubs;

namespace Application.Features.Commands.ChatCommands
{
    public class SendMessageCommand : IRequest<List<ChatHistoryApiModel>>
    {

        public int ChatId { get; set; }
        public int UserId { get; set; }
        public string ChatMessage { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, List<ChatHistoryApiModel>>
        {
            private readonly IApplicationDbContext _context;
            public SendMessageCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<List<ChatHistoryApiModel>> Handle(SendMessageCommand command, CancellationToken cancellationToken)
            {
                List<ChatHistoryApiModel> apiModel = new List<ChatHistoryApiModel>();
                ChatHubs ch = new ChatHubs(_context);
                await ch.SendMessage(command.ChatId, command.UserId, command.ChatMessage);
                return apiModel = _context.chatHistory
                                          .Where(y => y.ChatId == command.ChatId)
                                          .Select(x => new ChatHistoryApiModel
                                          {
                                              Id = x.Id,
                                              ChatId = x.ChatId,
                                              ChatMessageSenderId = x.ChatMessageSenderId,
                                              ChatMessage = x.ChatMessage
                                          })
                                          .ToList();

            }

        }


    }
}
