using System;
using System.Collections.Generic;
using System.Text;
using static Twilio.Rest.Verify.V2.Service.VerificationResource;

namespace Application.ApiModels
{
    public class OtpVerificationModel
    {
        public OtpVerificationModel()
        {
            send_code_attempts = new List<object>();
            lookup = new object();
        }

        public string sid { get; set; }
        public string service_sid { get; set; }
        public string account_sid { get; set; }
        public string to { get; set; }
        public ChannelEnum channel { get; set; }
        public string status { get; set; }
        public bool? valid { get; set; }
        public DateTime? date_created { get; set; }
        public DateTime? date_updated { get; set; }
        public object lookup { get; set; }
        public object amount { get; set; }
        public object payee { get; set; }
        public List<object> send_code_attempts { get; set; }
        public Uri url { get; set; }
        public int statuscode { get; set; }

        public class Carrier
        {
            public object error_code { get; set; }
            public string name { get; set; }
            public string mobile_country_code { get; set; }
            public string mobile_network_code { get; set; }
            public string type { get; set; }
        }

        public class Lookup
        {
            public Lookup()
            {
                carrier = new Carrier();
            }
            public Carrier carrier { get; set; }
        }

        public class SendCodeAttempt
        {
            public DateTime time { get; set; }
            public string channel { get; set; }
            public object channel_id { get; set; }
        }


    }
}
