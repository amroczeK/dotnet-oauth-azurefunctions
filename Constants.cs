namespace Solution.RuralWater.AZF
{
    public class Constants
    {
        public const string ClientId = "359c10ed-2372-4cae-9134-2ad5ba5129e9";
        public const string TenantId = "16bca4e9-5156-4674-821a-a7f501ab4d73";
        public const string Scope = "api://16bca4e9-5156-4674-821a-a7f501ab4d73/w04-azu9905-mgmt-soltn-api-app-app-reg/access_as_user";
        public const string Username = "solution.admin@dataexchange.work";
        public const string Password = "Fuyo2820";

        public const string Origin = "http://localhost:7071";
        public const string BearerToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6Ik1yNS1BVWliZkJpaTdOZDFqQmViYXhib1hXMCIsImtpZCI6Ik1yNS1BVWliZkJpaTdOZDFqQmViYXhib1hXMCJ9.eyJhdWQiOiJhcGk6Ly8xNmJjYTRlOS01MTU2LTQ2NzQtODIxYS1hN2Y1MDFhYjRkNzMvdzA0LWF6dTk5MDUtbWdtdC1zb2x0bi1hcGktYXBwLWFwcC1yZWciLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC8xNmJjYTRlOS01MTU2LTQ2NzQtODIxYS1hN2Y1MDFhYjRkNzMvIiwiaWF0IjoxNjQ1NjgxMDU3LCJuYmYiOjE2NDU2ODEwNTcsImV4cCI6MTY0NTY4NTI0MywiYWNyIjoiMSIsImFpbyI6IkFVUUF1LzhUQUFBQWFGNjAzUmFzQkovSVk2OEFINnFDMDMzZVdTYzNtNSsvQnhPaWxQV0Q2eWpXeFF6SExQNG45dnRITUhWRjFUUHo2VE1ab3lramNXMklCVXlHUzZNbDhBPT0iLCJhbXIiOlsicHdkIl0sImFwcGlkIjoiMzU5YzEwZWQtMjM3Mi00Y2FlLTkxMzQtMmFkNWJhNTEyOWU5IiwiYXBwaWRhY3IiOiIxIiwiZW1haWwiOiJzb2x1dGlvbi5hZG1pbkBkYXRhZXhjaGFuZ2Uud29yayIsImlkcCI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0LzExMzI2MzA1LWE1NTEtNDA5OC1hZjM2LTJmYjVkNDMxMzg4OC8iLCJpcGFkZHIiOiIyMDMuMzUuODIuMTcxIiwibmFtZSI6IlNvbHV0aW9uIEFkbWluIiwib2lkIjoiMjNjYTQzZmMtOTY5Ny00NWI4LWI3MmQtY2U2ODU0ZWU1MmJjIiwicmgiOiIwLkFVSUE2YVM4RmxaUmRFYUNHcWYxQWF0TmMtMFFuRFZ5STY1TWtUUXExYnBSS2VsQ0FPSS4iLCJzY3AiOiJhY2Nlc3NfYXNfdXNlciIsInN1YiI6Imd5LUpCT1cza01EaFhfTzFYbXFsR0E3NU80YXJqbU1PR21zU19RRklnbzgiLCJ0aWQiOiIxNmJjYTRlOS01MTU2LTQ2NzQtODIxYS1hN2Y1MDFhYjRkNzMiLCJ1bmlxdWVfbmFtZSI6InNvbHV0aW9uLmFkbWluQGRhdGFleGNoYW5nZS53b3JrIiwidXRpIjoiQ3JEejk2SEZRRWVuMi1rZnc1b0JBQSIsInZlciI6IjEuMCJ9.uhB8dvq6bi3ztgPYrgFzzxHvgXeD7VRfPiHuFhbJQnqvfjdDRV-13Lqb4sjhHrjgyZ2j0Jy8rk8mdUKzT4q4_EgIA6tbNOfOn76FfoqzjcL6jhtrgjY19Jf8fs9W_Kz-zwnHy-zyEWe_VlMezar2803gjEPCYExVId3kPt_Ij8WaPml5j9rEyuRysqUgBnojV3fWHiXQVY_fbERfKamlSjPnhbjEyiLOGDPFaMLtEUNNC3dUJROP3B9yZYOibAwYV5Z368USEElM0CizQrbCjgeoaP_YJxfGvvroG0am64eNiboYp3ogYwqOHCN1VqCqmecu6TAWXFNPD7nhtDjl1g";
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