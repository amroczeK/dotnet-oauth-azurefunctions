namespace Solution.RuralWater.AZF
{
    public class Constants
    {
        public const string Origin = "http://localhost:7071";
        public const string BearerToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6Ik1yNS1BVWliZkJpaTdOZDFqQmViYXhib1hXMCIsImtpZCI6Ik1yNS1BVWliZkJpaTdOZDFqQmViYXhib1hXMCJ9.eyJhdWQiOiJhcGk6Ly8xNmJjYTRlOS01MTU2LTQ2NzQtODIxYS1hN2Y1MDFhYjRkNzMvdzA0LWF6dTk5MDUtbWdtdC1zb2x0bi1hcGktYXBwLWFwcC1yZWciLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC8xNmJjYTRlOS01MTU2LTQ2NzQtODIxYS1hN2Y1MDFhYjRkNzMvIiwiaWF0IjoxNjQ1Njc1MjQ5LCJuYmYiOjE2NDU2NzUyNDksImV4cCI6MTY0NTY3OTIzMCwiYWNyIjoiMSIsImFpbyI6IkFXUUFtLzhUQUFBQVhBZFVHNi9xQjRUK1JTOEhEeUJhMUprejQyS2xiUnZCOHhtTCtleWRxSlJGSnN6VHFIMTE1QVBEdkVTZnI4eHpaM21ncHB6Sk5UQ25OVVE3WkdrcDBpSFVwMkRnbVNNMlA5U3RHNk85YWtLK2lQSDkzNERKMWg1a2ZPcE9iWE9YIiwiYW1yIjpbInB3ZCIsIm1mYSJdLCJhcHBpZCI6ImNhY2NmNjQwLTc5ZjMtNGRiYi05MTU1LWVkYzAzMjhhOTk3MiIsImFwcGlkYWNyIjoiMCIsImVtYWlsIjoiQWRyaWFuLk1yb2N6ZWtAdGVhbS50ZWxzdHJhLmNvbSIsImZhbWlseV9uYW1lIjoiTXJvY3playIsImdpdmVuX25hbWUiOiJBZHJpYW4iLCJncm91cHMiOlsiNTM0NWVhNmMtOTQ2Ni00YjJhLTgyOWUtNWMyMjRjMmRkYzVkIiwiZGEzOGQ5ZjUtNGUyMC00MDM0LThjNGYtZGVlYTc1MTk0ODRmIiwiMTdiOTJhNjktYzk4NC00Yzg2LThmMGEtMjA4Y2JmNTVmYTdiIiwiYjQ0MmEyNmMtYzJmNS00ZGUxLTg2OGYtZTlkMDkxZDljY2M4IiwiOTA5ODUwZDktMGNkYS00NDQ4LTk2YzQtYzUzMzc3ODYzNzM2IiwiYjVkZTRmNmItNmJkZi00N2I5LWFhMDgtZWFhYWExMDliYTIxIiwiZWM5ODk4MDItZGQyNi00Mjk1LWFmZjctYWNmNjE5ZDk5MTAwIiwiZTMwYThhZGYtNDZlYS00Mzg2LTliZjUtYjc2M2UwYWM3MTVhIiwiNTNiY2FjODYtNzA2Ny00Yjk2LWI2MTktZTY4ZjJhMTM2MjJmIiwiN2Q2OWVlNjMtMzZkNS00NDE1LTg3YmQtMDg0NjAyZGE2MThlIiwiNjY3ODAwMDYtYzg4ZC00ZTFhLWE2YjItOWIwN2E5ZjBiODI0IiwiNjcyOWQ2OGQtMzAxOS00YzIxLWJkZTktZjZiYTVjZTg1ODVjIiwiMGI0NmVmNzQtMGFiNC00YTlkLWIyMzEtZmY5MWZmNGMyYmU1IiwiZWRmYjE5NmMtMzk2NS00NTA3LTk4MzQtMDU2ZTgxNGI2Yzc0IiwiOTM0OWJjNDUtODRlYy00MGQ1LWI4NDItODE3MjY2OGEzOTRmIiwiZDg4MjI3MTYtNmE4Ny00NzdmLTliYzgtZjRjMjNiMmQzZmJiIiwiNWVhODEyOWMtZDlkMS00NDMwLWIyYTYtZjBhOTZlMzk3MGI5IiwiOTJjOGVlMGItN2IyOS00NzBlLThiOWEtMTM2MTRkYjY2YjU5IiwiNWVkYWFjNmQtNzcyMS00ZWE1LWEyYzMtYjY1N2Y2M2IyNjBhIiwiMWVkYmI4NmUtYWJjMy00ZTI0LWEwYTYtOTkwODNiZDBiZTQ2IiwiY2U3ZjEzOGEtYzlmYi00ODQzLTkzZDYtMzBmNzc4NGZiMjg3IiwiMDBjMGM1MTgtOTFmNy00ZDMzLWFiYWUtZGMwNTM3YjJlZWM1IiwiMjcyNmU5ZmMtNDM5My00NzBjLTkyZTAtYTE2NWY0NGYzNWRjIiwiMmQ3ODk0ZTAtZmM0NC00OGViLTg2OWEtMDRiZjg2M2Q1MGMzIiwiMTZiOWJkMDQtZTY1NC00NGRjLWE1OGEtODhlMjRhMmYwYTIyIiwiMTQ3MzI0MWItMTI5ZC00NTg3LTgwOGEtNzIwMzc0NDZjZjBlIiwiNzM4NjM2ZWQtYjFkZi00YzQyLWIyMjItNGI2ZjNiYzQzYzk2Il0sImlkcCI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0LzQ5ZGZjNmEzLTVmYjctNDlmNC1hZGVhLWM1NGU3MjViYjg1NC8iLCJpcGFkZHIiOiI2MC4yMjUuMC4xNzQiLCJuYW1lIjoiTXJvY3playwgQWRyaWFuIiwib2lkIjoiODRiMmU4ODYtNTNhYi00NTBmLWJmMDctYjg5OWE2ODAwNTliIiwicmgiOiIwLkFVSUE2YVM4RmxaUmRFYUNHcWYxQWF0TmMtMFFuRFZ5STY1TWtUUXExYnBSS2VsQ0FQYy4iLCJzY3AiOiJhY2Nlc3NfYXNfdXNlciIsInN1YiI6IldnYmVrS0J0NzZfMS1TNnNBVzlxZ194OUJVbmRValhrUjJBa2FvNkxOX3MiLCJ0aWQiOiIxNmJjYTRlOS01MTU2LTQ2NzQtODIxYS1hN2Y1MDFhYjRkNzMiLCJ1bmlxdWVfbmFtZSI6IkFkcmlhbi5Ncm9jemVrQHRlYW0udGVsc3RyYS5jb20iLCJ1dGkiOiJLSTh5M2dvb01raUxlbkU3eGpWTEFRIiwidmVyIjoiMS4wIn0.dfVjiRb-tPGYakzNVfbME12je80Kq_k-Lxtf7E8zGpwlhJF9-R-toAemwiJLleCIBGd3PZZiEB6BSWfatSV7I9OBS0SDlbVnWOOXZaYLjtaXnm5jMTTZQLcxjF5IUG3_R9XBhZ7GGPvwgaGvAF0u6H3lQE9KwgL09_REYVv7mT2c1DT0oP0f3dyV9LTnfzEygu4W25Hz07_Rs5s4BCAvxcgcenSrcJF-rXfGlzPRDKv8_xFGvF6oRM85WDL0E0V5nRjhrt5rvgKsjnUX-kxzbNfnHf6OnQ1EQrtu4XD_XuiF6qAKt51K9yUjpnXwg4rM-rDOOcSIVFRmm7GuY04sCQ";
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