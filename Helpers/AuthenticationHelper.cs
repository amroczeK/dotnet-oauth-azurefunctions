using System.Linq;
using System.Security;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Solution.RuralWater.AZF.Options;
using Solution.RuralWater.AZF.Models;
using System;
using System.Threading;
using Microsoft.Extensions.Options;

namespace Solution.RuralWater.AZF.Helpers
{

    public interface IAuthenticationHelper
    {
        Task<TokenResponse> GetAccessToken(ILogger logger, CancellationToken cancellationToken);
    }

    public class AuthenticationHelper : IAuthenticationHelper
    {
        private readonly AuthenticationOptions _authOptions;
        private readonly Secrets _secrets;

        public AuthenticationHelper(IOptions<AuthenticationOptions> authOptions, IOptions<Secrets> secrets)
        {
            _authOptions = authOptions?.Value ?? throw new ArgumentException(nameof(authOptions));
            _secrets = secrets?.Value ?? throw new ArgumentException(nameof(secrets));
        }

        /// <summary>
        /// Retrieve access token for app registration using specified Client ID, Username and Password for Password Credentials Flow.
        /// </summary>
        /// <returns>TokenReponse object</returns>
        public async Task<TokenResponse> GetAccessToken(ILogger logger, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Getting Access Token");
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
                logger.LogError(ex.Message, "Unhandled exception inside AuthenticationHelper.");
                response.Exception = ex.Message;
            }
            return response;
        }
    }
}
