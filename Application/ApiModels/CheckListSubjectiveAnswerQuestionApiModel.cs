using System;
using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;
using static Domain.CommonCodes.CommonEnums;
using System.Collections.Generic;

namespace Application.ApiModels
{
    public class CheckListSubjectiveAnswerQuestionApiModel : BaseApiModel
    {
        public CheckListSubjectiveAnswerQuestionApiModel()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
            lstCheckListQuestionOptionApiModel = new List<CheckListQuestionOptionApiModel>();
            lstcheckListSubjectiveQuestion_AnswersApiModel = new List<CheckListSubjectiveQuestion_AnswersApiModel>();
        }
        public CheckListType CheckListTypeId { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionDescription { get; set; }
        public int CheckListTypeChildId { get; set; }
        public int QuestionTypeId { get; set; }
        public int ChecklistId { get; set; }
        public List<CheckListQuestionOptionApiModel> lstCheckListQuestionOptionApiModel { get; set; }
        public List<CheckListSubjectiveQuestion_AnswersApiModel> lstcheckListSubjectiveQuestion_AnswersApiModel { get; set; }

    }
}
