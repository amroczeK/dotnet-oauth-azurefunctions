using System;
using Microsoft.AspNetCore.Http;

namespace Solution.RuralWater.AZF.Models
{
    public class AuthorizationResponse
    {
        public string Message { get; set; } = "";

        public int StatusCode { get; set; } = StatusCodes.Status200OK;

        public Boolean Valid { get; set; } = false;
    }
}