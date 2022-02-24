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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Solution.RuralWater.AZF.Helpers;
using Solution.RuralWater.AZF.Models.Flow;

namespace Solution.RuralWater.AZF.Functions
{
    public class Flow
    {
        [Function("GetFlowRdmw")]
        public static async Task<IActionResult> GetFlowRdmw(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "data/flow/rdmw")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("Rdmw");

            var config = new ConfigurationBuilder().AddEnvironmentVariables().Build();
            var password = config["Password"];

            AuthorizationHelper authorizationHelper = new AuthorizationHelper(logger);
            var validate = authorizationHelper.ValidateApiKey(req.Headers);

            if (!validate.valid)
            {
                return new BadRequestObjectResult(validate.message);
            }

            var queryDictionary = QueryHelpers.ParseQuery(req.Url.Query);

            string accountId = "";
            if (queryDictionary.TryGetValue("accountId", out var id))
            {
                if (String.IsNullOrEmpty(id))
                {
                    return new BadRequestObjectResult($"Query parameter 'accountId' must not be null or empty.");
                }
                accountId = id;
            } else {
                return new BadRequestObjectResult($"Query parameter 'accountId' is required.");
            }

            queryDictionary.TryGetValue("tz", out var tz);

            var authenticationHelper = new AuthenticationHelper(logger);
            var result = await authenticationHelper.GetAccessToken(password);

            if (!string.IsNullOrEmpty(accountId))
            {
                try
                {
                    logger.LogInformation($"Querying {Constants.GraphQlUrl}");

                    var client = new GraphQLHttpClient(Constants.GraphQlUrl, new NewtonsoftJsonSerializer());
                    client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.AuthorizationHeader, result.AccessToken);
                    client.HttpClient.DefaultRequestHeaders.Add("Origin", Constants.Origin);

                    var request = new GraphQLRequest
                    {
                        Query = Constants.Query,
                        Variables = new
                        {
                            egressDataXdsName = Constants.FlowXdsName,
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
                    return new OkObjectResult(response.Data.flowResponse);
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