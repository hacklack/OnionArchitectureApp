using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using static Domain.CommonCodes.CommonEnums;

namespace Application.Features.Commands.BubbleMeetCommands
{
    public class DeleteBubbleMeetCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteBubbleMeetCommandHandler : IRequestHandler<DeleteBubbleMeetCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public DeleteBubbleMeetCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteBubbleMeetCommand command, CancellationToken cancellationToken)
            {

                var checkListDetails = _context.checkListDetails.Where(y => y.CheckListTypeChildId == command.Id && y.CheckListTypeId == CheckListType.BubbleMeetChecklist).ToList();
                if (checkListDetails != null && checkListDetails.Count() > 0)
                {
                    foreach (var item in checkListDetails)
                    {
                        var chklistQuestion = _context.checkListSubjectiveAnswerQuestion.Where(y => y.ChecklistId == item.Id && y.CheckListTypeId == CheckListType.BubbleMeetChecklist).ToList();
                        if (chklistQuestion != null && chklistQuestion.Count() > 0)
                        {
                            foreach (var chkQuestion in chklistQuestion)
                            {
                                var chkQuestionOption = _context.checkListQuestionOption.Where(y => y.QuestionId == chkQuestion.Id).ToList();
                                if (chkQuestionOption != null && chkQuestionOption.Count() > 0)
                                {
                                    foreach (var option in chkQuestionOption)
                                    {
                                        _context.checkListQuestionOption.Remove(option);
                                        await _context.SaveChanges();
                                    }
                                }
                                _context.checkListSubjectiveAnswerQuestion.Remove(chkQuestion);
                                await _context.SaveChanges();
                                var chklistAnswers = _context.checkListSubjectiveQuestion_Answers.Where(y => y.ChecklistId == item.Id).ToList();

                                if (chklistAnswers != null && chklistAnswers.Count() > 0)
                                {
                                    foreach (var answer in chklistAnswers)
                                    {
                                        _context.checkListSubjectiveQuestion_Answers.Remove(answer);
                                        await _context.SaveChanges();
                                    }
                                }
                            }
                        }
                        _context.checkListDetails.Remove(item);
                        await _context.SaveChanges();
                    }
                }



                var meetMembers = _context.bubbleMeetMembers.Where(m => m.BubbleMeetId == command.Id).ToList();
                if (meetMembers.Count > 0)
                {
                    foreach (var mm in meetMembers)
                    {
                        _context.bubbleMeetMembers.Remove(mm);
                        await _context.SaveChanges();
                    }
                }

                var meetPermissions = _context.bubbleMeetMemberPermissions.Where(m => m.BubbleMeetId == command.Id && m.MeetTypeId == MeetType.BubbleMeet).ToList();
                if (meetPermissions.Count > 0)
                {
                    foreach (var pm in meetPermissions)
                    {
                        _context.bubbleMeetMemberPermissions.Remove(pm);
                        await _context.SaveChanges();
                    }
                }
                var meet = await _context.bubbleMeetDetails.Where(m => m.Id == command.Id).FirstOrDefaultAsync();
                if (meet == null)
                    return default;
                _context.bubbleMeetDetails.Remove(meet);
                await _context.SaveChanges();
                return command.Id;
            }
        }
    }

}
