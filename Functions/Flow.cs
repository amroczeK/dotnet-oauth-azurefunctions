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
using Solution.RuralWater.AZF.Models.Flow;
using Microsoft.Extensions.Options;
using Solution.RuralWater.AZF.Services;
using System.Collections.Generic;

namespace Solution.RuralWater.AZF.Functions
{
    public class Flow
    {
        private readonly AuthenticationOptions _authOptions;
        private readonly Secrets _secrets;
        private readonly IQueryService _queryService;

        public Flow(IOptions<AuthenticationOptions> authOptions, IOptions<Secrets> secrets, IQueryService queryService)
        {
            _authOptions = authOptions?.Value ?? throw new ArgumentException(nameof(authOptions));
            _secrets = secrets?.Value ?? throw new ArgumentException(nameof(secrets));
            _queryService = queryService;
        }


        [Function("GetMeasurements")]
        public async Task<HttpResponseData> GetMeasurements(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "measurements")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("Rdmw");

            var response = req.CreateResponse(HttpStatusCode.OK);

            // Parse query parameters
            var queryDictionary = QueryHelpers.ParseQuery(req.Url.Query);

            var reqParams = QueryParams.ConvertDictionaryTo<MeasurementsReqParams>(queryDictionary);

            object flowParams = new
            {
                accountId = _authOptions.AccountId,
                //SiteId = reqParams.site_id,
                //DeviceId = reqParams.device_id,
                tz = "UTC"
            };

            Dictionary<string, object> test = QueryParams.ConvertObjectToDictionary(flowParams);

            // Required: Convert parameters to dynamic object because GraphQLRequest Variables expects Anonymous Type...
            dynamic dynamicQueryParams = QueryParams.DictionaryToDynamic(test);

            // Get Bearer token using Password Credentials flow to be able to query GraphQL layer
            var authenticationHelper = new AuthenticationHelper(logger, _authOptions, _secrets);
            var result = await authenticationHelper.GetAccessToken();

            if (result.AccessToken == null)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            try
            {
                logger.LogInformation("Querying {GraphQlUrl}", _authOptions.GraphQlUrl);

                GraphQLHttpClient client = _queryService.CreateClient(result.AccessToken);

                const string xdsName = Constants.FlowXdsName;
                const string xdsViewName = "rdmw";
                const string version = "v1";
                GraphQLRequest request = _queryService.CreateRequest(xdsName, xdsViewName, version, flowParams);

                var data = await client.SendQueryAsync<FlowGraphQlResponse>(request);

                await response.WriteAsJsonAsync(data.Data.flowResponse);
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError("Error occured querying GraphQL: {error}", ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }

        }

        [Function("GetFlowRdmw")]
        public async Task<HttpResponseData> GetFlowRdmw(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "data/flow/rdmw")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("Rdmw");

            var response = req.CreateResponse(HttpStatusCode.OK);

            // Validate Authorization header and ApiKey
            AuthorizationHelper authorizationHelper = new AuthorizationHelper(logger, _secrets);
            var validate = authorizationHelper.ValidateApiKey(req.Headers);

            if (!validate.Valid)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync(validate.Message);
                return response;
            }

            // Parse query parameters
            var queryDictionary = QueryHelpers.ParseQuery(req.Url.Query);

            // Validate required query parameters
            response = await QueryParams.ValidateQueryParams(response, queryDictionary);
            if (response.StatusCode == HttpStatusCode.BadRequest) return response;

            // Required: Convert parameters to dynamic object because GraphQLRequest Variables expects Anonymous Type...
            dynamic dynamicQueryParams = QueryParams.DictionaryToDynamic(queryDictionary);

            // Get Bearer token using Password Credentials flow to be able to query GraphQL layer
            var authenticationHelper = new AuthenticationHelper(logger, _authOptions, _secrets);
            var result = await authenticationHelper.GetAccessToken();

            if (result.AccessToken == null)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            try
            {
                logger.LogInformation("Querying {GraphQlUrl}", _authOptions.GraphQlUrl);

                GraphQLHttpClient client = _queryService.CreateClient(result.AccessToken);

                const string xdsName = Constants.FlowXdsName;
                const string xdsViewName = "rdmw";
                const string version = "v1";
                GraphQLRequest request = _queryService.CreateRequest(xdsName, xdsViewName, version, dynamicQueryParams);

                var data = await client.SendQueryAsync<FlowGraphQlResponse>(request);

                await response.WriteAsJsonAsync(data.Data.flowResponse);
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError("Error occured querying GraphQL: {error}", ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }

        }
    }
}