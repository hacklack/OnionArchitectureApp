using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Domain.Common;
using static Domain.CommonCodes.CommonEnums;

namespace Application.ApiModels
{
    public class BubbleSafetyDetailsApiModel : BaseApiModel
    {
        public BubbleSafetyDetailsApiModel()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }
        public BubbleSaftyType BubbleSaftyTypeId { get; set; }
        public int BubblePODId { get; set; }
        public double BubbleSaftyValue { get; set; }
      
    }
}
