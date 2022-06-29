using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;
using static Domain.CommonCodes.CommonEnums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.ApiModels
{
   public class ChatHistoryApiModel : BaseApiModel
    {
        public ChatHistoryApiModel()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }
        public int ChatId { get; set; }
        public int ChatMessageSenderId { get; set; }
        public string ChatMessage { get; set; }
        public string CreatedOnString { get; set; }
        public string UpdatedOnString { get; set; }

        // public ChatDetailsApiModel ChatDetails { get; set; }
        // public UserDeviceDetailsApiModel UserDetails { get; set; }
    }
}
