using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;

namespace Domain.Entities
{
   public class OtpHistory :BaseEntity
    {
        public OtpHistory()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }
        public int UserId { get; set; }
        public string Otp { get; set; }
        public DateTime OtpTimeStamp { get; set; }
        public bool OtpStatus { get; set; }
    }
}
