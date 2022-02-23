namespace Solution.RuralWater.AZF
{
    public class Constants
    {
        public const string Origin = "http://localhost:7071";
        public const string BearerToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6Ik1yNS1BVWliZkJpaTdOZDFqQmViYXhib1hXMCIsImtpZCI6Ik1yNS1BVWliZkJpaTdOZDFqQmViYXhib1hXMCJ9.eyJhdWQiOiJhcGk6Ly8xNmJjYTRlOS01MTU2LTQ2NzQtODIxYS1hN2Y1MDFhYjRkNzMvdzA0LWF6dTk5MDUtbWdtdC1zb2x0bi1hcGktYXBwLWFwcC1yZWciLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC8xNmJjYTRlOS01MTU2LTQ2NzQtODIxYS1hN2Y1MDFhYjRkNzMvIiwiaWF0IjoxNjQ1NTk4ODEzLCJuYmYiOjE2NDU1OTg4MTMsImV4cCI6MTY0NTYwMzQ5OSwiYWNyIjoiMSIsImFpbyI6IkFXUUFtLzhUQUFBQWFST09tdnNJZ3ZRck5XbzczNUJCMzVvNm5GdlRyN2RwWktlTkw0eDNTeWJOL3ZUblZCVUNjYjZ0TkhKQk9rT3FxcVAxekpwbk5lOTJndFYwRElwVHhVV0JUNnVlV1pzUDNaL0M2MUowRUJWOFJyMEpwcjVMWEpqOGxXc29sckNPIiwiYW1yIjpbInB3ZCIsIm1mYSJdLCJhcHBpZCI6ImNhY2NmNjQwLTc5ZjMtNGRiYi05MTU1LWVkYzAzMjhhOTk3MiIsImFwcGlkYWNyIjoiMCIsImVtYWlsIjoiQWRyaWFuLk1yb2N6ZWtAdGVhbS50ZWxzdHJhLmNvbSIsImZhbWlseV9uYW1lIjoiTXJvY3playIsImdpdmVuX25hbWUiOiJBZHJpYW4iLCJncm91cHMiOlsiNTM0NWVhNmMtOTQ2Ni00YjJhLTgyOWUtNWMyMjRjMmRkYzVkIiwiZGEzOGQ5ZjUtNGUyMC00MDM0LThjNGYtZGVlYTc1MTk0ODRmIiwiMTdiOTJhNjktYzk4NC00Yzg2LThmMGEtMjA4Y2JmNTVmYTdiIiwiYjQ0MmEyNmMtYzJmNS00ZGUxLTg2OGYtZTlkMDkxZDljY2M4IiwiOTA5ODUwZDktMGNkYS00NDQ4LTk2YzQtYzUzMzc3ODYzNzM2IiwiYjVkZTRmNmItNmJkZi00N2I5LWFhMDgtZWFhYWExMDliYTIxIiwiZWM5ODk4MDItZGQyNi00Mjk1LWFmZjctYWNmNjE5ZDk5MTAwIiwiZTMwYThhZGYtNDZlYS00Mzg2LTliZjUtYjc2M2UwYWM3MTVhIiwiNTNiY2FjODYtNzA2Ny00Yjk2LWI2MTktZTY4ZjJhMTM2MjJmIiwiN2Q2OWVlNjMtMzZkNS00NDE1LTg3YmQtMDg0NjAyZGE2MThlIiwiNjY3ODAwMDYtYzg4ZC00ZTFhLWE2YjItOWIwN2E5ZjBiODI0IiwiNjcyOWQ2OGQtMzAxOS00YzIxLWJkZTktZjZiYTVjZTg1ODVjIiwiMGI0NmVmNzQtMGFiNC00YTlkLWIyMzEtZmY5MWZmNGMyYmU1IiwiZWRmYjE5NmMtMzk2NS00NTA3LTk4MzQtMDU2ZTgxNGI2Yzc0IiwiOTM0OWJjNDUtODRlYy00MGQ1LWI4NDItODE3MjY2OGEzOTRmIiwiZDg4MjI3MTYtNmE4Ny00NzdmLTliYzgtZjRjMjNiMmQzZmJiIiwiNWVhODEyOWMtZDlkMS00NDMwLWIyYTYtZjBhOTZlMzk3MGI5IiwiOTJjOGVlMGItN2IyOS00NzBlLThiOWEtMTM2MTRkYjY2YjU5IiwiNWVkYWFjNmQtNzcyMS00ZWE1LWEyYzMtYjY1N2Y2M2IyNjBhIiwiMWVkYmI4NmUtYWJjMy00ZTI0LWEwYTYtOTkwODNiZDBiZTQ2IiwiY2U3ZjEzOGEtYzlmYi00ODQzLTkzZDYtMzBmNzc4NGZiMjg3IiwiMDBjMGM1MTgtOTFmNy00ZDMzLWFiYWUtZGMwNTM3YjJlZWM1IiwiMjcyNmU5ZmMtNDM5My00NzBjLTkyZTAtYTE2NWY0NGYzNWRjIiwiMmQ3ODk0ZTAtZmM0NC00OGViLTg2OWEtMDRiZjg2M2Q1MGMzIiwiMTZiOWJkMDQtZTY1NC00NGRjLWE1OGEtODhlMjRhMmYwYTIyIiwiMTQ3MzI0MWItMTI5ZC00NTg3LTgwOGEtNzIwMzc0NDZjZjBlIiwiNzM4NjM2ZWQtYjFkZi00YzQyLWIyMjItNGI2ZjNiYzQzYzk2Il0sImlkcCI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0LzQ5ZGZjNmEzLTVmYjctNDlmNC1hZGVhLWM1NGU3MjViYjg1NC8iLCJpcGFkZHIiOiIyMDMuMzUuMTg1LjI1MyIsIm5hbWUiOiJNcm9jemVrLCBBZHJpYW4iLCJvaWQiOiI4NGIyZTg4Ni01M2FiLTQ1MGYtYmYwNy1iODk5YTY4MDA1OWIiLCJyaCI6IjAuQVVJQTZhUzhGbFpSZEVhQ0dxZjFBYXROYy0wUW5EVnlJNjVNa1RRcTFicFJLZWxDQVBjLiIsInNjcCI6ImFjY2Vzc19hc191c2VyIiwic3ViIjoiV2diZWtLQnQ3Nl8xLVM2c0FXOXFnX3g5QlVuZFVqWGtSMkFrYW82TE5fcyIsInRpZCI6IjE2YmNhNGU5LTUxNTYtNDY3NC04MjFhLWE3ZjUwMWFiNGQ3MyIsInVuaXF1ZV9uYW1lIjoiQWRyaWFuLk1yb2N6ZWtAdGVhbS50ZWxzdHJhLmNvbSIsInV0aSI6ImhHbHlwczhZRGt1UU1zSlhXTmExQUEiLCJ2ZXIiOiIxLjAifQ.fQZA9qNgOy5y85N-CVZtgGSLFmtDTRHL57sL_1xVfh6gnZpnDlykeoY6vxv8DTEh7CdPfT9eUvdKs9IKjpavvBGxjqFyckwjgfkYPuwnO-zf4CUCucxmO32mfb4WeiWHE2_q8Ev8MX63KmQop01YPxoX99A_7B4MkPQdckpDi7BUXGx27i5mamLgWU7WFeKFgRfjRQvsAHtwE6T3onPz3Y6fk4qxixXx0ZPdcgbDKNXa13AEHgz9KwDc7RKw-12Dh_63YhOGZrxixZxm--zRL9hvh7jr56LbD743_6V7e_KTc8Bgzt-kl0_xjOHE8P-7XwWQIAztZZsYdcf75Qeqnw";
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