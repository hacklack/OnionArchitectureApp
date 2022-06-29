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
namespace Application.Features.Queries.BubbleMeetQueries
{
    public class GetAllOrphanUsersForBubbleMeetQuery : IRequest<List<UserDetails>>
    {
        public int UserId { get; set; }
        // public int BubblType { get; set; }
        public class GetAllOrphanUsersForBubbleMeetHandler : IRequestHandler<GetAllOrphanUsersForBubbleMeetQuery, List<UserDetails>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllOrphanUsersForBubbleMeetHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<List<UserDetails>> Handle(GetAllOrphanUsersForBubbleMeetQuery query, CancellationToken cancellationToken)
            {
                var bubbleLst = _context.bubbleMembers.Where(y => y.UserId == query.UserId).Select(bm => bm.BubbleId).ToList();
                List<UserDetails> userList = new List<UserDetails>();

                foreach (var item in bubbleLst)
                {
                    var lstBubbleMembers = _context.bubbleMembers.Where(y => y.BubbleId == item && y.UserId != query.UserId).Select(x => x.UserId).ToList();
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
