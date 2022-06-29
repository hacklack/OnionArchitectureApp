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
using static Domain.CommonCodes.CommonEnums;

namespace Application.Features.Queries.PODMeetQueries
{
    public class GetPODMeetWithFiltersQuery : IRequest<List<PODMeetDetailsApiModel>>
    {
        public string PODMeetName { get; set; }
        public int PODMeetMemberId { get; set; }
        public class GetPODMeetWithFiltersHandler : IRequestHandler<GetPODMeetWithFiltersQuery, List<PODMeetDetailsApiModel>>
        {
            private readonly IApplicationDbContext _context;
            public GetPODMeetWithFiltersHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<List<PODMeetDetailsApiModel>> Handle(GetPODMeetWithFiltersQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    List<PODMeetDetailsApiModel> lstPODMeetDetailsApiModel = new List<PODMeetDetailsApiModel>();
                    lstPODMeetDetailsApiModel = await _context.podMeetDetails.Join(_context.podMeetMembers, bmd => bmd.Id, bmm => bmm.PODMeetId, (bmd, bmm) => new { bmd, bmm })
                                                   .Where(y => (!string.IsNullOrEmpty(query.PODMeetName)) ? y.bmd.Title == query.PODMeetName : y.bmd.Title == y.bmd.Title
                                                   && (!string.IsNullOrEmpty(Convert.ToString(query.PODMeetMemberId)) ? y.bmm.UserId == query.PODMeetMemberId : y.bmm.UserId > 0)
                                                )
                                                .Select(x => new PODMeetDetailsApiModel()
                                                {
                                                    Id = x.bmd.Id,
                                                    IsChatAllowed = x.bmd.IsChatAllowed,
                                                    MeetDescription = x.bmd.MeetDescription,
                                                    Title = x.bmd.Title,
                                                    MeetPlace = x.bmd.MeetPlace,
                                                    MeetTiming = x.bmd.MeetTiming,
                                                    CreatedBy = x.bmd.CreatedBy,
                                                    IsAdmin = (_context.bubbleMeetMemberPermissions
                                       .Where(p => p.UserId == query.PODMeetMemberId 
                                                && p.BubbleMeetId == x.bmd.Id
                                                && p.UserPermissionTypeId == 0 
                                                && p.MeetTypeId == MeetType.PODMeet)
                                       .Select(x => x.UserPermissionStatus )
                                       .FirstOrDefault())
                                                }).Distinct().ToListAsync();


                    if (lstPODMeetDetailsApiModel == null)
                    {
                        return null;
                    }
                    return lstPODMeetDetailsApiModel;
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
    }
}
