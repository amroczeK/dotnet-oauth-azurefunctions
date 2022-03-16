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
        public string site_id { get; set; } = "";

        [JsonPropertyName("DeviceId")]
        public string device_id { get; set; } = "";

        [JsonPropertyName("Offset")]
        public int? offset { get; set; }

        [JsonPropertyName("Limit")]
        public int? limit { get; set; }
    }
}