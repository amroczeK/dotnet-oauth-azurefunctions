using System;
using Microsoft.AspNetCore.Http;

namespace Solution.RuralWater.AZF.Models
{
    /// <summary>
    /// Object with properties to describe if Authorization was successful or not.
    /// </summary>
    public class AuthorizationResponse
    {
        /// <summary>
        /// Exception message if Authorization fails.
        /// </summary>
        public string Message { get; set; } = "";

        /// <summary>
        /// Status code from Authorization helper, default = 200.
        /// </summary>
        public int StatusCode { get; set; } = StatusCodes.Status200OK;

        /// <summary>
        /// Boolean to indicate if ApiKey from Hydstra is valid.
        /// </summary>
        public Boolean Valid { get; set; } = false;
    }
}