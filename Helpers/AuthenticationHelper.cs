using System.Linq;
using System.Security;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Solution.RuralWater.AZF.Config;
using Solution.RuralWater.AZF.Models;

namespace Solution.RuralWater.AZF.Helpers
{

    public interface IAuthenticationHelper
    {
        Task<TokenResponse> GetAccessToken(string password, Options options);
        //Task GetAzureFunctionKey();
    }

    public class AuthenticationHelper : IAuthenticationHelper
    {
        //private readonly IPublicClientApplication _publicClientApplication;
        private readonly ILogger _logger;

        public AuthenticationHelper(ILogger logger)
        {
            //_publicClientApplication = publicClientApplication;
            _logger = logger;
        }

        public async Task<TokenResponse> GetAccessToken(string password, Options options)
        {
            var authorityUri = $"https://login.microsoftonline.com/{options.TenantId}/oauth2/v2.0/token";
            string[] scopes = new string[] { options.Scope };

            var app = PublicClientApplicationBuilder.Create(options.ClientId)
                        .WithAuthority(authorityUri)
                        .Build();
            var accounts = await app.GetAccountsAsync();

            AuthenticationResult result = null;
            string errorMessage = "";
            if (accounts.Any())
            {
                result = await app.AcquireTokenSilent(scopes, accounts.FirstOrDefault())
                                  .ExecuteAsync();
            }
            else
            {
                try
                {
                    var securePassword = new SecureString();
                    foreach (char c in password)        // you should fetch the password
                        securePassword.AppendChar(c);  // keystroke by keystroke

                    result = await app.AcquireTokenByUsernamePassword(scopes, options.Username, securePassword)
                                       .ExecuteAsync();
                }
                catch (MsalException ex)
                {
                    _logger.LogError(ex.Message);
                    errorMessage = ex.Message;
                }
            }

            var response = new TokenResponse
            {
                AccessToken = result.AccessToken,
                Exception = errorMessage
            };
            return response;
        }
    }
}
