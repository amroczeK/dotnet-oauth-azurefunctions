using System.Collections.Generic;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace Solution.RuralWater.AZF.Models.Flow
{
    public class FlowParams
    {
        [JsonProperty("accountId", Required = Required.Always)]
        public string accountId { get; set; }

        [JsonProperty("tz")]
        public string tz { get; set; } = "UTC";

        [JsonProperty("deviceId")]
        public string? deviceId { get; set; }

        [JsonProperty("siteId")]
        public string? siteId { get; set; }

        [JsonProperty("ts")]
        public string? ts { get; set; }

        [JsonProperty("time")]
        public string? time { get; set; }

        [JsonProperty("page")]
        public string? page { get; set; }

        [JsonProperty("perPage")]
        public string? perPage { get; set; }

        [JsonProperty("sortBy")]
        public string? sortBy { get; set; }

        [JsonProperty("sort")]
        public string? sort { get; set; }

        [JsonProperty("combineWith")]
        public string? combineWith { get; set; }

        public FlowParams()
        {

        }

        public FlowParams(Dictionary<string, StringValues> queryParams)
        {
            var json = JsonConvert.SerializeObject(queryParams, Newtonsoft.Json.Formatting.Indented);
        }
    }
}