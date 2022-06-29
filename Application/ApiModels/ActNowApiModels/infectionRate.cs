using System;
using System.Collections.Generic;
using System.Text;
using Application.ApiModels.ActNowApiModels;
namespace Application.ApiModels.ActNowApiModels
{
   public class infectionRate
    {
        public List<sources> sources { get; set; }
        public List<object> anomalies { get; set; }
    }
}
