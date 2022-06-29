using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;
using static Domain.CommonCodes.CommonEnums;

namespace Domain.Entities
{
   public class BubbleDetails :BaseEntity
    {
        public BubbleDetails()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }
        public string BubbleName { get; set; }
        public string BubbleSize { get; set; }
        public string BubbleDescription { get; set; }
        public string BubbleZipCode { get; set; }
        public BubbleType BubbleType { get; set; }
        public DateTime BubbleValidity { get; set; }
        public bool IsOtherCountyMemberAllowed { get; set; }
    }
}
