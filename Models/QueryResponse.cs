using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Solution.RuralWater.AZF.Models
{
    /// <summary>
    /// Generic response class for response type.
    /// </summary>
    public class QueryResponse<T>
    {
        [JsonPropertyName("egressData")]
        public List<T> EgressData { get; set; }
    }
}