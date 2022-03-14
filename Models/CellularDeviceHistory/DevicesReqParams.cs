using System.Text.Json.Serialization;

namespace Solution.RuralWater.AZF.Models.CellularDeviceHistory
{
    public class DevicesReqParams
    {
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