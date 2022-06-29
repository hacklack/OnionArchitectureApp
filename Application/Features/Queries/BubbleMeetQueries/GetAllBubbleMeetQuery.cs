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
using static Domain.CommonCodes.CommonEnums;

namespace Application.Features.Queries.BubbleMeetQueries
{
    public class GetAllBubbleMeetQuery : IRequest<List<BubbleMeetDetailsApiModel>>
    {
        public class GetAllBubbleMeetQueriesHandler : IRequestHandler<GetAllBubbleMeetQuery, List<BubbleMeetDetailsApiModel>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllBubbleMeetQueriesHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<List<BubbleMeetDetailsApiModel>> Handle(GetAllBubbleMeetQuery query, CancellationToken cancellationToken)
            {
                BubbleMeetDetails dbModel = new BubbleMeetDetails();
                List<BubbleMeetDetailsApiModel> lstBubbleMeetDetails = new List<BubbleMeetDetailsApiModel>();
                List<int> lstMeetId = _context.bubbleMeetDetails.Select(x => x.Id).ToList();
                foreach (var item in lstMeetId)
                {
                    BubbleMeetDetailsApiModel BubbleMeetDetail = new BubbleMeetDetailsApiModel();

                    BubbleMeetDetail = _context.bubbleMeetDetails.Select(x => new BubbleMeetDetailsApiModel
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
                        IsChatAllowed = x.IsChatAllowed,
                        lstUsers = _context.bubbleMeetMembers
                            .Join(_context.userDetails, bm => bm.UserId, ud => ud.Id, (bm, ud) => new { bm, ud })
                            .Where(y => y.bm.BubbleMeetId == item && y.ud.IsActive == true)
                            .Select(xu => new UserApiModels
                            {
                                Id = xu.ud.Id,
                                Username = xu.ud.Username,
                                County = xu.ud.County,
                                CountyName = _context.counties.Where(y => y.Fips == xu.ud.County).Select(x => x.CountyName).FirstOrDefault(),
                                PhoneNo = xu.ud.PhoneNo,
                                IsAdmin = (_context.bubbleMeetMemberPermissions
                                       .Where(p => p.UserId == xu.ud.Id && p.BubbleMeetId == item
                                       && p.UserPermissionTypeId == 0 && p.MeetTypeId == MeetType.BubbleMeet)
                                       .Select(x => x.UserPermissionStatus)
                                       .FirstOrDefault()),
                                countyDetails=_context.counties.Where(y=>y.Fips==xu.ud.County).Select(x=>new CountiesApiModel {
                                Id=x.Id,
                                Fips= x.Fips,
                                Country=x.Country,
                                CountyName=x.CountyName,
                                State=x.State,
                                CreatedBy=x.CreatedBy,
                                UpdatedBy=x.UpdatedBy,
                                CreatedOn=x.CreatedOn,
                                UpdatedOn=x.UpdatedOn
                                
                                }).FirstOrDefault()
                            }).ToList()
                    }).FirstOrDefault();
                    lstBubbleMeetDetails.Add(BubbleMeetDetail);
                }
                return lstBubbleMeetDetails;
            }
        }
    }
}
