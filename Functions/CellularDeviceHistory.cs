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
        private readonly Config _config;
        private readonly Secrets _secrets;

        public CellularDeviceHistory(IOptions<Config> config, IOptions<Secrets> secrets)
        {
            _config = config.Value ?? throw new ArgumentException(nameof(config));
            _secrets = secrets.Value ?? throw new ArgumentException(nameof(secrets));
        }

        public CellularDeviceHistory(Secrets secrets, Config config)
        {
            _secrets = secrets;
            _config = config;
        }

        [Function("GetCellularDeviceHistoryRdmw")]
        public async Task<HttpResponseData> GetCellularDeviceHistoryRdmw(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "data/cellular-device-history/rdmw")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("Rdmw");

            var response = req.CreateResponse(HttpStatusCode.OK);

            // Validate Authorization header and ApiKey
            AuthorizationHelper authorizationHelper = new AuthorizationHelper(logger, _config, _secrets);
            var validate = authorizationHelper.ValidateApiKey(req.Headers);

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

            var authenticationHelper = new AuthenticationHelper(logger, _config, _secrets);
            var result = await authenticationHelper.GetAccessToken();

            try
            {
                logger.LogInformation("Querying {GraphQlUrl}", _config.GraphQlUrl);

                QueryService qs = new QueryService();
                GraphQLHttpClient client = qs.CreateClient(_config, result.AccessToken);

                var xdsName = Constants.CellularDeviceHistoryXdsName;
                var xdsViewName = "rdmw";
                var version = "v1";
                var request = qs.CreateRequest(xdsName, xdsViewName, version, dynamicQueryParams);

                GraphQLResponse<CDHGraphQlResponse> data = await client.SendQueryAsync<CDHGraphQlResponse>(request);

                await response.WriteAsJsonAsync(data.Data.cellularDeviceHistoryResponse);
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
