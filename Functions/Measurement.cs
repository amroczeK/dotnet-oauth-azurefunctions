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
using Solution.RuralWater.AZF.Models.Measurement;
using Microsoft.Extensions.Options;
using Solution.RuralWater.AZF.Services;
using System.Text.Json;
using Solution.RuralWater.AZF.Models;

namespace Solution.RuralWater.AZF.Functions
{
    public class Measurement
    {
        private readonly AuthenticationOptions _authOptions;
        private readonly Secrets _secrets;
        private readonly IQueryService _queryService;
        private readonly AuthenticationHelper _authenticationHelper;
        private readonly AuthorizationHelper _authorizationHelper;

        public Measurement(IOptions<AuthenticationOptions> authOptions, IOptions<Secrets> secrets, IQueryService queryService, AuthenticationHelper authenticationHelper, AuthorizationHelper authorizationHelper)
        {
            _authOptions = authOptions?.Value ?? throw new ArgumentException(nameof(authOptions));
            _secrets = secrets?.Value ?? throw new ArgumentException(nameof(secrets));
            _queryService = queryService;
            _authenticationHelper = authenticationHelper;
            _authorizationHelper = authorizationHelper;
        }

        [Function("GetMeasurementRdmw")]
        public async Task<HttpResponseData> GetMeasurementRdmw(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "measurements")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("GetMeasurementRdmw");
            logger.LogInformation("Incoming request: {0}", req.Url.AbsoluteUri);

            var response = req.CreateResponse(HttpStatusCode.OK);

            // Parse query parameters
            var queryDictionary = QueryHelpers.ParseQuery(req.Url.Query);

            Measurements reqParams = null;
            try
            {
                reqParams = QueryParamHelpers.ConvertDictionaryTo<Measurements>(queryDictionary);
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

            if (result.AccessToken == null)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            try
            {
                GraphQLHttpClient client = _queryService.CreateClient(result.AccessToken);

                const string xdsName = Constants.MeasurementXdsName;
                const string xdsViewName = "rdmw";
                const string version = "v1";
                GraphQLRequest request = _queryService.CreateRequest(xdsName, xdsViewName, version, reqParams);

                logger.LogInformation("Querying: {0}\n{1}\nEgress Data Params: {2}", _authOptions.GraphQlUrl, request.Values, JsonSerializer.Serialize<Measurements>(reqParams));
                var data = await client.SendQueryAsync<QueryResponse<Rdmw>>(request);

                await response.WriteAsJsonAsync(data.Data.EgressData);
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