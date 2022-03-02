using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Solution.RuralWater.AZF.Config
{
    public class Options
    {
        public string ClientId { get; set; }
        public string TenantId { get; set; }
        public string Scope { get; set; }
        public string Origin { get; set; }
        public string GraphQlUrl { get; set; }
        public string Username { get; set; }

        public Options()
        {
            var config = new ConfigurationBuilder().AddEnvironmentVariables().Build();

            ClientId = config["ClientId"] ?? throw new ArgumentNullException("ClientId");
            TenantId = config["TenantId"] ?? throw new ArgumentNullException("TenantId");
            Scope = config["Scope"] ?? throw new ArgumentNullException("Scope");
            Origin = config["Origin"] ?? throw new ArgumentNullException("Origin");
            GraphQlUrl = config["GraphQlUrl"] ?? throw new ArgumentNullException("GraphQlUrl");
            Username = config["Username"] ?? throw new ArgumentNullException("Username");
        }
    }
}