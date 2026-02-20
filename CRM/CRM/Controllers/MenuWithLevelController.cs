using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BuildExeServices.Repository;
using BuildExeServices.Models;
using Microsoft.AspNetCore.Authorization;
using BuildExeServices.Library;
namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuWithLevelController : ControllerBase
    {
        private readonly ILevelRepository _levelRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public MenuWithLevelController(ILevelRepository levelRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _levelRepository = levelRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }
        [HttpGet("{Companyid}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> Get(int Companyid, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product =  await _levelRepository.Getmenuwithlevel(Companyid, BranchId);
            return new OkObjectResult(product);
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
