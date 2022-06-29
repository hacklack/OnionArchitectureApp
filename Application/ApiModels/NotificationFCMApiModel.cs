using FirebaseAdmin.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using static Domain.CommonCodes.CommonEnums;

namespace Application.ApiModels
{
   public class NotificationFCMApiModel : BaseApiModel
    {
        public NotificationFCMApiModel()
        {
            NotificationBody = new Notification();
            NotificationData = new Dictionary<string, string>();
            FcmMessageBody = new Message();
        }
        public Message FcmMessageBody { get; set; }
        public Notification NotificationBody { get; set; }
        public Dictionary<string, string> NotificationData { get; set; }
        public string DeviceToken { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public NotificationTypes NotificationTypeId { get; set; }
    }
}
