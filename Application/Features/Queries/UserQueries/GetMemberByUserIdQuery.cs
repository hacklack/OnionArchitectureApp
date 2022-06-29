using MediatR;
using Domain.Entities;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using System.Linq;
using System.Collections.Generic;

namespace Application.Features.Queries.UserQueries
{
    public class GetMemberByUserIdQuery : IRequest<List<UserDetails>>
    {
        public int UserId { get; set; }
        public class GetMemberByUserIdQueryHandler : IRequestHandler<GetMemberByUserIdQuery, List<UserDetails>>
        {
            private readonly IApplicationDbContext _context;
            public GetMemberByUserIdQueryHandler(IApplicationDbContext context) => _context = context;

            public async Task<List<UserDetails>> Handle(GetMemberByUserIdQuery query, CancellationToken cancellationToken)
            {
                var user = await _context.userDetails
                                .Where(a => (a.CreatedBy == query.UserId)
                                && (a.IsActive == true)
                                ).ToListAsync();
                return user;
            }
        }
    }
}
