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

namespace Application.Features.Queries.BubbleMeetQueries
{
    public class GetBubbleMeetWithFiltersQuery : IRequest<List<BubbleMeetDetailsApiModel>>
    {
        public string BubbleMeetName { get; set; }
        public int BubbleMeetMemberId { get; set; }
        public class GetBubbleMeetWithFiltersHandler : IRequestHandler<GetBubbleMeetWithFiltersQuery, List<BubbleMeetDetailsApiModel>>
        {
            private readonly IApplicationDbContext _context;
            public GetBubbleMeetWithFiltersHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<List<BubbleMeetDetailsApiModel>> Handle(GetBubbleMeetWithFiltersQuery query, CancellationToken cancellationToken)
            {
                try
                {


                    List<BubbleMeetDetailsApiModel> lstbubbleMeetDetailsApiModel = new List<BubbleMeetDetailsApiModel>();
                    lstbubbleMeetDetailsApiModel = await _context.bubbleMeetDetails.Join(_context.bubbleMeetMembers, bmd => bmd.Id, bmm => bmm.BubbleMeetId, (bmd, bmm) => new { bmd, bmm })
                                                   .Where(y => (!string.IsNullOrEmpty(query.BubbleMeetName)) ? y.bmd.Title == query.BubbleMeetName : y.bmd.Title == y.bmd.Title
                                                   && (!string.IsNullOrEmpty(Convert.ToString(query.BubbleMeetMemberId)) ? y.bmm.UserId == query.BubbleMeetMemberId : y.bmm.UserId > 0)
                                                )
                                                .Select(x => new BubbleMeetDetailsApiModel()
                                                {
                                                    Id = x.bmd.Id,
                                                    IsChatAllowed = x.bmd.IsChatAllowed,
                                                    MeetDescription = x.bmd.MeetDescription,
                                                    Title = x.bmd.Title,
                                                    MeetPlace = x.bmd.MeetPlace,
                                                    MeetTiming = x.bmd.MeetTiming,
                                                    CreatedBy = x.bmd.CreatedBy,
                                                    IsAdmin = (_context.bubbleMeetMemberPermissions
                                       .Where(p => p.UserId == query.BubbleMeetMemberId && p.BubbleMeetId == x.bmd.Id
                                       && p.UserPermissionTypeId == 0 && p.MeetTypeId == MeetType.BubbleMeet)
                                       .Select(x => x.UserPermissionStatus)
                                       .FirstOrDefault())

                                                }).Distinct().ToListAsync();


                    if (lstbubbleMeetDetailsApiModel == null)
                    {
                        return null;
                    }
                    return lstbubbleMeetDetailsApiModel;
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
    }
}
