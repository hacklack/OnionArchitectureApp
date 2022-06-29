using System;
using System.Collections.Generic;
using System.Text;
using static Domain.CommonCodes.CommonEnums;

namespace Application.ApiModels
{
    public class CheckListQuestionTypeApiModel : BaseApiModel
    {
        public CheckListQuestionTypeApiModel()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }

        public string QuestionTypeTitle { get; set; }
        public string QuestionTypeDescription { get; set; }
        public CheckListType CheckListTypeId { get; set; }
        
    }
}
