using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Solution.RuralWater.AZF.Helpers;
using Solution.RuralWater.AZF.Models;

namespace RuralWater
{
    public static class SiteHistory
    {
        // [Function("SiteHistory")]
        // public static HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
        //     FunctionContext executionContext)
        // {
        //     var logger = executionContext.GetLogger("SiteHistory");
        //     logger.LogInformation("C# HTTP trigger function processed a request.");

        //     var response = req.CreateResponse(HttpStatusCode.OK);
        //     response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        //     response.WriteString("Welcome to Azure Functions!");

        //     return response;
        // }

        [Function("Raw")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "data/site-history/raw")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("Raw");

            AuthorizationHelper authorizationHelper = new AuthorizationHelper(logger);
            var validate = authorizationHelper.ValidateApiKey(req.Headers);

            if (!validate.valid)
            {
                return new BadRequestObjectResult(validate.message);
            }

            var queryDictionary = QueryHelpers.ParseQuery(req.Url.Query);

            string accountId = queryDictionary["accountId"];
            string tz = queryDictionary["tz"];

            var authenticationHelper = new AuthenticationHelper(logger);
            var result = await authenticationHelper.GetAccessToken();
            //return new OkObjectResult(result.AccessToken);

            // var data = new Data
            // {
            //     EgressData = {
            //                 new SiteHistoryType{
            //                     DataSourceId = "123"
            //                 },
            //                 new SiteHistoryType {
            //                     DataSourceId = "1234"
            //                 }
            //             }
            // };
            // string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            // Console.WriteLine("HERE:\n", json);

            if (!string.IsNullOrEmpty(accountId))
            {
                try
                {
                    var url = "https://api-app.w04azu9905.datahub.telstra.com/graphql";
                    logger.LogInformation($"Querying {url}");

                    var query = @"query ExampleQuery(
                                    $xdsName: String!
                                    $viewName: String!
                                    $version: String!
                                    $includeTimeZone: Boolean!
                                    $params: JSON
                                    ) {
                                        egressData(
                                            xdsName: $egressDataXdsName
                                            viewName: $egressDataViewName
                                            version: $egressDataVersion
                                            includeTimeZone: $egressDataIncludeTimeZone
                                            params: $egressDataParams
                                        )
                                    }";

                    var variables = new Variables
                    {
                        XdsName = "site-history",
                        ViewName = "raw",
                        Version = "v1",
                        IncludeTimeZone = false,
                        Params = new
                        {
                            accountId,
                            tz
                        }
                    };

                    var client = new GraphQLHttpClient(url, new NewtonsoftJsonSerializer());
                    client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6Ik1yNS1BVWliZkJpaTdOZDFqQmViYXhib1hXMCIsImtpZCI6Ik1yNS1BVWliZkJpaTdOZDFqQmViYXhib1hXMCJ9.eyJhdWQiOiJhcGk6Ly8xNmJjYTRlOS01MTU2LTQ2NzQtODIxYS1hN2Y1MDFhYjRkNzMvdzA0LWF6dTk5MDUtbWdtdC1zb2x0bi1hcGktYXBwLWFwcC1yZWciLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC8xNmJjYTRlOS01MTU2LTQ2NzQtODIxYS1hN2Y1MDFhYjRkNzMvIiwiaWF0IjoxNjQ1NTg0NDkyLCJuYmYiOjE2NDU1ODQ0OTIsImV4cCI6MTY0NTU5MDAzNiwiYWNyIjoiMSIsImFpbyI6IkFXUUFtLzhUQUFBQUdadTFpMkpZT2RWMml6V01ibEpxY2d0WGJmQ09hK1c5amNPR01sbldGL0IzZ0ZpK3J4RS9SVXpIVXgvTng4MSszNWNKYi9naUhkSzNHSWFzZWZTYnJMRyt2UHl6T3Y3OFQ5d2pnbDAvTXhkenRidjkyaTNYdys0OTZUTFhmdnpRIiwiYW1yIjpbInB3ZCIsIm1mYSJdLCJhcHBpZCI6ImNhY2NmNjQwLTc5ZjMtNGRiYi05MTU1LWVkYzAzMjhhOTk3MiIsImFwcGlkYWNyIjoiMCIsImVtYWlsIjoiQWRyaWFuLk1yb2N6ZWtAdGVhbS50ZWxzdHJhLmNvbSIsImZhbWlseV9uYW1lIjoiTXJvY3playIsImdpdmVuX25hbWUiOiJBZHJpYW4iLCJncm91cHMiOlsiNTM0NWVhNmMtOTQ2Ni00YjJhLTgyOWUtNWMyMjRjMmRkYzVkIiwiZGEzOGQ5ZjUtNGUyMC00MDM0LThjNGYtZGVlYTc1MTk0ODRmIiwiMTdiOTJhNjktYzk4NC00Yzg2LThmMGEtMjA4Y2JmNTVmYTdiIiwiYjQ0MmEyNmMtYzJmNS00ZGUxLTg2OGYtZTlkMDkxZDljY2M4IiwiOTA5ODUwZDktMGNkYS00NDQ4LTk2YzQtYzUzMzc3ODYzNzM2IiwiYjVkZTRmNmItNmJkZi00N2I5LWFhMDgtZWFhYWExMDliYTIxIiwiZWM5ODk4MDItZGQyNi00Mjk1LWFmZjctYWNmNjE5ZDk5MTAwIiwiZTMwYThhZGYtNDZlYS00Mzg2LTliZjUtYjc2M2UwYWM3MTVhIiwiNTNiY2FjODYtNzA2Ny00Yjk2LWI2MTktZTY4ZjJhMTM2MjJmIiwiN2Q2OWVlNjMtMzZkNS00NDE1LTg3YmQtMDg0NjAyZGE2MThlIiwiNjY3ODAwMDYtYzg4ZC00ZTFhLWE2YjItOWIwN2E5ZjBiODI0IiwiNjcyOWQ2OGQtMzAxOS00YzIxLWJkZTktZjZiYTVjZTg1ODVjIiwiMGI0NmVmNzQtMGFiNC00YTlkLWIyMzEtZmY5MWZmNGMyYmU1IiwiZWRmYjE5NmMtMzk2NS00NTA3LTk4MzQtMDU2ZTgxNGI2Yzc0IiwiOTM0OWJjNDUtODRlYy00MGQ1LWI4NDItODE3MjY2OGEzOTRmIiwiZDg4MjI3MTYtNmE4Ny00NzdmLTliYzgtZjRjMjNiMmQzZmJiIiwiNWVhODEyOWMtZDlkMS00NDMwLWIyYTYtZjBhOTZlMzk3MGI5IiwiOTJjOGVlMGItN2IyOS00NzBlLThiOWEtMTM2MTRkYjY2YjU5IiwiNWVkYWFjNmQtNzcyMS00ZWE1LWEyYzMtYjY1N2Y2M2IyNjBhIiwiMWVkYmI4NmUtYWJjMy00ZTI0LWEwYTYtOTkwODNiZDBiZTQ2IiwiY2U3ZjEzOGEtYzlmYi00ODQzLTkzZDYtMzBmNzc4NGZiMjg3IiwiMDBjMGM1MTgtOTFmNy00ZDMzLWFiYWUtZGMwNTM3YjJlZWM1IiwiMjcyNmU5ZmMtNDM5My00NzBjLTkyZTAtYTE2NWY0NGYzNWRjIiwiMmQ3ODk0ZTAtZmM0NC00OGViLTg2OWEtMDRiZjg2M2Q1MGMzIiwiMTZiOWJkMDQtZTY1NC00NGRjLWE1OGEtODhlMjRhMmYwYTIyIiwiMTQ3MzI0MWItMTI5ZC00NTg3LTgwOGEtNzIwMzc0NDZjZjBlIiwiNzM4NjM2ZWQtYjFkZi00YzQyLWIyMjItNGI2ZjNiYzQzYzk2Il0sImlkcCI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0LzQ5ZGZjNmEzLTVmYjctNDlmNC1hZGVhLWM1NGU3MjViYjg1NC8iLCJpcGFkZHIiOiI2MC4yMjUuMC4xNzQiLCJuYW1lIjoiTXJvY3playwgQWRyaWFuIiwib2lkIjoiODRiMmU4ODYtNTNhYi00NTBmLWJmMDctYjg5OWE2ODAwNTliIiwicmgiOiIwLkFVSUE2YVM4RmxaUmRFYUNHcWYxQWF0TmMtMFFuRFZ5STY1TWtUUXExYnBSS2VsQ0FQYy4iLCJzY3AiOiJhY2Nlc3NfYXNfdXNlciIsInN1YiI6IldnYmVrS0J0NzZfMS1TNnNBVzlxZ194OUJVbmRValhrUjJBa2FvNkxOX3MiLCJ0aWQiOiIxNmJjYTRlOS01MTU2LTQ2NzQtODIxYS1hN2Y1MDFhYjRkNzMiLCJ1bmlxdWVfbmFtZSI6IkFkcmlhbi5Ncm9jemVrQHRlYW0udGVsc3RyYS5jb20iLCJ1dGkiOiJLY3VZTzRLeFZFR2oweEt6TVdRT0FRIiwidmVyIjoiMS4wIn0.szW_oVtMzrisdpw5DT7uR_BWNoDxj8MNXMo_DR-2CoW7ignZ8azMQt8mflEIP8DJXuw-i-TVTVRXyMFdlpI-0TYmRQ7rM1tiSwd3Gk0stcyyz4wuA7lunUxMuOnOm_4pNGyWe3Zb4XopksjRe5jJ_pWhIBx5BLQMmUv16uY0wrwQK-bQ97Jwr2-lC4B7vomMWNwE0N_RhwGJbnDVpnQXGR34KS_i8AmZBooJOJJS1OzMQXpVQfZen_x1Mn2wFLPB1LFP76Lr780IIqksBiuQAtXcLKBZt8hHRhrFJCeYvMA5yj3uHDvvAi5DYLeqbJoUyNhUHtPvlwcHnqPwucHOrQ");
                    var request = new GraphQLRequest
                    {
                        Query = query,
                        //Variables = JsonConvert.SerializeObject(variables)
                        //Variables = variables
                        // Variables = new {
                        //     xdsName = "site-history",
                        //     viewName = "raw",
                        //     version = "v1",
                        //     includeTimeZone = false,
                        //     params = new {
                        //         accountId = "11",
                        //         tz = "UTC"
                        //     }
                        // }
                        Variables = new
                        {
                            egressDataXdsName = "site-history",
                            egressDataViewName = "raw",
                            egressDataVersion = "v1",
                            egressDataIncludeTimeZone = false,
                            egressDataParams = new
                            {
                                accountId = "11",
                                tz = "UTC"
                            }
                        }
                    };

                    //Console.WriteLine(request.Variables);
                    //Console.WriteLine(request.Query);
                    //Console.WriteLine(JsonConvert.SerializeObject(request));

                    var response = await client.SendQueryAsync<dynamic>(request);
                    //Console.WriteLine(response);
                    //return new OkObjectResult(response);
                    return new OkObjectResult("OK");
                }
                catch (Exception ex)
                {
                    logger.LogError($"Error occured querying GraphQL: {ex.Message}");
                    return new BadRequestObjectResult($"Error occured querying GraphQL: {ex.Message}");
                }
            }
            else
            {
                logger.LogError("Query parameter not set in request body.");
                return new BadRequestObjectResult("Query parameter not set in request body.");
            }
        }
    }
}
