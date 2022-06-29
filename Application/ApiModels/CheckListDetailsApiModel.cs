using System;
using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;
using static Domain.CommonCodes.CommonEnums;

namespace Application.ApiModels
{
    public class CheckListDetailsApiModel : BaseApiModel
    {
        public CheckListDetailsApiModel()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }
        public CheckListType CheckListTypeId { get; set; }
        public int CheckListTypeChildId { get; set; }
        public string ChecklistName { get; set; }
        
    }
}
