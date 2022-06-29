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
    public class GetAllCountiesByNameQuery : IRequest<IEnumerable<Counties>>
    {
        public string Name { get; set; }
        public class GetAllCountiesByNameQueryHandler : IRequestHandler<GetAllCountiesByNameQuery, IEnumerable<Counties>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllCountiesByNameQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Counties>> Handle(GetAllCountiesByNameQuery query, CancellationToken cancellationToken)
            {
                var CountyList = await _context.counties
                    .Where(x => x.CountyName.Contains(query.Name)).ToListAsync();
                return CountyList.AsReadOnly();
            }
        }
    }
}
