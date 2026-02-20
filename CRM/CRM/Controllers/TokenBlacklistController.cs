using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System;

using System.IdentityModel.Tokens.Jwt;
using BuildExeServices.Models;

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenBlacklistController : ControllerBase
    {
        // Thread-safe storage for blacklisted tokens
        private static readonly ConcurrentDictionary<string, DateTime> TokenBlacklist = new ConcurrentDictionary<string, DateTime>();

        /// <summary>
        /// Blacklist a token (on logout).
        /// </summary>
        /// <param name="token">The JWT to blacklist.</param>
        [HttpPost("logout")]
        public IActionResult Logout([FromBody] TokenClass tokenClass)
        {
            if (string.IsNullOrWhiteSpace(tokenClass.Token))
            {
                return BadRequest("Token is required.");
            }

            // Add the token to the blacklist with its expiration time
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            if (jwtTokenHandler.CanReadToken(tokenClass.Token))
            {
                var jwtToken = jwtTokenHandler.ReadJwtToken(tokenClass.Token);
                var expiration = jwtToken.ValidTo;

                TokenBlacklist[tokenClass.Token] = expiration;
                return Ok("Token blacklisted successfully.");
            }
            else
            {
                return BadRequest("Invalid token.");
            }
        }

        /// <summary>
        /// Validate the token against the blacklist.
        /// </summary>
        /// <param name="token">The JWT to validate.</param>
        /// <returns>True if valid, false otherwise.</returns>
        public static bool IsTokenValid(string token)
        {
            // Check if the token is blacklisted
            return !TokenBlacklist.ContainsKey(token);
        }
    }
}
