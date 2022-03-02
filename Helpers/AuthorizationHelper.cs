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
        private const string MissingApiKeyHeaderError = "Authorization header is missing";
        private const string InvalidAuthorizationHeaderError = "Authorization header is invalid";
        private const string InvalidApiKeyError = "ApiKey is invalid";
        public const string ApiKeyHeaderName = "Authorization";
        private const string ApiKeyHeaderAuthorizationType = "ApiKey";
        private readonly ILogger _logger;

        public AuthorizationHelper(ILogger logger)
        {
            _logger = logger;
        }

        public AuthorizationResponse ValidateApiKey(HttpHeadersCollection headers, string vaultApiKey)
        {
            var response = new AuthorizationResponse();
            _logger.LogInformation("Validating Authorization header and ApiKey.");

            if (headers.TryGetValues(ApiKeyHeaderName, out var output))
            {
                string[] authHeaderParts = output.FirstOrDefault().Split(" ");
                if (authHeaderParts.Length != 2 || authHeaderParts[0] != ApiKeyHeaderAuthorizationType)
                {
                    _logger.LogError($"{InvalidAuthorizationHeaderError}");
                    response.message = InvalidAuthorizationHeaderError;
                    response.statusCode = StatusCodes.Status401Unauthorized;
                    response.valid = false;
                }
                else if (!authHeaderParts[1].Equals(vaultApiKey))
                {
                    _logger.LogError($"{InvalidApiKeyError}");
                    response.message = InvalidApiKeyError;
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