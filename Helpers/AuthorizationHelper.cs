using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Solution.RuralWater.AZF.Models;
using Solution.RuralWater.AZF.Options;

namespace Solution.RuralWater.AZF.Helpers
{
    public interface IAuthorizationHelper
    {
        AuthorizationResponse ValidateApiKey(HttpHeadersCollection headers);
    }

    public class AuthorizationHelper : IAuthorizationHelper
    {
        private const string MissingApiKeyHeaderError = "Authorization header is missing";
        private const string InvalidAuthorizationHeaderError = "Authorization header is invalid";
        private const string InvalidApiKeyError = "ApiKey is invalid";
        public const string ApiKeyHeaderName = "Authorization";
        private const string AuthorizationType = "ApiKey";
        private readonly Secrets _secrets;
        private readonly ILogger _logger;

        public AuthorizationHelper(ILogger logger, Secrets secrets)
        {
            _logger = logger;
            _secrets = secrets ?? throw new ArgumentException(nameof(secrets));
        }

        /// <summary>
        /// Parses request headers for Authorization header and ApiKey key and validates the value.
        /// </summary>
        /// <returns>AuthorizationResponse object</returns>
        public AuthorizationResponse ValidateApiKey(HttpHeadersCollection headers)
        {
            var response = new AuthorizationResponse();
            _logger.LogInformation("Validating Authorization header and ApiKey.");

            if (headers.TryGetValues(ApiKeyHeaderName, out var output))
            {
                string[] authHeaderParts = output.FirstOrDefault().Split(" ");
                if (authHeaderParts.Length != 2 || authHeaderParts[0] != AuthorizationType)
                {
                    _logger.LogError(InvalidAuthorizationHeaderError);
                    response.Message = InvalidAuthorizationHeaderError;
                    response.StatusCode = StatusCodes.Status401Unauthorized;
                    response.Valid = false;
                }
                else if (!authHeaderParts[1].Equals(_secrets.VaultApiKey))
                {
                    _logger.LogError(InvalidApiKeyError);
                    response.Message = InvalidApiKeyError;
                    response.StatusCode = StatusCodes.Status401Unauthorized;
                    response.Valid = false;
                }
            }
            else
            {
                _logger.LogError(MissingApiKeyHeaderError);
                response.Message = MissingApiKeyHeaderError;
                response.StatusCode = StatusCodes.Status401Unauthorized;
                response.Valid = false;
            }
            response.Valid = true;
            return response;
        }
    }
}