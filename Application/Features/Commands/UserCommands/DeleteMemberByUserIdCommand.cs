using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Application.Features.Commands.UserCommands
{
    public class DeleteMemberByUserIdCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public int MemberId { get; set; }
        public class DeleteMemberByUserIdCommandHandler : IRequestHandler<DeleteMemberByUserIdCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public DeleteMemberByUserIdCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteMemberByUserIdCommand command, CancellationToken cancellationToken)
            {
                var user = await _context.userDetails
                                .Where(a => a.Id == command.MemberId 
                                    && a.CreatedBy == command.UserId
                                    && a.IsActive==true).FirstOrDefaultAsync();
                if (user == null) return default;
                _context.userDetails.Remove(user);
                await _context.SaveChanges();
                return user.Id;
            }
        }
    }
}
