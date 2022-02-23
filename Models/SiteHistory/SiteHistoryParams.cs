using Newtonsoft.Json;

namespace Solution.RuralWater.AZF.Models.SiteHistory
{
    public class SiteHistoryParams
    {
        [JsonProperty("accountId")]
        public string AccountId { get; set; }

        [JsonProperty("tz")]
        public string Tz { get; set; } = "UTC";

        [JsonProperty("deviceExternalid")]
        public string DeviceExternalid { get; set; }

        [JsonProperty("siteName")]
        public string SiteName { get; set; }

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