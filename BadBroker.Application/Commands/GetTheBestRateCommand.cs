using BadBroker.Application.Commands.Interfaces;
using BadBroker.Application.Dtos;
using BadBroker.Application.ExternalServices.Repositories;
using BadBroker.Application.Helpers;
using BadBroker.Application.Validations;
using BadBroker.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BadBroker.Application.Commands
{
    public class GetTheBestRateCommand : IGetTheBestRateCommand
    {
        public GetTheBestRateCommand(IRatesRepository repository)
        {
            _repository = repository;
        }

        private Filter _filter;
        private readonly IRatesRepository _repository;

        public string Message { get; set; }
        public bool Success { get; set; }
        public BestRateDto Response { get; private set; }

        public ICommand Configure(Filter filter)
        {
            _filter = filter;
            return this;
        }

        public async Task Execute()
        {
            try
            {
                var validations = new FilterValidations();
                var validationResponse = await validations.Validate(_filter);
                if (validationResponse.Count > 0)
                {
                    Success = false;
                    Message = string.Join(" | ", validationResponse);
                }
                else
                {
                    var result = await _repository.GetTimeSeries(_filter);
                    if (result.Success)
                    {
                        Response = GetBestRate(result);
                        Response.Revenue = Response.Revenue - double.Parse(_filter.Amount);
                        Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Success = false;
                Message = ex.ToString();
            }
        }

        private BestRateDto GetBestRate(TimeSeriesResponse result)
        {
            double maxRate = 0;
            var response = new BestRateDto
            {
                Rates = GetRates(result.Rates)
            };

            for (int i = 0; i < response.Rates.Count; i++)
            {
                for (int j = 1; j < response.Rates.Count; j++)
                {
                    var fee = (response.Rates[i].EUR * 100) / response.Rates[j].EUR - (j - i);
                    if (maxRate < fee)
                    {
                        maxRate = fee;
                        response.Revenue = maxRate;
                        response.BuyDate = response.Rates[i].Date;
                        response.SellDate = response.Rates[j].Date;
                        response.Tool = Constants.CURRENCY_EUR;
                    }

                    fee = (response.Rates[i].GBP * 100) / response.Rates[j].GBP - (response.Rates[j].Date - response.Rates[i].Date).TotalDays;
                    if (maxRate < fee)
                    {
                        maxRate = fee;
                        response.Revenue = maxRate;
                        response.BuyDate = response.Rates[i].Date;
                        response.SellDate = response.Rates[j].Date;
                        response.Tool = Constants.CURRENCY_GBP;
                    }

                    fee = (response.Rates[i].JPY * 100) / response.Rates[j].JPY - (response.Rates[j].Date - response.Rates[i].Date).TotalDays;
                    if (maxRate < fee)
                    {
                        maxRate = fee;
                        response.Revenue = maxRate;
                        response.BuyDate = response.Rates[i].Date;
                        response.SellDate = response.Rates[j].Date;
                        response.Tool = Constants.CURRENCY_JPY;
                    }

                    fee = (response.Rates[i].RUB * 100) / response.Rates[j].RUB - (response.Rates[j].Date - response.Rates[i].Date).TotalDays;
                    if (maxRate < fee)
                    {
                        maxRate = fee;
                        response.Revenue = maxRate;
                        response.BuyDate = response.Rates[i].Date;
                        response.SellDate = response.Rates[j].Date;
                        response.Tool = Constants.CURRENCY_RUB;
                    }
                }
            }

            return response;
        }

        private List<RateDto> GetRates(Dictionary<string, Rate> rates)
        {
            var response = new List<RateDto>();
            foreach (KeyValuePair<string, Rate> rate in rates)
            {
                if (rate.Key.Contains("-")) {
                    var dateParts = rate.Key.Split("-");
                    int year = int.Parse(dateParts[0]);
                    int month = int.Parse(dateParts[1]);
                    int day = int.Parse(dateParts[2]);

                    response.Add(new RateDto()
                    {
                        Date = new DateTime(year, month, day),
                        EUR = rate.Value.EUR,
                        GBP = rate.Value.GBP,
                        JPY = rate.Value.JPY,
                        RUB = rate.Value.RUB
                    });
                }
            }

            return response;
        }
    }
}