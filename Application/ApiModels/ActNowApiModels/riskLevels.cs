using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ApiModels.ActNowApiModels
{
   public class riskLevels
    {
        public int overall { get; set; }
        public int testPositivityRatio { get; set; }
        public int caseDensity { get; set; }
        public int contactTracerCapacityRatio { get; set; }
        public int infectionRate { get; set; }
        public int icuHeadroomRatio { get; set; }
        public int icuCapacityRatio { get; set; }
    }
}
