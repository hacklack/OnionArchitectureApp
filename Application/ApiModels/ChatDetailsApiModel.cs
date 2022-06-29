using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;
using static Domain.CommonCodes.CommonEnums;

namespace Application.ApiModels
{
   public class ChatDetailsApiModel : BaseApiModel
    {
        public ChatDetailsApiModel()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
            lstChatHistory = new List<ChatHistoryApiModel>();
            lstChatMembers = new List<ChatMembersDataApiModel>();
            lstUserDetails = new List<UserApiModels>();
        }
        public int ChatTypeId { get; set; }
        public int ChatParentTypeId { get; set; }
        public int ChatParentId { get; set; }
        public List<ChatMembersDataApiModel> lstChatMembers { get; set; }
        public List<ChatHistoryApiModel> lstChatHistory { get; set; }
        public List<UserApiModels> lstUserDetails { get; set; }
    }
}
