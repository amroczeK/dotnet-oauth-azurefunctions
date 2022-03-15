# Rural Water Azure Functions

### Pre-requisites running in local development
1. Create a `local.settings.json` in the root directory using the example provided.
2. Populate the 'Password' key with the password of your account used to generate Access Token for you registered application using 'Password Credentials' flow.
3. To run the azure functions run `func start` and 'allow' connections.

### Pre-requisites running in production environment e.g. deployed Function
1. Deploy the Azure Function to your tenancy.
2. Create an Azure Key Vault to connect your Azure Function following this tutorial. https://www.youtube.com/watch?v=6HKj5hOuD00
3. Reference local.settings.json.example for the steps below.
4. After watching the tutorial above, navigate to your Azure Function and go to Settings -> Configuration -> Application settings and add the secrets with the following 'name' syntax 'Secrets:keyName' and have the value reference the URL to the secrets in the Azure Key Vault e.g. '@Microsoft.KeyVault(SecretUri=https://ruralwaterazf.vault.azure.net/secrets/ApiKey/5179385f0ea84425b5d77e159ec35ac7)'
5. Add the application options using the synax 'AuthOptions:keyName' and add the appropriate value for your applications configuration.
6. These options and secrets will get injected into the application during runtime.