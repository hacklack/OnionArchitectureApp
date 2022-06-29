using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Application.Features.Commands.ChecklistCommands
{
   public class DeleteChecklistCheckListQuestionOptionCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteChecklistCheckListQuestionOptionHandler : IRequestHandler<DeleteChecklistCheckListQuestionOptionCommand, int>
        {
            private readonly IApplicationDbContext _context;
           public DeleteChecklistCheckListQuestionOptionHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteChecklistCheckListQuestionOptionCommand command, CancellationToken cancellationToken)
            {
                var ckQuestionOption = await _context.checkListQuestionOption.Where(ck => ck.Id == command.Id).FirstOrDefaultAsync();
                if (ckQuestionOption == null)
                    return default;
                _context.checkListQuestionOption.Remove(ckQuestionOption);
                await _context.SaveChanges();
                return ckQuestionOption.Id;
            }
        }
    }
}
