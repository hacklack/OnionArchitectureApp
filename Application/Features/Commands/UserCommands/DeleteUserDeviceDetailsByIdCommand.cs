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
   public class DeleteUserDeviceDetailsByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteUserDeviceDetailsByIdHandler : IRequestHandler<DeleteUserDeviceDetailsByIdCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public DeleteUserDeviceDetailsByIdHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteUserDeviceDetailsByIdCommand command, CancellationToken cancellationToken)
            {
                var userDevice = await _context.userDeviceDetails.Where(a => a.Id == command.Id).FirstOrDefaultAsync();
                if (userDevice == null) return default;
                 _context.userDeviceDetails.Remove(userDevice);
                await _context.SaveChanges();
                return userDevice.Id;
            }
        }
    }
}
