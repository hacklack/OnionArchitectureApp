using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ApiModels.ActNowApiModels
{
    public class root
    {
        public string fips { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string county { get; set; }
        public string level { get; set; }
        public object lat { get; set; }
        public string locationId { get; set; }
        public object _long { get; set; }
        public int population { get; set; }
        public metrics metrics { get; set; }
        public riskLevels riskLevels { get; set; }
        public actuals actuals { get; set; }
        public annotations annotations { get; set; }
        public string lastUpdatedDate { get; set; }
        public string url { get; set; }
    }
}
