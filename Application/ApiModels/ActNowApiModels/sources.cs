using System;
using System.Collections.Generic;
using System.Text;
using static Domain.CommonCodes.CommonEnums;

namespace Application.ApiModels.ActNowApiModels
{
   public class sources 
    {
        public sources()
        {
        }
        public string type { get; set; }
        public string url { get; set; }
        public string name { get; set; }
    }
}
