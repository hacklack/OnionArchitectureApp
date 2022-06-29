using System;
using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;
using static Domain.CommonCodes.CommonEnums;

namespace Domain.Entities
{
    public class CheckListSubjectiveAnswerQuestion : BaseEntity
    {
        public CheckListSubjectiveAnswerQuestion()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }
        public CheckListType CheckListTypeId { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionDescription { get; set; }
        public int CheckListTypeChildId { get; set; }
        public int QuestionTypeId { get; set; }
        public int ChecklistId { get; set; }
        [ForeignKey("QuestionTypeId")]
        public CheckListQuestionType CheckListQuestionType { get; set; }
        [ForeignKey("ChecklistId")]
        public CheckListDetails CheckListDetails { get; set; }
      
    }
}
