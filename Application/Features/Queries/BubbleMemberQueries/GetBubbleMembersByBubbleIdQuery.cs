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
    public class GetBubbleMembersByBubbleIdQuery : IRequest<BubbleMembersApiModel>
    {
        public int BubbleId { get; set; }
        public class GetBubbleMembersByBubbleIdHandler : IRequestHandler<GetBubbleMembersByBubbleIdQuery, BubbleMembersApiModel>
        {
            private readonly IApplicationDbContext _context;
            public GetBubbleMembersByBubbleIdHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<BubbleMembersApiModel> Handle(GetBubbleMembersByBubbleIdQuery query, CancellationToken cancellationToken)
            {
                BubbleMembersApiModel bubbmeMemberDetails = new BubbleMembersApiModel();

                bubbmeMemberDetails.UserList = _context.userDetails
                    .Join(_context.bubbleMembers, u => u.Id, bm => bm.UserId, (u, bm) => new { u, bm })
                    .Where(bmd => bmd.bm.BubbleId == query.BubbleId && bmd.u.IsActive == true)
                    .Select(x => new UserApiModels()
                    {
                        Id = x.u.Id,
                        Username = x.u.Username,
                        ZipCode = x.u.ZipCode,
                        County = x.u.County,
                        CountyName = _context.counties.Where(y => y.Fips == x.u.County).Select(x => x.CountyName).FirstOrDefault(),
                        ProfilePicUrl = x.u.ProfilePicUrl,
                        PhoneNo = x.u.PhoneNo,
                        IsAdmin= (_context.bubbleMeetMemberPermissions
                                       .Where(p => p.UserId == x.u.Id && p.BubbleMeetId == x.bm.BubbleId
                                       && p.UserPermissionTypeId == 0 && p.MeetTypeId == MeetType.Bubble)
                                       .Select(x => x.UserPermissionStatus)
                                       .FirstOrDefault()),
                        countyDetails = _context.counties.Where(y => y.Fips == x.u.County).Select(x => new CountiesApiModel
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
                    }).ToList();

                bubbmeMemberDetails.Bubble = _context.bubbleDetails.Where(x => x.Id == query.BubbleId).Select(y => new BubbleApiModel()
                {
                    Id = y.Id,
                    BubbleName = y.BubbleName,
                    BubbleDescription = y.BubbleDescription,
                    BubbleSize = y.BubbleSize,
                    BubbleType = y.BubbleType,
                    BubbleZipCode = y.BubbleZipCode,
                    BubbleValidity = y.BubbleValidity
                }).FirstOrDefault();

                if (bubbmeMemberDetails == null)
                {
                    return null;
                }
                return bubbmeMemberDetails;
            }
        }

    }
}
