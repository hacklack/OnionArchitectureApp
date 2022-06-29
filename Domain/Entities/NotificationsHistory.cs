using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;
using static Domain.CommonCodes.CommonEnums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class NotificationsHistory : BaseEntity
    {
        public NotificationsHistory()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public int UserDeviceId { get; set; }
        public int PODBubbleId { get; set; }
        public string NotificationUserTitle { get; set; }
        public string NotificationUserDescription { get; set; }
        public NotificationTypeChild NotificationTypeChild { get; set; }
        public NotificationCategories NotificationCategoryId { get; set; }

        [ForeignKey("UserId")]
        public UserDetails UserDetails { get; set; }

        [ForeignKey("NotificationId")]
        public Notifications Notifications { get; set; }


    }
}
