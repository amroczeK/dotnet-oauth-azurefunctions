using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Solution.RuralWater.AZF.Config;
using Solution.RuralWater.AZF.Helpers;
using Solution.RuralWater.AZF.Models.Flow;

namespace Solution.RuralWater.AZF.Functions
{
    public class Flow
    {
        [Function("GetFlowRdmw")]
        public async Task<HttpResponseData> GetFlowRdmw(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "data/flow/rdmw")] HttpRequestData req,
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

            // Fails
            FlowParams flowParams = queryParams.ConvertDictionaryTo<FlowParams>(queryDictionary);
            // Fails
            //object flowParams = ConvertDictionaryTo<FlowParams>(queryDictionary);

            // Fails
            //var json = JsonConvert.SerializeObject(flowParams);
            //var obj = JsonConvert.DeserializeObject(json);

            var authenticationHelper = new AuthenticationHelper(logger);
            var result = await authenticationHelper.GetAccessToken(secrets.Password);

            // Fails
            // FlowParams test = new FlowParams{
            //     accountId = "11",
            //     tz = "UTC"
            // };

            // Fails
            //object test3 = (object)test;

            try
            {
                logger.LogInformation($"Querying {Constants.GraphQlUrl}");

                var client = new GraphQLHttpClient(Constants.GraphQlUrl, new NewtonsoftJsonSerializer());
                client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.AuthorizationHeader, result.AccessToken);
                client.HttpClient.DefaultRequestHeaders.Add("Origin", Constants.Origin);

                // Works
                object parameters = new
                {
                    accountId = flowParams.accountId,
                    tz = flowParams.tz
                };

                var request = new GraphQLRequest
                {
                    Query = Constants.Query,
                    Variables = new
                    {
                        egressDataXdsName = Constants.FlowXdsName,
                        egressDataViewName = "rdmw",
                        egressDataVersion = "v1",
                        egressDataIncludeTimeZone = false,
                        egressDataParams = parameters
                    }
                };

                var data = await client.SendQueryAsync<GraphQlResponse>(request);

                await response.WriteAsJsonAsync(data.Data.flowResponse);
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