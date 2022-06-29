using System;
using Domain.Common;
using static Domain.CommonCodes.CommonEnums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
   public class CheckListQuestionOption : BaseEntity
    {
        public CheckListQuestionOption()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }
        public string AnswerOption { get; set; }
        public int QuestionId { get; set; }
        public int QuestionTypeId { get; set; }

        
        [ForeignKey("QuestionTypeId")]
        public CheckListQuestionType CheckListQuestionType { get; set; }
        

    }
}
