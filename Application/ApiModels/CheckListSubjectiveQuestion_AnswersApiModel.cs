using System;
using Domain.Common;
using static Domain.CommonCodes.CommonEnums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.ApiModels
{
    public class CheckListSubjectiveQuestion_AnswersApiModel : BaseApiModel
    {
        public CheckListSubjectiveQuestion_AnswersApiModel()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
            AnswerOptionId = 0;
        }
        public int AnswerOptionId { get; set; }
        public string SingleAnswer { get; set; }
        public int CheckListTypeChildId { get; set; }
        public int ChecklistId { get; set; }
        public int CheckListQuestionId { get; set; }
        public int UserId { get; set; }
        public string GenericAnswer { get; set; }
    }
}
