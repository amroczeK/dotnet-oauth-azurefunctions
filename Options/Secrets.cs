using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Solution.RuralWater.AZF.Options
{
    public class Secrets
    {
        public string Password { get; set; }
        public string VaultApiKey { get; set; }
    }
}