using BuildExeServices.Models;
using BuildExeServices.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Repository;
using BuildExeServices.Models;
using System.Transactions;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;
        public LoginController(ICompanyRepository companyRepository, IUserRepository userRepository)
        {
            _companyRepository = companyRepository;
            _userRepository = userRepository;
        }
        private static string key = "XLTRPNZ7ZsKGr5RKOLSNsJe9rgcPLLjn";
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] Users users)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var userid = await _userRepository.Getforlogin(users);
                    if (userid > 0)
                    {
                        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);

                        var tokenOptions = new JwtSecurityToken(
                            issuer: "https://localhost:44356",
                            audience: "https://localhost:44356",
                            claims: new List<Claim>(),
                            expires: DateTime.Now.AddMinutes(2),
                            signingCredentials: signingCredentials
                            );
                        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                        return Ok(new { Token = tokenString });

                    }
                    return Unauthorized();
                }
            }
            catch (Exception)
            { throw; }
        }


        [HttpPost("New")]
        public async Task<IActionResult> PostNew([FromBody] Users users)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var product = await _userRepository.GetforNewLogin(users, "Web");
                    var jsonArray = JArray.Parse(product.ToString());

                    var statusCode = jsonArray[0]["statusCode"]?.Value<int>() ?? -1;

                    if (statusCode == 1)
                    {
                        string clientIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "UNKNOWN";
                        DateTime loginTime = DateTime.UtcNow;
                        string userNameFromResponse = jsonArray[0]["userName"]?.ToString() ?? users.UserId;

                        Console.WriteLine("Login success - logging for: " + userNameFromResponse + " at " + clientIp);

                        try
                        {
                            await _userRepository.InsertLoginLog(userNameFromResponse, clientIp, loginTime);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error inserting login log: " + ex.Message);
                        }
                    }

                    scope.Complete();

                    return new OkObjectResult(product);
                }
            }
            catch (Exception)
            { throw; }
        }


        [HttpPost("New/mobile")]

        public async Task<IActionResult> PostNewmobile([FromBody] Users users)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var product = await _userRepository.GetforNewLogin(users, "Mobile");
                    return new OkObjectResult(product);
                }
            }
            catch (Exception)
            { throw; }
        }

        [HttpGet("GetStarted/{UserId}")]
        public async Task<IActionResult> GetUserDetails(int UserId)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var product = await _userRepository.GetStarted(UserId);
                    scope.Complete();
                    return new OkObjectResult(product);
                }
            }
            catch (Exception)
            { throw; }
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var products = await _companyRepository.Getlogincompany();
                return new OkObjectResult(products);
            }
            catch (Exception)
            { throw; }
        }

        [HttpGet("GetHidden")]
        public async Task<IActionResult> Getlogincompanynothidden()
        {
            try
            {
                var products = await _companyRepository.Getlogincompanynothidden();
                return new OkObjectResult(products);
            }
            catch (Exception)
            { throw; }
        }

        [HttpGet("{Companyid}")]
        public async Task<IActionResult> Get(int Companyid)
        {
            try
            {
                var product = await _companyRepository.GetloginBranch(Companyid);
                return new OkObjectResult(product);
            }
            catch (Exception)
            { throw; }
        }

        [HttpGet("{Companyid}/mobile")]
        public async Task<IActionResult> Getmobile(int Companyid)
        {
            try
            {
                var product = await _companyRepository.GetloginBranch(Companyid);
                return new OkObjectResult(product);
            }
            catch (Exception)
            { throw; }
        }

        [HttpGet("{Companyid}/{BranchId}/{UserName}/{Password}")]
        [Authorize]
        public async Task<IActionResult> Get(int Companyid, int BranchId, string UserName, string Password)
        {
            try
            {
                var product = await _userRepository.Getforlogin(Companyid, BranchId, UserName, Password);
                return new OkObjectResult(product);
            }
            catch (Exception)
            { throw; }
        }


    }
}
