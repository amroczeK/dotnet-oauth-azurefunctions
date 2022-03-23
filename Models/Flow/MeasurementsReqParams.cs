using System;
using System.Text.Json.Serialization;

namespace Solution.RuralWater.AZF.Models.Flow
{
    public class MeasurementsReqParams
    {
        private dynamic _deviceId;
        private dynamic _siteId;

        [JsonPropertyName("accountId")]
        public string accountId { get; set; }

        [JsonPropertyName("tz")]
        public string tz { get; set; } = "UTC";

        [JsonPropertyName("StartTime")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string start_time { get; set; }

        [JsonPropertyName("EndTime")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string end_time { get; set; }

        [JsonPropertyName("SiteId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public dynamic site_id {
            get { return _siteId; }
            set { _siteId = CommaDelimitedCheck(value); }
        }

        [JsonPropertyName("DeviceId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public dynamic device_id
        {
            get { return _deviceId; }
            set { _deviceId = CommaDelimitedCheck(value); }
        }

        [JsonPropertyName("Offset")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? offset { get; set; }

        [JsonPropertyName("Limit")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? limit { get; set; }

        /// <summary>
        /// Check if value is comma separated/delimited and dynamically return string or string[] conditionally.
        /// </summary>
        /// <returns>String or String[]</returns>
        /// <remarks>
        /// Customers API driver sends list of device/site identifiers as a comma delimited string in requests query params.
        /// </remarks>
        private dynamic CommaDelimitedCheck(string value)
        {
            string[] array = value.Replace(" ", String.Empty).Split(',');
            if (array.Length > 1) return array;
            return value;
        }
    }
}