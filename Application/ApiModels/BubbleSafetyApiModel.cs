using System;
using System.Collections.Generic;
using System.Text;
using static Domain.CommonCodes.CommonEnums;

namespace Application.ApiModels
{
   public class BubbleSafetyApiModel : BaseApiModel
    {
        public BubbleSafetyApiModel()
        {
        }
        public int BubbleCounty { get; set; }
        public int BubbleId { get; set; }
    }
}
