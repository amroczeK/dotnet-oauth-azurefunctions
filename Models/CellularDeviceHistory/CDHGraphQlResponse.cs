using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Solution.RuralWater.AZF.Models.CellularDeviceHistory
{
    public class CDHGraphQlResponse
    {
        [JsonPropertyName("egressData")]
        public List<CellularDeviceHistoryResponse> cellularDeviceHistoryResponse { get; set; }
    }
}