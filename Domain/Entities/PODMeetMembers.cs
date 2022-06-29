using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;
using static Domain.CommonCodes.CommonEnums;

namespace Domain.Entities
{
    public class PODMeetMembers : BaseEntity
    {
        public PODMeetMembers()
        {
            UpdatedOn = DateTime.UtcNow;
            CreatedOn = DateTime.UtcNow;
        }
        public int PODMeetId { get; set; }
        public int BubbleId { get; set; }
        public int UserId { get; set; }
        public int PODId { get; set; }

        [ForeignKey("UserId")]
        public UserDetails UserDetails { get; set; }

        [ForeignKey("PODMeetId")]
        public PODMeetDetails PODMeetDetails { get; set; }

        [ForeignKey("BubbleId")]
        public BubbleDetails BubbleDetails { get; set; }
        [ForeignKey("PODId")]
        public PodDetails PODDetails { get; set; }
    }
}
