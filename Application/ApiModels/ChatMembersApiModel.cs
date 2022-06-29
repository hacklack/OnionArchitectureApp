using System;
using System.Collections.Generic;
using System.Text;
using static Domain.CommonCodes.CommonEnums;

namespace Application.ApiModels
{
    public class ChatMembersApiModel : BaseApiModel
    {
        public ChatMembersApiModel()
        {
            UserList = new List<UserApiModels>();
        }
        public List<UserApiModels> UserList { get; set; }
    }
}
