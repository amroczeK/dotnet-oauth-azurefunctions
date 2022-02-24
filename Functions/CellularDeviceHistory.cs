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
using Solution.RuralWater.AZF.Helpers;
using Solution.RuralWater.AZF.Models.CellularDeviceHistory;

namespace Solution.RuralWater.AZF.Functions
{
    public class CellularDeviceHistory
    {
        [Function("GetCellularDeviceHistoryRdmw")]
        public static async Task<IActionResult> GetCellularDeviceHistoryRdmw(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "data/cellular-device-history/rdmw")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("Rdmw");

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
                            egressDataXdsName = Constants.CellularDeviceHistoryXdsName,
                            egressDataViewName = "rdmw",
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
                    return new OkObjectResult(response.Data.cellularDeviceHistoryResponse);
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