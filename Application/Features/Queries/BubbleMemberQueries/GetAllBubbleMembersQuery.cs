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
    public class GetAllBubbleMembersQuery : IRequest<IEnumerable<BubbleMembers>>
    {
        public class GetAllBubbleMembersHandler : IRequestHandler<GetAllBubbleMembersQuery, IEnumerable<BubbleMembers>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllBubbleMembersHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<BubbleMembers>> Handle(GetAllBubbleMembersQuery query, CancellationToken cancellationToken)
            {
                var members = await _context.bubbleMembers.ToListAsync();
                if (members == null)
                {
                    return null;
                }
                return members.AsReadOnly();
            }
        }

    }
}
