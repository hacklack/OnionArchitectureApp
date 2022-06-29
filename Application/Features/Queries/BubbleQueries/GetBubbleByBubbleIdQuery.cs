using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Application.ApiModels;
using static Domain.CommonCodes.CommonEnums;

namespace Application.Features.Commands.BubbleCommands
{
    public class GetBubbleByBubbleIdQuery : IRequest<BubbleApiModel>
    {
        public int Id { get; set; }
        public class GetBubbleByBubbleIdQueryHandler : IRequestHandler<GetBubbleByBubbleIdQuery, BubbleApiModel>
        {
            private readonly IApplicationDbContext _context;
            public GetBubbleByBubbleIdQueryHandler(IApplicationDbContext context) => _context = context;

            public async Task<BubbleApiModel> Handle(GetBubbleByBubbleIdQuery query, CancellationToken cancellationToken)
            {
                BubbleApiModel bubbleApiModel = new BubbleApiModel();
                bubbleApiModel = _context.bubbleDetails
                    .Join(_context.bubbleMembers, bd => bd.Id, bm => bm.BubbleId, (bd, bm) => new { bd, bm })
                    .Join(_context.userDetails, bma => bma.bm.UserId, ud => ud.Id, (bma, ud) => new { bma, ud })
                    .Where(bda => bda.bma.bm.BubbleId == query.Id && bda.bma.bm.IsBubbleAdmin == true && bda.bma.bm.UserId == bda.ud.Id)
                    .Select(x => new BubbleApiModel
                    {
                        Id = x.bma.bd.Id,
                        BubbleName = x.bma.bd.BubbleName,
                        BubbleDescription = x.bma.bd.BubbleDescription,
                        BubbleSize = x.bma.bd.BubbleSize,
                        BubbleType = x.bma.bd.BubbleType,
                        BubbleValidity = x.bma.bd.BubbleValidity,
                        BubbleZipCode = x.bma.bd.BubbleZipCode,
                        CreatedBy = x.bma.bd.CreatedBy,
                        CreatedOn = x.bma.bd.CreatedOn,
                        IsOtherCountyMemberAllowed = x.bma.bd.IsOtherCountyMemberAllowed,
                        BubbleCounty = x.ud.County,
                        BubbleCountyName = _context.counties.Where(y => y.Fips == x.ud.County).Select(x => x.CountyName).FirstOrDefault(),
                        bubbleSafetyDetails =_context.bubbleSafetyDetails
                        .Join(_context.bubbleDetails,bsd=>bsd.BubblePODId,bd=>bd.Id,(bsd,bd)=>new { bsd,bd})
                        .Where(y=>y.bsd.BubbleSaftyTypeId==BubbleSaftyType.BubbleSaftyLevel && y.bsd.BubblePODId==x.bma.bd.Id)
                        .Select(xbsd=>new BubbleSafetyDetailsApiModel {
                        BubbleSaftyValue=xbsd.bsd.BubbleSaftyValue
                        }).FirstOrDefault()

                    }).FirstOrDefault();
                bubbleApiModel.lstPodUser = _context.bubbleMembers
                                        .Join(_context.userDetails, bm => bm.UserId, ud => ud.Id, (bm, ud) => new { bm, ud })
                                        .Where(bmw => bmw.bm.BubbleId == query.Id && bmw.bm.UserId == bmw.ud.Id && bmw.ud.IsActive == true)
                                        .Select(x => new UserApiModels
                                        {
                                            Id = x.ud.Id,
                                            Username = x.ud.Username,
                                            County = x.ud.County,
                                            CountyName = _context.counties.Where(y => y.Fips == x.ud.County).Select(x => x.CountyName).FirstOrDefault(),
                                            PhoneNo = x.ud.ProfilePicUrl,
                                            IsAdmin = x.bm.IsBubbleAdmin,
                                            lstUserPermission = (_context.bubbleMeetMemberPermissions
                                           .Where(p => p.UserId == x.ud.Id && p.BubbleMeetId == x.bm.BubbleId
                                           && p.MeetTypeId == MeetType.Bubble)
                                           .Select(x => new BubbleMeetPermissionsApiModel
                                           {
                                               Id = x.Id,
                                               PermissionParenttId = x.BubbleMeetId,
                                               MeetTypeId = x.MeetTypeId,
                                               UserPermissionTypeId = x.UserPermissionTypeId,
                                               UserPermissionStatus = x.UserPermissionStatus,
                                               UserId = x.UserId
                                           })
                                           .ToList()),
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
                                        }).ToList();
                if (bubbleApiModel == null)
                {
                    return null;
                }
                return bubbleApiModel;
            }
        }
    }
}