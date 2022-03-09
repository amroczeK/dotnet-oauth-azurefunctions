namespace Solution.RuralWater.AZF.Options
{
    public class Secrets
    {
        /// <remarks>
        /// The <c>Password</c> is a <see langword="string"/> that is assigned to the 
        /// username used for the Password Credentials authentication flow.
        /// <para>
        /// The password is stored in the Azure Key Vault attached to this Azure Function and accessed during runtime.
        /// </para>
        /// </remarks>
        public string Password { get; set; }

        /// <remarks>
        /// The <c>VaultApiKey</c> is a <see langword="string"/> that is assigned to the 
        /// Azure Function.
        /// <para>
        /// It is stored in the Azure Key Vault attached to this Azure Function and accessed during runtime.
        /// </para>
        /// <para>
        /// It is stored in the Azure Key Vault attached to this Azure Function and accessed during runtime.
        /// </para>
        /// </remarks>
        public string VaultApiKey { get; set; }
    }
}