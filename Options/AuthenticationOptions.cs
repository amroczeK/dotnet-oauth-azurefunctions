namespace Solution.RuralWater.AZF.Options
{
    public class AuthenticationOptions
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