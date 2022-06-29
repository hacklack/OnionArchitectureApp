using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Domain.Common;
using static Domain.CommonCodes.CommonEnums;

namespace Domain.Entities
{
    public class ChatMembers : BaseEntity
    {
        public ChatMembers()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }
        public int ChatId { get; set; }
        public int ChatMemberId { get; set; }
        public bool ChatMemberStatus { get; set; }


        [ForeignKey("ChatId")]
        public ChatDetails ChatDetails { get; set; }

        [ForeignKey("ChatMemberId")]
        public UserDetails UserDetails { get; set; }
    }
}
