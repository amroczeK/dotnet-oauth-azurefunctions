using System;
using System.Text.Json.Serialization;
using Solution.RuralWater.AZF.Helpers;

namespace Solution.RuralWater.AZF.Models.Measurement
{
    public class Measurements
    {
        private string[] _deviceId;
        private string[] _siteId;
        private int? _limit;

        /// <summary>
        /// Customer Account Id
        /// </summary>
        [JsonPropertyName("accountId")]
        public string accountId { get; set; }

        /// <summary>
        /// Timezone. Default value : UTC.
        /// </summary>
        [JsonPropertyName("tz")]
        public string tz { get; set; } = "UTC";

        /// <summary>
        /// DateTime string e.g. 2022-03-15T10:15:01.000Z.
        /// </summary>
        /// <remarks>
        /// Client REST API /GET request uses 'start_time' query param, App Api GraphQL layer resolver expects 'Time_Min' to parse underscore
        /// and converts to dot notation Time.Min expected by Self Generating Egress API in re-constructed request by App Api.
        /// </remarks>
        [JsonPropertyName("Time_Min")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string start_time { get; set; }

        /// <summary>
        /// DateTime string e.g. 2022-03-15T10:15:01.000Z.
        /// </summary>
        /// <remarks>
        /// Client REST API /GET request uses 'end_time' query param, App Api GraphQL layer resolver expects 'Time_Max' to parse underscore
        /// and converts to dot notation Time.Max expected by Self Generating Egress API in re-constructed request by App Api.
        /// </remarks>
        [JsonPropertyName("Time_Max")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string end_time { get; set; }

        /// <summary>
        /// Array of Site Identifiers.
        /// <p>The maximum count of device id in one request is 10.</p>
        /// </summary>
        /// <remarks>
        /// Client REST API /GET request uses 'site_id' query param, a comma delimited string of ids
        /// and the App Api GraphQL layer resolver expects an array of strings for one or more site id's with parameter key
        /// denoted as SiteId in the JsonPropertyName below.
        /// </remarks>
        [JsonPropertyName("SiteId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public dynamic site_id
        {
            get { return _siteId; }
            set { _siteId = QueryParamHelpers.ConvertCommaDelimitedString(value, "site_id", 10); }
        }

        /// <summary>
        /// Array of Site Identifiers.
        /// </summary>
        /// <remarks>
        /// Client REST API /GET request uses 'device_id' query param, a comma delimited string of ids
        /// and the App Api GraphQL resolver expects an array of strings for one or more device id's with parameter key
        /// denoted as DeviceId in the JsonPropertyName below.
        /// </remarks>
        [JsonPropertyName("DeviceId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public dynamic device_id
        {
            get { return _deviceId; }
            set { _deviceId = QueryParamHelpers.ConvertCommaDelimitedString(value, "device_id", 10); }
        }

        /// <summary>
        /// The number of devices to skip from the result set. Default value : 0
        /// </summary>
        [JsonPropertyName("Offset")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? offset { get; set; }

        /// <summary>
        /// The maximum number of devices to return. Default value : 1000.
        /// </summary>
        /// <remarks>
        /// Client REST API /GET request uses 'limit' query param, and the App Api GraphQL resolver expects the parameter key to be
        /// denoted as PerPage in the JsonPropertyName below. Self generating egress api PerPage param defaults to 10.
        /// </remarks>
        [JsonPropertyName("PerPage")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public dynamic limit
        {
            get { return _limit; }
            set { _limit = QueryParamHelpers.ValidateLimit(value, "limit", 1000); }
        }
    }
}