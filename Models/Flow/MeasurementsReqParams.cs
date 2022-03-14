using System.Text.Json.Serialization;

namespace Solution.RuralWater.AZF.Models.Flow
{
    public class MeasurementsReqParams
    {
        [JsonPropertyName("start_time")]
        public string start_time { get; set; }

        [JsonPropertyName("end_time")]
        public string end_time { get; set; }

        [JsonPropertyName("site_id")]
        public string site_id { get; set; }

        [JsonPropertyName("device_id")]
        public string device_id { get; set; }

        [JsonPropertyName("offset")]
        public int offset { get; set; }

        [JsonPropertyName("limit")]
        public int limit { get; set; }
    }
}