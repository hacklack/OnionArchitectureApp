using System;
using System.Collections.Generic;
using System.Text;
using Application.ApiModels.ActNowApiModels;
namespace Application.ApiModels.ActNowApiModels
{
   public class annotations
    {
        public cases cases { get; set; }
        public deaths deaths { get; set; }
        public object positiveTests { get; set; }
        public object negativeTests { get; set; }
        public object contactTracers { get; set; }
        public object hospitalBeds { get; set; }
        public object icuBeds { get; set; }
        public object newCases { get; set; }
        public object newDeaths { get; set; }
        public object vaccinesDistributed { get; set; }
        public object vaccinationsInitiated { get; set; }
        public vaccinationsCompleted vaccinationsCompleted { get; set; }
        public object vaccinesAdministered { get; set; }
        public testPositivityRatio testPositivityRatio { get; set; }
        public caseDensity caseDensity { get; set; }
        public object contactTracerCapacityRatio { get; set; }
        public infectionRate infectionRate { get; set; }
        public infectionRateCI90 infectionRateCI90 { get; set; }
        public object icuHeadroomRatio { get; set; }
        public object icuCapacityRatio { get; set; }
        public object vaccinationsInitiatedRatio { get; set; }
        public object vaccinationsCompletedRatio { get; set; }
    }
}
