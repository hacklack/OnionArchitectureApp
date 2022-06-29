using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Domain.Common;
using static Domain.CommonCodes.CommonEnums;

namespace Domain.Entities
{
    public class Counties : BaseEntity
    {
        public Counties()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }
        public int Fips { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string CountyName { get; set; }
    }
}
