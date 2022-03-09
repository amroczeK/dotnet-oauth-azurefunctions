using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Solution.RuralWater.AZF.Options
{
    public class Config
    {
        public string AccountId { get; set; }
        public string ClientId { get; set; }
        public string TenantId { get; set; }
        public string Scope { get; set; }
        public string Origin { get; set; }
        public string GraphQlUrl { get; set; }
        public string AadUsername { get; set; }
    }
}