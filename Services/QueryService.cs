using System;
using System.Net.Http.Headers;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.Extensions.Options;
using Solution.RuralWater.AZF.Options;

namespace Solution.RuralWater.AZF.Services
{
    public class QueryService : IQueryService
    {
        private readonly Config _config;

        public QueryService(IOptions<Config> config) {
            _config = config.Value ?? throw new ArgumentException(nameof(config));
        }
        public GraphQLHttpClient CreateClient(string accessToken)
        {
            var client = new GraphQLHttpClient(_config.GraphQlUrl, new NewtonsoftJsonSerializer());
            client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.AuthorizationType, accessToken);
            client.HttpClient.DefaultRequestHeaders.Add("Origin", _config.Origin);
            return client;
        }

        public GraphQLRequest CreateRequest(string xdsName, string xdsViewName, string version, dynamic queryParams)
        {
            var request = new GraphQLRequest
            {
                Query = Constants.Query,
                Variables = new
                {
                    egressDataXdsName = xdsName,
                    egressDataViewName = xdsViewName,
                    egressDataVersion = version,
                    egressDataIncludeTimeZone = false,
                    egressDataParams = queryParams
                }
            };
            return request;
        }
    }
}