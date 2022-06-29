using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;
using static Domain.CommonCodes.CommonEnums;

namespace Application.ApiModels
{
    public class UserDeviceDetailsApiModel : BaseApiModel
    {
        public UserDeviceDetailsApiModel()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }
        public string DeviceToken { get; set; }
        public DeviceType DeviceTypeId { get; set; }
        public int UserId { get; set; }
        
    }
}
