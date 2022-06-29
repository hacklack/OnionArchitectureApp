using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;
using static Domain.CommonCodes.CommonEnums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.ApiModels
{
    public class NotificationsHistoryApiModel : BaseApiModel
    {
        public NotificationsHistoryApiModel()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public int UserDeviceId { get; set; }
        public int PODBubbleId { get; set; }
        public string NotificationTitle { get; set; }
        public string NotificationUserTitle { get; set; }
        public string NotificationDescription { get; set; }
        public string NotificationUserDescription { get; set; }
        public NotificationTypes NotificationTypeId { get; set; }
        public NotificationTypeChild NotificationTypeChild { get; set; }
        public NotificationCategories NotificationCategoryId { get; set; }


    }
}
