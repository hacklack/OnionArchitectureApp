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

namespace Application.Features.Queries.PodQueries
{

    public class GetAllPodQuery : IRequest<List<PodDetailsApiModel>>
    {
        public class GetAllPodQueriesHandler : IRequestHandler<GetAllPodQuery, List<PodDetailsApiModel>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllPodQueriesHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<List<PodDetailsApiModel>> Handle(GetAllPodQuery query, CancellationToken cancellationToken)
            {
               List<PodDetailsApiModel> lstPODDetailsApiModel = new List<PodDetailsApiModel>();

                lstPODDetailsApiModel = await _context.podDetails
                        .Select(x => new PodDetailsApiModel
                        {
                            Id = x.Id,
                            PODName = x.PODName,
                            PODBubbleType = x.PODBubbleType,
                            PODDescription = x.PODDescription,
                            PODSize = x.PODSize,
                            lstPodBubbleApiModel = _context.bubbleDetails
                                                    .Join(_context.podMembers, bubDet => bubDet.Id, podMem => podMem.BubbleId, (bubDet, podMem) => new { bubDet, podMem })
                                                    .Where(pmd => pmd.podMem.PODId == x.Id)
                                                    .Select(x => new BubbleApiModel
                                                    {
                                                        Id = x.podMem.BubbleDetails.Id,
                                                        BubbleName = x.podMem.BubbleDetails.BubbleName,
                                                        BubbleDescription = x.podMem.BubbleDetails.BubbleDescription,
                                                        BubbleSize = x.podMem.BubbleDetails.BubbleSize,
                                                        BubbleType = x.podMem.BubbleDetails.BubbleType,
                                                        BubbleValidity = x.podMem.BubbleDetails.BubbleValidity,
                                                        BubbleZipCode = x.podMem.BubbleDetails.BubbleZipCode,
                                                        CreatedBy = x.podMem.BubbleDetails.CreatedBy,
                                                        UpdatedBy = x.podMem.BubbleDetails.UpdatedBy,
                                                        CreatedOn = x.podMem.BubbleDetails.CreatedOn,
                                                        UpdatedOn = x.podMem.BubbleDetails.UpdatedOn,
                                                    }).ToList()
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
