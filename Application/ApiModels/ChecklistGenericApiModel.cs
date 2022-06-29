using System;
using System.Collections.Generic;
using System.Text;
using static Domain.CommonCodes.CommonEnums;

namespace Application.ApiModels
{
  public class ChecklistGenericApiModel 
    {
        public ChecklistGenericApiModel()
        {
            lstCheckListSubjectiveAnswerQuestionApiModel = new List<CheckListSubjectiveAnswerQuestionApiModel>();
          //  lstCheckListSubjectiveQuestion_AnswersApiModel = new List<CheckListSubjectiveQuestion_AnswersApiModel>();
        }
        public CheckListType CheckListTypeId { get; set; }
        public int CheckListTypeChildId { get; set; }
        public int ChecklistId { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public string ChecklistName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsChecklistAnswered { get; set; }
        public List<CheckListSubjectiveAnswerQuestionApiModel> lstCheckListSubjectiveAnswerQuestionApiModel { get; set; }
       // public List<CheckListSubjectiveQuestion_AnswersApiModel> lstCheckListSubjectiveQuestion_AnswersApiModel { get; set; }

    }
}
