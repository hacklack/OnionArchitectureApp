using System;
using System.Collections.Generic;
using System.Text;
using static Domain.CommonCodes.CommonEnums;

namespace Application.ApiModels
{
   public class BubbleApiModel:BaseApiModel
    {
        public BubbleApiModel()
        {
            lstPodUser = new List<UserApiModels>();
            bubbleSafetyDetails = new BubbleSafetyDetailsApiModel();
        }
        public string BubbleName { get; set; }
        public string BubbleSize { get; set; }
        public string BubbleDescription { get; set; }
        public string BubbleZipCode { get; set; }
        public BubbleType BubbleType { get; set; }
        public DateTime BubbleValidity { get; set; }
        public string message { get; set; }
        public bool IsOtherCountyMemberAllowed { get; set; }
        public List<UserApiModels> lstPodUser { get; set; }
        public int BubbleCounty { get; set; }
        public string BubbleCountyName { get; set; }
        public bool IsAdmin { get; set; }
        public BubbleSafetyDetailsApiModel bubbleSafetyDetails { get; set; }
    }
}
