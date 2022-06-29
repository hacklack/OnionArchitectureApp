using System;
using System.Collections.Generic;
using System.Text;
using static Domain.CommonCodes.CommonEnums;

namespace Application.ApiModels
{
    public class PODApiModel:BaseApiModel
    {
        public PODApiModel()
        {
            lstBubbleApiModel = new List<BubbleApiModel>();
            lstUser = new List<UserApiModels>();
            PODBubbleType = new BubbleType();
            PodDetailsApiModel = new PodDetailsApiModel();
            lstPodDetailsApiModel = new List<PodDetailsApiModel>();
        }

        public PodDetailsApiModel PodDetailsApiModel { get; set; }
        public BubbleType PODBubbleType { get; set; }
        public List<PodDetailsApiModel> lstPodDetailsApiModel { get; set; }
        public List<BubbleApiModel> lstBubbleApiModel { get; set; }
        public List<UserApiModels> lstUser { get; set; }

    }

}

