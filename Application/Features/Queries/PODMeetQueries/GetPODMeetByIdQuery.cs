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

namespace Application.Features.Queries.PODMeetQueries
{
    public class GetPODMeetByIdQuery : IRequest<PODMeetDetailsApiModel>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public class GetPODMeetByIdHandler : IRequestHandler<GetPODMeetByIdQuery, PODMeetDetailsApiModel>
        {
            private readonly IApplicationDbContext _context;
            public GetPODMeetByIdHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<PODMeetDetailsApiModel> Handle(GetPODMeetByIdQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    PODMeetDetailsApiModel apiModel = new PODMeetDetailsApiModel();
                    var podMeetDetails = _context.podMeetDetails.Where(y => y.Id == query.Id).FirstOrDefault();
                    apiModel.Id = podMeetDetails.Id;
                    apiModel.Title = podMeetDetails.Title;
                    apiModel.MeetDescription = podMeetDetails.MeetDescription;
                    apiModel.MeetPlace = podMeetDetails.MeetPlace;
                    apiModel.MeetTiming = podMeetDetails.MeetTiming;
                    apiModel.MeetDate = podMeetDetails.MeetDate;
                    apiModel.County = podMeetDetails.County;
                    apiModel.CountyName = _context.counties.Where(y => y.Fips == podMeetDetails.County).Select(x => x.CountyName).FirstOrDefault();
                    apiModel.IsChatAllowed = podMeetDetails.IsChatAllowed;
                    apiModel.CreatedBy = podMeetDetails.CreatedBy;
                    apiModel.CreatedOn = podMeetDetails.CreatedOn;
                    apiModel.UpdatedBy = podMeetDetails.UpdatedBy;
                    apiModel.UpdatedOn = podMeetDetails.UpdatedOn;
                    apiModel.IsAdmin = (_context.bubbleMeetMemberPermissions
                                                             .Where(p => p.BubbleMeetId == query.Id && p.UserId == query.UserId && p.UserPermissionTypeId == 0 && p.MeetTypeId == MeetType.PODMeet && p.UserPermissionStatus == true)
                                                             .Select(x => x.UserPermissionStatus)
                                                             .FirstOrDefault());
                    apiModel.countyDetails = _context.counties.Where(y => y.Fips == podMeetDetails.County).Select(x => new CountiesApiModel
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

                    }).FirstOrDefault();

                    var lstbubbleIds = _context.podMeetMembers.Where(y => y.PODMeetId == query.Id).GroupBy(g => g.BubbleId).Select(x => x.Key).ToList();
                    foreach (var bubbleId in lstbubbleIds)
                    {
                        int pmAdmin = _context.bubbleMeetMemberPermissions.Where(yy => yy.BubbleMeetId == bubbleId && yy.UserPermissionTypeId == 0 && yy.MeetTypeId == MeetType.Bubble && yy.UserPermissionStatus == true)
                                                                       .Select(xx => xx.UserId)
                                                                       .FirstOrDefault();
                        bool isAdmin = (_context.bubbleMeetMemberPermissions.Where(yp => yp.BubbleMeetId == query.Id && yp.UserPermissionTypeId == 0 && yp.MeetTypeId == MeetType.PODMeet && yp.UserPermissionStatus == true && yp.UserId == pmAdmin).Count() > 0) ? true : false;
                        BubbleApiModel bubbleApiModel = new BubbleApiModel();
                        bubbleApiModel = _context.bubbleDetails
                                    .Join(_context.bubbleMembers, bd => bd.Id, pmd => pmd.BubbleId, (bd, pmd) => new { bd, pmd })
                                    .Where(qy => qy.pmd.BubbleId == bubbleId)
                                    .Select(xpd => new BubbleApiModel
                                    {
                                        Id = xpd.bd.Id,
                                        BubbleName = xpd.bd.BubbleName,
                                        BubbleDescription = xpd.bd.BubbleDescription,
                                        BubbleSize = xpd.bd.BubbleSize,
                                        BubbleType = xpd.bd.BubbleType,
                                        BubbleValidity = xpd.bd.BubbleValidity,
                                        BubbleZipCode = xpd.bd.BubbleZipCode,
                                        IsOtherCountyMemberAllowed = xpd.bd.IsOtherCountyMemberAllowed,
                                        CreatedBy = xpd.bd.CreatedBy,
                                        UpdatedBy = xpd.bd.UpdatedBy,
                                        UpdatedOn = xpd.bd.UpdatedOn,
                                        CreatedOn = xpd.bd.CreatedOn,
                                        IsAdmin = isAdmin,
                                        lstPodUser = _context.userDetails
                                        .Join(_context.bubbleMembers, udp => udp.Id, bmp => bmp.UserId, (udp, bmp) => new { udp, bmp })
                                        .Where(w => w.bmp.BubbleId == xpd.bd.Id && w.udp.IsActive == true)
                                                     .Select(uda => new UserApiModels
                                                     {
                                                         Id = uda.udp.Id,
                                                         Username = uda.udp.Username,
                                                         County = uda.udp.County,
                                                         CountyName = _context.counties.Where(y => y.Fips == uda.udp.County).Select(x => x.CountyName).FirstOrDefault(),
                                                         ProfilePicUrl = uda.udp.ProfilePicUrl,
                                                         PhoneNo = uda.udp.PhoneNo,
                                                         IsAdmin = (_context.bubbleMeetMemberPermissions
                                                                 .Where(p => p.BubbleMeetId == query.Id && p.UserId == uda.udp.Id && p.UserPermissionTypeId == 0 && p.MeetTypeId == MeetType.PODMeet && p.UserPermissionStatus == true)
                                                                     .Select(x => x.UserPermissionStatus)
                                                                     .FirstOrDefault()),
                                                         countyDetails = _context.counties.Where(y => y.Fips == uda.udp.County).Select(x => new CountiesApiModel
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
                                    }).FirstOrDefault();
                        apiModel.lstbubbles.Add(bubbleApiModel);
                    }

                    return apiModel;
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
    }
}
