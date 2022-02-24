using System.Collections.Generic;
using Newtonsoft.Json;

namespace Solution.RuralWater.AZF.Models.Flow
{
    public class GraphQlResponse
    {
        [JsonProperty("egressData")]
        public List<FlowResponse> flowResponse { get; set; }
    }
}