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
namespace Application.Features.Queries.PODMeetQueries
{
    public class GetAllOrphanUsersForPODMeetQuery : IRequest<List<UserDetails>>
    {
        public int UserId { get; set; }
        // public int BubblType { get; set; }
        public class GetAllOrphanUsersForPODMeetHandler : IRequestHandler<GetAllOrphanUsersForPODMeetQuery, List<UserDetails>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllOrphanUsersForPODMeetHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<List<UserDetails>> Handle(GetAllOrphanUsersForPODMeetQuery query, CancellationToken cancellationToken)
            {
                var podLst = _context.podBubbleMembers.Where(y =>y.BubbleMemberId == query.UserId).Select(bm => bm.BubbleId).ToList();
                List<UserDetails> userList = new List<UserDetails>();

                foreach (var item in podLst)
                {
                    var lstBubbleMembers = _context.podBubbleMembers.Where(y => y.BubbleId == item && y.BubbleMemberId != query.UserId).Select(x => x.BubbleMemberId).ToList();
                    foreach (var itemUser in lstBubbleMembers)
                    {
                        UserDetails user = new UserDetails();
                        user = _context.userDetails.Where(w => w.Id == itemUser && w.IsActive == true).FirstOrDefault();
                        userList.Add(user);
                    }
                    
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
