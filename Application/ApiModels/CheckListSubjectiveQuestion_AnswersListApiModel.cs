using System;
using Domain.Common;
using static Domain.CommonCodes.CommonEnums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Application.ApiModels
{
    public class CheckListSubjectiveQuestion_AnswersListApiModel 
    {
        public CheckListSubjectiveQuestion_AnswersListApiModel()
        {
            lstCheckListSubjectiveQuestion_AnswersApiModel = new List<CheckListSubjectiveQuestion_AnswersApiModel>();
        }
        public CheckListType CheckListTypeId { get; set; }
        public int CheckListTypeChildId { get; set; }
        public int CheckListId { get; set; }
        public List<CheckListSubjectiveQuestion_AnswersApiModel> lstCheckListSubjectiveQuestion_AnswersApiModel { get; set; }

    }
}
