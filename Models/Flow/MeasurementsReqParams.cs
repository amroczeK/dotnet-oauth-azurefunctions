using System.Text.Json.Serialization;

namespace Solution.RuralWater.AZF.Models.Flow
{
    public class MeasurementsReqParams
    {
        [JsonPropertyName("start_time")]
        public string StartTime { get; set; }

        [JsonPropertyName("end_time")]
        public string EndTime { get; set; }

        [JsonPropertyName("site_id")]
        public string SiteId { get; set; }

        [JsonPropertyName("device_id")]
        public string DeviceId { get; set; }

        [JsonPropertyName("offset")]
        public int Offset { get; set; }

        [JsonPropertyName("limit")]
        public int Limit { get; set; }
    }
}