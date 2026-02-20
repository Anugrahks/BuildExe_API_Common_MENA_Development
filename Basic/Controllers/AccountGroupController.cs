using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
using System.Transactions;
using BuildExeBasic.Repository;
using Microsoft.AspNetCore.Authorization;
using BuildExeBasic.Library;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeBasic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountGroupController : ControllerBase
    {
        private readonly IAccountGroupRepository _accountGroupRepository  ;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public AccountGroupController(IAccountGroupRepository accountGroupRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _accountGroupRepository = accountGroupRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;

        }

        [HttpGet()]
        [Authorize]

        public async Task<IActionResult> Get()
        {
            try
            {
                var designation = await _accountGroupRepository.Get();
                return new OkObjectResult(designation);
            }
            catch (Exception )
            { throw; }
        }


        [HttpGet("{id}")]
        [Authorize]

        public async Task<IActionResult> Get(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var department = await _accountGroupRepository.GetByID(id);
                return new OkObjectResult(department);
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
