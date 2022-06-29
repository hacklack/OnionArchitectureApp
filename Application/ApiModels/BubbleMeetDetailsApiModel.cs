using System;
using System.Collections.Generic;
using System.Text;
using static Domain.CommonCodes.CommonEnums;

namespace Application.ApiModels
{
    public class BubbleMeetDetailsApiModel : BaseApiModel
    {
        public BubbleMeetDetailsApiModel()
        {
            lstUsers = new List<UserApiModels>();
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
        public List<UserApiModels> lstUsers { get; set; }
        public bool IsAdmin { get; set; }
        public CountiesApiModel countyDetails { get; set; }

    }
}
