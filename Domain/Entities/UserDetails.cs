using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;

namespace Domain.Entities
{
    public class UserDetails : BaseEntity
    {
        public UserDetails()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
            IsActive = true;
        }
        public string Username { get; set; }
        public string PhoneNo { get; set; }
        public string ZipCode { get; set; }
        public string ProfilePicUrl { get; set; }
        public int County { get; set; }
        public string Longitute { get; set; }
        public string Latitude { get; set; }
        public bool IsActive { get; set; }
    }
}
