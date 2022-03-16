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
        public string start_time { get; set; } = "";

        [JsonPropertyName("EndTime")]
        public string end_time { get; set; } = "";

        [JsonPropertyName("SiteId")]
        public string site_id { get; set; } = "";

        [JsonPropertyName("DeviceId")]
        public string device_id { get; set; } = "";

        [JsonPropertyName("Offset")]
        public int? offset { get; set; }

        [JsonPropertyName("Limit")]
        public int? limit { get; set; }
    }
}