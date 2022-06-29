using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System;
using System.Collections.Generic;
using Application.ApiModels;
using static Domain.CommonCodes.CommonEnums;

namespace Application.Features.Queries.ChatQueries
{
    public class GetChatMembersByUserIdChatypeIdQuery : IRequest<List<UserApiModels>>
    {
        public int BubblePODId { get; set; }
        public MeetType ChatParentTypeId { get; set; }
        public class GetChatMembersByUserIdChatypeIdHandler : IRequestHandler<GetChatMembersByUserIdChatypeIdQuery, List<UserApiModels>>
        {
            private readonly IApplicationDbContext _context;
            public GetChatMembersByUserIdChatypeIdHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<List<UserApiModels>> Handle(GetChatMembersByUserIdChatypeIdQuery query, CancellationToken cancellationToken)
            {
                List<UserApiModels> chatMemberDetails = new List<UserApiModels>();
                if (query.ChatParentTypeId == MeetType.Bubble)
                {
                    chatMemberDetails = _context.userDetails
                        .Join(_context.bubbleMembers, u => u.Id, bm => bm.UserId, (u, bm) => new { u, bm })
                        .Where(bmd => bmd.bm.BubbleId == query.BubblePODId && bmd.u.IsActive == true)
                        .Select(x => new UserApiModels()
                        {
                            Id = x.u.Id,
                            Username = x.u.Username,
                            ZipCode = x.u.ZipCode,
                            County = x.u.County,
                            CountyName = _context.counties.Where(y => y.Fips == x.u.County).Select(x => x.CountyName).FirstOrDefault(),
                            ProfilePicUrl = x.u.ProfilePicUrl,
                            PhoneNo = x.u.PhoneNo,
                            IsAdmin= (_context.bubbleMeetMemberPermissions
                                           .Where(p => p.UserId == x.u.Id && p.BubbleMeetId == x.bm.BubbleId
                                           && p.MeetTypeId == MeetType.Bubble && p.UserPermissionTypeId==UserPermission.IsAdmin)
                                           .Select(x =>x.UserPermissionStatus).FirstOrDefault()),
                            ChatId = (_context.chatMembers
                                            .Join(_context.chatDetails, cm => cm.ChatId, cd => cd.Id, (cm, cd) => new { cm, cd })
                                            .Where(ct => ct.cm.ChatMemberId == x.u.Id
                                                && ct.cd.ChatTypeId == (int)ChatTypes.PersonalChat
                                                && ct.cd.ChatParentId == query.BubblePODId
                                                && ct.cd.ChatParentTypeId == (int)query.ChatParentTypeId))
                                                .Select(ctx =>ctx.cd.Id).FirstOrDefault(),
                            lstUserPermission = (_context.bubbleMeetMemberPermissions
                                           .Where(p => p.UserId == x.u.Id && p.BubbleMeetId == x.bm.BubbleId
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
                            countyDetails = _context.counties.Where(y => y.Fips == x.u.County).Select(x => new CountiesApiModel
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
                }
                else if (query.ChatParentTypeId == MeetType.BubbleMeet)
                {
                    chatMemberDetails = _context.userDetails
                            .Join(_context.bubbleMeetMembers, u => u.Id, bm => bm.UserId, (u, bm) => new { u, bm })
                            .Where(bmd => bmd.bm.BubbleMeetId == query.BubblePODId && bmd.u.IsActive == true)
                            .Select(x => new UserApiModels()
                            {
                                Id = x.u.Id,
                                Username = x.u.Username,
                                ZipCode = x.u.ZipCode,
                                County = x.u.County,
                                CountyName = _context.counties.Where(y => y.Fips == x.u.County).Select(x => x.CountyName).FirstOrDefault(),
                                ProfilePicUrl = x.u.ProfilePicUrl,
                                PhoneNo = x.u.PhoneNo,
                                IsAdmin = (_context.bubbleMeetMemberPermissions
                                           .Where(p => p.UserId == x.u.Id && p.BubbleMeetId == x.bm.BubbleMeetId
                                           && p.MeetTypeId == MeetType.BubbleMeet && p.UserPermissionTypeId == UserPermission.IsAdmin)
                                           .Select(x => x.UserPermissionStatus).FirstOrDefault()),
                                ChatId = (_context.chatMembers
                                            .Join(_context.chatDetails, cm => cm.ChatId, cd => cd.Id, (cm, cd) => new { cm, cd })
                                            .Where(ct => ct.cm.ChatMemberId == x.u.Id
                                                && ct.cd.ChatTypeId == (int)ChatTypes.PersonalChat
                                                && ct.cd.ChatParentId == query.BubblePODId
                                                && ct.cd.ChatParentTypeId == (int)query.ChatParentTypeId))
                                                .Select(ctx => ctx.cd.Id).FirstOrDefault(),
                                lstUserPermission = (_context.bubbleMeetMemberPermissions
                                               .Where(p => p.UserId == x.u.Id && p.BubbleMeetId == x.bm.BubbleMeetId
                                               && p.MeetTypeId == MeetType.BubbleMeet)
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
                                countyDetails = _context.counties.Where(y => y.Fips == x.u.County).Select(x => new CountiesApiModel
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
                }
                else if (query.ChatParentTypeId == MeetType.POD)
                {
                    chatMemberDetails = _context.userDetails
                                 .Join(_context.podBubbleMembers, u => u.Id, bm => bm.BubbleMemberId, (u, bm) => new { u, bm })
                                 .Where(bmd => bmd.bm.PODId == query.BubblePODId && bmd.u.IsActive == true)
                                 .Select(x => new UserApiModels()
                                 {
                                     Id = x.u.Id,
                                     Username = x.u.Username,
                                     ZipCode = x.u.ZipCode,
                                     County = x.u.County,
                                     CountyName = _context.counties.Where(y => y.Fips == x.u.County).Select(x => x.CountyName).FirstOrDefault(),
                                     ProfilePicUrl = x.u.ProfilePicUrl,
                                     PhoneNo = x.u.PhoneNo,
                                     IsAdmin = (_context.bubbleMeetMemberPermissions
                                           .Where(p => p.UserId == x.u.Id && p.BubbleMeetId == x.bm.PODId
                                           && p.MeetTypeId == MeetType.POD && p.UserPermissionTypeId == UserPermission.IsAdmin)
                                           .Select(x => x.UserPermissionStatus).FirstOrDefault()),
                                     ChatId =(_context.chatMembers
                                            .Join(_context.chatDetails, cm => cm.ChatId, cd => cd.Id, (cm, cd) => new { cm, cd })
                                            .Where(ct => ct.cm.ChatMemberId == x.u.Id
                                                && ct.cd.ChatTypeId == (int)ChatTypes.PersonalChat
                                                && ct.cd.ChatParentId == query.BubblePODId
                                                && ct.cd.ChatParentTypeId == (int)query.ChatParentTypeId))
                                                .Select(ctx => ctx.cd.Id).FirstOrDefault(),
                                     lstUserPermission = (_context.bubbleMeetMemberPermissions
                                                    .Where(p => p.UserId == x.u.Id && p.BubbleMeetId == x.bm.PODId
                                                    && p.MeetTypeId == MeetType.POD)
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
                                     countyDetails = _context.counties.Where(y => y.Fips == x.u.County).Select(x => new CountiesApiModel
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
                }
                else if (query.ChatParentTypeId == MeetType.PODMeet)
                {
                    chatMemberDetails = _context.userDetails
                                    .Join(_context.podMeetMembers, u => u.Id, bm => bm.UserId, (u, bm) => new { u, bm })
                                    .Where(bmd => bmd.bm.PODMeetId == query.BubblePODId && bmd.u.IsActive == true)
                                    .Select(x => new UserApiModels()
                                    {
                                        Id = x.u.Id,
                                        Username = x.u.Username,
                                        ZipCode = x.u.ZipCode,
                                        County = x.u.County,
                                        CountyName = _context.counties.Where(y => y.Fips == x.u.County).Select(x => x.CountyName).FirstOrDefault(),
                                        ProfilePicUrl = x.u.ProfilePicUrl,
                                        PhoneNo = x.u.PhoneNo,
                                        IsAdmin = (_context.bubbleMeetMemberPermissions
                                           .Where(p => p.UserId == x.u.Id && p.BubbleMeetId == x.bm.PODMeetId
                                           && p.MeetTypeId == MeetType.PODMeet && p.UserPermissionTypeId == UserPermission.IsAdmin)
                                           .Select(x => x.UserPermissionStatus).FirstOrDefault()),
                                        ChatId = (_context.chatMembers
                                            .Join(_context.chatDetails, cm => cm.ChatId, cd => cd.Id, (cm, cd) => new { cm, cd })
                                            .Where(ct => ct.cm.ChatMemberId == x.u.Id
                                                && ct.cd.ChatTypeId == (int)ChatTypes.PersonalChat
                                                && ct.cd.ChatParentId == query.BubblePODId
                                                && ct.cd.ChatParentTypeId == (int)query.ChatParentTypeId))
                                                .Select(ctx => ctx.cd.Id).FirstOrDefault(),
                                        lstUserPermission = (_context.bubbleMeetMemberPermissions
                                                       .Where(p => p.UserId == x.u.Id && p.BubbleMeetId == x.bm.PODMeetId
                                                       && p.MeetTypeId == MeetType.PODMeet)
                                                       .Select(x     => new BubbleMeetPermissionsApiModel
                                                        {
                                                           Id = x.Id,
                                                           PermissionParenttId = x.BubbleMeetId,
                                                           MeetTypeId = x.MeetTypeId,
                                                           UserPermissionTypeId = x.UserPermissionTypeId,
                                                           UserPermissionStatus = x.UserPermissionStatus,
                                                           UserId = x.UserId
                                                       })
                                                       .ToList()),
                                        countyDetails = _context.counties.Where(y => y.Fips == x.u.County).Select(x => new CountiesApiModel
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
                                    })  .ToList();
                }

                if (chatMemberDetails == null)
                {
                    return null;
                }
                return chatMemberDetails;
            }
        }

    }
}
