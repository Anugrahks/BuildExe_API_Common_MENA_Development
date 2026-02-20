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
using System.Security.Policy;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeBasic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountTypeController : ControllerBase
    {
        private readonly IAccountTypeRepository _accountTypeRepository  ;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public AccountTypeController(IAccountTypeRepository accountTypeRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _accountTypeRepository = accountTypeRepository;
                _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
           
        }

        [HttpGet()]
        [Authorize]

        public async Task < IActionResult> Get()
        {
                try
            {
                var designation = await _accountTypeRepository.Get();
                return new OkObjectResult(designation);
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


        [HttpGet("{id}")]
        [Authorize]

        public async Task<IActionResult> Get(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var department = await _accountTypeRepository.GetByID(id);
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
