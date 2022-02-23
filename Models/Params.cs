using Newtonsoft.Json;

namespace Solution.RuralWater.AZF.Models
{
    public class Params
    {
        [JsonProperty("accountId")]
        public string AccountId { get; set; } = "11";

        [JsonProperty("tz")]
        public string Tz { get; set; } = "UTC";
    }
}