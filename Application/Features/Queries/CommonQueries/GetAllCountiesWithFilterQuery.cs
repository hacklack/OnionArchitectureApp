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
    public class GetAllCountiesWithFilterQuery : IRequest<List<CountiesApiModel>>
    {
        public string searchTxt { get; set; }

        public class GetAllCountiesWithFilterQueryHandler : IRequestHandler<GetAllCountiesWithFilterQuery, List<CountiesApiModel>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllCountiesWithFilterQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<List<CountiesApiModel>> Handle(GetAllCountiesWithFilterQuery query, CancellationToken cancellationToken)
            {
                var CountyList = await _context.counties.Where(y=>y.CountyName.Contains(query.searchTxt))
                                .Select(x=>new CountiesApiModel {
                                  Id=x.Fips,
                                  //Country=x.Country,
                                  Fips=x.Fips,
                                  CountyNameFormatted=x.CountyName+"-"+x.State,
                                  //CountyName=x.CountyName,
                                  //State=x.State
                                 }).ToListAsync();
                return CountyList;
            }
        }
    }
}
