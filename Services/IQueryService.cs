using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;

namespace Solution.RuralWater.AZF.Services
{
    public interface IQueryService
    {
        GraphQLHttpClient CreateClient(string accessToken);
        GraphQLRequest CreateRequest(string xdsName, string xdsViewName, string version, dynamic queryParams);
    }
}