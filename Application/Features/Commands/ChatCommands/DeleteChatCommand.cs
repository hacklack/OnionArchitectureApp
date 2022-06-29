using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using static Domain.CommonCodes.CommonEnums;

namespace Application.Features.Commands.ChatCommands
{
    public class DeleteChatCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteChatHandler : IRequestHandler<DeleteChatCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public DeleteChatHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteChatCommand command, CancellationToken cancellationToken)
            {
                var bubble = await _context.chatDetails.Where(b => b.Id == command.Id).FirstOrDefaultAsync();
                if (bubble == null)
                    return default;
                bubble.ChatStatus = false;
                await _context.SaveChanges();
                return bubble.Id;
            }
        }
    }
}
