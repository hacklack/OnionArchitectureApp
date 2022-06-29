using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;
using static Domain.CommonCodes.CommonEnums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.ApiModels
{
    public class NotificationsHistoryListApiModel : BaseApiModel
    {
        public NotificationsHistoryListApiModel()
        {
            lstNotificationHistory = new List<NotificationsHistoryApiModel>();
        }
        public List<NotificationsHistoryApiModel> lstNotificationHistory { get; set; }
        public int Count { get; set; }
        public int UserId { get; set; }
        
    }
}
