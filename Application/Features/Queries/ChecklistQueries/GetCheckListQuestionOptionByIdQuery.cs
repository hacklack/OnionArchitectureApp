using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System;
using System.Collections.Generic;
using Application.ApiModels;

namespace Application.Features.Queries.ChecklistQueries
{
    public class GetCheckListQuestionOptionByIdQuery : IRequest<CheckListQuestionOptionApiModel>
    {
        public int Id { get; set; }
        public class GetCheckListQuestionOptionByIdHandler : IRequestHandler<GetCheckListQuestionOptionByIdQuery, CheckListQuestionOptionApiModel>
        {
            private readonly IApplicationDbContext _context;
            public GetCheckListQuestionOptionByIdHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<CheckListQuestionOptionApiModel> Handle(GetCheckListQuestionOptionByIdQuery query, CancellationToken cancellationToken)
            {

              var chkQuestionOption =   await _context.checkListQuestionOption
                    .Where(ck => ck.Id == query.Id)
                    .Select(x=>new CheckListQuestionOptionApiModel() {
                   Id=x.Id,
                   AnswerOption=x.AnswerOption,
                   QuestionId=x.QuestionId,
                   QuestionTypeId=x.QuestionTypeId,
                   }).FirstOrDefaultAsync();

                if (chkQuestionOption == null)
                {
                    return null;
                }
                return chkQuestionOption;
            }
        }

    }
}
