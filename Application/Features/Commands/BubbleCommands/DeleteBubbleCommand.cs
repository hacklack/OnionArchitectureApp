using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using static Domain.CommonCodes.CommonEnums;

namespace Application.Features.Commands.BubbleCommands
{
    public class DeleteBubbleCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteBubbleCommandHandler : IRequestHandler<DeleteBubbleCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public DeleteBubbleCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteBubbleCommand command, CancellationToken cancellationToken)
            {
                var chkDetails = _context.checkListDetails.Where(c => c.CheckListTypeChildId == command.Id && c.CheckListTypeId == CheckListType.BubbleCheckList).FirstOrDefault();
                if (chkDetails != null)
                {
                    var chkQuestions = _context.checkListSubjectiveAnswerQuestion.Where(cq => cq.ChecklistId == chkDetails.Id && cq.CheckListTypeId == CheckListType.BubbleCheckList).ToList();
                    if (chkQuestions != null && chkQuestions.Count > 0)
                    {
                        foreach (var chkQuest in chkQuestions)
                        {
                            var chkQuestionOptions = _context.checkListQuestionOption.Where(cqo => cqo.QuestionId == chkQuest.Id).ToList();
                            if (chkQuestionOptions != null && chkQuestionOptions.Count > 0)
                            {
                                foreach (var item in chkQuestionOptions)
                                {
                                    _context.checkListQuestionOption.Remove(item);
                                    await _context.SaveChanges();
                                }
                            }
                            var chkAnswers = _context.checkListSubjectiveQuestion_Answers.Where(cqa => cqa.CheckListQuestionId == chkQuest.Id).ToList();
                            if (chkAnswers != null && chkAnswers.Count > 0)
                            {
                                foreach (var answer in chkAnswers)
                                {
                                    _context.checkListSubjectiveQuestion_Answers.Remove(answer);
                                    await _context.SaveChanges();
                                }
                            }
                            _context.checkListSubjectiveAnswerQuestion.Remove(chkQuest);
                            await _context.SaveChanges();
                        }
                    }
                    _context.checkListDetails.Remove(chkDetails);

                }
                var lstBubbleMem = _context.bubbleMembers.Where(b => b.BubbleId == command.Id).ToList();
                if (lstBubbleMem != null && lstBubbleMem.Count > 0)
                {
                    foreach (var item in lstBubbleMem)
                    {
                        _context.bubbleMembers.Remove(item);
                        await _context.SaveChanges();
                    }
                }
                var bubble = await _context.bubbleDetails.Where(b => b.Id == command.Id).FirstOrDefaultAsync();
                if (bubble == null)
                    return default;
                _context.bubbleDetails.Remove(bubble);
                await _context.SaveChanges();
                return bubble.Id;
            }
        }
    }
}
