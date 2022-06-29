using System;
using System.Collections.Generic;
using System.Text;
using Application.ApiModels.ActNowApiModels;

namespace Application.ApiModels.ActNowApiModels
{
   public class metrics
    {
        public double testPositivityRatio { get; set; }
        public testPositivityRatio testPositivityRatioDetails { get; set; }
        public double caseDensity { get; set; }
        public object contactTracerCapacityRatio { get; set; }
        public double infectionRate { get; set; }
        public double infectionRateCI90 { get; set; }
        public object icuHeadroomRatio { get; set; }
        public object icuHeadroomDetails { get; set; }
        public object icuCapacityRatio { get; set; }
        public double vaccinationsInitiatedRatio { get; set; }
        public double vaccinationsCompletedRatio { get; set; }
    }
}
