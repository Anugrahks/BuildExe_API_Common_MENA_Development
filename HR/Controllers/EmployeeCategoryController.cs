using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
using System.Transactions;
using BuildExeHR.Repository;
using Microsoft.AspNetCore.Authorization;
using BuildExeHR.Library;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeCategoryController : ControllerBase
    {
        private readonly IEmployeeCategoryRepository _employeeCategoryRepository  ;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public EmployeeCategoryController(IEmployeeCategoryRepository employeeCategoryRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _employeeCategoryRepository = employeeCategoryRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
           
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var department = await _employeeCategoryRepository.Get();
            return new OkObjectResult(department);
            }
            catch (Exception)
            { throw; }
        }

        [HttpGet("Advance")]
        [Authorize]
        public async Task<IActionResult> GetAdvance()
        {
            try
            {
                var department = await _employeeCategoryRepository.GetAdvance();
                return new OkObjectResult(department);
            }
            catch (Exception)
            { throw; }
        }


        [HttpPost]
        [Authorize]

        public async Task<IActionResult> Post([FromBody] EmployeeCategory  employeeCategory, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await _employeeCategoryRepository.Insert(employeeCategory);
                    scope.Complete();
                    return new OkResult();


                }
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

        [HttpGet("{CompanyId}/{Branchid}/{CategoryId}")]
        [Authorize]
        public async Task<IActionResult> Get(int CompanyId, int Branchid, int CategoryId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var material = await _employeeCategoryRepository.GetbyCategoryPersonal(CompanyId, Branchid, CategoryId);
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
