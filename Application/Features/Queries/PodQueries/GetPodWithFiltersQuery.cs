using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace Application.Features.Queries.PodQueries
{
    public class GetPodWithFiltersQuery : IRequest<PodDetails>
    {
        public string PName { get; set; }
        public class GetPodWithFiltersHandler : IRequestHandler<GetPodWithFiltersQuery, PodDetails>
        {
            private readonly IApplicationDbContext _context;
            public GetPodWithFiltersHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<PodDetails> Handle(GetPodWithFiltersQuery query, CancellationToken cancellationToken)
            {
                var podList = await _context.podDetails.Where(m => m.PODName == query.PName).FirstOrDefaultAsync();
                if (podList == null)
                {
                    return null;
                }
                return podList;
            }
        }
    }
}
