using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Domain.Common;
using static Domain.CommonCodes.CommonEnums;

namespace Application.ApiModels
{
    public class ChatMembersDataApiModel : BaseApiModel
    {
        public ChatMembersDataApiModel()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }
        public int ChatId { get; set; }
        public int ChatMemberId { get; set; }
        public bool ChatMemberStatus { get; set; }
        public string UserName { get; set; }
        public ChatDetailsApiModel ChatDetails { get; set; }
        public UserApiModels UserDetails { get; set; }
    }
}
