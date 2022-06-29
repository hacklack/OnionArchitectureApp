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
namespace Application.Features.Queries.PODMeetQueries
{
    public class GetAllUsersForPODMeetQuery : IRequest<List<PODMeetDetailsApiModel>>
    {
        public int UserId { get; set; }
        //public int BubblType { get; set; }
        public class GetAllUsersForBubbleMeetHandler : IRequestHandler<GetAllUsersForPODMeetQuery, List<PODMeetDetailsApiModel>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllUsersForBubbleMeetHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<List<PODMeetDetailsApiModel>>Handle(GetAllUsersForPODMeetQuery query, CancellationToken cancellationToken)
            {
                var lstMeets = _context.podMeetMembers.Where(x => x.UserId == query.UserId).GroupBy(g=>g.PODMeetId).Select(y => y.Key).ToList();
                List<PODMeetDetailsApiModel> lstMeetDetail = new List<PODMeetDetailsApiModel>();
                foreach (var item in lstMeets)
                {
                    PODMeetDetailsApiModel meet = new PODMeetDetailsApiModel();
                    meet = _context.podMeetDetails
                        .Where(y => y.Id == item)
                        .Select(x => new PODMeetDetailsApiModel {
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
                            IsAdmin= (_context.bubbleMeetMemberPermissions
                                       .Where(p => p.UserId == query.UserId && p.BubbleMeetId == item
                                       && p.UserPermissionTypeId == 0 && p.MeetTypeId == MeetType.PODMeet)
                                       .Select(x => x.UserPermissionStatus)
                                       .FirstOrDefault()),
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
