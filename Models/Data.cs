using System.Collections.Generic;
using Newtonsoft.Json;

namespace Solution.RuralWater.AZF.Models
{
    [JsonObject(Title = "data")]
    public class Data
    {
        [JsonProperty("egressData")]
        public List<SiteHistoryType> EgressData { get; set; }
    }
}