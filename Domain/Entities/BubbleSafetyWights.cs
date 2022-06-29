using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;
using static Domain.CommonCodes.CommonEnums;

namespace Domain.Entities
{
   public class BubbleSafetyWights : BaseEntity
    {
        public BubbleSafetyWights()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }
        public BubbleSaftyFieldType BubbleWightFiledTypeId { get; set; }
        public double BubbleWightValue { get; set; }
    }
}
