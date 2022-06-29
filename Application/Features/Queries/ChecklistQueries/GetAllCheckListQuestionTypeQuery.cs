using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Queries.ChecklistQueries
{
   public class GetAllCheckListQuestionTypeQuery : IRequest<IEnumerable<CheckListQuestionType>>
    {
       public int? ChecklistTypeId { get; set; }
        public class GetAllCheckListQuestionTypeHandler : IRequestHandler<GetAllCheckListQuestionTypeQuery, IEnumerable<CheckListQuestionType>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllCheckListQuestionTypeHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<CheckListQuestionType>> Handle(GetAllCheckListQuestionTypeQuery query, CancellationToken cancellationToken)
            {
                var chkQuestionTypeList = await _context.checkListQuestionTypes.Where(x=>(!string.IsNullOrEmpty(Convert.ToString(query.ChecklistTypeId)))?(int)x.CheckListTypeId==(int)query.ChecklistTypeId:x.CheckListTypeId>0).ToListAsync();
                if (chkQuestionTypeList == null)
                {
                    return null;
                }
                return chkQuestionTypeList.AsReadOnly();
            }
        }
    }
}
