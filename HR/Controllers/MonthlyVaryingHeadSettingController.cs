using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Repository;
using BuildExeHR.Models;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using BuildExeHR.Library;

namespace BuildExeHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonthlyVaryingHeadSettingController : ControllerBase
    {
        private readonly IMonthlyVaryingHeadSettingRepository _monthlyVaryingHeadSettingRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public MonthlyVaryingHeadSettingController(IMonthlyVaryingHeadSettingRepository monthlyVaryingHeadSettingRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            
            _monthlyVaryingHeadSettingRepository = monthlyVaryingHeadSettingRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }
        [HttpGet("{CompanyId}/{BranchId}/{UserId}/{MonthId}/{YearId}")]
        [Authorize]
        public async Task< IActionResult> Get(int CompanyId, int Branchid, int UserId, int MonthId, int YearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _monthlyVaryingHeadSettingRepository.Get(CompanyId, Branchid, UserId, MonthId, YearId,0,0,DateTime.Now,DateTime.Now);
                return new OkObjectResult(purchase);
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


        [HttpGet("{CompanyId}/{BranchId}/{UserId}/{MonthId}/{YearId}/{EmployeeId}/{DurationId}/{FromDate}/{ToDate}")]
        [Authorize]
        public async Task<IActionResult> Get(int CompanyId, int Branchid, int UserId, int MonthId, int YearId, int EmployeeId, int DurationId, DateTime FromDate, DateTime ToDate, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _monthlyVaryingHeadSettingRepository.Get(CompanyId, Branchid, UserId, MonthId, YearId, EmployeeId, DurationId, FromDate, ToDate);
                    return new OkObjectResult(purchase);
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

        [HttpGet("GetUser/{CompanyId}/{BranchId}/{UserId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> GetUser(int CompanyId, int Branchid, int UserId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _monthlyVaryingHeadSettingRepository.GetByUser(CompanyId, Branchid, UserId, FinancialYearId);
                return new OkObjectResult(purchase);
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

        [HttpGet("GetforApproval/{CompanyId}/{BranchId}/{UserId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> Getforapproval(int CompanyId, int Branchid, int UserId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _monthlyVaryingHeadSettingRepository.GetByApproval(CompanyId, Branchid, UserId, FinancialYearId);
                return new OkObjectResult(purchase);
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

        [HttpPost()]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] IEnumerable<MonthlyVaryingHeadSettingsMaster> monthlyVaryingHeadSettingsMasters, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var department = await _monthlyVaryingHeadSettingRepository.Insert(monthlyVaryingHeadSettingsMasters);
                    scope.Complete();
                    return new OkObjectResult(department);

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

        [HttpPut()]
        [Authorize]
        public async Task<IActionResult> PUT([FromBody] IEnumerable<MonthlyVaryingHeadSettingsMaster> monthlyVaryingHeadSettingsMasters, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var department = await _monthlyVaryingHeadSettingRepository.Update(monthlyVaryingHeadSettingsMasters);
                    scope.Complete();
                    return new OkObjectResult(department);

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

        [HttpDelete("{Id}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int Id, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _monthlyVaryingHeadSettingRepository.Delete(Id, UserId);
                return new OkObjectResult(purchase);
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

        [HttpGet("EditDelete/{Id}")]
        [Authorize]
        public async Task<IActionResult> EditDelete(int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _monthlyVaryingHeadSettingRepository.CheckEditDelete(Id);
                return new OkObjectResult(purchase);
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
