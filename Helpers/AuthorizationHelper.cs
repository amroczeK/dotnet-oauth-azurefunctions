using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Solution.RuralWater.AZF.Models;

namespace Solution.RuralWater.AZF.Helpers
{
    public interface IAuthorizationHelper
    {
        AuthorizationResponse ValidateApiKey(HttpHeadersCollection headers, string vaultApiKey);
    }

    public class AuthorizationHelper : IAuthorizationHelper
    {
        private const string MissingApiKeyHeaderError = "ApiKey header is missing";
        private const string InvalidApiKeyError = "ApiKey is invalid";
        private const string MissingApiKeyError = "ApiKey is null or empty";
        public const string ApiKeyHeaderName = "ApiKey";
        private readonly ILogger _logger;

        public AuthorizationHelper(ILogger logger)
        {
            _logger = logger;
        }

        public AuthorizationResponse ValidateApiKey(HttpHeadersCollection headers, string vaultApiKey)
        {
            var response = new AuthorizationResponse();
            _logger.LogInformation("Validating ApiKey header.");

            if (headers.TryGetValues(ApiKeyHeaderName, out var output))
            {
                var apiKey = output.FirstOrDefault();
                if (!string.IsNullOrEmpty(apiKey))
                {
                    if (!apiKey.Equals(vaultApiKey))
                    {
                        _logger.LogError($"{InvalidApiKeyError}");
                        response.message = InvalidApiKeyError;
                        response.statusCode = StatusCodes.Status401Unauthorized;
                        response.valid = false;
                    }
                }
                else
                {
                    _logger.LogError($"{MissingApiKeyError}");
                    response.message = MissingApiKeyError;
                    response.statusCode = StatusCodes.Status401Unauthorized;
                    response.valid = false;
                }
            }
            else
            {
                _logger.LogError($"{MissingApiKeyHeaderError}");
                response.message = MissingApiKeyHeaderError;
                response.statusCode = StatusCodes.Status401Unauthorized;
                response.valid = false;
            }
            return response;
        }
    }
}