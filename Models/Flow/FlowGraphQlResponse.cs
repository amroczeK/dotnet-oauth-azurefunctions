using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Solution.RuralWater.AZF.Models.Flow
{
    public class FlowGraphQlResponse
    {
        [JsonPropertyName("egressData")]
        public List<FlowResponse> flowResponse { get; set; }
    }
}