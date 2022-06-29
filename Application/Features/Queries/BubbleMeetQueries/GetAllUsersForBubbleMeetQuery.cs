using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.ApiModels;
using System.Linq;
using static Domain.CommonCodes.CommonEnums;
namespace Application.Features.Queries.BubbleMeetQueries
{
    public class GetAllUsersForBubbleMeetQuery : IRequest<List<BubbleMeetDetailsApiModel>>
    {
        public int UserId { get; set; }
        //public int BubblType { get; set; }
        public class GetAllUsersForBubbleMeetHandler : IRequestHandler<GetAllUsersForBubbleMeetQuery, List<BubbleMeetDetailsApiModel>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllUsersForBubbleMeetHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<List<BubbleMeetDetailsApiModel>> Handle(GetAllUsersForBubbleMeetQuery query, CancellationToken cancellationToken)
            {
                var lstMeets = _context.bubbleMeetMembers.Where(x => x.UserId == query.UserId).Select(y => y.BubbleMeetId).ToList();
                List<BubbleMeetDetailsApiModel> lstMeetDetail = new List<BubbleMeetDetailsApiModel>();
                foreach (var item in lstMeets)
                {
                    BubbleMeetDetailsApiModel meet = new BubbleMeetDetailsApiModel();
                    meet = _context.bubbleMeetMembers
                        .Join(_context.bubbleMeetDetails,bmm=>bmm.BubbleMeetId,bm=>bm.Id,(bmm,bm)=>new { bmm,bm})
                        .Where(y => y.bm.Id == item && y.bmm.UserId == query.UserId)
                        .Select(x => new BubbleMeetDetailsApiModel {
                            Id = x.bm.Id,
                            Title = x.bm.Title,
                            MeetDescription = x.bm.MeetDescription,
                            MeetPlace = x.bm.MeetPlace,
                            MeetTiming = x.bm.MeetTiming,
                            MeetDate = x.bm.MeetDate,
                            County = x.bm.County,
                            CountyName = _context.counties.Where(y => y.Fips == x.bm.County).Select(x => x.CountyName).FirstOrDefault(),
                            CreatedBy = x.bm.CreatedBy,
                            CreatedOn = x.bm.CreatedOn,
                            UpdatedBy = x.bm.UpdatedBy,
                            UpdatedOn = x.bm.UpdatedOn,
                            IsChatAllowed=x.bm.IsChatAllowed,
                            IsAdmin= (_context.bubbleMeetMemberPermissions
                                       .Where(p => p.UserId == x.bmm.UserId && p.BubbleMeetId == item
                                       && p.UserPermissionTypeId == 0 && p.MeetTypeId == MeetType.BubbleMeet)
                                       .Select(x => x.UserPermissionStatus)
                                       .FirstOrDefault()),
                            countyDetails = _context.counties.Where(y => y.Fips == x.bm.County).Select(x => new CountiesApiModel
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
                    lstMeetDetail.Add(meet);    
                }


                if (lstMeetDetail == null)
                {
                    return null;
                }
                return lstMeetDetail;
            }
        }
    }
}
