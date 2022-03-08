using System;
using Microsoft.AspNetCore.Http;

namespace Solution.RuralWater.AZF.Models
{
    public class AuthorizationResponse
    {
        public string message { get; set; } = "";

        public int statusCode { get; set; } = StatusCodes.Status200OK;

        public Boolean valid { get; set; } = false;
    }
}