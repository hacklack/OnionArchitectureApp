using System;
using Domain.Common;
using static Domain.CommonCodes.CommonEnums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.ApiModels
{
   public class CheckListQuestionOptionApiModel : BaseApiModel
    {
        public CheckListQuestionOptionApiModel()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }
        public string AnswerOption { get; set; }
        public int QuestionId { get; set; }
        public int QuestionTypeId { get; set; }

    }
}
