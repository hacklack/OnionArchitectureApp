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

namespace Application.Features.Commands.ChecklistCommands
{
    public class CreateUpdateChecklistQuestionCommand : IRequest<ChecklistGenericApiModel>
    {
        public CheckListType CheckListTypeId { get; set; }
        public int CheckListTypeChildId { get; set; }
        public int CheckListId { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        
        public List<CheckListSubjectiveAnswerQuestionApiModel> lstCheckListSubjectiveAnswerQuestionApiModel { get; set; }

        public class CreateUpdateChecklistQuestionHandler : IRequestHandler<CreateUpdateChecklistQuestionCommand, ChecklistGenericApiModel>
        {
            private readonly IApplicationDbContext _context;
            private IMediator _mediator;
            public CreateUpdateChecklistQuestionHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<ChecklistGenericApiModel> Handle(CreateUpdateChecklistQuestionCommand command, CancellationToken cancellationToken)
            {
                try
                {

                    ChecklistGenericApiModel apiModel = new ChecklistGenericApiModel();
                    if (command.lstCheckListSubjectiveAnswerQuestionApiModel.Count > 0)
                    {
                        CheckListDetails dbchecklistDetails = new CheckListDetails();
                        dbchecklistDetails.ChecklistName = Convert.ToString(command.CheckListTypeChildId) + "_"+ Convert.ToString(command.CheckListTypeId);
                        dbchecklistDetails.CheckListTypeChildId = command.CheckListTypeChildId;
                        dbchecklistDetails.CheckListTypeId = command.CheckListTypeId;
                        dbchecklistDetails.CreatedBy = command.CreatedBy;
                        dbchecklistDetails.UpdatedBy = command.UpdatedBy;
                        _context.checkListDetails.Add(dbchecklistDetails);
                        await _context.SaveChanges();
                        foreach (var subjectiveAnswer in command.lstCheckListSubjectiveAnswerQuestionApiModel)
                        {
                            CheckListSubjectiveAnswerQuestionApiModel apiSubjectiveModel = new CheckListSubjectiveAnswerQuestionApiModel();
                            CheckListSubjectiveAnswerQuestion dbModel = new CheckListSubjectiveAnswerQuestion();
                            if (!string.IsNullOrEmpty(subjectiveAnswer.QuestionTitle))
                            {
                                if (string.IsNullOrEmpty(Convert.ToString(subjectiveAnswer.Id)) || subjectiveAnswer.Id == 0)
                                {
                                    dbModel.QuestionTitle = subjectiveAnswer.QuestionTitle;
                                    dbModel.QuestionDescription = subjectiveAnswer.QuestionDescription;
                                    dbModel.ChecklistId = dbchecklistDetails.Id;
                                    dbModel.QuestionTypeId = subjectiveAnswer.QuestionTypeId;
                                    dbModel.CheckListTypeId = command.CheckListTypeId;
                                    dbModel.CheckListTypeChildId = command.CheckListTypeChildId;
                                    dbModel.CreatedBy = command.CreatedBy;
                                    dbModel.UpdatedBy = command.UpdatedBy;
                                    _context.checkListSubjectiveAnswerQuestion.Add(dbModel);
                                    await _context.SaveChanges();
                                }
                                else
                                {
                                    dbModel = _context.checkListSubjectiveAnswerQuestion.Where(x => x.Id == subjectiveAnswer.Id).FirstOrDefault();
                                    dbModel.QuestionTitle = subjectiveAnswer.QuestionTitle;
                                    dbModel.QuestionDescription = subjectiveAnswer.QuestionDescription;
                                    dbModel.ChecklistId = dbchecklistDetails.Id;
                                    dbModel.QuestionTypeId = subjectiveAnswer.QuestionTypeId;
                                    dbModel.CheckListTypeId = command.CheckListTypeId;
                                    dbModel.CheckListTypeChildId = command.CheckListTypeChildId;
                                    dbModel.CreatedBy = command.CreatedBy;
                                    dbModel.UpdatedBy = command.UpdatedBy;
                                    dbModel.UpdatedOn = DateTime.UtcNow;
                                    await _context.SaveChanges();
                                }
                                foreach (var subjectiveAnswerOption in subjectiveAnswer.lstCheckListQuestionOptionApiModel)
                                {
                                    CheckListQuestionOption dbModelOptions = new CheckListQuestionOption();
                                    CheckListQuestionOptionApiModel apiOptionModel = new CheckListQuestionOptionApiModel();
                                    if (!string.IsNullOrEmpty(subjectiveAnswerOption.AnswerOption))
                                    {
                                        if (string.IsNullOrEmpty(Convert.ToString(subjectiveAnswerOption.Id)) || subjectiveAnswerOption.Id == 0)
                                        {

                                            dbModelOptions.QuestionId = dbModel.Id;
                                            dbModelOptions.QuestionTypeId = dbModel.QuestionTypeId;
                                            dbModelOptions.AnswerOption = subjectiveAnswerOption.AnswerOption;
                                            dbModelOptions.CreatedBy = command.CreatedBy;
                                            dbModelOptions.UpdatedBy = command.UpdatedBy;
                                            _context.checkListQuestionOption.Add(dbModelOptions);
                                            await _context.SaveChanges();
                                        }
                                        else
                                        {
                                            dbModelOptions = _context.checkListQuestionOption.Where(x => x.Id == subjectiveAnswerOption.Id).FirstOrDefault();
                                            dbModelOptions.QuestionId = subjectiveAnswer.Id;
                                            dbModelOptions.QuestionTypeId = subjectiveAnswer.QuestionTypeId;
                                            dbModelOptions.AnswerOption = subjectiveAnswerOption.AnswerOption;
                                            dbModelOptions.CreatedBy = command.CreatedBy;
                                            dbModelOptions.UpdatedBy = command.UpdatedBy;
                                            dbModelOptions.UpdatedOn = DateTime.UtcNow;
                                            await _context.SaveChanges();
                                        }
                                    }
                                    apiOptionModel.QuestionTypeId = dbModelOptions.QuestionTypeId;
                                    apiOptionModel.QuestionId = dbModelOptions.QuestionId;
                                    apiOptionModel.Id = dbModelOptions.Id;
                                    apiOptionModel.AnswerOption = dbModelOptions.AnswerOption;
                                    apiOptionModel.UpdatedBy = dbModelOptions.UpdatedBy;
                                    apiOptionModel.CreatedBy = dbModel.CreatedBy;
                                    apiOptionModel.UpdatedOn = dbModel.UpdatedOn;
                                    apiOptionModel.CreatedOn = dbModel.CreatedOn;
                                    apiSubjectiveModel.lstCheckListQuestionOptionApiModel.Add(apiOptionModel);
                                }
                            }
                            apiSubjectiveModel.CheckListTypeChildId = dbModel.CheckListTypeChildId;
                            apiSubjectiveModel.CheckListTypeId = dbModel.CheckListTypeId;
                            apiSubjectiveModel.Id = dbModel.Id;
                            apiSubjectiveModel.QuestionTitle = dbModel.QuestionTitle;
                            apiSubjectiveModel.QuestionDescription = dbModel.QuestionDescription;
                            apiSubjectiveModel.QuestionTypeId = dbModel.QuestionTypeId;
                            apiSubjectiveModel.UpdatedBy = dbModel.UpdatedBy;
                            apiSubjectiveModel.UpdatedOn = dbModel.UpdatedOn;
                            apiSubjectiveModel.CreatedBy = dbModel.CreatedBy;
                            apiSubjectiveModel.CreatedOn = dbModel.CreatedOn;
                            apiModel.lstCheckListSubjectiveAnswerQuestionApiModel.Add(apiSubjectiveModel);
                        }
                        apiModel.CheckListTypeId = command.CheckListTypeId;
                        apiModel.CheckListTypeChildId = command.CheckListTypeChildId;
                        apiModel.ChecklistId = dbchecklistDetails.Id;
                        apiModel.CreatedBy = command.CreatedBy;
                        apiModel.UpdatedBy = command.UpdatedBy;
                    }
                   
                    return apiModel;
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
        }
    }
}
