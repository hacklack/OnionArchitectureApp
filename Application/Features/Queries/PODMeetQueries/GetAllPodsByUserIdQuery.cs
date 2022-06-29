using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.ApiModels;

namespace Application.Features.Queries.PODMeetQueries
{

    public class GetAllPodsByUserIdQuery : IRequest<List<PodDetailsApiModel>>
    {
        public int UserId { get; set; }
        public class GetAllPodsByUserIdHandler : IRequestHandler<GetAllPodsByUserIdQuery, List<PodDetailsApiModel>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllPodsByUserIdHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<List<PodDetailsApiModel>> Handle(GetAllPodsByUserIdQuery query, CancellationToken cancellationToken)
            {
                List<PodDetailsApiModel> lstPODDetailsApiModel = new List<PodDetailsApiModel>();

                lstPODDetailsApiModel = await _context.podDetails
                                        .Join(_context.podBubbleMembers, pd => pd.Id, pmm => pmm.PODId, (pd, pmm) => new { pd, pmm })
                                        .Where(y => y.pmm.BubbleMemberId == query.UserId)
                        .Select(x => new PodDetailsApiModel
                        {
                            Id = x.pd.Id,
                            PODName = x.pd.PODName,
                            PODBubbleType = x.pd.PODBubbleType,
                            PODDescription = x.pd.PODDescription,
                            PODSize = x.pd.PODSize,
                            CreatedBy = x.pd.CreatedBy,
                            UpdatedBy = x.pd.UpdatedBy,
                            CreatedOn = x.pd.CreatedOn,
                            UpdatedOn = x.pd.UpdatedOn
                        })
                        .ToListAsync();
                if (lstPODDetailsApiModel == null)
                {
                    return default;
                }
                return lstPODDetailsApiModel;
            }
        }
    }
}
