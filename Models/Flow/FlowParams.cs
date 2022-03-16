using System.Text.Json.Serialization;

namespace Solution.RuralWater.AZF.Models.Flow
{
    public class FlowParams
    {
        [JsonPropertyName("accountId")]
        public string accountId { get; set; }

        [JsonPropertyName("tz")]
        public string tz { get; set; } = "UTC";

        [JsonPropertyName("deviceId")]
        public string deviceId { get; set; } = "";

        [JsonPropertyName("siteId")]
        public string siteId { get; set; } = "";

        [JsonPropertyName("ts")]
        public string ts { get; set; } = "";

        [JsonPropertyName("time")]
        public string time { get; set; } = "";

        [JsonPropertyName("page")]
        public string page { get; set; } = "";

        [JsonPropertyName("perPage")]
        public string perPage { get; set; } = "";

        [JsonPropertyName("sortBy")]
        public string sortBy { get; set; } = "";

        [JsonPropertyName("sort")]
        public string sort { get; set; } = "";

        [JsonPropertyName("combineWith")]
        public string combineWith { get; set; } = "";

        public FlowParams()
        {

        }
    }
}