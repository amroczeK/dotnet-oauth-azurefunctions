namespace Solution.RuralWater.AZF
{
    public class Constants
    {
        public const string AuthorizationType = "Bearer";

        public const string CellularDeviceHistoryXdsName = "cellular-device-history";

        public const string DeviceHealthXdsName = "device-health";

        public const string DeviceHistoryXdsName = "device-history";

        public const string FlowXdsName = "flow";

        public const string SiteHistoryXdsName = "site-history";

        public const string Query = @"query ExampleQuery(
                                    $egressDataXdsName: String!
                                    $egressDataViewName: String!
                                    $egressDataVersion: String!
                                    $egressDataIncludeTimeZone: Boolean!
                                    $egressDataParams: JSON
                                    ) {
                                        egressData(
                                            xdsName: $egressDataXdsName
                                            viewName: $egressDataViewName
                                            version: $egressDataVersion
                                            includeTimeZone: $egressDataIncludeTimeZone
                                            params: $egressDataParams
                                        )
                                    }";
    }
}