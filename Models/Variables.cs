using System.Text.Json.Serialization;

namespace Solution.RuralWater.AZF.Models
{
    public class Variables
    {
        [JsonPropertyName("xdsName")]
        /// <summary>
        /// Name of XDS for GraphQL request to Egress APIs.
        /// </summary>
        public string? XdsName { get; set; }

        [JsonPropertyName("viewName")]
        /// <summary>
        /// XDS view name used for GraphQL request to Egress APIs.
        /// </summary>
        public string? ViewName { get; set; }

        [JsonPropertyName("version")]
        /// <summary>
        /// Egress API version.
        /// </summary>
        public string? Version { get; set; }

        [JsonPropertyName("includeTimeZone")]
        /// <summary>
        /// Enable or disable timezone inclusion, default = false.
        /// </summary>
        public bool IncludeTimeZone { get; set; } = false;

        [JsonPropertyName("params")]
        /// <summary>
        /// Object of query parameters for the GraphQL request to Egress APIs.
        /// </summary>
        public dynamic? Params { get; set; }
    }
}