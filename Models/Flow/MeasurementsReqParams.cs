using System.Text.Json.Serialization;

namespace Solution.RuralWater.AZF.Models.Flow
{
    public class MeasurementsReqParams
    {
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
        public string site_id { get; set; }

        [JsonPropertyName("DeviceId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string device_id { get; set; }

        [JsonPropertyName("Offset")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? offset { get; set; }

        [JsonPropertyName("Limit")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? limit { get; set; }
    }
}