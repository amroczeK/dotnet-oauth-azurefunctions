using System;
using System.Net;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Solution.RuralWater.AZF.Options;
using Solution.RuralWater.AZF.Helpers;
using Solution.RuralWater.AZF.Models.CellularDeviceHistory;
using Microsoft.Extensions.Options;
using Solution.RuralWater.AZF.Services;

namespace Solution.RuralWater.AZF.Functions
{
    public class CellularDeviceHistory
    {
        private readonly AuthenticationOptions _authOptions;
        private readonly Secrets _secrets;
        private readonly IQueryService _queryService;
        private readonly AuthenticationHelper _authenticationHelper;
        private readonly AuthorizationHelper _authorizationHelper;

        public CellularDeviceHistory(IOptions<AuthenticationOptions> authOptions, IOptions<Secrets> secrets, IQueryService queryService, AuthenticationHelper authenticationHelper, AuthorizationHelper authorizationHelper)
        {
            _authOptions = authOptions?.Value ?? throw new ArgumentException(nameof(authOptions));
            _secrets = secrets?.Value ?? throw new ArgumentException(nameof(secrets));
            _queryService = queryService;
            _authenticationHelper = authenticationHelper;
            _authorizationHelper = authorizationHelper;
        }

        [Function("GetCellularDeviceHistoryRdmw")]
        public async Task<HttpResponseData> GetCellularDeviceHistoryRdmw(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "data/cellular-device-history/rdmw")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("Rdmw");

            var response = req.CreateResponse(HttpStatusCode.OK);

            // Validate Authorization header and ApiKey
            var validate = _authorizationHelper.ValidateApiKey(req.Headers, logger);

            if (!validate.Valid)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync(validate.Message);
                return response;
            }

            // Parse query parameters
            var queryDictionary = QueryHelpers.ParseQuery(req.Url.Query);

            // Validate required query parameters
            response = await QueryParamHelpers.ValidateQueryParams(response, queryDictionary);
            if (response.StatusCode == HttpStatusCode.BadRequest) return response;

            // Required: Convert parameters to dynamic object because GraphQLRequest Variables expects Anonymous Type...
            dynamic dynamicQueryParams = QueryParamHelpers.DictionaryToDynamic(queryDictionary);

            // Get Bearer token using Password Credentials flow to be able to query GraphQL layer
            var result = await _authenticationHelper.GetAccessToken(logger);

            try
            {
                logger.LogInformation("Querying {GraphQlUrl}", _authOptions.GraphQlUrl);

                GraphQLHttpClient client = _queryService.CreateClient(result.AccessToken);

                const string xdsName = Constants.CellularDeviceHistoryXdsName;
                const string xdsViewName = "rdmw";
                const string version = "v1";
                GraphQLRequest request = _queryService.CreateRequest(xdsName, xdsViewName, version, dynamicQueryParams);

                var data = await client.SendQueryAsync<CDHGraphQlResponse>(request);

                await response.WriteAsJsonAsync(data.Data.cellularDeviceHistoryResponse);
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError("Error occurred querying GraphQL: {error}", ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }

        [Function("GetDevices")]
        public async Task<HttpResponseData> GetDevices(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "devices")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("Rdmw");

            var response = req.CreateResponse(HttpStatusCode.OK);

            // Validate Authorization header and ApiKey
            var validate = _authorizationHelper.ValidateApiKey(req.Headers, logger);

            if (!validate.Valid)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync(validate.Message);
                return response;
            }

            // Parse query parameters
            var queryDictionary = QueryHelpers.ParseQuery(req.Url.Query);

            var reqParams = new DevicesReqParams();
            try
            {
                reqParams = QueryParamHelpers.ConvertDictionaryTo<DevicesReqParams>(queryDictionary);
                reqParams.accountId = _authOptions.AccountId;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.InnerException.Message);
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync(ex.InnerException.Message);
                return response;
            }

            // Get Bearer token using Password Credentials flow to be able to query GraphQL layer
            var result = await _authenticationHelper.GetAccessToken(logger);

            try
            {
                logger.LogInformation("Querying {GraphQlUrl}", _authOptions.GraphQlUrl);

                GraphQLHttpClient client = _queryService.CreateClient(result.AccessToken);

                const string xdsName = Constants.CellularDeviceHistoryXdsName;
                const string xdsViewName = "rdmw";
                const string version = "v1";
                GraphQLRequest request = _queryService.CreateRequest(xdsName, xdsViewName, version, reqParams);

                var data = await client.SendQueryAsync<CDHGraphQlResponse>(request);

                await response.WriteAsJsonAsync(data.Data.cellularDeviceHistoryResponse);
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError("Error occurred querying GraphQL: {error}", ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }
    }
}
