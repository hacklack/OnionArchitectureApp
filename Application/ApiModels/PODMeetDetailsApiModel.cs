using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Application.ApiModels;
using static Domain.CommonCodes.CommonEnums;


namespace Domain.Entities
{
    public class PODMeetDetailsApiModel : BaseApiModel
    {
        public PODMeetDetailsApiModel()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
            lstPods = new List<PodDetailsApiModel>();
            lstbubbles = new List<BubbleApiModel>();
            countyDetails = new CountiesApiModel();
        }
        public string Title { get; set; }
        public string MeetDescription { get; set; }
        public DateTime MeetTiming { get; set; }
        public DateTime MeetDate { get; set; }
        public string MeetPlace { get; set; }
        public int County { get; set; }
        public string CountyName { get; set; }
        public bool IsChatAllowed { get; set; }
        public List<PodDetailsApiModel> lstPods { get; set; }
        public List<BubbleApiModel> lstbubbles { get; set; }
        public bool IsAdmin { get; set; }
        public CountiesApiModel countyDetails { get; set; }

    }
}
