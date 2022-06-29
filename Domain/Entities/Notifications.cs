using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;
using static Domain.CommonCodes.CommonEnums;

namespace Domain.Entities
{
    public class Notifications : BaseEntity
    {
        public Notifications()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }
        public NotificationTypes NotificationTypeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string NotificationImage { get; set; }
    }
}
