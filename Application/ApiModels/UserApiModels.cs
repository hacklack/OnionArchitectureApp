using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ApiModels
{
    public class UserApiModels : BaseApiModel
    {
        public UserApiModels()
        {
            lstUserPermission = new List<BubbleMeetPermissionsApiModel>();
            countyDetails = new CountiesApiModel();
        }
        public string Username { get; set; }
        public string PhoneNo { get; set; }
        public string ZipCode { get; set; }
        public int County { get; set; }
        public string CountyName { get; set; }
        public string ProfilePicUrl { get; set; }
        public string Longitute { get; set; }
        public string Latitude { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public IFormFile file { get; set; }
        public int ChatId { get; set; }
        public List<BubbleMeetPermissionsApiModel> lstUserPermission { get; set; }
        public CountiesApiModel countyDetails { get;set;}

    }
}
