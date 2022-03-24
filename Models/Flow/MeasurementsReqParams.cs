using System;
using System.Text.Json.Serialization;
using Solution.RuralWater.AZF.Helpers;

namespace Solution.RuralWater.AZF.Models.Flow
{
    public class MeasurementsReqParams
    {
        private string[] _deviceId;
        private string[] _siteId;

        [JsonPropertyName("accountId")]
        public string accountId { get; set; }

        [JsonPropertyName("tz")]
        public string tz { get; set; } = "UTC";

        /// <summary>
        /// Array of Site Identifiers.
        /// <p>The maximum count of site id in one request is 10.</p>
        /// </summary>
        /// <remarks>
        /// Client REST API /GET request uses 'site_id' query param, a comma delimited string of ids
        /// and the GraphQL resolver expects an array of strings for one or more site id's with parameter key
        /// denoted as SiteId in the JsonPropertyName below.
        /// <p>The maximum count of site id in one request is 10.</p>
        /// </remarks>
        [JsonPropertyName("StartTime")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string start_time { get; set; }

        [JsonPropertyName("EndTime")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string end_time { get; set; }

        /// <summary>
        /// Array of Site Identifiers.
        /// <p>The maximum count of device id in one request is 10.</p>
        /// </summary>
        /// <remarks>
        /// Client REST API /GET request uses 'site_id' query param, a comma delimited string of ids
        /// and the GraphQL resolver expects an array of strings for one or more site id's with parameter key
        /// denoted as SiteId in the JsonPropertyName below.
        /// </remarks>
        [JsonPropertyName("SiteId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public dynamic site_id {
            get { return _siteId; }
            set { _siteId = QueryParamHelpers.ConvertCommaDelimitedString(value, "SiteId", 10); }
        }

        /// <summary>
        /// Array of Site Identifiers.
        /// </summary>
        /// <remarks>
        /// Client REST API /GET request uses 'device_id' query param, a comma delimited string of ids
        /// and the GraphQL resolver expects an array of strings for one or more device id's with parameter key
        /// denoted as DeviceId in the JsonPropertyName below.
        /// </remarks>
        [JsonPropertyName("DeviceId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public dynamic device_id
        {
            get { return _deviceId; }
            set { _deviceId = QueryParamHelpers.ConvertCommaDelimitedString(value, "DeviceId", 10); }
        }

        [JsonPropertyName("Offset")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? offset { get; set; }

        [JsonPropertyName("Limit")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? limit { get; set; }
    }
}