using MediatR;
using Domain.Entities;
using Application.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using static Domain.CommonCodes.CommonEnums;
using Application.ApiModels;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Application.Services;

namespace Application.Features.Commands.ChecklistCommands
{
    public class CreateUpdateChecklistResponseCommand : IRequest<CheckListSubjectiveQuestion_AnswersListApiModel>
    {
        public CheckListType CheckListTypeId { get; set; }
        public int CheckListTypeChildId { get; set; }
        public int CheckListId { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public List<CheckListSubjectiveQuestion_AnswersApiModel> lstCheckListSubjectiveQuestion_AnswersApiModel { get; set; }

        public class CreateUpdateChecklistQuestionGenericHandler : IRequestHandler<CreateUpdateChecklistResponseCommand, CheckListSubjectiveQuestion_AnswersListApiModel>
        {
            private readonly IApplicationDbContext _context;
            private IMediator _mediator;
            public CreateUpdateChecklistQuestionGenericHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<CheckListSubjectiveQuestion_AnswersListApiModel> Handle(CreateUpdateChecklistResponseCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    CheckListSubjectiveQuestion_AnswersListApiModel lstApiModel = new CheckListSubjectiveQuestion_AnswersListApiModel();
                    CheckListSubjectiveQuestion_AnswersApiModel apisubjectiveUserAnswer = new CheckListSubjectiveQuestion_AnswersApiModel();

                    if (command.lstCheckListSubjectiveQuestion_AnswersApiModel.Count > 0)
                    {

                        foreach (var subjectiveUserAnswer in command.lstCheckListSubjectiveQuestion_AnswersApiModel)
                        {
                            CheckListSubjectiveQuestion_Answers chAnswersDBModel = new CheckListSubjectiveQuestion_Answers();
                            int cnt = _context.checkListSubjectiveQuestion_Answers.Where(y => y.CheckListQuestionId == subjectiveUserAnswer.CheckListQuestionId && y.UserId == subjectiveUserAnswer.UserId && y.AnswerOptionId == subjectiveUserAnswer.AnswerOptionId).Count();
                            if ( cnt== 0 && subjectiveUserAnswer.Id == 0)
                            {

                                chAnswersDBModel.SingleAnswer = (!string.IsNullOrEmpty(subjectiveUserAnswer.SingleAnswer)) ? subjectiveUserAnswer.SingleAnswer : CommonStaticStrings.SingleAnswerDefault;
                                chAnswersDBModel.AnswerOptionId = (subjectiveUserAnswer.AnswerOptionId == 0 || string.IsNullOrEmpty(Convert.ToString(subjectiveUserAnswer.AnswerOptionId))) ? 0 : subjectiveUserAnswer.AnswerOptionId;
                                chAnswersDBModel.UserId = subjectiveUserAnswer.UserId;
                                chAnswersDBModel.CheckListQuestionId = subjectiveUserAnswer.CheckListQuestionId;
                                chAnswersDBModel.CheckListTypeChildId = command.CheckListTypeChildId;
                                chAnswersDBModel.ChecklistId = command.CheckListId;
                                chAnswersDBModel.CreatedBy = command.CreatedBy;
                                chAnswersDBModel.UpdatedBy = command.UpdatedBy;
                                _context.checkListSubjectiveQuestion_Answers.Add(chAnswersDBModel);
                                await _context.SaveChanges();

                            }
                            else
                            {
                                chAnswersDBModel = _context.checkListSubjectiveQuestion_Answers.Where(y => y.Id == subjectiveUserAnswer.Id).SingleOrDefault();
                                chAnswersDBModel.SingleAnswer = (!string.IsNullOrEmpty(subjectiveUserAnswer.SingleAnswer)) ? subjectiveUserAnswer.SingleAnswer : CommonStaticStrings.SingleAnswerDefault;
                                chAnswersDBModel.AnswerOptionId = (subjectiveUserAnswer.AnswerOptionId == 0) ? 0 : subjectiveUserAnswer.AnswerOptionId;
                                chAnswersDBModel.UserId = subjectiveUserAnswer.UserId;
                                chAnswersDBModel.CheckListQuestionId = subjectiveUserAnswer.CheckListQuestionId;
                                chAnswersDBModel.CheckListTypeChildId = command.CheckListTypeChildId;
                                chAnswersDBModel.ChecklistId = command.CheckListId;
                                chAnswersDBModel.UpdatedBy = command.UpdatedBy;
                                chAnswersDBModel.UpdatedOn = command.UpdatedOn;
                                await _context.SaveChanges();
                            }
                            apisubjectiveUserAnswer.Id = chAnswersDBModel.Id;
                            apisubjectiveUserAnswer.SingleAnswer = chAnswersDBModel.SingleAnswer;
                            apisubjectiveUserAnswer.AnswerOptionId = chAnswersDBModel.AnswerOptionId;
                            apisubjectiveUserAnswer.CheckListQuestionId = chAnswersDBModel.CheckListQuestionId;
                            apisubjectiveUserAnswer.UserId = chAnswersDBModel.UserId;
                            lstApiModel.lstCheckListSubjectiveQuestion_AnswersApiModel.Add(apisubjectiveUserAnswer);
                        }
                        lstApiModel.CheckListId = command.CheckListId;
                        lstApiModel.CheckListTypeChildId = command.CheckListTypeChildId;
                        lstApiModel.CheckListTypeId = command.CheckListTypeId;
                    }
                    return lstApiModel;

                }
                catch (Exception ex)
                {

                    throw;
                }

            }
        }
    }
}
