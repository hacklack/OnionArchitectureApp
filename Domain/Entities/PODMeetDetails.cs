using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Domain.Common;
using static Domain.CommonCodes.CommonEnums;


namespace Domain.Entities
{
    public class PODMeetDetails : BaseEntity
    {
        public PODMeetDetails()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }
        public string Title { get; set; }
        public string MeetDescription { get; set; }
        public DateTime MeetTiming { get; set; }
        public DateTime MeetDate { get; set; }
        public string MeetPlace { get; set; }
        public int County { get; set; }
        public bool IsChatAllowed { get; set; }

    }
}
