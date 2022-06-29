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
    public class GetAllPODMeetQuery : IRequest<List<PODMeetDetailsApiModel>>
    {
        public class GetAllPODMeetHandler : IRequestHandler<GetAllPODMeetQuery, List<PODMeetDetailsApiModel>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllPODMeetHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<List<PODMeetDetailsApiModel>> Handle(GetAllPODMeetQuery query, CancellationToken cancellationToken)
            {
                try
                {

                    List<PodDetailsApiModel> lstPodsTM = new List<PodDetailsApiModel>();
                    PODMeetDetails dbModel = new PODMeetDetails();
                    List<PODMeetDetailsApiModel> lstPODMeetDetails = new List<PODMeetDetailsApiModel>();
                    List<int> lstMeetId = _context.podMeetDetails.Select(x => x.Id).ToList();
                    foreach (var item in lstMeetId)
                    {
                        var lstPodIds = _context.podMeetMembers.Where(y => y.PODMeetId == item).GroupBy(g => g.PODId).Select(x => x.Key).ToList();
                        foreach (var podId in lstPodIds)
                        {
                            PodDetailsApiModel podDet = new PodDetailsApiModel();
                            podDet = _context.podDetails
                                   .Where(y => y.Id == podId)
                                   .Select(xu => new PodDetailsApiModel
                                   {
                                       Id = xu.Id,
                                       PODBubbleType = xu.PODBubbleType,
                                       PODDescription = xu.PODDescription,
                                       PODName = xu.PODName,
                                       PODSize = xu.PODSize,
                                       CreatedBy = xu.CreatedBy,
                                       UpdatedBy = xu.UpdatedBy,
                                       CreatedOn = xu.CreatedOn,
                                       UpdatedOn = xu.UpdatedOn
                                   }).FirstOrDefault();
                            lstPodsTM.Add(podDet);
                        }
                        PODMeetDetailsApiModel podMeetDetail = new PODMeetDetailsApiModel();

                        podMeetDetail = _context.podMeetDetails.Where(y => y.Id == item).Select(x => new PODMeetDetailsApiModel
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
                            lstPods = lstPodsTM,
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
                            //IsAdmin= (_context.bubbleMeetMemberPermissions
                            //               .Where(p => p.UserId == x.CreatedBy && p.BubbleMeetId == x.Id
                            //               && p.UserPermissionTypeId == 0 && p.MeetTypeId == MeetType.PODMeet)
                            //               .Select(x => x.UserPermissionStatus)
                            //.FirstOrDefault()),


                        }).FirstOrDefault();
                        lstPODMeetDetails.Add(podMeetDetail);
                    }
                    return lstPODMeetDetails;
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
    }
}
