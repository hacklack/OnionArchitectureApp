using System;
using Domain.Common;
using static Domain.CommonCodes.CommonEnums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class CheckListSubjectiveQuestion_Answers : BaseEntity
    {
        public CheckListSubjectiveQuestion_Answers()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
            AnswerOptionId = 0;
            CheckListTypeChildId = 0;
            ChecklistId = 0;
            CheckListQuestionId = 0;
            UserId = 0;
        }
        public int AnswerOptionId { get; set; }
        public string SingleAnswer { get; set; }
        public int CheckListTypeChildId { get; set; }
        public int ChecklistId { get; set; }
        public int CheckListQuestionId { get; set; }
        public int UserId { get; set; }

        [ForeignKey("CheckListQuestionId")]
        public CheckListSubjectiveAnswerQuestion CheckListSubjectiveAnswerQuestion { get; set; }
        [ForeignKey("UserId")]

        public UserDetails UserDetails { get; set; }

    }
}
