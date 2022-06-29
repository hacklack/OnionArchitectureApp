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
   public class GetPODCreateInitialsQuerys :IRequest<PODApiModel>
    {
        public int UserId { get; set; }
        public class GetPODCreateInitialsHandler : IRequestHandler<GetPODCreateInitialsQuerys, PODApiModel>
        {
            private readonly IApplicationDbContext _context;
            public GetPODCreateInitialsHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<PODApiModel> Handle(GetPODCreateInitialsQuerys query, CancellationToken cancellationToken)
            {
                PODApiModel bubbleList = new PODApiModel();
                bubbleList.PODBubbleType = new BubbleType(); 
                bubbleList.lstBubbleApiModel = await _context.bubbleDetails
                                        .Join(_context.bubbleMembers, a => a.Id, bm => bm.BubbleId, (a, bm) => new { a, bm })
                                        .Where(abm => abm.bm.UserId == query.UserId

                                         ).Select(
                                                 x => new BubbleApiModel()
                                                 {
                                                     Id = x.a.Id,
                                                    // BubbleDescription = x.a.BubbleDescription,
                                                     BubbleName = x.a.BubbleName,
                                                    // BubbleSize = x.a.BubbleSize,
                                                     BubbleType = x.a.BubbleType,
                                                    // BubbleValidity = x.a.BubbleValidity,
                                                   //  BubbleZipCode = x.a.BubbleZipCode,
                                                   //  IsOtherCountyMemberAllowed = x.a.IsOtherCountyMemberAllowed,
                                                  //   CreatedBy = x.a.CreatedBy,
                                                   //  CreatedOn = x.a.CreatedOn
                                                 }
                                            ).ToListAsync();
                if (bubbleList == null)
                {
                    return null;
                }
                return bubbleList;
            }
        }
    }
}
