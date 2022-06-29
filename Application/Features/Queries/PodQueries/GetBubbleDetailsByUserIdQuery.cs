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

namespace Application.Features.Queries.PodQueries
{
    public class GetBubbleDetailsByUserIdQuery : IRequest<BubbleApiModel>
    {
        public int UserId { get; set; }
        public class GetBubbleDetailsByUserIdHandler : IRequestHandler<GetBubbleDetailsByUserIdQuery, BubbleApiModel>
        {
            private readonly IApplicationDbContext _context;
            public GetBubbleDetailsByUserIdHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<BubbleApiModel> Handle(GetBubbleDetailsByUserIdQuery query, CancellationToken cancellationToken)
            {
                BubbleApiModel bubbmeDetails = new BubbleApiModel();

                bubbmeDetails = _context.bubbleDetails
                    .Join(_context.bubbleMembers, b => b.Id, bm => bm.BubbleId, (b, bm) => new { b, bm })
                    .Where(bmd => bmd.bm.UserId == query.UserId)
                    .Select(x => new BubbleApiModel()
                    {
                        Id = x.b.Id,
                        BubbleName = x.b.BubbleName,
                        BubbleDescription = x.b.BubbleDescription,
                        BubbleSize = x.b.BubbleSize,
                        BubbleType = x.b.BubbleType,
                        BubbleZipCode = x.b.BubbleZipCode,
                        BubbleValidity = x.b.BubbleValidity,
                        IsOtherCountyMemberAllowed = x.b.IsOtherCountyMemberAllowed,
                        CreatedBy = x.b.CreatedBy,
                        CreatedOn = x.b.CreatedOn
                    }).FirstOrDefault();

                bubbmeDetails.lstPodUser = _context.bubbleMembers.Join(_context.userDetails,bm=>bm.UserId,ud=>ud.Id,(bm,ud)=>new { bm,ud}).Where(x => x.bm.BubbleId == bubbmeDetails.Id && x.ud.IsActive == true).Select(y => new UserApiModels()
                {
                    Id = y.ud.Id,
                    Username = y.ud.Username,
                    PhoneNo = y.ud.PhoneNo,
                    ProfilePicUrl = y.ud.ProfilePicUrl,
                    ZipCode = y.ud.ZipCode,
                    County = y.ud.County,
                    CountyName = _context.counties.Where(yy => yy.Fips == y.ud.County).Select(x => x.CountyName).FirstOrDefault(),
                    CreatedBy = y.ud.CreatedBy,
                    CreatedOn = y.ud.CreatedOn,
                    lstUserPermission = (_context.bubbleMeetMemberPermissions
                                       .Where(p => p.UserId == y.ud.Id && p.BubbleMeetId == bubbmeDetails.Id && p.MeetTypeId == MeetType.Bubble)
                                       .Select(kx=>new BubbleMeetPermissionsApiModel {
                                        Id=kx.Id,
                                        MeetTypeId=kx.MeetTypeId,
                                        UserPermissionTypeId=kx.UserPermissionTypeId,
                                        UserPermissionStatus=kx.UserPermissionStatus,
                                        PermissionParenttId=kx.BubbleMeetId,
                                        CreatedBy=kx.CreatedBy,
                                        UpdatedBy=kx.UpdatedBy,
                                        CreatedOn=kx.CreatedOn,
                                        UpdatedOn=kx.UpdatedOn
                                       }).ToList()),
                    countyDetails = _context.counties.Where(xy => xy.Fips == y.ud.County).Select(x => new CountiesApiModel
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

                return bubbmeDetails;
            }
        }

    }
}
