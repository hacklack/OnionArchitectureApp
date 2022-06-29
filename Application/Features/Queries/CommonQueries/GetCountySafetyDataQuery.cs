using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.ApiModels;
using System.Linq;
using System.Net.Http;
using Application.Services;
using System.Net.Http.Headers;
using Application.ApiModels.ActNowApiModels;
using RestSharp;

namespace Application.Features.Queries.CommonQueries
{
    public class GetCountySafetyDataQuery : IRequest<string>
    {
        public string Fips { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }

        public class GetAllCountiesQueryHandler : IRequestHandler<GetCountySafetyDataQuery,string>
        {
            private readonly IApplicationDbContext _context;
            public GetAllCountiesQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<string> Handle(GetCountySafetyDataQuery query, CancellationToken cancellationToken)
            {
                List<int> countiesFips = new List<int>();
                string fips = string.Empty;
                countiesFips =  _context.counties.Where(y=>y.Fips>9015).Select(x => x.Fips).ToList();
                foreach (var item in countiesFips)
                {
                    if (Convert.ToString(item).Length == 4)
                    {
                        fips = "0" + item;
                    }
                    else
                    {
                        fips = item.ToString();
                    }
                    BubbleSafetyCalculationFields bubbleSafetyCalculationFields = new BubbleSafetyCalculationFields();
                    RestClient client = new RestClient(CommonStaticStrings.BubbleSaftyUrl1 + fips + CommonStaticStrings.BubbleSaftyUrl2);
                    RestRequest request = new RestRequest();
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.Method = Method.GET;
                    IRestResponse<root> responseT = client.Execute<root>(request);


                    if (_context.bubbleSafetyWightsCalculationFields.Where(y => y.Fips == fips).Count() == 0)
                    {
                        bubbleSafetyCalculationFields.Fips = responseT.Data.fips;
                        bubbleSafetyCalculationFields.Population = responseT.Data.population;
                        bubbleSafetyCalculationFields.TestPositivityRatio = responseT.Data.metrics.testPositivityRatio;
                        bubbleSafetyCalculationFields.CaseDensity = responseT.Data.metrics.caseDensity;
                        bubbleSafetyCalculationFields.InfectionRate = responseT.Data.metrics.infectionRate;
                        bubbleSafetyCalculationFields.InfectionRateCI90 = responseT.Data.metrics.infectionRateCI90;
                        bubbleSafetyCalculationFields.ActualCases = responseT.Data.actuals.cases;
                        bubbleSafetyCalculationFields.ActualDeaths = responseT.Data.actuals.deaths;
                        bubbleSafetyCalculationFields.ActualVaccineCompleted = responseT.Data.actuals.vaccinationsCompleted;
                        bubbleSafetyCalculationFields.VaccineToPopulationRatio = Convert.ToDouble(responseT.Data.actuals.vaccinationsCompleted) / Convert.ToDouble(responseT.Data.population);
                        bubbleSafetyCalculationFields.DeathToPopulationRatio = Convert.ToDouble(responseT.Data.actuals.deaths) / Convert.ToDouble(responseT.Data.population);
                        bubbleSafetyCalculationFields.CasesToPopulationRatio = Convert.ToDouble(responseT.Data.actuals.cases) / Convert.ToDouble(responseT.Data.population);
                        bubbleSafetyCalculationFields.UpdatedBy = query.UpdatedBy;
                        bubbleSafetyCalculationFields.CreatedBy = query.CreatedBy;
                        _context.bubbleSafetyWightsCalculationFields.Add(bubbleSafetyCalculationFields);
                    }
                    else
                    {
                        bubbleSafetyCalculationFields = _context.bubbleSafetyWightsCalculationFields.Where(y => y.Fips == fips).FirstOrDefault();
                        bubbleSafetyCalculationFields.Fips = responseT.Data.fips;
                        bubbleSafetyCalculationFields.Population = responseT.Data.population;
                        bubbleSafetyCalculationFields.TestPositivityRatio = responseT.Data.metrics.testPositivityRatio;
                        bubbleSafetyCalculationFields.CaseDensity = responseT.Data.metrics.caseDensity;
                        bubbleSafetyCalculationFields.InfectionRate = responseT.Data.metrics.infectionRate;
                        bubbleSafetyCalculationFields.InfectionRateCI90 = responseT.Data.metrics.infectionRateCI90;
                        bubbleSafetyCalculationFields.ActualCases = responseT.Data.actuals.cases;
                        bubbleSafetyCalculationFields.ActualDeaths = responseT.Data.actuals.deaths;
                        bubbleSafetyCalculationFields.ActualVaccineCompleted = responseT.Data.actuals.vaccinationsCompleted;
                        bubbleSafetyCalculationFields.VaccineToPopulationRatio = Convert.ToDouble(responseT.Data.actuals.vaccinationsCompleted) / Convert.ToDouble(responseT.Data.population);
                        bubbleSafetyCalculationFields.DeathToPopulationRatio = Convert.ToDouble(responseT.Data.actuals.deaths) / Convert.ToDouble(responseT.Data.population);
                        bubbleSafetyCalculationFields.CasesToPopulationRatio = Convert.ToDouble(responseT.Data.actuals.cases) / Convert.ToDouble(responseT.Data.population);
                        bubbleSafetyCalculationFields.UpdatedBy = query.UpdatedBy;
                        bubbleSafetyCalculationFields.CreatedBy = query.CreatedBy;
                    }
                    await _context.SaveChanges();
                }
                return "OK";
            }
        }
    }
}
