using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Application.Features.Queries.BubbleMemberQueries
{


    public class CountBubbleMembersByBubbleIdQuery : IRequest<int>
    {
        public int BubbleId { get; set; }
        public class CountBubbleMembersByBubbleIdHandler : IRequestHandler<CountBubbleMembersByBubbleIdQuery, int>
        {
            private readonly IApplicationDbContext _context;
            public CountBubbleMembersByBubbleIdHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(CountBubbleMembersByBubbleIdQuery query, CancellationToken cancellationToken)
            {
                var members = _context.bubbleMembers.Where(m => m.BubbleId == query.BubbleId);
                if (members == null)
                {
                    return 0;
                }
                return members.Count();
            }
        }

    }
}
