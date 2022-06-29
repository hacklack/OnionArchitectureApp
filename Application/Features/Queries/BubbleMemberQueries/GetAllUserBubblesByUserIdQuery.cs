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

namespace Application.Features.Queries.BubbleMemberQueries
{
    public class GetAllUserBubblesByUserIdQuery : IRequest<BubbleMembersApiModel>
    {
        public int UserId { get; set; }
        public class GetBubbleMembersByBubbleIdHandler : IRequestHandler<GetAllUserBubblesByUserIdQuery, BubbleMembersApiModel>
        {
            private readonly IApplicationDbContext _context;
            public GetBubbleMembersByBubbleIdHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<BubbleMembersApiModel> Handle(GetAllUserBubblesByUserIdQuery query, CancellationToken cancellationToken)
            {
                BubbleMembersApiModel bubbmeMemberDetails = new BubbleMembersApiModel();

                bubbmeMemberDetails.BubblesList = _context.bubbleDetails
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
                    }).ToList();

                bubbmeMemberDetails.User = _context.userDetails.Where(x => x.Id == query.UserId && x.IsActive == true).Select(y => new UserApiModels()
                {
                    Id = y.Id,
                    Username = y.Username,
                    PhoneNo = y.PhoneNo,
                    ProfilePicUrl = y.ProfilePicUrl,
                    ZipCode = y.ZipCode,
                    County = y.County,
                    CountyName = _context.counties.Where(yy => yy.Fips == y.County).Select(x => x.CountyName).FirstOrDefault(),
                    CreatedBy = y.CreatedBy,
                    CreatedOn = y.CreatedOn,
                    countyDetails = _context.counties.Where(xy => xy.Fips == y.County).Select(x => new CountiesApiModel
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
                }).FirstOrDefault();

                return bubbmeMemberDetails;
            }
        }

    }
}
