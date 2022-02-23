using System.Collections.Generic;
using Newtonsoft.Json;

namespace Solution.RuralWater.AZF.Models.SiteHistory
{
    public class GraphQlResponse
    {
        [JsonProperty("egressData")]
        public List<SiteHistoryResponse> siteHistoryResponse { get; set; }
    }
}