using Newtonsoft.Json;

namespace Solution.RuralWater.AZF.Models.Flow
{
    public class FlowParams
    {
        [JsonProperty("accountId")]
        public string AccountId { get; set; }

        [JsonProperty("tz")]
        public string Tz { get; set; } = "UTC";

        [JsonProperty("deviceId")]
        public string DeviceId { get; set; }

        [JsonProperty("siteId")]
        public string SiteId { get; set; }

        [JsonProperty("ts")]
        public string Ts { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("page")]
        public string Page { get; set; }

        [JsonProperty("perPage")]
        public string PerPage { get; set; }

        [JsonProperty("sortBy")]
        public string SortBy { get; set; }

        [JsonProperty("sort")]
        public string Sort { get; set; }

        [JsonProperty("combineWith")]
        public string CombineWith { get; set; }
    }
}