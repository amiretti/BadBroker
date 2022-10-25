using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BadBroker.Models
{
    public class TimeSeriesResponse
    {
        [JsonPropertyName("base")]
        public string Base { get; set; }

        [JsonPropertyName("end_date")]
        public string EndDate { get; set; }

        [JsonPropertyName("rates")]
        public Dictionary<string, Rate> Rates { get; set; }

        [JsonPropertyName("start_date")]
        public string StartDate { get; set; }

        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("timeseries")]
        public bool Timeseries { get; set; }
    }
}
