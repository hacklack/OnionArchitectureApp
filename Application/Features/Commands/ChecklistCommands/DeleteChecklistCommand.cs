using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Application.ApiModels;

namespace Application.Features.Commands.ChecklistCommands
{
    public class DeleteChecklistCommand : IRequest<DeletionApimodel>
    {
        public int Id { get; set; }
        public class DeleteChecklistHandler : IRequestHandler<DeleteChecklistCommand, DeletionApimodel>
        {
            private readonly IApplicationDbContext _context;
            public DeleteChecklistHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<DeletionApimodel> Handle(DeleteChecklistCommand command, CancellationToken cancellationToken)
            {
                DeletionApimodel deletionApimodel = new DeletionApimodel();
                var chkList = _context.checkListDetails.Where(cklst => cklst.Id == command.Id).FirstOrDefault();

                var cksubjectiveAnswerLst = _context.checkListSubjectiveAnswerQuestion.Where(ck => ck.ChecklistId == command.Id).ToList();
                if (cksubjectiveAnswerLst.Count <= 0)
                    return default;
                foreach (var chkAnswer in cksubjectiveAnswerLst)
                {
                    var cksubjectiveAnswerOption = await _context.checkListQuestionOption.Where(cko => cko.QuestionId == chkAnswer.Id).ToListAsync();
                    if (cksubjectiveAnswerOption.Count() > 0)
                    {
                        foreach (var item in cksubjectiveAnswerOption)
                        {
                            _context.checkListQuestionOption.Remove(item);
                            await _context.SaveChanges();
                        }
                    }
                    _context.checkListSubjectiveAnswerQuestion.Remove(chkAnswer);
                    await _context.SaveChanges();
                }


                _context.checkListDetails.Remove(chkList);
                await _context.SaveChanges();
              deletionApimodel.Id = chkList.Id;
                return deletionApimodel;
            }
        }
    }
}
