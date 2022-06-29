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
using static Domain.CommonCodes.CommonEnums;

namespace Application.Features.Commands.UserCommands
{
    public class CreateUpdateUserDeviceDetailsCommand : IRequest<UserDeviceDetails>
    {
        public string DeviceToken { get; set; }
        public DeviceType DeviceTypeId { get; set; }
        public int UserId { get; set; }
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public class CreateUpdateUserDeviceDetailsHandler : IRequestHandler<CreateUpdateUserDeviceDetailsCommand, UserDeviceDetails>
        {
            private readonly IApplicationDbContext _context;
            public CreateUpdateUserDeviceDetailsHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<UserDeviceDetails> Handle(CreateUpdateUserDeviceDetailsCommand command, CancellationToken cancellationToken)
            {
                UserDeviceDetails userDeviceDetails;
                if (_context.userDeviceDetails.Where(u => u.DeviceToken == command.DeviceToken && u.DeviceTypeId == command.DeviceTypeId).Count() > 0)
                {
                    var deviceDetails = _context.userDeviceDetails.Where(y => y.DeviceToken == command.DeviceToken && y.DeviceTypeId == command.DeviceTypeId).FirstOrDefault();
                    userDeviceDetails = new UserDeviceDetails();
                    deviceDetails.UserId = command.UserId;
                    deviceDetails.UpdatedBy = command.UpdatedBy;

                    await _context.SaveChanges();
                    return deviceDetails;
                }
                else if (command.Id > 0)
                {
                    var deviceDetails = _context.userDeviceDetails.Where(y => y.Id == command.Id).FirstOrDefault();
                    userDeviceDetails = new UserDeviceDetails();
                    deviceDetails.DeviceToken = command.DeviceToken;
                    deviceDetails.UserId = command.UserId;
                    deviceDetails.UpdatedBy = command.UpdatedBy;

                    await _context.SaveChanges();
                    return deviceDetails;

                }
                else
                {
                    userDeviceDetails = new UserDeviceDetails();
                    userDeviceDetails.UserId = command.UserId;
                    userDeviceDetails.DeviceToken = command.DeviceToken;
                    userDeviceDetails.DeviceTypeId = command.DeviceTypeId;
                    userDeviceDetails.CreatedBy = command.CreatedBy;
                    userDeviceDetails.UpdatedBy = command.UpdatedBy;

                    _context.userDeviceDetails.Add(userDeviceDetails);
                    await _context.SaveChanges();
                    return userDeviceDetails;
                }

            }

        }

    }
}
