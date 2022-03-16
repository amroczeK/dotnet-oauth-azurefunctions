namespace Solution.RuralWater.AZF.Options
{
    /// <summary>
    /// Object of options containing properties with values used by GraphQL request builder, Authentication and Authorization helper to get an Access Token and validate incoming ApiKey from Hydstra.
    /// <para>
    /// The properties are configured in the Azure Functions Application settings.
    /// </para>
    /// </summary>
    public class AuthenticationOptions
    {
        /// <remarks>
        /// Account Id of Condamine account.
        /// </remarks>
        public string AccountId { get; set; }

        /// <remarks>
        /// Client Id of the application registration used to generate the access token for that has authorization to the GraphQL layer.
        /// </remarks>
        public string ClientId { get; set; }

        /// <remarks>
        /// Tenant Id of the application registration with the configured Client Id.
        /// </remarks>
        public string TenantId { get; set; }

        /// <remarks>
        /// Tenant Id of the application registration with the configured Client Id.
        /// </remarks>
        public string Scope { get; set; }

        /// <remarks>
        /// Origin URL of the application used to configure the GraphQL request.
        /// </remarks>
        public string Origin { get; set; }

        /// <remarks>
        /// GraphQL endpoint used to configure the GraphQL client for the request.
        /// </remarks>
        public string GraphQlUrl { get; set; }

        /// <remarks>
        /// Username used for the Password Credentials flow to get Access Token.
        /// </remarks>
        public string AadUsername { get; set; }
    }
}