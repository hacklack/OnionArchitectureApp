using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Application.ApiModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Queries.BubbleMeetQueries
{
    public class GetBubbleMeetByIdQuery : IRequest<BubbleMeetDetailsApiModel>
    {
        public int Id { get; set; }
        public class GetBubbleMeetByIdQueriesHandler : IRequestHandler<GetBubbleMeetByIdQuery, BubbleMeetDetailsApiModel>
        {
            private readonly IApplicationDbContext _context;
            public GetBubbleMeetByIdQueriesHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<BubbleMeetDetailsApiModel> Handle(GetBubbleMeetByIdQuery query, CancellationToken cancellationToken)
            {
                BubbleMeetDetailsApiModel meetDetail = new BubbleMeetDetailsApiModel();
                meetDetail = _context.bubbleMeetDetails.Where(y => y.Id == query.Id).Select(x => new BubbleMeetDetailsApiModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    MeetDescription = x.MeetDescription,
                    MeetPlace = x.MeetPlace,
                    MeetTiming = x.MeetTiming,
                    MeetDate = x.MeetDate,
                    County = x.County,
                    CountyName = _context.counties.Where(y => y.Fips == x.County).Select(x => x.CountyName).FirstOrDefault(),
                    CreatedBy = x.CreatedBy,
                    CreatedOn = x.CreatedOn,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedOn = x.UpdatedOn,
                    IsChatAllowed=x.IsChatAllowed,
                    lstUsers = _context.bubbleMeetMembers
                                .Join(_context.userDetails, bm => bm.UserId, ud => ud.Id, (bm, ud) => new { bm, ud })
                                .Where(y => y.bm.BubbleMeetId == query.Id && y.ud.IsActive == true)
                                .Select(xu => new UserApiModels
                                {
                                    Id = xu.ud.Id,
                                    Username = xu.ud.Username,
                                    County = xu.ud.County,
                                    CountyName = _context.counties.Where(y => y.Fips == xu.ud.County).Select(x => x.CountyName).FirstOrDefault(),
                                    PhoneNo = xu.ud.PhoneNo,
                                    ProfilePicUrl=xu.ud.ProfilePicUrl,
                                    IsAdmin= (_context.bubbleMeetMemberPermissions
                                       .Where(p => p.UserId == xu.ud.Id && p.BubbleMeetId == xu.bm.BubbleMeetId
                                       && p.UserPermissionTypeId == 0)
                                       .Select(x => x.UserPermissionStatus)
                                       .FirstOrDefault()),
                                    UpdatedBy=xu.ud.UpdatedBy,
                                    CreatedBy = xu.ud.CreatedBy,
                                    CreatedOn = xu.ud.CreatedOn,
                                    UpdatedOn = xu.ud.UpdatedOn,
                                    countyDetails = _context.counties.Where(y => y.Fips == xu.ud.County).Select(x => new CountiesApiModel
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
                                }).ToList(),
                    
                    countyDetails = _context.counties.Where(y => y.Fips == x.County).Select(x => new CountiesApiModel
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

                return meetDetail;
            }
        }
    }
}
