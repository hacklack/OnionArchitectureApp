using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Domain.Entities;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using System.Linq;

namespace Application.Features.Queries.UserQueries
{
    public class GetUserByIdQuery : IRequest<UserDetails>
    {
        public int Id { get; set; }
        public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDetails>
        {
            private readonly IApplicationDbContext _context;
            public GetUserByIdQueryHandler(IApplicationDbContext context) => _context = context;

            public async Task<UserDetails> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
            {
                var user =  _context.userDetails.Where(a => (a.Id == query.Id) && (a.IsActive == true)).FirstOrDefault();
                if (user == null)
                {
                    return null;
                }
                return user;
            }
        }
    }
}
