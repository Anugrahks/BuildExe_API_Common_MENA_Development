using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BuildExeServices.Models;
using BuildExeServices.Repository;

using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using BuildExeServices.Library;

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadController : ControllerBase
    {
        private readonly IFacebookRepository _facebookRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public LeadController(IFacebookRepository facebookRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _facebookRepository = facebookRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpGet("insights/{adAccountId}/{accessToken}")]
        public async Task<IActionResult> GetAdAccountInsights(string adAccountId, string accessToken, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var result = await _facebookRepository.GetAdAccountInsights(adAccountId, accessToken);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new
                    {
                        message = $"An error occurred: {ex.Message}",
                        statusCode = 0
                    });
                }
            }
            else
            {
                return Unauthorized("Invalid MdHash");
            }
        }
    }
}
