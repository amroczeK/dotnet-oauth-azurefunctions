using System;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Solution.RuralWater.AZF.Config;
using Solution.RuralWater.AZF.Helpers;
using Solution.RuralWater.AZF.Models.CellularDeviceHistory;

namespace Solution.RuralWater.AZF.Functions
{
    public class CellularDeviceHistory
    {
        [Function("GetCellularDeviceHistoryRdmw")]
        public static async Task<HttpResponseData> GetCellularDeviceHistoryRdmw(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "data/cellular-device-history/rdmw")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("Rdmw");

            var response = req.CreateResponse(HttpStatusCode.OK);

            Options options = null;
            Secrets secrets = null;
            try
            {
                // Retrieve options passed to environment vars from Azure Function configuration during runtime
                options = new Options();
                // Retrieve secrets passed to environment vars from Azure Key Vault during runtime
                secrets = new Secrets();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error retrieving Application settings: {ex.Message}");
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync($"Error retrieving Application settings: {ex.Message}");
                return response;
            }

            // Validate Authorization header and ApiKey
            AuthorizationHelper authorizationHelper = new AuthorizationHelper(logger);
            var validate = authorizationHelper.ValidateApiKey(req.Headers, secrets.VaultApiKey);

            if (!validate.valid)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync(validate.message);
                return response;
            }

            // Parse query parameters
            var queryDictionary = QueryHelpers.ParseQuery(req.Url.Query);

            // Validate required query parameters
            var queryParams = new QueryParams();
            response = await queryParams.ValidateQueryParams(response, queryDictionary);
            if (response.StatusCode == HttpStatusCode.BadRequest) return response;

            // Required: Convert parameters to dynamic object because GraphQLRequest Variables expects Anonymous Type...
            dynamic dynamicQueryParams = queryParams.DictionaryToDynamic(queryDictionary);

            var authenticationHelper = new AuthenticationHelper(logger);
            var result = await authenticationHelper.GetAccessToken(secrets.Password, options);

            try
            {
                logger.LogInformation($"Querying {options.GraphQlUrl}");

                var client = new GraphQLHttpClient(options.GraphQlUrl, new NewtonsoftJsonSerializer());
                client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.AuthorizationHeader, result.AccessToken);
                client.HttpClient.DefaultRequestHeaders.Add("Origin", options.Origin);

                var request = new GraphQLRequest
                {
                    Query = Constants.Query,
                    Variables = new
                    {
                        egressDataXdsName = Constants.CellularDeviceHistoryXdsName,
                        egressDataViewName = "rdmw",
                        egressDataVersion = "v1",
                        egressDataIncludeTimeZone = false,
                        egressDataParams = dynamicQueryParams
                    }
                };

                var data = await client.SendQueryAsync<GraphQlResponse>(request);

                await response.WriteAsJsonAsync(data.Data.cellularDeviceHistoryResponse);
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error occured querying GraphQL: {ex.Message}");
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync($"Error occured querying GraphQL: {ex.Message}");
                return response;
            }
        }

    }
}
