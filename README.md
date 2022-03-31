# Rural Water Azure Functions

### Pre-requisites running in local development
1. Create a `local.settings.json` in the root directory using the example provided and populate the environment variables.
2. Populate the 'Password' key with the password of your account used to generate Access Token for you registered application using 'Password Credentials' flow.
3. To run the azure functions run `func start` and 'allow' connections.

### Pre-requisites running in production environment e.g. deployed Function
1. Deploy the Azure Function to your tenancy.
2. Create an Azure Key Vault to connect your Azure Function following this tutorial. https://www.youtube.com/watch?v=6HKj5hOuD00
3. Make sure you have turned on System Assigned Identity for the Azure Function via Settings -> Identity and applied it's Object (principal) ID to the Azure Key Vault's access policy, as per video above.
4. Reference local.settings.json.example for the steps below.
5. After watching the tutorial above, navigate to your Azure Function and go to Settings -> Configuration -> Application settings add the application options with the following 'name' syntax 'AuthOptions:keyName' and add the appropriate value for your applications configuration.
6. Repeat the above for your secrets 'Secrets:keyName' and have the value reference the URL to the secrets in the Azure Key Vault e.g. '@Microsoft.KeyVault(SecretUri=https://xxx.vault.azure.net/secrets/ApiKey/xxxxxxxxxx)'
7. Navigate to App keys and create a new host key for the Azure Function, host keys apply to all HTTP functions.
8. These options and secrets will get injected into the application during runtime.
9. (OPTIONAL) If you don't want to use AuthorizationLevel.Function, you can add the created Host key's value to configuration with key name 'Secrets:ApiKey' and incoming requests can use 'Authorization' header with key 'ApiKey', and utilize the AuthorizationHelper.ValidateApiKey method to authorize the request.

### Troubleshooting notes
1. If the client/tenant id you are pointing the Azure Function towards sits in a different tenancy/directory as to where the user account is created in Active Directory, you will receive the following error:
```
AADSTS50020: User account 'Unhandled exception inside AuthenticationHelper.' from identity provider 'https://sts.windows.net/xxxxx/' does not exist in tenant 'Directory Name' and cannot access the application 'xxxxxxx'(xxxxxxx) in that tenant. The account needs to be added as an external user in the tenant first. Sign out and sign in again with a different Azure Active Directory user account.
```

To resolve this login to the portal application on that environment using the user account that you are using for the Password Credentials flow and accept the prompts to allow the user access.

2. Make sure CORS is configured to accept your Azure Functions URL/domain in the App API's ConfigMap

### Postman collection
Refer to: https://dev.azure.com/telstradx/Data%20Exchange/_git/Dh.Postman?path=/Solutions/Condamine/Azure%20Functions