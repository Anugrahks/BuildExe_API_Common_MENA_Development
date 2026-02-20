using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
using BuildExeHR.Repository;

using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using BuildExeHR.Library;
namespace BuildExeHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeLabourGroupController : ControllerBase
    {
        private readonly IEmployeeLabourGroupRepository _employeeLabourGroupRepository;
        private readonly IEmployeeListRepository _employeeListRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public EmployeeLabourGroupController(IEmployeeLabourGroupRepository employeeLabourGroupRepository, IEmployeeListRepository employeeListRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _employeeLabourGroupRepository = employeeLabourGroupRepository;
            _employeeListRepository = employeeListRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }
        [HttpGet]
        [Authorize]
        public async Task< IActionResult> Get()
        {
            try
            {
                var designation =await _employeeLabourGroupRepository.Get();
            return new OkObjectResult(designation);
            }
            catch (Exception)
            { throw; }
        }

        [HttpGet("{CompanyId}/{Branchid}/{CategoryId}")]
        [Authorize]
        public async Task<IActionResult> Get(int CompanyId, int Branchid, int CategoryId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var material =await _employeeListRepository.GetByLabourGroup (CompanyId, Branchid, CategoryId);
            return new OkObjectResult(material);
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
