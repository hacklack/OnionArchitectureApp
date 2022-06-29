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
   public class GetAllCheckListQuestionGenericQuery : IRequest<ChecklistGenericApiModel>
    {
        public class GetAllCheckListQuestionGenericHandler : IRequestHandler<GetAllCheckListQuestionGenericQuery, ChecklistGenericApiModel>
        {
            private readonly IApplicationDbContext _context;
            public GetAllCheckListQuestionGenericHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<ChecklistGenericApiModel> Handle(GetAllCheckListQuestionGenericQuery query, CancellationToken cancellationToken)
            {
                ChecklistGenericApiModel apiModel = new ChecklistGenericApiModel();
                List<CheckListSubjectiveAnswerQuestionApiModel> apiSubjectiveListmodel = new List<CheckListSubjectiveAnswerQuestionApiModel>();
                
                apiModel.lstCheckListSubjectiveAnswerQuestionApiModel = await _context.checkListSubjectiveAnswerQuestion
                    .Select(x => new CheckListSubjectiveAnswerQuestionApiModel
                    {
                        CheckListTypeId=x.CheckListTypeId,
                        CheckListTypeChildId=x.CheckListTypeChildId,
                        QuestionTypeId=x.QuestionTypeId,
                        Id = x.Id,
                        QuestionTitle=x.QuestionTitle,
                        QuestionDescription=x.QuestionDescription,
                        CreatedBy=x.CreatedBy,
                        CreatedOn=x.CreatedOn,
                        UpdatedBy=x.UpdatedBy,
                        UpdatedOn=x.UpdatedOn,
                        lstCheckListQuestionOptionApiModel=_context.checkListSubjectiveAnswerQuestion
                        .Join(_context.checkListQuestionOption,cksub=>cksub.Id,ques=>ques.QuestionId,(cksub,ques)=>new { cksub,ques})
                        .Where(qo => qo.ques.QuestionId==x.Id && qo.ques.QuestionTypeId==x.QuestionTypeId)
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
