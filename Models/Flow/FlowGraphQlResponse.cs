using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Solution.RuralWater.AZF.Models.Flow
{
    /// <summary>
    /// Object with the response returned from GraphQL containing the response from the Egress API.
    /// </summary>
    /// <remarks>
    /// GraphQL request returns the data within an Array with the key named 'egressData'.
    /// </remarks>
    public class FlowGraphQlResponse
    {
        [JsonPropertyName("egressData")]
        public List<FlowResponse> flowResponse { get; set; }
    }
}