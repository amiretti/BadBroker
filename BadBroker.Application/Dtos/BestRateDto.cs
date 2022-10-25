using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BadBroker.Application.Dtos
{
    public class BestRateDto
    {
        public BestRateDto() { }

        [JsonPropertyName("rates")]
        public List<RateDto> Rates { get; set; }

        [JsonPropertyName("buyDate")]
        public DateTime BuyDate { get; set; }

        [JsonPropertyName("sellDate")]
        public DateTime SellDate { get; set; }

        [JsonPropertyName("tool")]
        public string Tool { get; set; }

        [JsonPropertyName("revenue")]
        public double Revenue { get; set; }
    }
}