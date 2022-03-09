using System.Linq;
using System.Security;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Solution.RuralWater.AZF.Options;
using Solution.RuralWater.AZF.Models;
using System;

namespace Solution.RuralWater.AZF.Helpers
{

    public interface IAuthenticationHelper
    {
        Task<TokenResponse> GetAccessToken();
    }

    public class AuthenticationHelper : IAuthenticationHelper
    {
        private readonly AuthenticationOptions _authOptions;
        private readonly Secrets _secrets;
        private readonly ILogger _logger;

        public AuthenticationHelper(ILogger logger, AuthenticationOptions authOptions, Secrets secrets)
        {
            _logger = logger;
            _authOptions = authOptions;
            _secrets = secrets;
        }

        /// <summary>
        /// Retrieve access token for app registration using specified Client ID, Username and Password for Password Credentials Flow.
        /// </summary>
        /// <returns>TokenReponse object</returns>
        public async Task<TokenResponse> GetAccessToken()
        {
            _logger.LogInformation("Getting Access Token");
            var response = new TokenResponse();
            try
            {
                var authorityUri = $"https://login.microsoftonline.com/{_authOptions.TenantId}";
                string[] scopes = new string[] { _authOptions.Scope };
                var app = PublicClientApplicationBuilder.Create(_authOptions.ClientId)
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

                    result = await app.AcquireTokenByUsernamePassword(scopes, _authOptions.AadUsername, securePassword)
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
