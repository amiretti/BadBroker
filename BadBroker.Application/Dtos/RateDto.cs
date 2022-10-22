using System;
using System.Text.Json.Serialization;

namespace BadBroker.Application.Dtos
{
    public class RateDto
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("rub")]
        public double RUB { get; set; }

        [JsonPropertyName("eur")]
        public double EUR { get; set; }

        [JsonPropertyName("gbp")]
        public double GBP { get; set; }

        [JsonPropertyName("jpy")]
        public double JPY { get; set; }
    }
}