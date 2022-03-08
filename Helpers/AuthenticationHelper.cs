using System.Linq;
using System.Security;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Solution.RuralWater.AZF.Options;
using Solution.RuralWater.AZF.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace Solution.RuralWater.AZF.Helpers
{

    public interface IAuthenticationHelper
    {
        Task<TokenResponse> GetAccessToken();
        //Task GetAzureFunctionKey();
    }

    public class AuthenticationHelper : IAuthenticationHelper
    {
        private readonly Config _config;
        private readonly Secrets _secrets;
        private readonly ILogger _logger;

        public AuthenticationHelper(ILogger logger, Config config, Secrets secrets)
        {
            _logger = logger;
            _config = config;
            _secrets = secrets;
        }

        public async Task<TokenResponse> GetAccessToken()
        {
            _logger.LogInformation("Getting Access Token");
            var response = new TokenResponse();
            try
            {
                var authorityUri = $"https://login.microsoftonline.com/{_config.TenantId}/oauth2/v2.0/token";
                string[] scopes = new string[] { _config.Scope };
                var app = PublicClientApplicationBuilder.Create(_config.ClientId)
                        .WithAuthority(new Uri(authorityUri))
                        .Build();
                var accounts = await app.GetAccountsAsync();
                AuthenticationResult result = null;

                if (accounts.Any())
                {
                    result = await app.AcquireTokenSilent(scopes, accounts.FirstOrDefault())
                                      .ExecuteAsync();
                }
                else
                {

                    var securePassword = new SecureString();
                    foreach (char c in _secrets.Password)        // you should fetch the password
                        securePassword.AppendChar(c);  // keystroke by keystroke

                    result = await app.AcquireTokenByUsernamePassword(scopes, _config.AadUsername, securePassword)
                                       .ExecuteAsync();
                    response.AccessToken = result.AccessToken;
                    return response;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Exception = ex.Message;
            }
            return response;
        }
    }
}
