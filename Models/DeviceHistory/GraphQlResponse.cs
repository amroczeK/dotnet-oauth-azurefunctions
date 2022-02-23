using System.Collections.Generic;
using Newtonsoft.Json;

namespace Solution.RuralWater.AZF.Models.DeviceHistory
{
    public class GraphQlResponse
    {
        [JsonProperty("egressData")]
        public List<DeviceHistoryResponse>? deviceHistoryResponse { get; set; } = null;
    }
}