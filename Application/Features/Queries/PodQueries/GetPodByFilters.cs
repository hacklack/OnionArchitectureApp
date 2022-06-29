using Application.ApiModels;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Domain.CommonCodes.CommonEnums;

namespace Application.Features.Queries.PodQueries
{
    public class GetPodByFiltersQuery : IRequest<List<PodDetailsApiModel>>
    {
        public int UserId { get; set; }
        public string FromSize { get; set; }
        public string ToSize { get; set; }
        public string PodName { get; set; }
        public int BubbleType { get; set; }
        public string PodDate { get; set; }

        public class GetPodByFiltersHandler : IRequestHandler<GetPodByFiltersQuery, List<PodDetailsApiModel>>
        {
            private readonly IApplicationDbContext _context;

            public GetPodByFiltersHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<PodDetailsApiModel>> Handle(GetPodByFiltersQuery query, CancellationToken cancellationToken)
            {

                List<PodDetailsApiModel> lstPodDetailsApiModel = new List<PodDetailsApiModel>();
                lstPodDetailsApiModel = await _context.podDetails.Join(_context.podBubbleMembers, pd => pd.Id, oPbm => oPbm.PODId, (pd, oPbm) => new { pd, oPbm })
                           .Where(pod => (string.IsNullOrEmpty(query.PodName) ? pod.pd.PODName != null : pod.pd.PODName == query.PodName)
                                   && ((query.BubbleType == 0) ? pod.pd.PODBubbleType > 0 : pod.pd.PODBubbleType == query.BubbleType)
                                   && ((query.PodDate == null) ? pod.pd.CreatedOn <= DateTime.UtcNow : pod.pd.CreatedOn.Date == Convert.ToDateTime(query.PodDate).Date)
                                   && ((!string.IsNullOrEmpty(query.FromSize) && !string.IsNullOrEmpty(query.ToSize))
                                   ? pod.pd.PODSize >= Convert.ToInt32(query.FromSize) && pod.pd.PODSize <= Convert.ToInt32(query.ToSize)
                                   : pod.pd.PODSize > 0)
                                   && ((query.UserId == 0) ? pod.oPbm.BubbleMemberId > 0 : pod.oPbm.BubbleMemberId == query.UserId)
                                   )
                           .Select(x => new PodDetailsApiModel
                           {
                               Id = x.pd.Id,
                               PODName = x.pd.PODName,
                               PODBubbleType = x.pd.PODBubbleType,
                               PODDescription = x.pd.PODDescription,
                               PODSize = x.pd.PODSize,
                               IsAdmin = _context.bubbleMeetMemberPermissions
                                                                .Where(p => p.UserId == query.UserId && p.BubbleMeetId == x.pd.Id
                                                                && p.UserPermissionTypeId == 0 && p.MeetTypeId == MeetType.POD)
                                                                .Select(x => x.UserPermissionStatus)
                                                                .FirstOrDefault(),
                                podSafetyDetails = _context.bubbleSafetyDetails
                               .Join(_context.podDetails, bsd => bsd.BubblePODId, bd => bd.Id, (bsd, bd) => new { bsd, bd })
                               .Where(y => y.bsd.BubbleSaftyTypeId == BubbleSaftyType.PODSaftyLevel && y.bsd.BubblePODId == x.pd.Id)
                               .Select(xbsd => new BubbleSafetyDetailsApiModel
                               {
                                   BubbleSaftyValue = xbsd.bsd.BubbleSaftyValue
                               }).FirstOrDefault(),
                               lstPodBubbleApiModel = _context.bubbleDetails
                                                    .Join(_context.podBubbleMembers, bd => bd.Id, pbm => pbm.BubbleId, (bd, pbm) => new { bd, pbm })
                                                    .Where(u => (query.UserId == 0) ? u.pbm.BubbleMemberId > 0 : u.pbm.BubbleMemberId == query.UserId
                                                    )
                                                    .Select(x => new BubbleApiModel
                                                    {
                                                        Id = x.bd.Id,
                                                        BubbleName = x.bd.BubbleName,
                                                        BubbleDescription = x.bd.BubbleDescription,
                                                        BubbleSize = x.bd.BubbleSize,
                                                        BubbleType = x.bd.BubbleType,
                                                        BubbleValidity = x.bd.BubbleValidity,
                                                        BubbleZipCode = x.bd.BubbleZipCode,
                                                        CreatedBy = x.bd.CreatedBy,
                                                        UpdatedBy = x.bd.UpdatedBy,
                                                        CreatedOn = x.bd.CreatedOn,
                                                        UpdatedOn = x.bd.UpdatedOn,
                                                        //lstPodUser = _context.userDetails
                                                        //        .Join(_context.podBubbleMembers, user => user.Id, podBubMem => podBubMem.BubbleMemberId, (user, podBubMem) => new { user, podBubMem })
                                                        //        .Where(pbm => (pbm.podBubMem.BubbleMemberId == query.UserId) &&(pbm.user.IsActive == true))
                                                        //        .Select(x => new UserApiModels
                                                        //        {
                                                        //            Id = x.user.Id,
                                                        //            Username = x.user.Username,
                                                        //            ProfilePicUrl = x.user.ProfilePicUrl,
                                                        //            County = x.user.County,
                                                        //            PhoneNo = x.user.PhoneNo,
                                                        //            ZipCode = x.user.ZipCode,
                                                        //            CreatedBy = x.user.CreatedBy,
                                                        //            UpdatedBy = x.user.UpdatedBy,
                                                        //            CreatedOn = x.user.CreatedOn,
                                                        //            UpdatedOn = x.user.UpdatedOn
                                                        //        }).ToList()
                                                    }).ToList()
                           }).ToListAsync();

                if (lstPodDetailsApiModel == null)
                {
                    return null;
                }
                return lstPodDetailsApiModel;
            }
        }
    }
}
