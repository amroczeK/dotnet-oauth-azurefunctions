using Newtonsoft.Json;

namespace Solution.RuralWater.AZF.Models.SiteHistory
{
    public class SiteHistoryResponse
    {
        [JsonProperty("data_source_id")]
        public string DataSourceId { get; set; }

        [JsonProperty("site_externalid")]
        public string SiteExternalId { get; set; }

        [JsonProperty("loc_address_street")]
        public string LocAddressStreet { get; set; }

        [JsonProperty("loc_address_suburb")]
        public string LocAddressSuburb { get; set; }

        [JsonProperty("loc_address_state")]
        public string LocAddressState { get; set; }

        [JsonProperty("loc_address_postcode")]
        public string LocAddressPostcode { get; set; }

        [JsonProperty("time_duration_start")]
        public string TimeDurationStart { get; set; }

        [JsonProperty("time_duration_end")]
        public string TimeDurationEnd { get; set; }
    }
}