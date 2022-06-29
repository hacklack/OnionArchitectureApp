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
   public class GetAllCheckListQuestionOptionQuery : IRequest<IEnumerable<CheckListQuestionOption>>
    {
        public class GetAllCheckListQuestionOptionHandler : IRequestHandler<GetAllCheckListQuestionOptionQuery, IEnumerable<CheckListQuestionOption>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllCheckListQuestionOptionHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<CheckListQuestionOption>> Handle(GetAllCheckListQuestionOptionQuery query, CancellationToken cancellationToken)
            {
                var chkQuestionOption = await _context.checkListQuestionOption.ToListAsync();
                if (chkQuestionOption == null)
                {
                    return null;
                }
                return chkQuestionOption.AsReadOnly();
            }
        }
    }
}
