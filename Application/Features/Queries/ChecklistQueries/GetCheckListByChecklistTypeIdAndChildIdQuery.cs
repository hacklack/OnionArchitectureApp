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
using static Domain.CommonCodes.CommonEnums;
using Application.Services;

namespace Application.Features.Queries.ChecklistQueries
{
    public class GetCheckListByChecklistTypeIdAndChildIdQuery : IRequest<ChecklistGenericApiModel>
    {
        public int ChecklistTypeId  { get; set; }
        public int CheckListTypeChildId { get; set; }
        public int UserId { get; set; }
        public class GetCheckListByChecklistTypeIdAndChildIdHandler : IRequestHandler<GetCheckListByChecklistTypeIdAndChildIdQuery, ChecklistGenericApiModel>
        {

            private readonly IApplicationDbContext _context;
            public GetCheckListByChecklistTypeIdAndChildIdHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<ChecklistGenericApiModel> Handle(GetCheckListByChecklistTypeIdAndChildIdQuery query, CancellationToken cancellationToken)
            {   
                bool isAdmin = false;
                if (query.ChecklistTypeId == (int)CheckListType.BubbleCheckList)
                {
                    isAdmin = _context.bubbleMembers.Where(y => y.BubbleId == query.CheckListTypeChildId && y.UserId == query.UserId).Select(x => x.IsBubbleAdmin).FirstOrDefault();
                }
                else if (query.ChecklistTypeId == (int)CheckListType.BubbleMeetChecklist)
                {
                    isAdmin = _context.bubbleMeetMemberPermissions.Where(y => y.BubbleMeetId == query.CheckListTypeChildId && y.UserId == query.UserId && y.UserPermissionTypeId==UserPermission.IsAdmin && y.MeetTypeId==MeetType.BubbleMeet).Select(x => x.UserPermissionStatus).FirstOrDefault();
                }
                else if (query.ChecklistTypeId == (int)CheckListType.PODCheckList)  
                {
                    isAdmin = (_context.podDetails.Where(y => y.Id == query.CheckListTypeChildId && y.CreatedBy == query.UserId).Count()>0)?true:false;
                }
                else if (query.ChecklistTypeId == (int)CheckListType.PODMeetChecklist)
                {
                    isAdmin = _context.bubbleMeetMemberPermissions.Where(y => y.BubbleMeetId == query.CheckListTypeChildId && y.UserId == query.UserId && y.UserPermissionTypeId == UserPermission.IsAdmin && y.MeetTypeId == MeetType.PODMeet).Select(x => x.UserPermissionStatus).FirstOrDefault();
                }
                ChecklistGenericApiModel apiModel = new ChecklistGenericApiModel();
                List<CheckListSubjectiveAnswerQuestionApiModel> apiSubjectiveListmodel = new List<CheckListSubjectiveAnswerQuestionApiModel>();

                var chkDetails = _context.checkListDetails.Where(y => y.CheckListTypeId == (CheckListType)query.ChecklistTypeId && y.CheckListTypeChildId==query.CheckListTypeChildId).FirstOrDefault();
                apiModel.ChecklistId = chkDetails.Id;
                apiModel.ChecklistName = chkDetails.ChecklistName;
                apiModel.CheckListTypeChildId = chkDetails.CheckListTypeChildId;
                apiModel.CheckListTypeId = chkDetails.CheckListTypeId;  
                apiModel.CreatedBy = chkDetails.CreatedBy;
                apiModel.CreatedOn = chkDetails.CreatedOn;
                apiModel.IsAdmin = isAdmin;
                apiModel.IsChecklistAnswered = (_context.checkListSubjectiveQuestion_Answers.Where(ca => ca.CheckListTypeChildId == query.CheckListTypeChildId && ca.UserId == query.UserId).Count() > 0) ? true : false;
                apiModel.lstCheckListSubjectiveAnswerQuestionApiModel = await _context.checkListSubjectiveAnswerQuestion
                    .Where(y => y.CheckListTypeChildId == query.CheckListTypeChildId && y.ChecklistId == chkDetails.Id)
                    .Select(x => new CheckListSubjectiveAnswerQuestionApiModel
                    {
                        CheckListTypeId = x.CheckListTypeId,
                        CheckListTypeChildId = x.CheckListTypeChildId,
                        QuestionTypeId = x.QuestionTypeId,
                        ChecklistId = chkDetails.Id,
                        Id = x.Id,
                        QuestionTitle = x.QuestionTitle,
                        QuestionDescription = x.QuestionDescription,
                        CreatedBy = x.CreatedBy,
                        CreatedOn = x.CreatedOn,
                        UpdatedBy = x.UpdatedBy,
                        UpdatedOn = x.UpdatedOn,
                        lstCheckListQuestionOptionApiModel = _context.checkListSubjectiveAnswerQuestion
                        .Join(_context.checkListQuestionOption, cksub => cksub.Id, ques => ques.QuestionId, (cksub, ques) => new { cksub, ques })
                        .Where(qo => qo.ques.QuestionId == x.Id && qo.ques.QuestionTypeId == x.QuestionTypeId && qo.cksub.ChecklistId == chkDetails.Id)
                        .Select(y => new CheckListQuestionOptionApiModel
                        {
                            QuestionTypeId = y.ques.QuestionTypeId,
                            QuestionId = y.ques.QuestionId,
                            Id = y.ques.Id,
                            AnswerOption = y.ques.AnswerOption,
                            
                        }).ToList(),
                        lstcheckListSubjectiveQuestion_AnswersApiModel = _context.checkListSubjectiveQuestion_Answers
                                                    .Where(y => y.CheckListQuestionId == x.Id && y.UserId==query.UserId)
                                                    .Select(ca => new CheckListSubjectiveQuestion_AnswersApiModel {
                                                    Id=ca.Id,
                                                    UserId=ca.UserId,
                                                    GenericAnswer=(ca.SingleAnswer == CommonStaticStrings.SingleAnswerDefault)
                                                    ?_context.checkListQuestionOption.Where(y=>y.Id== ca.AnswerOptionId).Select(yo=>yo.AnswerOption).FirstOrDefault()
                                                    :ca.SingleAnswer,
                                                    AnswerOptionId=ca.AnswerOptionId,
                                                    SingleAnswer=ca.SingleAnswer
                                                    
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
