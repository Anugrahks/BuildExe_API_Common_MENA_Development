using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
using System.Transactions;
using BuildExeBasic.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeBasic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenGeneratorController : ControllerBase
    {
        private static string key = "XLTRPNZ7ZsKGr5RKOLSNsJe9rgcPLLjn";

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                    var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);

                    var tokenOptions = new JwtSecurityToken(
                        issuer: "https://localhost",
                        audience: "https://localhost",
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(10),
                        signingCredentials: signingCredentials
                        );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    return Ok(new { Token = tokenString });
                }
            }
            catch (Exception)
            { throw; }
        }

    }
}
