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

namespace Application.Features.Queries.BubbleQueries
{
   public class GetAllBubbleQuery:IRequest<List<BubbleApiModel>>
    {
        public class GetAllBubbleQueryHandler : IRequestHandler<GetAllBubbleQuery,List<BubbleApiModel>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllBubbleQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<List<BubbleApiModel>> Handle(GetAllBubbleQuery query, CancellationToken cancellationToken)
            {
                var bubbleList = await _context.bubbleDetails
                    .Join(_context.bubbleMembers, bd => bd.Id, bm => bm.BubbleId, (bd, bm) => new { bd, bm })
                    .Join(_context.userDetails, bma => bma.bm.UserId, ud => ud.Id, (bma, ud) => new { bma, ud })
                    .Where(bda => bda.bma.bm.IsBubbleAdmin == true && bda.bma.bm.UserId == bda.ud.Id)
                    .Select(x => new BubbleApiModel
                    {
                        Id = x.bma.bd.Id,
                        BubbleName = x.bma.bd.BubbleName,
                        BubbleDescription = x.bma.bd.BubbleDescription,
                        BubbleSize = x.bma.bd.BubbleSize,
                        BubbleType = x.bma.bd.BubbleType,
                        BubbleValidity = x.bma.bd.BubbleValidity,
                        BubbleZipCode = x.bma.bd.BubbleZipCode,
                        CreatedBy = x.bma.bd.CreatedBy,
                        CreatedOn = x.bma.bd.CreatedOn,
                        IsOtherCountyMemberAllowed = x.bma.bd.IsOtherCountyMemberAllowed,
                        BubbleCounty = x.ud.County
                    }).ToListAsync();
                if (bubbleList == null)
                {
                    return null;
                }
                return bubbleList;
            }
        }
    }
}
