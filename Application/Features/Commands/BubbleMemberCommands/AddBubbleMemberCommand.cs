using MediatR;
using Domain.Entities;
using Application.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using Application.ApiModels;
using System;
using System.Linq;
using System.Collections.Generic;
using static Domain.CommonCodes.CommonEnums;

namespace Application.Features.Commands.BubbleMemberCommands
{
    public class AddBubbleMemberCommand:IRequest<List<BubbleMembers>>
    {
        public int BubbleId { get; set; }
        public int createdBy { get; set; }
        public int updatedBy { get; set; }
        public DateTime updatedOn { get; set; }
        public List<int> UserIds { get; set; }
        public BubbleType MemberBubbleType { get; set; }


        public class AddBubbleMemberCommandHandler : IRequestHandler<AddBubbleMemberCommand, List<BubbleMembers>>
        {
            private readonly IApplicationDbContext _context;
            public AddBubbleMemberCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<BubbleMembers>> Handle(AddBubbleMemberCommand command, CancellationToken cancellationToken)
            {
                List<BubbleMembers> lstmembers = new List<BubbleMembers>();
               
                foreach (var item in command.UserIds)
                {
                    if (_context.bubbleMembers.Where(x => x.UserId == item).Count() == 0)
                    {
                        var member = new BubbleMembers();
                        member.BubbleId = command.BubbleId;
                        member.UserId = item;
                        member.MemberBubbleType = command.MemberBubbleType;
                        member.CreatedBy = command.createdBy;
                        member.UpdatedBy = command.updatedBy;
                        _context.bubbleMembers.Add(member);
                        lstmembers.Add(member);
                        await _context.SaveChanges();
                    }
                }
                return lstmembers ;
            }
        }
    }
}
