using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ApiModels.ActNowApiModels
{
   public class hospitalBeds
    {
        public object capacity { get; set; }
        public object currentUsageTotal { get; set; }
        public object currentUsageCovid { get; set; }
        public object typicalUsageRate { get; set; }
    }
}
