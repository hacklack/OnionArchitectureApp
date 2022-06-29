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
using Application.Services;

namespace Application.Features.Queries.ChecklistQueries
{
    public class GetCheckUsersListByChecklistTypeIdAndChildIdQuery : IRequest<List<UserApiModels>>
    {
        public int ChecklistTypeId { get; set; }
        public int CheckListTypeChildId { get; set; }
        //public int UserId { get; set; }
        public class GetCheckUsersListByChecklistTypeIdAndChildIdHandler : IRequestHandler<GetCheckUsersListByChecklistTypeIdAndChildIdQuery, List<UserApiModels>>
        {

            private readonly IApplicationDbContext _context;
            public GetCheckUsersListByChecklistTypeIdAndChildIdHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<List<UserApiModels>> Handle(GetCheckUsersListByChecklistTypeIdAndChildIdQuery query, CancellationToken cancellationToken)
            {
                List<UserApiModels> lstApiModel = new List<UserApiModels>();
                UserApiModels apiModel = new UserApiModels();
                if (query.ChecklistTypeId == (int)CheckListType.BubbleCheckList)
                {
                    lstApiModel = _context.bubbleMembers
                     .Join(_context.userDetails, bm => bm.UserId, ud => ud.Id, (bm, ud) => new { bm, ud })
                     .Join(_context.checkListSubjectiveQuestion_Answers, udc => udc.ud.Id, a => a.UserId, (udc, a) => new { udc, a })
                     .Where(y => y.a.CheckListTypeChildId == query.CheckListTypeChildId && y.a.UserId == y.udc.ud.Id)
                     .Select(x => new UserApiModels
                     {
                         Id=x.udc.ud.Id,
                         Username=x.udc.ud.Username,
                         County= x.udc.ud.County,
                         CountyName = _context.counties.Where(y => y.Fips == x.udc.ud.County).Select(x => x.CountyName).FirstOrDefault(),
                         PhoneNo = x.udc.ud.PhoneNo,
                         ProfilePicUrl= x.udc.ud.ProfilePicUrl,
                         IsAdmin =x.udc.bm.IsBubbleAdmin,
                         countyDetails = _context.counties.Where(y => y.Fips == x.udc.ud.County).Select(x => new CountiesApiModel
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
                     }).Distinct().ToList();
                }
                else if (query.ChecklistTypeId == (int)CheckListType.BubbleMeetChecklist)
                {
                    lstApiModel = _context.bubbleMeetMembers
                     .Join(_context.userDetails, bm => bm.UserId, ud => ud.Id, (bm, ud) => new { bm, ud })
                     .Join(_context.checkListSubjectiveQuestion_Answers, udc => udc.ud.Id, a => a.UserId, (udc, a) => new { udc, a })
                     .Where(y => y.a.CheckListTypeChildId == query.CheckListTypeChildId && y.a.UserId == y.udc.ud.Id)
                     .Select(x => new UserApiModels
                     {
                         Id = x.udc.ud.Id,
                         Username = x.udc.ud.Username,
                         County = x.udc.ud.County,
                         CountyName = _context.counties.Where(y => y.Fips == x.udc.ud.County).Select(x => x.CountyName).FirstOrDefault(),
                         PhoneNo = x.udc.ud.PhoneNo,
                         ProfilePicUrl = x.udc.ud.ProfilePicUrl,
                         IsAdmin = (x.udc.bm.CreatedBy==x.udc.bm.UserId)?true:false,
                         countyDetails = _context.counties.Where(y => y.Fips == x.udc.ud.County).Select(x => new CountiesApiModel
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
                     })
                     .Distinct()
                     .ToList();
                }
                else if (query.ChecklistTypeId == (int)CheckListType.PODCheckList)
                {
                    lstApiModel = _context.podBubbleMembers
                     .Join(_context.userDetails, bm => bm.BubbleMemberId, ud => ud.Id, (bm, ud) => new { bm, ud })
                     .Join(_context.checkListSubjectiveQuestion_Answers, udc => udc.ud.Id, a => a.UserId, (udc, a) => new { udc, a })
                     .Where(y => y.a.CheckListTypeChildId == query.CheckListTypeChildId && y.a.UserId == y.udc.ud.Id)
                     .Select(x => new UserApiModels
                     {
                         Id = x.udc.ud.Id,
                         Username = x.udc.ud.Username,
                         County = x.udc.ud.County,
                         CountyName = _context.counties.Where(y => y.Fips == x.udc.ud.County).Select(x => x.CountyName).FirstOrDefault(),
                         PhoneNo = x.udc.ud.PhoneNo,
                         ProfilePicUrl = x.udc.ud.ProfilePicUrl,
                         IsAdmin = (x.udc.bm.CreatedBy == x.udc.bm.BubbleMemberId) ? true : false,
                         countyDetails = _context.counties.Where(y => y.Fips == x.udc.ud.County).Select(x => new CountiesApiModel
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
                     }).Distinct().ToList();
                }
                else if (query.ChecklistTypeId == (int)CheckListType.PODMeetChecklist)
                {
                    lstApiModel = _context.podMeetMembers
                     .Join(_context.userDetails, bm => bm.UserId, ud => ud.Id, (bm, ud) => new { bm, ud })
                     .Join(_context.checkListSubjectiveQuestion_Answers, udc => udc.ud.Id, a => a.UserId, (udc, a) => new { udc, a })
                     .Where(y => y.a.CheckListTypeChildId == query.CheckListTypeChildId && y.a.UserId == y.udc.ud.Id)
                     .Select(x => new UserApiModels
                     {
                         Id = x.udc.ud.Id,
                         Username = x.udc.ud.Username,
                         County = x.udc.ud.County,
                         CountyName = _context.counties.Where(y => y.Fips == x.udc.ud.County).Select(x => x.CountyName).FirstOrDefault(),
                         PhoneNo = x.udc.ud.PhoneNo,
                         ProfilePicUrl = x.udc.ud.ProfilePicUrl,
                         IsAdmin = (x.udc.bm.CreatedBy == x.udc.bm.UserId) ? true : false,
                         countyDetails = _context.counties.Where(y => y.Fips == x.udc.ud.County).Select(x => new CountiesApiModel
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
                     }).Distinct().ToList();
                }
                if (lstApiModel == null)
                {
                    return null;
                }
                return lstApiModel;
            }
        }
    }
}
