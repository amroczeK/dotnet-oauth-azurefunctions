using Newtonsoft.Json;

namespace Solution.RuralWater.AZF.Models
{
    public class SiteHistoryType
    {
        [JsonProperty("data_source_id")]
        public string DataSourceId { get; set; } = "1";

        [JsonProperty("site_externalid")]
        public string SiteExternalId { get; set; } = "1";

        [JsonProperty("loc_address_street")]
        public string LocAddressStreet { get; set; } = "1";

        [JsonProperty("loc_address_suburb")]
        public string LocAddressSuburb { get; set; } = "1";

        [JsonProperty("loc_address_state")]
        public string LocAddressState { get; set; } = "1";

        [JsonProperty("loc_address_postcode")]
        public string LocAddressPostcode { get; set; } = "1";

        [JsonProperty("time_duration_start")]
        public string TimeDurationStart { get; set; } = "1";

        [JsonProperty("time_duration_end")]
        public string TimeDurationEnd { get; set; } = "1";
    }
}