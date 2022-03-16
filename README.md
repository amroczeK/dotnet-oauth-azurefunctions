# Rural Water Azure Functions

### Pre-requisites running in local development
1. Create a `local.settings.json` in the root directory using the example provided and populate the environment variables.
2. Populate the 'Password' key with the password of your account used to generate Access Token for you registered application using 'Password Credentials' flow.
3. To run the azure functions run `func start` and 'allow' connections.

### Pre-requisites running in production environment e.g. deployed Function
1. Deploy the Azure Function to your tenancy.
2. Create an Azure Key Vault to connect your Azure Function following this tutorial. https://www.youtube.com/watch?v=6HKj5hOuD00
3. Reference local.settings.json.example for the steps below.
4. After watching the tutorial above, navigate to your Azure Function and go to Settings -> Configuration -> Application settings add the application options with the following 'name' syntax 'AuthOptions:keyName' and add the appropriate value for your applications configuration.
5. Repeat the above for your secrets 'Secrets:keyName' and have the value reference the URL to the secrets in the Azure Key Vault e.g. '@Microsoft.KeyVault(SecretUri=https://xxx.vault.azure.net/secrets/ApiKey/xxxxxxxxxx)'
6. Navigate to App keys and create a new host key for the Azure Function, host keys apply to all HTTP functions.
7. These options and secrets will get injected into the application during runtime.
8. (OPTIONAL) If you don't want to use AuthorizationLevel.Function, you can add the created Host key's value to configuration with key name 'Secrets:ApiKey' and incoming requests can use 'Authorization' header with key 'ApiKey', and utilize the AuthorizationHelper.ValidateApiKey method to authorize the request.