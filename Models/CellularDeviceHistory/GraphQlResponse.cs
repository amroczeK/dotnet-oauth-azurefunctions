using System.Collections.Generic;
using Newtonsoft.Json;

namespace Solution.RuralWater.AZF.Models.CellularDeviceHistory
{
    public class GraphQlResponse
    {
        [JsonProperty("egressData")]
        public List<CellularDeviceHistoryResponse> cellularDeviceHistoryResponse { get; set; }
    }
}