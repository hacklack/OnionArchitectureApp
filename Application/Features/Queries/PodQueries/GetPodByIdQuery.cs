using Application.ApiModels;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Domain.CommonCodes.CommonEnums;

namespace Application.Features.Queries.PodQueries
{
    public class GetPodByIdQuery : IRequest<PodDetailsApiModel>
    {
        public int PodId { get; set; }
        public class GetPodByIdHandler : IRequestHandler<GetPodByIdQuery, PodDetailsApiModel>
        {
            private readonly IApplicationDbContext _context;
            public GetPodByIdHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<PodDetailsApiModel> Handle(GetPodByIdQuery query, CancellationToken cancellationToken)
            {
                PodDetailsApiModel podDetailsApiModel = new PodDetailsApiModel();
                if (query.PodId > 0)
                {
                    podDetailsApiModel = await _context.podDetails.Where(p => p.Id == query.PodId)
                        .Select(x => new PodDetailsApiModel
                        {
                            Id = x.Id,
                            PODName = x.PODName,
                            PODBubbleType = x.PODBubbleType,
                            PODDescription = x.PODDescription,
                            PODSize = x.PODSize,
                            podSafetyDetails = _context.bubbleSafetyDetails
                                                   .Join(_context.podDetails, bsd => bsd.BubblePODId, bd => bd.Id, (bsd, bd) => new { bsd, bd })
                                                   .Where(y => y.bsd.BubbleSaftyTypeId == BubbleSaftyType.PODSaftyLevel && y.bsd.BubblePODId == x.Id)
                                                   .Select(xbsd => new BubbleSafetyDetailsApiModel
                                                   {
                                                       BubbleSaftyValue = xbsd.bsd.BubbleSaftyValue
                                                   }).FirstOrDefault(),
                            lstPodBubbleApiModel = _context.bubbleDetails
                                                    .Join(_context.podMembers, bubDet => bubDet.Id, podMem => podMem.BubbleId, (bubDet, podMem) => new { bubDet, podMem })
                                                    .Where(pmd => pmd.podMem.PODId == query.PodId)
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
                                                        lstPodUser = _context.userDetails
                                           .Join(_context.podBubbleMembers, user => user.Id, podBubMem => podBubMem.BubbleMemberId, (user, podBubMem) => new { user, podBubMem })
                                           // .Join(_context.podMembers, bubMem => bubMem.podBubMem.BubbleId, pm => pm.BubbleId, (bubMem, pm) => new { bubMem, pm })
                                           .Where(pbm => pbm.podBubMem.PODId == query.PodId && (pbm.podBubMem.BubbleId == x.podMem.BubbleDetails.Id) && (pbm.user.IsActive == true))
                                           .Select(x => new UserApiModels
                                           {
                                               Id = x.user.Id,
                                               Username = x.user.Username,
                                               ProfilePicUrl = x.user.ProfilePicUrl,
                                               County = x.user.County,
                                               CountyName = _context.counties.Where(y => y.Fips == x.user.County).Select(x => x.CountyName).FirstOrDefault(),
                                               PhoneNo = x.user.PhoneNo,
                                               ZipCode = x.user.ZipCode,
                                               CreatedBy = x.user.CreatedBy,
                                               UpdatedBy = x.user.UpdatedBy,
                                               CreatedOn = x.user.CreatedOn,
                                               UpdatedOn = x.user.UpdatedOn,
                                               countyDetails = _context.counties.Where(y => y.Fips == x.user.County).Select(x => new CountiesApiModel
                                               {
                                                   Id = x.Id,
                                                   Fips = x.Fips,
                                                   Country = x.Country,
                                                   CountyName = x.CountyName,
                                                   State = x.State,
                                                   CreatedBy = x.CreatedBy,
                                                   UpdatedBy = x.UpdatedBy,
                                                   CreatedOn = x.CreatedOn,
                                                   UpdatedOn = x.UpdatedOn

                                               }).FirstOrDefault()
                                           }).ToList()
                                                    }).ToList()
                        }).FirstOrDefaultAsync();

                    //pODApiModel.lstUser = await _context.userDetails
                    //                       .Join(_context.podBubbleMembers, user => user.Id, podBubMem => podBubMem.BubbleMemberId, (user, podBubMem) => new { user, podBubMem })
                    //                       .Join(_context.podMembers, bubMem => bubMem.podBubMem.BubbleId, pm => pm.BubbleId, (bubMem, pm) => new { bubMem, pm })
                    //                       .Where(pbm => pbm.bubMem.podBubMem.PODId == query.PodId && pbm.pm.PODId == query.PodId)
                    //                       .Select(x => new UserApiModels {
                    //                       Id=x.bubMem.user.Id,
                    //                       Username = x.bubMem.user.Username,
                    //                       ProfilePicUrl = x.bubMem.user.ProfilePicUrl,
                    //                       County = x.bubMem.user.County,
                    //                       PhoneNo = x.bubMem.user.PhoneNo,
                    //                       ZipCode = x.bubMem.user.ZipCode,
                    //                       CreatedBy = x.bubMem.user.CreatedBy,
                    //                       UpdatedBy = x.bubMem.user.UpdatedBy,
                    //                       CreatedOn = x.bubMem.user.CreatedOn,
                    //                       UpdatedOn = x.bubMem.user.UpdatedOn
                    //                       }).ToListAsync();
                }
                if (podDetailsApiModel == null)
                {
                    return null;
                }
                return podDetailsApiModel;
            }

        }

    }
}
