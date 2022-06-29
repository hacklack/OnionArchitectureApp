using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;
using static Domain.CommonCodes.CommonEnums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
   public class ChatHistory : BaseEntity
    {
        public ChatHistory()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }
        public int ChatId { get; set; }
        public int ChatMessageSenderId { get; set; }
        public string ChatMessage { get; set; }

        [ForeignKey("ChatId")]
        public ChatDetails ChatDetails { get; set; }
        [ForeignKey("ChatMessageSenderId")]
        public UserDetails UserDetails { get; set; }
    }
}
