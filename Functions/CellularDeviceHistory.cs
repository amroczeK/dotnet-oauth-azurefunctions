using System;
using System.Net;
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

            Secrets secrets = new Secrets(logger);

            AuthorizationHelper authorizationHelper = new AuthorizationHelper(logger);
            var validate = authorizationHelper.ValidateApiKey(req.Headers, secrets.VaultApiKey);

            if (!validate.valid)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync(validate.message);
                return response;
            }

            var queryDictionary = QueryHelpers.ParseQuery(req.Url.Query);

            var queryParams = new QueryParams();
            response = await queryParams.ValidateHeaders(response, queryDictionary);
            if (response.StatusCode == HttpStatusCode.BadRequest) return response;

            CellularDeviceHistoryParams cdhp = queryParams.ConvertDictionaryTo<CellularDeviceHistoryParams>(queryDictionary);

            var authenticationHelper = new AuthenticationHelper(logger);
            var result = await authenticationHelper.GetAccessToken(secrets.Password);

            try
            {
                logger.LogInformation($"Querying {Constants.GraphQlUrl}");

                var client = new GraphQLHttpClient(Constants.GraphQlUrl, new NewtonsoftJsonSerializer());
                client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.AuthorizationHeader, result.AccessToken);
                client.HttpClient.DefaultRequestHeaders.Add("Origin", Constants.Origin);

                object parameters = new
                {
                    accountId = cdhp.accountId,
                    tz = cdhp.tz
                };

                var request = new GraphQLRequest
                {
                    Query = Constants.Query,
                    Variables = new
                    {
                        egressDataXdsName = Constants.CellularDeviceHistoryXdsName,
                        egressDataViewName = "rdmw",
                        egressDataVersion = "v1",
                        egressDataIncludeTimeZone = false,
                        egressDataParams = parameters
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
