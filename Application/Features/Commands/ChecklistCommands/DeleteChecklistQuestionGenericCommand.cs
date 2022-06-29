using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Application.Features.Commands.ChecklistCommands
{
    public class DeleteChecklistQuestionGenericCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteChecklistQuestionGenericHandler : IRequestHandler<DeleteChecklistQuestionGenericCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public DeleteChecklistQuestionGenericHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteChecklistQuestionGenericCommand command, CancellationToken cancellationToken)
            {

                var cksubjectiveAnswer = await _context.checkListSubjectiveAnswerQuestion.Where(ck => ck.Id == command.Id).FirstOrDefaultAsync();
                var cksubjectiveAnswerOption = await _context.checkListQuestionOption.Where(cko => cko.QuestionId == command.Id).ToListAsync();
                if (cksubjectiveAnswer == null)
                    return default;
                if (cksubjectiveAnswerOption.Count() > 0)
                {
                    foreach (var item in cksubjectiveAnswerOption)
                    {
                        _context.checkListQuestionOption.Remove(item);
                        await _context.SaveChanges();
                    }
                }
                _context.checkListSubjectiveAnswerQuestion.Remove(cksubjectiveAnswer);
                
                await _context.SaveChanges();
                return cksubjectiveAnswer.Id;
            }
        }
    }
}
