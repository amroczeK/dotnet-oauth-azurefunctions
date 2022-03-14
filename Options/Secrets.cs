namespace Solution.RuralWater.AZF.Options
{
    /// <summary>
    /// Object of options containing sensitive values used by Authentication and Authorization helper to get an Access Token and validate incoming ApiKey from Hydstra.
    /// <para>
    /// The properties are configured in the Azure Functions Application settings and accessed during runtime.
    /// </para>
    /// </summary>
    public class Secrets
    {
        /// <remarks>
        /// The <c>Password</c> is a <see langword="string"/> that is assigned to the 
        /// username used for the Password Credentials authentication flow.
        /// <para>
        /// The value is stored in the Azure Key Vault.
        /// </para>
        /// </remarks>
        public string Password { get; set; }

        /// <remarks>
        /// The <c>ApiKey</c> is a <see langword="string"/> that is assigned to the 
        /// Azure Function.
        /// <para>
        /// The value is stored in the Azure Key Vault.
        /// </para>
        /// </remarks>
        public string ApiKey { get; set; }
    }
}