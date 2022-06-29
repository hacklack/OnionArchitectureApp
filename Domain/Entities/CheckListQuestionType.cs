using System;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using static Domain.CommonCodes.CommonEnums;

namespace Domain.Entities
{
    public class CheckListQuestionType : BaseEntity
    {
        public CheckListQuestionType()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }

        public string QuestionTypeTitle { get; set; }
        public string QuestionTypeDescription { get; set; }
        public CheckListType CheckListTypeId { get; set; }
        public bool IsOptionsAllowed { get; set; }
        public int QuestionTypePirmeId { get; set; }


    }
}
