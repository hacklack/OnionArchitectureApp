using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static Domain.CommonCodes.CommonEnums;
namespace Application.Features.Queries.BubbleQueries
{
    public class GetAllOrphanUsersQuery : IRequest<List<UserDetails>>
    {
        public int UserId { get; set; }
        public int BubblType { get; set; }
        public class GetAllUsersQueryHandler : IRequestHandler<GetAllOrphanUsersQuery, List<UserDetails>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllUsersQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<List<UserDetails>> Handle(GetAllOrphanUsersQuery query, CancellationToken cancellationToken)
            {
                var membersLst = _context.bubbleMembers.Select(bm=>bm.UserId).ToList();
                List<UserDetails> userList = new List<UserDetails>();
                if (query.BubblType == (int)BubbleType.Single)
                {
                    userList = await _context.userDetails.Where(ud => !membersLst.Contains(ud.Id) && ud.IsActive == true && ud.CreatedBy==query.UserId && ud.Id!=query.UserId).ToListAsync();
                }
                else if (query.BubblType == (int)BubbleType.Multi)
                {
                    userList = await _context.userDetails.Where(ud => !membersLst.Contains(ud.Id) && ud.IsActive == true && ud.Id != query.UserId).ToListAsync();
                }
                  
                if (userList == null)
                {
                    return null;
                }
                return userList;
            }
        }
    }
}
