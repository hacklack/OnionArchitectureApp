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
   public class DeleteUserByIdCommand:IRequest<int>
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public class DeleteUserByIdCommandHandler : IRequestHandler<DeleteUserByIdCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public DeleteUserByIdCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteUserByIdCommand command, CancellationToken cancellationToken)
            {
                var user = await _context.userDetails.Where(a => a.Id == command.Id && a.IsActive == true).FirstOrDefaultAsync();
                if (user == null) return default;
                user.IsActive = command.IsActive;
                // _context.userDetails.Remove(user);
                await _context.SaveChanges();
                return user.Id;
            }
        }
    }
}
