using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.ApiModels;
using System.Linq;

namespace Application.Features.Queries.CommonQueries
{
    public class GetAllCountiesQuery : IRequest<List<CountiesApiModel>>
    {
        public class GetAllCountiesQueryHandler : IRequestHandler<GetAllCountiesQuery, List<CountiesApiModel>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllCountiesQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<List<CountiesApiModel>> Handle(GetAllCountiesQuery query, CancellationToken cancellationToken)
            {
                var CountyList = await _context.counties
                                .Select(x=>new CountiesApiModel {
                                  Id=x.Fips,
                                  Country=x.Country,
                                  CountyNameFormatted=x.CountyName+"-"+x.State,
                                  CountyName=x.CountyName,
                                  State=x.State,
                                  Fips=x.Fips
                                 }).ToListAsync();
                return CountyList;
            }
        }
    }
}
