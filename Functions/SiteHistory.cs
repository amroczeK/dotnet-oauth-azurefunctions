using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Solution.RuralWater.AZF;
using Solution.RuralWater.AZF.Helpers;
using Solution.RuralWater.AZF.Models;
using Solution.RuralWater.AZF.Models.SiteHistory;

namespace RuralWater
{
    public static class SiteHistory
    {
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

            if (!string.IsNullOrEmpty(accountId))
            {
                try
                {
                    logger.LogInformation($"Querying {Constants.GraphQlUrl}");

                    var client = new GraphQLHttpClient(Constants.GraphQlUrl, new NewtonsoftJsonSerializer());
                    client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.AuthorizationHeader, Constants.BearerToken);
                    client.HttpClient.DefaultRequestHeaders.Add("Origin", Constants.Origin);

                    var request = new GraphQLRequest
                    {
                        Query = Constants.Query,
                        Variables = new
                        {
                            egressDataXdsName = Constants.SiteHistoryXdsName,
                            egressDataViewName = "raw",
                            egressDataVersion = "v1",
                            egressDataIncludeTimeZone = false,
                            egressDataParams = new
                            {
                                accountId,
                                tz
                            }
                        }
                    };

                    var response = await client.SendQueryAsync<GraphQlResponse>(request);
                    return new OkObjectResult(response.Data.siteHistoryResponse);
                }
                catch (Exception ex)
                {
                    logger.LogError($"Error occured querying GraphQL: {ex.Message}");
                    return new BadRequestObjectResult($"Error occurred querying GraphQL: {ex.Message}");
                }
            }
            else
            {
                logger.LogError("Query parameter 'accountId' not set.");
                return new BadRequestObjectResult("Query parameter 'accountId' not set.");
            }
        }
    }
}
