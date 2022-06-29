using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;
using static Domain.CommonCodes.CommonEnums;

namespace Domain.Entities
{
    public class UserDeviceDetails : BaseEntity
    {
        public UserDeviceDetails()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }
        public string DeviceToken { get; set; }
        public DeviceType DeviceTypeId { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public UserDetails UserDetails { get; set; }
    }
}
