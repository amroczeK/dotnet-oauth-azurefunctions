using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Primitives;

namespace Solution.RuralWater.AZF.Helpers
{

    // public interface IQueryParamMapper
    // {
    //     dynamic MapParams(dynamic model, Dictionary<string, StringValues> queryDictionary);
    // }
    // public class QueryParamMapper : IQueryParamMapper
    // {
    //     public MapParams(dynamic model, Dictionary<string, StringValues> queryDictionary)
    //     {
    //         var config = new MapperConfiguration(cfg => { });
    //         var mapper = config.CreateMapper();
    //         var queryParams = mapper.Map<model>(queryDictionary);
    //         return queryParams;
    //     }
    // }

    public interface IQueryParams
    {
        T ConvertDictionaryTo<T>(IDictionary<string, StringValues> dictionary) where T : new();

        Task<HttpResponseData> ValidateQueryParams (HttpResponseData response, Dictionary<string, StringValues> queryDictionary);

        dynamic DictionaryToDynamic(Dictionary<string, StringValues> queryDictionary);
    }

    public class QueryParams : IQueryParams
    {
        public QueryParams()
        {

        }

        public T ConvertDictionaryTo<T>(IDictionary<string, StringValues> dictionary) where T : new()
        {
            Type type = typeof(T);
            T ret = new T();

            foreach (var keyValue in dictionary)
            {
                type.GetProperty(keyValue.Key).SetValue(ret, keyValue.Value.ToString(), null);
            }

            return ret;
        }

        public async Task<HttpResponseData> ValidateQueryParams(HttpResponseData response, Dictionary<string, StringValues> queryDictionary)
        {
            if (queryDictionary.TryGetValue("accountId", out var id))
            {
                if (String.IsNullOrEmpty(id))
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    await response.WriteStringAsync("Query parameter 'accountId' must not be null or empty.");
                }
            }
            else
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync("Query parameter 'accountId' must not be null or empty.");
            }
            return response;
        }

        public dynamic DictionaryToDynamic(Dictionary<string, StringValues> queryDictionary)
        {
            dynamic result = queryDictionary.Aggregate(new ExpandoObject() as IDictionary<string, Object>,
                                        (a, p) => { a.Add(p.Key, p.Value); return a; });

            return result;
        }
    }
}