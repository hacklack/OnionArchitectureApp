using System;
using System.Collections.Generic;
using System.Text;
using static Domain.CommonCodes.CommonEnums;

namespace Application.ApiModels
{
    public class BubbleMeetPermissionsApiModel : BaseApiModel
    {
        public BubbleMeetPermissionsApiModel()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
            LstBubbleIds = new List<int>();
        }
        public int PermissionParenttId { get; set; }
        public List<int> LstBubbleIds { get; set; }
        public int UserId { get; set; }
        public UserPermission UserPermissionTypeId { get; set; }
        public MeetType MeetTypeId { get; set; }
        public bool UserPermissionStatus { get; set; }
    }
}
