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
            set { _siteId = QueryParamHelpers.ConvertCommaDelimitedString(value); }
        }

        [JsonPropertyName("DeviceId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public dynamic device_id
        {
            get { return _deviceId; }
            set { _deviceId = QueryParamHelpers.ConvertCommaDelimitedString(value); }
        }

        [JsonPropertyName("Offset")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? offset { get; set; }

        [JsonPropertyName("Limit")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? limit { get; set; }
    }
}