using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Domain.Common;
using static Domain.CommonCodes.CommonEnums;

namespace Domain.Entities
{
    public class BubbleSafetyDetails : BaseEntity
    {
        public BubbleSafetyDetails()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }
        public BubbleSaftyType BubbleSaftyTypeId { get; set; }
        public int BubblePODId { get; set; }
        public double BubbleSaftyValue { get; set; }
      
    }
}
