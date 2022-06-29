using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.ApiModels;

namespace Application.Features.Queries.ChecklistQueries
{
    public class GetCheckListByChecklistIdQuery : IRequest<ChecklistGenericApiModel>
    {
        public int ChecklistId { get; set; }
        public class GetCheckListByChecklistIdHandler : IRequestHandler<GetCheckListByChecklistIdQuery, ChecklistGenericApiModel>
        {

            private readonly IApplicationDbContext _context;
            public GetCheckListByChecklistIdHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<ChecklistGenericApiModel> Handle(GetCheckListByChecklistIdQuery query, CancellationToken cancellationToken)
            {
                ChecklistGenericApiModel apiModel = new ChecklistGenericApiModel();
                List<CheckListSubjectiveAnswerQuestionApiModel> apiSubjectiveListmodel = new List<CheckListSubjectiveAnswerQuestionApiModel>();

               var chkDetails = _context.checkListDetails.Where(y => y.Id == query.ChecklistId).FirstOrDefault();
                apiModel.ChecklistId = chkDetails.Id;
                apiModel.ChecklistName = chkDetails.ChecklistName;
                apiModel.CheckListTypeChildId = chkDetails.CheckListTypeChildId;
                apiModel.CheckListTypeId = chkDetails.CheckListTypeId;
                apiModel.CreatedBy = chkDetails.CreatedBy;
                apiModel.CreatedOn = chkDetails.CreatedOn;

                apiModel.lstCheckListSubjectiveAnswerQuestionApiModel = await _context.checkListSubjectiveAnswerQuestion
                    .Select(x => new CheckListSubjectiveAnswerQuestionApiModel
                    {
                        CheckListTypeId = x.CheckListTypeId,
                        CheckListTypeChildId = x.CheckListTypeChildId,
                        QuestionTypeId = x.QuestionTypeId,
                        Id = x.Id,
                        QuestionTitle = x.QuestionTitle,
                        QuestionDescription = x.QuestionDescription,
                        CreatedBy = x.CreatedBy,
                        CreatedOn = x.CreatedOn,
                        UpdatedBy = x.UpdatedBy,
                        UpdatedOn = x.UpdatedOn,
                        lstCheckListQuestionOptionApiModel = _context.checkListSubjectiveAnswerQuestion
                        .Join(_context.checkListQuestionOption, cksub => cksub.Id, ques => ques.QuestionId, (cksub, ques) => new { cksub, ques })
                        .Where(qo => qo.ques.QuestionId == x.Id && qo.ques.QuestionTypeId == x.QuestionTypeId && qo.cksub.ChecklistId==query.ChecklistId)
                        .Select(y => new CheckListQuestionOptionApiModel
                        {
                            QuestionTypeId = y.ques.QuestionTypeId,
                            QuestionId = y.ques.QuestionId,
                            Id = y.ques.Id,
                            AnswerOption = y.ques.AnswerOption
                        }).ToList()

                    })
                    .ToListAsync();
                if (apiModel == null)
                {
                    return null;
                }
                return apiModel;
            }
        }
    }
}
