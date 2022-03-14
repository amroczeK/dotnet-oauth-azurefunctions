using System.Text.Json.Serialization;

namespace Solution.RuralWater.AZF.Models.CellularDeviceHistory
{
    public class DevicesReqParams
    {
        [JsonPropertyName("SiteId")]
        public string site_id { get; set; }

        [JsonPropertyName("DeviceId")]
        public string device_id { get; set; }

        [JsonPropertyName("Offset")]
        public int offset { get; set; }

        [JsonPropertyName("Limit")]
        public int limit { get; set; }
    }
}