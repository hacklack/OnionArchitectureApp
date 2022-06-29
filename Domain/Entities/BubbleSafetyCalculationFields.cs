using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Domain.Common;
using static Domain.CommonCodes.CommonEnums;

namespace Domain.Entities
{
    public class BubbleSafetyCalculationFields : BaseEntity
    {
        public BubbleSafetyCalculationFields()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }
        public string Fips { get; set; }
        public double Population { get; set; }
        public double TestPositivityRatio { get; set; }
        public double CaseDensity { get; set; }
        public double InfectionRate { get; set; }
        public double InfectionRateCI90 { get; set; }
        public double ActualCases { get; set; }
        public double ActualDeaths { get; set; }
        public double ActualVaccineCompleted { get; set; }
        public double VaccineToPopulationRatio { get; set; }
        public double DeathToPopulationRatio { get; set; }
        public double CasesToPopulationRatio { get; set; }
    }
}
