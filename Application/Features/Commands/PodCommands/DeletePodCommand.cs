using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Domain.CommonCodes.CommonEnums;
namespace Application.Features.Commands.PodCommands
{
    public class DeletePodCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class DeletePodCommandHandler : IRequestHandler<DeletePodCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public DeletePodCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeletePodCommand command, CancellationToken cancellationToken)
            {
                var podbubbleMembers = _context.podBubbleMembers.Where(y => y.PODId == command.Id).ToList();
                if (podbubbleMembers != null && podbubbleMembers.Count > 0)
                {
                    foreach (var item in podbubbleMembers)
                    {
                        _context.podBubbleMembers.Remove(item);
                        await _context.SaveChanges();
                    }
                }
                var podMembers = _context.podMembers.Where(y => y.PODId == command.Id).ToList();
                if (podMembers != null && podMembers.Count > 0)
                {
                    foreach (var item in podMembers)
                    {
                        _context.podMembers.Remove(item);
                        await _context.SaveChanges();
                    }
                }
                var checkListDetails = _context.checkListDetails.Where(y => y.CheckListTypeChildId == command.Id && y.CheckListTypeId==CheckListType.PODCheckList).ToList();
                if (checkListDetails != null && checkListDetails.Count() > 0)
                {
                    foreach (var item in checkListDetails)
                    {
                        var chklistQuestion = _context.checkListSubjectiveAnswerQuestion.Where(y => y.ChecklistId == item.Id && y.CheckListTypeId == CheckListType.PODCheckList).ToList();
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
                                    foreach(var answer in chklistAnswers)
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
                var pod = await _context.podDetails.Where(m => m.Id == command.Id).FirstOrDefaultAsync();
                if (pod == null)
                    return default;
                _context.podDetails.Remove(pod);
                await _context.SaveChanges();
                return command.Id;
            }
        }
    }

}
