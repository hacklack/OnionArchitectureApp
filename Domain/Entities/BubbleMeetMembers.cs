using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;
using static Domain.CommonCodes.CommonEnums;

namespace Domain.Entities
{
    public class BubbleMeetMembers : BaseEntity
    {
        public BubbleMeetMembers()
        {
            UpdatedOn = DateTime.UtcNow;
            CreatedOn = DateTime.UtcNow;
        }
        public int BubbleMeetId { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public UserDetails UserDetails { get; set; }
        
        [ForeignKey("BubbleMeetId")]
        public BubbleMeetDetails BubbleMeetDetails { get; set; }
    }
}
