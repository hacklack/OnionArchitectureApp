using MediatR;
using Domain.Entities;
using Application.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using Application.ApiModels;
using System;
using System.Linq;
using System.Collections.Generic;
using Application.Services;

namespace Application.Features.Commands.BubbleMemberCommands
{
    public class DeleteMembersCommand : IRequest<int>
    {
        public List<int> UserIds { get; set; }
        public int BubbbleId { get; set; }

        public class DeleteMemberCommandHandler : IRequestHandler<DeleteMembersCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public DeleteMemberCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteMembersCommand command, CancellationToken cancellationToken)
            {
                var member = new BubbleMembers();
                BubbleSafetyDetails bubbleSafetyDetails = new BubbleSafetyDetails();
                List<int> leftBubbleUserIds = new List<int>();
                double bubbleSaftyValue = 0;
                foreach (var item in command.UserIds)
                {
                    member = _context
                            .bubbleMembers
                            .Where(m => m.UserId == item && m.BubbleId == command.BubbbleId)
                            .FirstOrDefault();
                    _context.bubbleMembers.Remove(member);
                    await _context.SaveChanges();
                }
                
                leftBubbleUserIds = _context.bubbleMembers.Where(y => y.BubbleId == command.BubbbleId).Select(x => x.UserId).ToList();
                NotificationsServices notificationsServices = new NotificationsServices(_context);
                bubbleSaftyValue =await notificationsServices.BubbleSaftyCalculation(command.UserIds);
                bubbleSafetyDetails = _context.bubbleSafetyDetails.Where(y => y.BubblePODId == command.BubbbleId).FirstOrDefault();
                bubbleSafetyDetails.BubbleSaftyValue = bubbleSaftyValue;
                await _context.SaveChanges();

                return command.BubbbleId;
            }
        }

    }
}
