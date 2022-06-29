using System;
using System.Collections.Generic;
using System.Text;
using Application.ApiModels.ActNowApiModels;

namespace Application.ApiModels.ActNowApiModels
{
  public class actuals
    {
        public int cases { get; set; }
        public int deaths { get; set; }
        public object positiveTests { get; set; }
        public object negativeTests { get; set; }
        public object contactTracers { get; set; }
        public hospitalBeds hospitalBeds { get; set; }
        public icuBeds icuBeds { get; set; }
        public int newCases { get; set; }
        public int newDeaths { get; set; }
        public object vaccinesDistributed { get; set; }
        public int vaccinationsInitiated { get; set; }
        public int vaccinationsCompleted { get; set; }
        public object vaccinesAdministered { get; set; }
        public object vaccinesAdministeredDemographics { get; set; }
        public object vaccinationsInitiatedDemographics { get; set; }
    }
}
