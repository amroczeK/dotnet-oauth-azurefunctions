using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Solution.RuralWater.AZF.Models.CellularDeviceHistory
{
    /// <summary>
    /// Object with the response returned from GraphQL containing the response from the Egress API.
    /// </summary>
    /// <remarks>
    /// GraphQL request returns the data within an Array with the key named 'egressData'.
    /// </remarks>
    public class CDHGraphQlResponse
    {
        [JsonPropertyName("egressData")]
        public List<CellularDeviceHistoryResponse> cellularDeviceHistoryResponse { get; set; }
    }
}