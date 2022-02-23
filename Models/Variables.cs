using Newtonsoft.Json;

namespace Solution.RuralWater.AZF.Models
{
    public class Variables
    {
        [JsonProperty("xdsName")]
        public string XdsName { get; set; }

        [JsonProperty("viewName")]
        public string ViewName { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("includeTimeZone")]
        public bool IncludeTimeZone { get; set; } = false;

        [JsonProperty("params")]
        public dynamic Params { get; set; }
    }
}