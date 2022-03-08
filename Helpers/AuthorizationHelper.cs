using System;
using System.Collections.Generic;
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
        AuthorizationResponse ValidateApiKey(HttpHeadersCollection headers);
    }

    public class AuthorizationHelper : IAuthorizationHelper
    {
        private const string MissingApiKeyHeaderError = "Authorization header is missing";
        private const string InvalidAuthorizationHeaderError = "Authorization header is invalid";
        private const string InvalidApiKeyError = "ApiKey is invalid";
        public const string ApiKeyHeaderName = "Authorization";
        private const string AuthorizationType = "ApiKey";
        private readonly Config _config;
        private readonly Secrets _secrets;
        private readonly ILogger _logger;

        public AuthorizationHelper(ILogger logger, Config config, Secrets secrets)
        {
            _logger = logger;
            _config = config;
            _secrets = secrets;
        }

        public AuthorizationResponse ValidateApiKey(HttpHeadersCollection headers)
        {
            var response = new AuthorizationResponse();
            _logger.LogInformation("Validating Authorization header and ApiKey.");

            if (headers.TryGetValues(ApiKeyHeaderName, out var output))
            {
                string[] authHeaderParts = output.FirstOrDefault().Split(" ");
                if (authHeaderParts.Length != 2 || authHeaderParts[0] != AuthorizationType)
                {
                    _logger.LogError($"{InvalidAuthorizationHeaderError}");
                    response.message = InvalidAuthorizationHeaderError;
                    response.statusCode = StatusCodes.Status401Unauthorized;
                    response.valid = false;
                }
                else if (!authHeaderParts[1].Equals(_secrets.VaultApiKey))
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
            response.valid = true;
            return response;
        }
    }
}