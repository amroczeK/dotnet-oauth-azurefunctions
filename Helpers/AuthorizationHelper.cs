using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Solution.RuralWater.AZF.Models;
using Solution.RuralWater.AZF.Options;

namespace Solution.RuralWater.AZF.Helpers
{
    public interface IAuthorizationHelper
    {
        AuthorizationResponse ValidateApiKey(HttpHeadersCollection headers, ILogger logger);
    }

    public class AuthorizationHelper : IAuthorizationHelper
    {
        private const string MissingApiKeyHeaderError = "Authorization header is missing";
        private const string InvalidAuthorizationHeaderError = "Authorization header is invalid";
        private const string InvalidApiKeyError = "ApiKey is invalid";
        public const string ApiKeyHeaderName = "Authorization";
        private const string AuthorizationType = "ApiKey";
        private readonly Secrets _secrets;

        public AuthorizationHelper(IOptions<Secrets> secrets)
        {
            _secrets = secrets?.Value ?? throw new ArgumentException(nameof(secrets));
        }

        /// <summary>
        /// Parses request headers for Authorization header and ApiKey key and validates the value.
        /// </summary>
        /// <returns>AuthorizationResponse object</returns>
        public AuthorizationResponse ValidateApiKey(HttpHeadersCollection headers, ILogger logger)
        {
            var response = new AuthorizationResponse();
            logger.LogInformation("Validating Authorization header and ApiKey.");

            if (headers.TryGetValues(ApiKeyHeaderName, out var output))
            {
                string[] authHeaderParts = output.FirstOrDefault().Split(" ");
                if (authHeaderParts.Length != 2 || authHeaderParts[0] != AuthorizationType)
                {
                    logger.LogError(InvalidAuthorizationHeaderError);
                    response.Message = InvalidAuthorizationHeaderError;
                    response.StatusCode = StatusCodes.Status401Unauthorized;
                    response.Valid = false;
                }
                else if (!authHeaderParts[1].Equals(_secrets.ApiKey))
                {
                    logger.LogError(InvalidApiKeyError);
                    response.Message = InvalidApiKeyError;
                    response.StatusCode = StatusCodes.Status401Unauthorized;
                    response.Valid = false;
                } else {
                    logger.LogInformation("Request is authorized.");
                    response.StatusCode = StatusCodes.Status200OK;
                    response.Valid = true;
                }
            }
            else
            {
                logger.LogError(MissingApiKeyHeaderError);
                response.Message = MissingApiKeyHeaderError;
                response.StatusCode = StatusCodes.Status401Unauthorized;
                response.Valid = false;
            }
            return response;
        }
    }
}