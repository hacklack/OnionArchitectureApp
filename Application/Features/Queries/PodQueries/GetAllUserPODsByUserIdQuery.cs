using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System;
using System.Collections.Generic;
using Application.ApiModels;
using static Domain.CommonCodes.CommonEnums;

namespace Application.Features.Queries.BubbleMemberQueries
{
    public class GetAllUserPODsByUserIdQuery : IRequest<List<PodDetailsApiModel>>
    {
        public int UserId { get; set; }
        public class GetAllUserPODsByUserIdHandler : IRequestHandler<GetAllUserPODsByUserIdQuery, List<PodDetailsApiModel>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllUserPODsByUserIdHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<List<PodDetailsApiModel>> Handle(GetAllUserPODsByUserIdQuery query, CancellationToken cancellationToken)
            {
                List<PodDetailsApiModel> lstPodDetailsApiModel = new List<PodDetailsApiModel>();
                if (query.UserId > 0)
                {
                    List<int> lstPodIds = _context.podDetails.Join(_context.podBubbleMembers, pod => pod.Id, pbm => pbm.PODId, (pod, pbm) => new { pod, pbm }).Where(y => y.pbm.BubbleMemberId == query.UserId).Select(x => x.pbm.PODId).Distinct().ToList();
                    foreach (var podId in lstPodIds)
                    {
                        List<int> lstBubbleds = _context.bubbleDetails.Join(_context.podBubbleMembers, bd => bd.Id, pbm => pbm.BubbleId, (bd, pbm) => new { bd, pbm }).Where(y => y.pbm.BubbleMemberId == query.UserId && y.pbm.PODId == podId).Select(x => x.pbm.BubbleId).Distinct().ToList();

                        foreach(var bubId in lstBubbleds)
                        {
                            PodDetailsApiModel podMod = new PodDetailsApiModel();

                            podMod = await _context.podDetails
                            .Join(_context.podBubbleMembers, pod => pod.Id, pbm => pbm.PODId, (pod, pbm) => new { pod, pbm })
                            .Join(_context.userDetails, pm => pm.pbm.BubbleMemberId, ud => ud.Id, (pm, ud) => new { pm, ud })
                            .Where(u => u.pm.pbm.BubbleMemberId == query.UserId 
                            && u.pm.pbm.PODDetails.Id == podId 
                            && u.pm.pbm.BubbleId==bubId
                            && u.ud.IsActive == true)
                            .Select(x => new PodDetailsApiModel
                            {
                                Id = x.pm.pod.Id,
                                PODName = x.pm.pod.PODName,
                                PODBubbleType = x.pm.pod.PODBubbleType,
                                PODDescription = x.pm.pod.PODDescription,
                                PODSize = x.pm.pod.PODSize,
                                lstPodBubbleApiModel = _context.bubbleDetails
                                                        .Join(_context.podBubbleMembers, bd => bd.Id, pbm => pbm.BubbleId, (bd, pbm) => new { bd, pbm })
                                                        //.Join(_context.bubbleDetails, pbm => pbm.pbm.BubbleId, bd => bd.Id, (pbm, bd) => new { pbm, bd })
                                                        .Join(_context.userDetails, pm => pm.pbm.BubbleMemberId, ud => ud.Id, (pm, ud) => new { pm, ud })
                                                        .Where(u => u.pm.pbm.Id == query.UserId && u.pm.pbm.PODId == podId && u.pm.pbm.BubbleId==bubId && u.ud.IsActive == true)
                                                        .Select(x => new BubbleApiModel
                                                        {
                                                            Id = x.pm.bd.Id,
                                                            BubbleName = x.pm.bd.BubbleName,
                                                            BubbleDescription = x.pm.bd.BubbleDescription,
                                                            BubbleSize = x.pm.bd.BubbleSize,
                                                            BubbleType = x.pm.bd.BubbleType,
                                                            BubbleValidity = x.pm.bd.BubbleValidity,
                                                            BubbleZipCode = x.pm.bd.BubbleZipCode,
                                                            CreatedBy = x.pm.bd.CreatedBy,
                                                            UpdatedBy = x.pm.bd.UpdatedBy,
                                                            CreatedOn = x.pm.bd.CreatedOn,
                                                            UpdatedOn = x.pm.bd.UpdatedOn,
                                                            lstPodUser = _context.userDetails
                                                            .Join(_context.podBubbleMembers, user => user.Id, podBubMem => podBubMem.BubbleMemberId, (user, podBubMem) => new { user, podBubMem })
                                                             //.Join(_context.podMembers, bubMem => bubMem.podBubMem.BubbleId, pm => pm.BubbleId, (bubMem, pm) => new { bubMem, pm })
                                                             .Where(pbm => pbm.podBubMem.PODId == podId && pbm.podBubMem.BubbleId==bubId && pbm.user.IsActive == true)
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
                            })
                            .FirstOrDefaultAsync();
                            lstPodDetailsApiModel.Add(podMod);

                        }
                       
                    }

                    
                }
                if (lstPodDetailsApiModel == null)
                {
                    return null;
                }
                return lstPodDetailsApiModel;
            }
        }

    }
}
