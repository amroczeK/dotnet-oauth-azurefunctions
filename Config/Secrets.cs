using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Solution.RuralWater.AZF.Config
{
    public class Secrets
    {
        public string Password { get; set; }
        public string VaultApiKey { get; set; }

        public Secrets()
        {
            var config = new ConfigurationBuilder().AddEnvironmentVariables().Build();

            Password = config["Password"] ?? throw new ArgumentNullException("Password");
            VaultApiKey = config["ApiKey"] ?? throw new ArgumentNullException("ApiKey");
        }
    }
}