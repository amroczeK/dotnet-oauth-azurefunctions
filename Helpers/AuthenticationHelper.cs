using System.Linq;
using System.Security;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Solution.RuralWater.AZF.Models;

namespace Solution.RuralWater.AZF.Helpers
{

    public interface IAuthenticationHelper
    {
        Task<TokenResponse> GetAccessToken();
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

        public async Task<TokenResponse> GetAccessToken()
        {
            var authorityUri = $"https://login.microsoftonline.com/{Constants.TenantId}/oauth2/v2.0/token";
            string[] scopes = new string[] { Constants.Scope };

            var app = PublicClientApplicationBuilder.Create(Constants.ClientId)
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
                    foreach (char c in Constants.Password)        // you should fetch the password
                        securePassword.AppendChar(c);  // keystroke by keystroke

                    result = await app.AcquireTokenByUsernamePassword(scopes, Constants.Username, securePassword)
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

        // public async Task GetAzureFunctionKey()
        // {
        //     string clientId = "client id";
        //     string clientSecret = "secret key";
        //     string tenantId = "tenant id";
        //     var functionName = "functionName";
        //     var webFunctionAppName = "functionApp name";
        //     string resourceGroup = "resource group name";
        //     //var credentials = new AzureCredentials(new ServicePrincipalLoginInformation { ClientId = clientId, ClientSecret = secret }, tenant, AzureEnvironment.AzureGlobalCloud);
        //     var credentials = SdkContext.AzureCredentialsFactory
        //         .FromServicePrincipal(clientId,
        //             clientSecret,
        //             tenantId,
        //             AzureEnvironment.AzureGlobalCloud);
        //     // var azure = Azure.Configure()
        //     //          .Authenticate(credentials)
        //     //          .WithDefaultSubscription();
        //     var azure = Microsoft.Azure.Management.Fluent.Azure
        //                 .Configure()
        //                 .Authenticate(credentials)
        //                 .WithDefaultSubscription();

        //     var webFunctionApp = azure.AppServices.FunctionApps.GetByResourceGroup(resourceGroup, webFunctionAppName);
        //     var ftpUsername = webFunctionApp.GetPublishingProfile().FtpUsername;
        //     var username = ftpUsername.Split('\\').ToList()[1];
        //     var password = webFunctionApp.GetPublishingProfile().FtpPassword;
        //     var base64Auth = Convert.ToBase64String(Encoding.Default.GetBytes($"{username}:{password}"));
        //     var apiUrl = new Uri($"https://{webFunctionAppName}.scm.azurewebsites.net/api");
        //     var siteUrl = new Uri($"https://{webFunctionAppName}.azurewebsites.net");
        //     string JWT;
        //     using (var client = new HttpClient())
        //     {
        //         client.DefaultRequestHeaders.Add("Authorization", $"Basic {base64Auth}");

        //         var result = client.GetAsync($"{apiUrl}/functions/admin/token").Result;
        //         JWT = result.Content.ReadAsStringAsync().Result.Trim('"'); //get  JWT for call funtion key
        //     }
        //     using (var client = new HttpClient())
        //     {
        //         client.DefaultRequestHeaders.Add("Authorization", "Bearer " + JWT);
        //         var key = client.GetAsync($"{siteUrl}/admin/functions/{functionName}/keys").Result.Content.ReadAsStringAsync().Result;
        //     }
        // }
    }
}
