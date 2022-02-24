using Newtonsoft.Json;

namespace Solution.RuralWater.AZF.Models.Flow
{
    public class FlowResponse
    {
        [JsonProperty("data_source_id")]
        public int DataSourceId { get; set; }

        [JsonProperty("device_id")]
        public string DeviceId { get; set; }

        [JsonProperty("site_id")]
        public string SiteId { get; set; }

        [JsonProperty("ts")]
        public string Ts { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("tenant")]
        public string Tenant { get; set; }

        [JsonProperty("fragment")]
        public string Fragment { get; set; }

        [JsonProperty("series")]
        public string Series { get; set; }

        [JsonProperty("ratemlday")]
        public string RateMlDay { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }
    }
}