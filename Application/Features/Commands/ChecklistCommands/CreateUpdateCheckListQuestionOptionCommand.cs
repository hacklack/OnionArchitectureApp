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
    public class CreateUpdateCheckListQuestionOptionCommand : IRequest<CheckListQuestionOptionApiModel>
    {

        public int Id { get; set; }
        public string AnswerOption { get; set; }
        public int QuestionId { get; set; }
        public int QuestionTypeId { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public class CreateUpdateCheckListQuestionOptionHandler : IRequestHandler<CreateUpdateCheckListQuestionOptionCommand, CheckListQuestionOptionApiModel>
        {
            private readonly IApplicationDbContext _context;
            public CreateUpdateCheckListQuestionOptionHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<CheckListQuestionOptionApiModel> Handle(CreateUpdateCheckListQuestionOptionCommand command, CancellationToken cancellationToken)
            {
                CheckListQuestionOption dbModel = new CheckListQuestionOption();
                CheckListQuestionOptionApiModel apiModel = new CheckListQuestionOptionApiModel();
                var chkTypeList = _context.checkListQuestionOption.Select(ck => ck.Id).ToList();

                if (string.IsNullOrEmpty(Convert.ToString(command.Id)) || command.Id == 0)
                {
                    dbModel.QuestionId = command.QuestionId;
                    dbModel.QuestionTypeId = command.QuestionTypeId;
                    dbModel.AnswerOption = command.AnswerOption;
                    dbModel.CreatedBy = command.CreatedBy;
                    dbModel.UpdatedBy = command.UpdatedBy;
                    _context.checkListQuestionOption.Add(dbModel);
                    await _context.SaveChanges();
                }
                else
                {
                    dbModel = _context.checkListQuestionOption.Where(x => x.Id == command.Id).FirstOrDefault();
                    dbModel.QuestionId = command.QuestionId;
                    dbModel.QuestionTypeId = command.QuestionTypeId;
                    dbModel.AnswerOption = command.AnswerOption;
                    dbModel.CreatedBy = command.CreatedBy;
                    dbModel.UpdatedBy = command.UpdatedBy;
                    dbModel.UpdatedOn = DateTime.UtcNow;
                    await _context.SaveChanges();
                }
                apiModel.Id = dbModel.Id;
                apiModel.QuestionId = dbModel.QuestionId;
                apiModel.QuestionTypeId = dbModel.QuestionTypeId;
                apiModel.AnswerOption = dbModel.AnswerOption;
                apiModel.CreatedBy = dbModel.CreatedBy;
                apiModel.CreatedOn = dbModel.CreatedOn;
                apiModel.UpdatedBy = dbModel.UpdatedBy;
                apiModel.UpdatedOn = dbModel.UpdatedOn;
                return apiModel;
            }
        }
    }
}
