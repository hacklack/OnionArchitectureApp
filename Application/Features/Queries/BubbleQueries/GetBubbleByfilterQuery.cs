using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System;
using System.Collections.Generic;
using static Domain.CommonCodes.CommonEnums;

namespace Application.Features.Commands.BubbleCommands
{
    public class GetBubbleByfilterQuery : IRequest<IEnumerable<BubbleDetails>>
    {
        public BubbleType Bubbletype { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }

        public class GetBubbleByfilterQueryHandler : IRequestHandler<GetBubbleByfilterQuery, IEnumerable<BubbleDetails>>
        {
            private readonly IApplicationDbContext _context;
            public GetBubbleByfilterQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<BubbleDetails>> Handle(GetBubbleByfilterQuery query, CancellationToken cancellationToken)
            {

                var bubble = await _context.bubbleDetails.Where(a => (query.Name == null ? a.BubbleName != null : a.BubbleName == query.Name)
                                                                  && (query.Bubbletype == 0 ? a.BubbleType != 0 : a.BubbleType == query.Bubbletype)
                                                                  && (query.Size == null ? a.BubbleSize != null : a.BubbleSize == query.Size)
                                                                  ).ToListAsync();
                if (bubble == null)
                {
                    return null;
                }
                return bubble.AsReadOnly();
            }
        }
    }
}
