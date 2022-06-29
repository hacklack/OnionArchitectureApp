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
    public class CreateUpdateChecklistQuestionType : IRequest<CheckListQuestionTypeApiModel>
    {

        public int Id { get; set; }
        public string QuestionTypeTitle { get; set; }
        public string QuestionTypeDescription { get; set; }
        public CheckListType CheckListTypeId { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public class CreateUpdateChecklistQuestionTypeCommandHandler : IRequestHandler<CreateUpdateChecklistQuestionType, CheckListQuestionTypeApiModel>
        {
            private readonly IApplicationDbContext _context;
            public CreateUpdateChecklistQuestionTypeCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<CheckListQuestionTypeApiModel> Handle(CreateUpdateChecklistQuestionType command, CancellationToken cancellationToken)
            {
                CheckListQuestionType dbModel = new CheckListQuestionType();
                CheckListQuestionTypeApiModel apiModel = new CheckListQuestionTypeApiModel();
                var chkTypeList = _context.checkListQuestionTypes.Select(ck => ck.Id).ToList();

                if (string.IsNullOrEmpty(Convert.ToString(command.Id)) || command.Id == 0)
                {
                    dbModel.QuestionTypeTitle = command.QuestionTypeTitle;
                    dbModel.QuestionTypeDescription = command.QuestionTypeDescription;
                    dbModel.CheckListTypeId = command.CheckListTypeId;
                    dbModel.CreatedBy = command.CreatedBy;
                    dbModel.UpdatedBy = command.UpdatedBy;
                    _context.checkListQuestionTypes.Add(dbModel);
                    await _context.SaveChanges();
                }
                else
                {
                    dbModel = _context.checkListQuestionTypes.Where(x => x.Id == command.Id).FirstOrDefault();
                    dbModel.QuestionTypeTitle = command.QuestionTypeTitle;
                    dbModel.QuestionTypeDescription = command.QuestionTypeDescription;
                    dbModel.CheckListTypeId = command.CheckListTypeId;
                    dbModel.CreatedBy = command.CreatedBy;
                    dbModel.UpdatedBy = command.UpdatedBy;
                    dbModel.UpdatedOn = DateTime.UtcNow;
                    await _context.SaveChanges();
                }
                apiModel.Id = dbModel.Id;
                apiModel.QuestionTypeTitle = dbModel.QuestionTypeTitle;
                apiModel.QuestionTypeDescription = dbModel.QuestionTypeDescription;
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
