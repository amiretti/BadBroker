using System.Text.Json.Serialization;

namespace BadBroker.Models
{
    public class Filter
    {
        [JsonPropertyName("startDate")]
        public string StartDate { get; set; }

        [JsonPropertyName("endDate")]
        public string EndDate { get; set; }

        [JsonPropertyName("moneyUsd")]
        public string Amount { get; set; }
    }
}
