using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;
using static Domain.CommonCodes.CommonEnums;

namespace Domain.Entities
{
    public class BubbleMeetMemberPermissions : BaseEntity
    {
        public BubbleMeetMemberPermissions()
        {
            UpdatedOn = DateTime.UtcNow;
            CreatedOn = DateTime.UtcNow;
        }
        public int BubbleMeetId { get; set; }
        public int UserId { get; set; }
        public UserPermission UserPermissionTypeId { get; set; }
        public MeetType MeetTypeId { get; set; }
        public bool UserPermissionStatus { get; set; }

        [ForeignKey("UserId")]
        public UserDetails UserDetails { get; set; }

    }
}
