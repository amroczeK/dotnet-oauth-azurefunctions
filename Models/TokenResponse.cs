namespace Solution.RuralWater.AZF.Models
{
    /// <summary>
    /// Object detailing Access Token generated using Authentication Helper and any exceptions caught.
    /// </summary>
    public class TokenResponse
    {
        /// <summary>
        /// Access token used in GraphQL request, generated using Password Credentials flow.
        /// </summary>
        public string? AccessToken { get; set; }

        /// <summary>
        /// Exception message that gets passed to parent.
        /// </summary>
        public string? Exception { get; set; }
    }
}