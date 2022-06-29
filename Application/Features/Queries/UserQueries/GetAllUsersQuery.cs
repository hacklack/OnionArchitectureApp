using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Queries.UserQueries
{
   public class GetAllUsersQuery : IRequest<IEnumerable<UserDetails>>
    {
        public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDetails>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllUsersQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<UserDetails>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
            {
                var userList = await _context.userDetails.Where(ud=>ud.IsActive==true).ToListAsync();
                if (userList == null)
                {
                    return null;
                }
                return userList.AsReadOnly();
            }
        }
    }
}
