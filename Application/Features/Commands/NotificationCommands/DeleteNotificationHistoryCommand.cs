using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using static Domain.CommonCodes.CommonEnums;

namespace Application.Features.Commands.NotificationCommands
{
    public class DeleteNotificationHistoryCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteNotificationHistoryHandler : IRequestHandler<DeleteNotificationHistoryCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public DeleteNotificationHistoryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteNotificationHistoryCommand command, CancellationToken cancellationToken)
            {
                var noti = await _context.notificationsHistory.Where(b => b.Id == command.Id).FirstOrDefaultAsync();
                if (noti == null)
                    return default;
                _context.notificationsHistory.Remove(noti);
                await _context.SaveChanges();
                return noti.Id;
            }
        }
    }
}
