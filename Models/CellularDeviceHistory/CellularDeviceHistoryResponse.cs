using Newtonsoft.Json;

namespace Solution.RuralWater.AZF.Models.CellularDeviceHistory
{
    public class CellularDeviceHistoryResponse
    {
        [JsonProperty("data_source_id")]
        public int DataSourceId { get; set; }

        [JsonProperty("device_id")]
        public string DeviceId { get; set; }

        [JsonProperty("site_id")]
        public string SiteId { get; set; }

        [JsonProperty("serial_number")]
        public string SerialNumber { get; set; }

        [JsonProperty("ts")]
        public string Ts { get; set; }

        [JsonProperty("last_updated")]
        public string LastUpdated { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("altitude")]
        public double? Altitude { get; set; }

        [JsonProperty("accuracy")]
        public double? Accuracy { get; set; }

        [JsonProperty("iccid")]
        public string IccId { get; set; }

        [JsonProperty("imsi")]
        public string Imsi { get; set; }

        [JsonProperty("imei")]
        public string Imei { get; set; }
    }
}