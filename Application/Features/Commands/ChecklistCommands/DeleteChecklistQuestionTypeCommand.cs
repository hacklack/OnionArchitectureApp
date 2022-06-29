using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Application.Features.Commands.ChecklistCommands
{
   public class DeleteChecklistQuestionTypeCommand:IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteChecklistQuestionTypeHandler : IRequestHandler<DeleteChecklistQuestionTypeCommand, int>
        {
            private readonly IApplicationDbContext _context;
           public DeleteChecklistQuestionTypeHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteChecklistQuestionTypeCommand command, CancellationToken cancellationToken)
            {
                var ckQuestionType = await _context.checkListQuestionTypes.Where(ck => ck.Id == command.Id).FirstOrDefaultAsync();
                if (ckQuestionType == null)
                    return default;
                _context.checkListQuestionTypes.Remove(ckQuestionType);
                await _context.SaveChanges();
                return ckQuestionType.Id;
            }
        }
    }
}
