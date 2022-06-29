using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ApiModels
{
    public class UserProfileImageApiModel
    {
        public IFormFile file { get; set; }
        public int id { get; set; }
        public string userProfileImagePath { get; set; }
    }
}
