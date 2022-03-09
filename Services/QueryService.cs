using System;
using System.Net.Http.Headers;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using Microsoft.Extensions.Options;
using Solution.RuralWater.AZF.Options;

namespace Solution.RuralWater.AZF.Services
{
    public class QueryService : IQueryService
    {
        private readonly AuthenticationOptions _authOptions;

        public QueryService(IOptions<AuthenticationOptions> authOptions) {
            _authOptions = authOptions?.Value ?? throw new ArgumentException(nameof(authOptions));
        }

        /// <summary>
        /// Create GraphQLHttpClient using endpoint and sets Authorization and Origin headers.
        /// </summary>
        /// <returns>GraphQLHttpClient object</returns>
        public GraphQLHttpClient CreateClient(string accessToken)
        {
            var client = new GraphQLHttpClient(_authOptions.GraphQlUrl, new SystemTextJsonSerializer());
            client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.AuthorizationType, accessToken);
            client.HttpClient.DefaultRequestHeaders.Add("Origin", _authOptions.Origin);
            return client;
        }

        /// <summary>
        /// Create GraphQLRequest using specified params.
        /// </summary>
        /// <param name="xdsName">Name of XDS</param>
        /// <param name="xdsViewName">Name of XDS View</param>
        /// <param name="version">Egress API version</param>
        /// <param name="queryParams">Anonymous Type query parameter object for GraphQL request</param>
        /// <returns>GraphQLRequest object</returns>
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