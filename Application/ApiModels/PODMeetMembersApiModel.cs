using System;
using System.Collections.Generic;
using System.Text;
using Application.ApiModels;
using System.ComponentModel.DataAnnotations.Schema;
using static Domain.CommonCodes.CommonEnums;

namespace Domain.Entities
{
    public class PODMeetMembersApiModel : BaseApiModel
    {
        public PODMeetMembersApiModel()
        {
            UpdatedOn = DateTime.UtcNow;
            CreatedOn = DateTime.UtcNow;
        }
        public int PODMeetId { get; set; }
        public int BubbleId { get; set; }
        public int UserId { get; set; }
        public int PODId { get; set; }

        public BubbleApiModel Bubble { get; set; }
        public UserApiModels User { get; set; }
        public PODMeetDetailsApiModel PODMeetDetails { get; set; }
    }
}
