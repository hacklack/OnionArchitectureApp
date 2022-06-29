using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Queries.UserQueries
{
   public class GetAllUsersDeviceTokenQuery : IRequest<List<UserDeviceDetails>>
    {
        public int Id { get; set; }
        public class GetAllUsersDeviceTokenHandler : IRequestHandler<GetAllUsersDeviceTokenQuery, List<UserDeviceDetails>>
        {
            
            private readonly IApplicationDbContext _context;
            public GetAllUsersDeviceTokenHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<List<UserDeviceDetails>> Handle(GetAllUsersDeviceTokenQuery query, CancellationToken cancellationToken)
            {
                List<UserDeviceDetails> lstUserDetails = new List<UserDeviceDetails>();
                if (query.Id > 0)
                {
                    lstUserDetails = await _context.userDeviceDetails.Where(ud => ud.Id == query.Id).ToListAsync();
                }
                else
                {
                    lstUserDetails = await _context.userDeviceDetails.ToListAsync();
                }
                if (lstUserDetails == null)
                {
                    return null;
                }
                return lstUserDetails;
            }
        }
    }
}
