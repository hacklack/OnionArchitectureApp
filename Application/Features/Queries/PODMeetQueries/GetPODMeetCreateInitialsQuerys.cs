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

namespace Application.Features.Queries.PODMeetQueries
{
    public class GetPODMeetCreateInitialsQuerys : IRequest<List<BubbleApiModel>>
    {
        public int UserId { get; set; }
        public class GetPODMeetCreateInitialsHandler : IRequestHandler<GetPODMeetCreateInitialsQuerys, List<BubbleApiModel>>
        {   
            private readonly IApplicationDbContext _context;
            public GetPODMeetCreateInitialsHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<List<BubbleApiModel>> Handle(GetPODMeetCreateInitialsQuerys query, CancellationToken cancellationToken)
            {
                List<int> bubbleIds = new List<int>();
                List<BubbleApiModel> bubbleList = new List<BubbleApiModel>();
                int bubbleId = _context.bubbleMembers.Where(y => y.UserId == query.UserId).Select(x => x.BubbleId).FirstOrDefault();
                List<int> podIds = _context.podMembers.Where(yb => yb.BubbleId == bubbleId).Select(xb => xb.PODId).ToList();
                foreach (var podId in podIds)
                {

                    List<int> bubbleIdsx = _context.podMembers.Where(yc => yc.PODId == podId).Select(xc => xc.BubbleId).Distinct().ToList();
                    bubbleIdsx.Remove(bubbleId);
                    foreach (var bub in bubbleIdsx)
                    {
                        if (!bubbleIds.Contains(bub))
                        {
                            bubbleIds.Add(bub);
                        }
                    }
                }   
                foreach (var item in bubbleIds)
                {
                    BubbleApiModel bubble = new BubbleApiModel();
                    bubble = _context.bubbleDetails.Where(yd => yd.Id == item).Select(xd => new BubbleApiModel
                    {
                        Id = xd.Id,
                        BubbleDescription = xd.BubbleDescription,
                        BubbleName = xd.BubbleName,
                        BubbleSize = xd.BubbleSize,
                        BubbleType = xd.BubbleType,
                        BubbleValidity = xd.BubbleValidity,
                        BubbleZipCode = xd.BubbleZipCode,
                        IsOtherCountyMemberAllowed = xd.IsOtherCountyMemberAllowed,
                        CreatedBy = xd.CreatedBy,
                        CreatedOn = xd.CreatedOn

                    }).FirstOrDefault();
                    bubbleList.Add(bubble);
                }
                //bubbleList = await _context.bubbleDetails
                //                        .Join(_context.podBubbleMembers, a => a.Id, bm => bm.BubbleId, (a, bm) => new { a, bm })
                //                        .Join(_context.podMembers, an => an.a.Id, pm => pm.BubbleId, (an, pm) => new { an, pm })
                //                        .Where(abm => abm.an.bm.BubbleMemberId == query.UserId && abm.an.a.Id == abm.pm.BubbleId

                //                         ).Select(
                //                                 x => new BubbleApiModel()
                //                                 {
                //                                     Id = x.an.a.Id,
                //                                     BubbleDescription = x.an.a.BubbleDescription,
                //                                     BubbleName = x.an.a.BubbleName,
                //                                     BubbleSize = x.an.a.BubbleSize,
                //                                     BubbleType = x.an.a.BubbleType,
                //                                     BubbleValidity = x.an.a.BubbleValidity,
                //                                     BubbleZipCode = x.an.a.BubbleZipCode,
                //                                     IsOtherCountyMemberAllowed = x.an.a.IsOtherCountyMemberAllowed,
                //                                     CreatedBy = x.an.a.CreatedBy,
                //                                     CreatedOn = x.an.a.CreatedOn
                //                                 }
                //                            ).ToListAsync();
                if (bubbleList == null)
                {
                    return null;
                }
                return bubbleList;
            }
        }
    }
}
