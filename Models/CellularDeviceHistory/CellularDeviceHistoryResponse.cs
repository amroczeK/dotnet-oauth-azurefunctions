using System.Text.Json.Serialization;

namespace Solution.RuralWater.AZF.Models.CellularDeviceHistory
{
    /// <summary>
    /// Object with properties for the response returned from Egress API for Cellular Device History XDS.
    /// </summary>
    public class CellularDeviceHistoryResponse
    {
        [JsonPropertyName("data_source_id")]
        public int DataSourceId { get; set; }

        [JsonPropertyName("device_id")]
        public string DeviceId { get; set; }

        [JsonPropertyName("site_id")]
        public string SiteId { get; set; }

        [JsonPropertyName("serial_number")]
        public string SerialNumber { get; set; }

        [JsonPropertyName("ts")]
        public string Ts { get; set; }

        [JsonPropertyName("last_updated")]
        public string LastUpdated { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("altitude")]
        public double? Altitude { get; set; }

        [JsonPropertyName("accuracy")]
        public double? Accuracy { get; set; }

        [JsonPropertyName("iccid")]
        public string IccId { get; set; }

        [JsonPropertyName("imsi")]
        public string Imsi { get; set; }

        [JsonPropertyName("imei")]
        public string Imei { get; set; }
    }
}