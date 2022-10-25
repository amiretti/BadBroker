using BadBroker.Application.ExternalServices;
using BadBroker.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace BadBroker.Storage.ExternalAPIOperations
{
    public class ExchangeRatesDataApi : IExchangeRatesDataApi
    {
        private readonly string _serviceUrl;
        private readonly string _serviceKey;
        private readonly IConfiguration _config;
        public ExchangeRatesDataApi(IConfiguration config)
        {
            _serviceUrl = config.GetSection("ApiConfig").GetSection("ServiceUrl").Value;
            _serviceKey = config.GetSection("ApiConfig").GetSection("ServiceKey").Value;
        }

        public async Task<TimeSeriesResponse> GetTimeSeries(ExternalFilter filter)
        {
            var url = string.Concat(_serviceUrl, $"?start_date={filter.StartDate}&end_date={filter.EndDate}&base={filter.Base}&symbols={string.Join(",", filter.Symbols)}");
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("apikey", _serviceKey);

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<TimeSeriesResponse>(content);
        }
    }
}