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
using System.Text.Json;
using Solution.RuralWater.AZF.Models;

namespace Solution.RuralWater.AZF.Functions
{
    public class CellularDeviceHistory
    {
        private readonly AuthenticationOptions _authOptions;
        private readonly Secrets _secrets;
        private readonly IQueryService _queryService;
        private readonly AuthenticationHelper _authenticationHelper;
        private readonly AuthorizationHelper _authorizationHelper;
        private readonly ILogger _logger;

        public CellularDeviceHistory(IOptions<AuthenticationOptions> authOptions, IOptions<Secrets> secrets, IQueryService queryService, AuthenticationHelper authenticationHelper, AuthorizationHelper authorizationHelper, ILoggerFactory loggerFactory)
        {
            _authOptions = authOptions?.Value ?? throw new ArgumentException(nameof(authOptions));
            _secrets = secrets?.Value ?? throw new ArgumentException(nameof(secrets));
            _queryService = queryService;
            _authenticationHelper = authenticationHelper;
            _authorizationHelper = authorizationHelper;
            _logger = loggerFactory.CreateLogger<CellularDeviceHistory>();
        }

        [Function("GetCdhRdmw")]
        public async Task<HttpResponseData> GetCdhRdmw(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "devices")] HttpRequestData req,
            FunctionContext executionContext)
        {
            _logger.LogInformation("Incoming request: {0}", req.Url.AbsoluteUri);

            var response = req.CreateResponse(HttpStatusCode.OK);

            // Parse query parameters
            var queryDictionary = QueryHelpers.ParseQuery(req.Url.Query);

            Devices reqParams = null;
            try
            {
                reqParams = QueryParamHelpers.ConvertDictionaryTo<Devices>(queryDictionary);
                reqParams.accountId = _authOptions.AccountId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex?.InnerException?.Message);
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync(ex?.InnerException?.Message);
                return response;
            }

            // Get Bearer token using Password Credentials flow to be able to query GraphQL layer
            var result = await _authenticationHelper.GetAccessToken(_logger);

            if (result.AccessToken == null)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            try
            {
                GraphQLHttpClient client = _queryService.CreateClient(result.AccessToken);

                const string xdsName = Constants.CellularDeviceHistoryXdsName;
                const string xdsViewName = "rdmw";
                const string version = "v1";
                GraphQLRequest request = _queryService.CreateRequest(xdsName, xdsViewName, version, reqParams);

                _logger.LogInformation("Querying: {0}\n{1}\nEgress Data Params: {2}", _authOptions.GraphQlUrl, request.Values, JsonSerializer.Serialize<Devices>(reqParams));
                var data = await client.SendQueryAsync<QueryResponse<Rdmw>>(request);

                await response.WriteAsJsonAsync(data.Data.EgressData);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred querying GraphQL: {error}", ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }
    }
}