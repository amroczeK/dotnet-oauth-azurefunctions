using System.Text.Json.Serialization;
using System;

namespace Solution.RuralWater.AZF.Models.CellularDeviceHistory
{
    /// <summary>
    /// Object with properties for the response returned from Egress API for Cellular Device History XDS on RDMW view.
    /// </summary>
    public class Rdmw
    {
        private double? _latitude;
        private double? _longitude;

        [JsonPropertyName("data_source_id")]
        public int DataSourceId { get; set; }

        [JsonPropertyName("device_id")]
        public string? DeviceId { get; set; }

        [JsonPropertyName("site_id")]
        public string? SiteId { get; set; }

        [JsonPropertyName("serial_number")]
        public string? SerialNumber { get; set; }

        [JsonPropertyName("ts")]
        public string? Ts { get; set; }

        [JsonPropertyName("last_updated")]
        public string? LastUpdated { get; set; }

        /// <summary>
        /// Latitude property
        /// </summary>
        /// <remarks>
        /// Latitude data for some devices during transformation are being stored as (string) "NaN" in the Postgres table.
        /// This is not deserializable by GraphQLHttpClient even when using JsonNumberHandling.AllowNamedFloatingPointLiterals
        /// in JsonSerializerOptions. The setter below is the work around to convert "NaN" to null.
        /// </remarks>
        [JsonPropertyName("latitude")]
        public double? Latitude
        {
            get
            {
                return _latitude;
            }
            set
            {
                if (Double.IsNaN(Double.Parse(value.ToString())))
                {
                    _latitude = null;
                }
                else
                {
                    _latitude = value;
                }
            }
        }

        /// <summary>
        /// Longitude property
        /// </summary>
        /// <remarks>
        /// Longitude data for some devices during transformation are being stored as (string) "NaN" in the Postgres table.
        /// This is not deserializable by GraphQLHttpClient even when using JsonNumberHandling.AllowNamedFloatingPointLiterals
        /// in JsonSerializerOptions. The setter below is the work around to convert "NaN" to null.
        /// </remarks>
        [JsonPropertyName("longitude")]
        public double? Longitude
        {
            get
            {
                return _longitude;
            }
            set
            {
                if (Double.IsNaN(Double.Parse(value.ToString())))
                {
                    _longitude = null;
                }
                else
                {
                    _longitude = value;
                }
            }
        }

        [JsonPropertyName("altitude")]
        public double? Altitude { get; set; }

        [JsonPropertyName("accuracy")]
        public double? Accuracy { get; set; }

        [JsonPropertyName("iccid")]
        public string? IccId { get; set; }

        [JsonPropertyName("imsi")]
        public string? Imsi { get; set; }

        [JsonPropertyName("imei")]
        public string? Imei { get; set; }
    }
}