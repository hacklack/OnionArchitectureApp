using Application.ApiModels;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Domain.CommonCodes.CommonEnums;

namespace Application.Features.Queries.BubbleQueries
{
    public class GetBubbleWithFiltersQuery : IRequest<List<BubbleApiModel>>
    {
        public int bubbleMemberId { get; set; }
        public int BubbleType { get; set; }
        public string createdDate { get; set; }
        public string FromSize { get; set; }
        public string ToSize { get; set; }

        public class GetBubbleWithFiltersHandler : IRequestHandler<GetBubbleWithFiltersQuery, List<BubbleApiModel>>
        {
            private readonly IApplicationDbContext _context;
            public GetBubbleWithFiltersHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<List<BubbleApiModel>> Handle(GetBubbleWithFiltersQuery query, CancellationToken cancellationToken)
            {
                List<BubbleApiModel> bubbleList = new List<BubbleApiModel>();
                if (query.BubbleType != 0)
                {
                    bubbleList = await _context.bubbleDetails
                                        .Join(_context.bubbleMembers, a => a.Id, bm => bm.BubbleId, (a, bm) => new { a, bm })
                                        .Where(abm => (int)abm.a.BubbleType == query.BubbleType
                                        && abm.bm.UserId == query.bubbleMemberId
                                        && ((!string.IsNullOrEmpty(query.FromSize) && !string.IsNullOrEmpty(query.ToSize))
                                        ? Convert.ToInt32(abm.a.BubbleSize) >= Convert.ToInt32(query.FromSize) && Convert.ToInt32(abm.a.BubbleSize) <= Convert.ToInt32(query.ToSize)
                                        : Convert.ToInt32(abm.a.BubbleSize) > 0)
                                        && (query.createdDate == null ? abm.a.CreatedOn <= DateTime.Today : abm.a.CreatedOn.Date == Convert.ToDateTime(query.createdDate).Date)
                                         ).Select(
                                                 x => new BubbleApiModel()
                                                 {
                                                     Id = x.a.Id,
                                                     BubbleDescription = x.a.BubbleDescription,
                                                     BubbleName = x.a.BubbleName,
                                                     BubbleSize = x.a.BubbleSize,
                                                     BubbleType = x.a.BubbleType,
                                                     BubbleValidity = x.a.BubbleValidity,
                                                     BubbleZipCode = x.a.BubbleZipCode,
                                                     IsOtherCountyMemberAllowed = x.a.IsOtherCountyMemberAllowed,
                                                     CreatedBy = x.a.CreatedBy,
                                                     CreatedOn = x.a.CreatedOn,
                                                     BubbleCountyName =(x.a.IsOtherCountyMemberAllowed)?string.Empty:_context.userDetails.Join(_context.counties,udc=>udc.County,cc=>cc.Fips,(udc,cc)=>new { udc,cc}).Where(cx=>cx.udc.Id==x.bm.UserId).Select(ccx=>ccx.cc.CountyName).FirstOrDefault(),
                                                     IsAdmin = _context.bubbleMeetMemberPermissions
                                                                .Where(p => p.UserId == query.bubbleMemberId && p.BubbleMeetId == x.a.Id
                                                                && p.UserPermissionTypeId == 0 && p.MeetTypeId == MeetType.Bubble)
                                                                .Select(x => x.UserPermissionStatus)
                                                                .FirstOrDefault(),
                                                     bubbleSafetyDetails = _context.bubbleSafetyDetails
                                                     .Join(_context.bubbleDetails, bsd => bsd.BubblePODId, bd => bd.Id, (bsd, bd) => new { bsd, bd })
                                                     .Where(y => y.bsd.BubbleSaftyTypeId == BubbleSaftyType.BubbleSaftyLevel && y.bsd.BubblePODId == x.a.Id)
                                                     .Select(xbsd => new BubbleSafetyDetailsApiModel
                                                     {
                                                         BubbleSaftyValue = xbsd.bsd.BubbleSaftyValue
                                                     }).FirstOrDefault(),
                                                     lstPodUser = _context.bubbleMembers
                                                         .Join(_context.userDetails, bm => bm.UserId, ud => ud.Id, (bm, ud) => new { bm, ud })
                                                         .Where(bmw => bmw.bm.BubbleId == x.a.Id && bmw.bm.UserId == bmw.ud.Id && bmw.ud.IsActive == true)
                                                         .Select(x => new UserApiModels
                                                         {
                                                             Id = x.ud.Id,
                                                             Username = x.ud.Username,
                                                             County = x.ud.County,
                                                             CountyName = _context.counties.Where(y => y.Fips == x.ud.County).Select(x => x.CountyName).FirstOrDefault(),
                                                             PhoneNo = x.ud.ProfilePicUrl,
                                                             IsAdmin = x.bm.IsBubbleAdmin,
                                                             countyDetails = _context.counties.Where(y => y.Fips == x.ud.County).Select(x => new CountiesApiModel
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
                                                         }).ToList()
                                                 }
                                            ).ToListAsync();

                    return bubbleList;
                }
                else
                {
                    bubbleList = await _context.bubbleDetails
                                        .Join(_context.bubbleMembers, a => a.Id, bm => bm.BubbleId, (a, bm) => new
                                        {
                                            a,
                                            bm
                                        })
                                        .Where(abm => abm.bm.UserId == query.bubbleMemberId
                                        && ((!string.IsNullOrEmpty(query.FromSize) && !string.IsNullOrEmpty(query.ToSize))
                                        ? Convert.ToInt32(abm.a.BubbleSize) >= Convert.ToInt32(query.FromSize) && Convert.ToInt32(abm.a.BubbleSize) <= Convert.ToInt32(query.ToSize)
                                        : Convert.ToInt32(abm.a.BubbleSize) > 0)
                                        && (query.createdDate == null ? abm.a.CreatedOn == abm.a.CreatedOn : abm.a.CreatedOn.Date == Convert.ToDateTime(query.createdDate))
                                         ).Select(
                                                 x => new BubbleApiModel()
                                                 {
                                                     Id = x.a.Id,
                                                     BubbleDescription = x.a.BubbleDescription,
                                                     BubbleName = x.a.BubbleName,
                                                     BubbleSize = x.a.BubbleSize,
                                                     BubbleType = x.a.BubbleType,
                                                     BubbleValidity = x.a.BubbleValidity,
                                                     BubbleZipCode = x.a.BubbleZipCode,
                                                     IsOtherCountyMemberAllowed = x.a.IsOtherCountyMemberAllowed,
                                                     CreatedBy = x.a.CreatedBy,
                                                     CreatedOn = x.a.CreatedOn,
                                                     IsAdmin = x.bm.IsBubbleAdmin,
                                                     BubbleCountyName = (x.a.IsOtherCountyMemberAllowed) ? string.Empty : _context.userDetails.Join(_context.counties, udc => udc.County, cc => cc.Fips, (udc, cc) => new { udc, cc }).Where(cx => cx.udc.Id == x.bm.UserId).Select(ccx => ccx.cc.CountyName).FirstOrDefault(),
                                                     bubbleSafetyDetails = _context.bubbleSafetyDetails
                                                    .Join(_context.bubbleDetails, bsd => bsd.BubblePODId, bd => bd.Id, (bsd, bd) => new { bsd, bd })
                                                    .Where(y => y.bsd.BubbleSaftyTypeId == BubbleSaftyType.BubbleSaftyLevel && y.bsd.BubblePODId == x.a.Id)
                                                    .Select(xbsd => new BubbleSafetyDetailsApiModel
                                                    {
                                                        BubbleSaftyValue = xbsd.bsd.BubbleSaftyValue
                                                    }).FirstOrDefault(),
                                                     lstPodUser = _context.bubbleMembers
                                                         .Join(_context.userDetails, bm => bm.UserId, ud => ud.Id, (bm, ud) => new { bm, ud })
                                                         .Where(bmw => bmw.bm.BubbleId == x.a.Id && bmw.bm.UserId == bmw.ud.Id && bmw.ud.IsActive == true)
                                                         .Select(x => new UserApiModels
                                                         {
                                                             Id = x.ud.Id,
                                                             Username = x.ud.Username,
                                                             County = x.ud.County,
                                                             CountyName = _context.counties.Where(y => y.Fips == x.ud.County).Select(x => x.CountyName).FirstOrDefault(),
                                                             ProfilePicUrl = x.ud.ProfilePicUrl,
                                                             PhoneNo = x.ud.PhoneNo,
                                                             IsAdmin = x.bm.IsBubbleAdmin,
                                                             countyDetails = _context.counties.Where(y => y.Fips == x.ud.County).Select(x => new CountiesApiModel
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
                                                         }).ToList()
                                                 }
                                            ).ToListAsync();
                    return bubbleList;
                }

            }
        }
    }
}