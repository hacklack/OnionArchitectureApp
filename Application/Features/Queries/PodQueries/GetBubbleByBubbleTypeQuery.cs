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
using Application.ApiModels;

namespace Application.Features.Queries.PodQueries
{
    public class GetBubbleByBubbleTypeQuery : IRequest<List<BubbleApiModel>>
    {
        public BubbleType Bubbletype { get; set; }

        public class GetBubbleByBubbleTypeHandler : IRequestHandler<GetBubbleByBubbleTypeQuery, List<BubbleApiModel>>
        {
            private readonly IApplicationDbContext _context;
            public GetBubbleByBubbleTypeHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<List<BubbleApiModel>> Handle(GetBubbleByBubbleTypeQuery query, CancellationToken cancellationToken)
            {

                var bubble = await _context.bubbleDetails
                    .Where(a => (query.Bubbletype == 0 ? a.BubbleType != 0 : a.BubbleType == query.Bubbletype))
                    .Select(x => new BubbleApiModel
                    {
                        Id = x.Id,
                        BubbleName = x.BubbleName,
                        BubbleDescription = x.BubbleDescription,
                        BubbleSize = x.BubbleSize,
                        BubbleType = x.BubbleType,
                        BubbleZipCode = x.BubbleZipCode,
                        IsOtherCountyMemberAllowed = x.IsOtherCountyMemberAllowed,
                        lstPodUser = (_context.userDetails
                                .Join(_context.bubbleMembers, ud => ud.Id, bm => bm.UserId, (ud, bm) => new { ud, bm })
                                .Where(y => y.bm.BubbleId == x.Id && y.ud.IsActive)
                                .Select(u => new UserApiModels
                                {
                                    Id = u.ud.Id,
                                    Username = u.ud.Username,
                                    County = u.ud.County,
                                    CountyName = _context.counties.Where(y => y.Fips == u.ud.County).Select(x => x.CountyName).FirstOrDefault(),
                                    ProfilePicUrl = u.ud.ProfilePicUrl,
                                    PhoneNo = u.ud.PhoneNo,
                                    IsAdmin = _context.bubbleMeetMemberPermissions
                                                                .Where(p => p.UserId == u.ud.Id && p.BubbleMeetId == x.Id
                                                                && p.UserPermissionTypeId == 0 && p.MeetTypeId == MeetType.Bubble)
                                                                .Select(x => x.UserPermissionStatus)
                                                                .FirstOrDefault(),
                                    countyDetails = _context.counties.Where(y => y.Fips == u.ud.County).Select(x => new CountiesApiModel
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
                                })).ToList()
                    }).ToListAsync();
                if (bubble == null)
                {
                    return null;
                }
                return bubble;
            }
        }
    }
}
