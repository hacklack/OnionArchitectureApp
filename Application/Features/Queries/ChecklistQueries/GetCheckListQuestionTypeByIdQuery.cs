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
    public class GetCheckListQuestionTypeByIdQuery : IRequest<CheckListQuestionTypeApiModel>
    {
        public int Id { get; set; }
        public class GetBubbleMembersByBubbleIdHandler : IRequestHandler<GetCheckListQuestionTypeByIdQuery, CheckListQuestionTypeApiModel>
        {
            private readonly IApplicationDbContext _context;
            public GetBubbleMembersByBubbleIdHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<CheckListQuestionTypeApiModel> Handle(GetCheckListQuestionTypeByIdQuery query, CancellationToken cancellationToken)
            {

              var chkQuestionType =   await _context.checkListQuestionTypes
                    .Where(ck => ck.Id == query.Id)
                    .Select(x=>new CheckListQuestionTypeApiModel() {
                   Id=x.Id,
                   QuestionTypeTitle=x.QuestionTypeTitle,
                   QuestionTypeDescription=x.QuestionTypeDescription,
                   CheckListTypeId=x.CheckListTypeId
                   }).FirstOrDefaultAsync();

                if (chkQuestionType == null)
                {
                    return null;
                }
                return chkQuestionType;
            }
        }

    }
}
