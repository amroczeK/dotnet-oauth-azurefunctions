using System.Text.Json;
using System.Text.Json.Serialization;

namespace Solution.RuralWater.AZF.Models.Flow
{
    public class FlowResponse
    {
        [JsonPropertyName("data_source_id")]
        public long DataSourceId { get; set; }

        [JsonPropertyName("device_id")]
        public string DeviceId { get; set; }

        [JsonPropertyName("site_id")]
        public string SiteId { get; set; }

        [JsonPropertyName("ts")]
        public string Ts { get; set; }

        [JsonPropertyName("time")]
        public string Time { get; set; }

        [JsonPropertyName("tenant")]
        public string Tenant { get; set; }

        [JsonPropertyName("fragment")]
        public string Fragment { get; set; }

        [JsonPropertyName("series")]
        public string Series { get; set; }

        [JsonPropertyName("ratemlday")]
        public double RateMlDay { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }
    }
}