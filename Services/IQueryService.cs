using GraphQL;
using GraphQL.Client.Http;
using Solution.RuralWater.AZF.Options;

namespace Solution.RuralWater.AZF.Services
{
    public interface IQueryService
    {
        GraphQLHttpClient CreateClient(Config config, string accessToken);
        GraphQLRequest CreateRequest(string xdsName, string xdsViewName, string version, dynamic queryParams);
    }
}