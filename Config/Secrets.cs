using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Solution.RuralWater.AZF.Config
{
    public class Secrets
    {
        public string Password { get; set; }
        public string VaultApiKey { get; set; }

        public Secrets (ILogger logger)
        {
            logger.LogInformation("Retrieving secrets from key vault.");
            
            var config = new ConfigurationBuilder().AddEnvironmentVariables().Build();

            try
            {
                Password = config["Password"];
                VaultApiKey = config["ApiKey"];
            }
            catch (Exception ex)
            {
                logger.LogError($"Error retrieving secrets: {ex.Message}");
            }
        }
    }
}