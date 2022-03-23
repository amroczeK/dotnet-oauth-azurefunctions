using System.Text.Json.Serialization;

namespace Solution.RuralWater.AZF.Models.CellularDeviceHistory
{
    public class DevicesReqParams
    {
        [JsonPropertyName("accountId")]
        public string accountId { get; set; }

        [JsonPropertyName("tz")]
        public string tz { get; set; } = "UTC";
        
        [JsonPropertyName("SiteId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string site_id { get; set; } = "";

        [JsonPropertyName("DeviceId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string device_id { get; set; } = "";

        [JsonPropertyName("Offset")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? offset { get; set; }

        [JsonPropertyName("Limit")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? limit { get; set; }
    }
}