namespace Solution.RuralWater.AZF
{
    public class Constants
    {
        public const string ClientId = "359c10ed-2372-4cae-9134-2ad5ba5129e9";

        public const string TenantId = "16bca4e9-5156-4674-821a-a7f501ab4d73";

        public const string Scope = "api://16bca4e9-5156-4674-821a-a7f501ab4d73/w04-azu9905-mgmt-soltn-api-app-app-reg/access_as_user";

        public const string Username = "solution.admin@dataexchange.work";

        public const string Origin = "http://localhost:7071";

        public const string GraphQlUrl = "https://api-app.w04azu9905.datahub.telstra.com/graphql";

        public const string AuthorizationHeader = "Bearer";

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