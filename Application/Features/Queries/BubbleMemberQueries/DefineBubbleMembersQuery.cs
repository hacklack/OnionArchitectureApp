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


    public class DefineBubbleMembersQuery:IRequest<int>
    {
        public int BubbleId { get; set; }
        public int MemberLength { get; set; }
        public class DefineBubbleMembersHandler : IRequestHandler<DefineBubbleMembersQuery, int>
        {
            private readonly IApplicationDbContext _context;
            public DefineBubbleMembersHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DefineBubbleMembersQuery query, CancellationToken cancellationToken)
            {
                var lengthdefine = query.MemberLength;
                var members = _context.bubbleMembers.Where(m => m.BubbleId == query.BubbleId);
                if (members == null)
                {
                    return 0;
                }
                var countmem = members.Count();
                if(countmem <= lengthdefine)
                {
                    return members.Count();
                }
                return 0;
            }
        }

    }
}
