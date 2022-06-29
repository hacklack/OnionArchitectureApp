using System;
using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;
using static Domain.CommonCodes.CommonEnums;

namespace Domain.Entities
{
    public class CheckListDetails : BaseEntity
    {
        public CheckListDetails()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }
        public CheckListType CheckListTypeId { get; set; }
        public int CheckListTypeChildId { get; set; }
        public string ChecklistName { get; set; }
        
    }
}
