using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ApiModels
{
  public  class PodDetailsApiModel:BaseApiModel
    {
        public PodDetailsApiModel()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
            lstPodBubbleApiModel = new List<BubbleApiModel>();
            podSafetyDetails = new BubbleSafetyDetailsApiModel();
        }

        public string PODName { get; set; }
        public int PODBubbleType { get; set; }
        public int PODSize { get; set; }
        public string PODDescription { get; set; }
        public bool IsAdmin { get; set; }
        public List<BubbleApiModel> lstPodBubbleApiModel { get; set; }
        public BubbleSafetyDetailsApiModel podSafetyDetails { get; set; }
    }
}
