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

namespace Application.Features.Commands.ChecklistCommands
{
    public class CreateUpdateCheckListSubjectiveAnswerQuestionCommand : IRequest<CheckListSubjectiveAnswerQuestionApiModel>
    {

        public int Id { get; set; }
        public CheckListType CheckListTypeId { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionDescription { get; set; }
        public int CheckListTypeChildId { get; set; }
        public int QuestionTypeId { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public class CreateUpdateCheckListSubjectiveAnswerQuestionHandler : IRequestHandler<CreateUpdateCheckListSubjectiveAnswerQuestionCommand, CheckListSubjectiveAnswerQuestionApiModel>
        {
            private readonly IApplicationDbContext _context;
            public CreateUpdateCheckListSubjectiveAnswerQuestionHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<CheckListSubjectiveAnswerQuestionApiModel> Handle(CreateUpdateCheckListSubjectiveAnswerQuestionCommand command, CancellationToken cancellationToken)
            {
                CheckListSubjectiveAnswerQuestion dbModel = new CheckListSubjectiveAnswerQuestion();
                CheckListSubjectiveAnswerQuestionApiModel apiModel = new CheckListSubjectiveAnswerQuestionApiModel();
                var chkSingleAnswerList = _context.checkListSubjectiveAnswerQuestion.Select(ck => ck.Id).ToList();

                if (string.IsNullOrEmpty(Convert.ToString(command.Id)) || command.Id == 0)
                {
                    dbModel.QuestionTitle = command.QuestionTitle;
                    dbModel.QuestionDescription = command.QuestionDescription;
                    dbModel.QuestionTypeId = command.QuestionTypeId;
                    dbModel.CheckListTypeId = command.CheckListTypeId;
                    dbModel.CheckListTypeChildId = command.CheckListTypeChildId;
                    dbModel.CreatedBy = command.CreatedBy;
                    dbModel.UpdatedBy = command.UpdatedBy;
                    _context.checkListSubjectiveAnswerQuestion.Add(dbModel);
                    await _context.SaveChanges();
                }
                else
                {
                    dbModel = _context.checkListSubjectiveAnswerQuestion.Where(x => x.Id == command.Id).FirstOrDefault();
                    dbModel.QuestionTitle = command.QuestionTitle;
                    dbModel.QuestionDescription = command.QuestionDescription;
                    dbModel.QuestionTypeId = command.QuestionTypeId;
                    dbModel.CheckListTypeId = command.CheckListTypeId;
                    dbModel.CheckListTypeChildId = command.CheckListTypeChildId;
                    dbModel.CreatedBy = command.CreatedBy;
                    dbModel.UpdatedBy = command.UpdatedBy;
                    dbModel.UpdatedOn = DateTime.UtcNow;
                    await _context.SaveChanges();
                }
                apiModel.Id = dbModel.Id;
                apiModel.QuestionTitle = dbModel.QuestionTitle;
                apiModel.QuestionDescription = dbModel.QuestionDescription;
                apiModel.QuestionTypeId = dbModel.QuestionTypeId;
                apiModel.CheckListTypeChildId = dbModel.CheckListTypeChildId;
                apiModel.CheckListTypeId = dbModel.CheckListTypeId;
                apiModel.CreatedBy = dbModel.CreatedBy;
                apiModel.CreatedOn = dbModel.CreatedOn;
                apiModel.UpdatedBy = dbModel.UpdatedBy;
                apiModel.UpdatedOn = dbModel.UpdatedOn;
                return apiModel;
            }
        }
    }
}
