using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Application.Features.Commands.UserCommands
{
    public class CreateUserCommand : IRequest<UserDetails>
    {
        public string Username { get; set; }
        public string PhoneNo { get; set; }
        public string ZipCode { get; set; }
        public int County { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public string Longitute { get; set; }
        public string Latitude { get; set; }
        public bool IsActive { get; set; }
        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDetails>
        {
            private readonly IApplicationDbContext _context;
            public CreateUserCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<UserDetails> Handle(CreateUserCommand command, CancellationToken cancellationToken)
            {
              UserDetails user =  new UserDetails();
                if (_context.userDetails.Where(u => u.PhoneNo == command.PhoneNo && u.IsActive==true).Count() > 0)
                {
                    user= _context.userDetails.Where(u => u.PhoneNo == command.PhoneNo && u.IsActive == true).FirstOrDefault();
                }
                else
                {
                    user.Username = command.Username;
                    user.PhoneNo = command.PhoneNo;
                    user.ZipCode = command.ZipCode;
                    user.County = command.County;
                    user.CreatedBy = command.CreatedBy;
                    user.UpdatedBy = command.UpdatedBy;
                    user.Longitute = command.Longitute;
                    user.Latitude = command.Latitude;
                    user.IsActive = command.IsActive;

                    _context.userDetails.Add(user);
                    await _context.SaveChanges();
                    //var response = new { UserId = user.Id };
                    
                }
                return user;
            }

        }

    }
}
