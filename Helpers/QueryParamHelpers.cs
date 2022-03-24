using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Solution.RuralWater.AZF.Models.Flow;

namespace Solution.RuralWater.AZF.Helpers
{
    /// <summary>
    /// Helper functions for validating query parameters and manipulating Dictionaries.
    /// </summary>
    /// <returns>TokenReponse object</returns>
    public static class QueryParamHelpers
    {
        /// <summary>
        /// Serialize object to json string then deserialize to dictionary of specified type.
        /// </summary>
        /// <returns>Dictionary of type</returns>
        public static Dictionary<string, TValue> ToDictionary<TValue>(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, TValue>>(json);
            return dictionary;
        }

        /// <summary>
        /// Performs reflection on dictionary of string values, converting to specified type.
        /// </summary>
        /// <returns>Object of specified type</returns>
        public static T ConvertDictionaryTo<T>(IDictionary<string, StringValues> dictionary) where T : new()
        {
            Type type = typeof(T);
            T ret = new T();

            foreach (var keyValue in dictionary)
            {
                type.GetProperty(keyValue.Key).SetValue(ret, keyValue.Value.ToString(), null);
            }

            return ret;
        }

        /// <summary>
        /// Validate query params in request from Hydstra.
        /// </summary>
        /// <returns>HttpResponseData object</returns>
        public static async Task<HttpResponseData> ValidateQueryParams(HttpResponseData response, Dictionary<string, StringValues> queryDictionary)
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

        /// <summary>
        /// Converts Dictionary into dynamic object used for params in Variables object of GraphQL Request which expects an Anonymous type.
        /// </summary>
        /// <returns>TokenReponse object</returns>
        public static dynamic DictionaryToDynamic(Dictionary<string, StringValues> queryDictionary)
        {
            dynamic result = queryDictionary.Aggregate(new ExpandoObject() as IDictionary<string, Object>,
                                        (a, p) => { a.Add(p.Key, p.Value); return a; });

            return result;
        }

        /// <summary>
        /// Convert dictionary to dynamic.
        /// </summary>
        /// <returns>Dynamic object</returns>
        public static dynamic DictionaryToDynamic(Dictionary<string, object> queryDictionary)
        {
            dynamic result = queryDictionary.Aggregate(new ExpandoObject() as IDictionary<string, Object>,
                                        (a, p) => { a.Add(p.Key, p.Value); return a; });

            return result;
        }

        /// <summary>
        /// Convert object to dictionary.
        /// </summary>
        /// <returns>Dictionary string object</returns>
        public static Dictionary<string, object> ConvertObjectToDictionary(object arg)
        {
            return arg.GetType().GetProperties().ToDictionary(property => property.Name, property => property.GetValue(arg));
        }

        /// <summary>
        /// Converts comma delimited string of from query parameter to array of strings, expected by GraphQL resolver.
        /// </summary>
        /// <returns>String[]</returns>
        /// <remarks>
        /// Customers API driver sends list of device/site identifiers as a comma delimited string in requests query params.
        /// </remarks>
        public static string[] ConvertCommaDelimitedString(string value)
        {
            string[] array = value.Replace(" ", String.Empty).Split(',');
            return array;
        }
    }
}