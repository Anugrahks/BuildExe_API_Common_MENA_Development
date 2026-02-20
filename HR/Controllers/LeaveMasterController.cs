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
    public class LeaveMasterController : ControllerBase
    {
        private readonly ILeaveMasterRepository _leaveMasterRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public LeaveMasterController(ILeaveMasterRepository leaveMasterRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _leaveMasterRepository = leaveMasterRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;

        }
        [HttpGet("{companyid}/{Branchid}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> Get(int companyid, int branchid, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var designation = await _leaveMasterRepository.Getleave(companyid, branchid, UserId);
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
            else
            {
                return Unauthorized("Invalid MdHash");
            }
        }

        [HttpGet("WithHoliday/{companyid}/{Branchid}/{EmployeeId}")]
        [Authorize]
        public async Task<IActionResult> Getwithholiday(int companyid, int branchid, int EmployeeId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var designation = await _leaveMasterRepository.Getleavewithholiday(companyid, branchid, EmployeeId,DateTime.Now);
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
            else
            {
                return Unauthorized("Invalid MdHash");
            }
        }


        [HttpGet("WithHoliday/{companyid}/{Branchid}/{EmployeeId}/{DateWorked}")]
        [Authorize]
        public async Task<IActionResult> Getwithholiday(int companyid, int branchid, int EmployeeId,DateTime DateWorked, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var designation = await _leaveMasterRepository.Getleavewithholiday(companyid, branchid, EmployeeId, DateWorked);
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
            else
            {
                return Unauthorized("Invalid MdHash");
            }
        }

        [HttpGet("Monthly/{companyid}/{Branchid}/{EmployeeId}")]
        [Authorize]
        public async Task<IActionResult> Getwithemployee(int companyid, int branchid, int EmployeeId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var designation = await _leaveMasterRepository.Getleavewithemployee(companyid, branchid, EmployeeId,0,0);
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
            else
            {
                return Unauthorized("Invalid MdHash");
            }
        }

        [HttpGet("Monthly/{companyid}/{Branchid}/{EmployeeId}/{MonthId}/{YearId}")]
        [Authorize]
        public async Task<IActionResult> Getwithemployee(int companyid, int branchid, int EmployeeId,int MonthId, int YearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var designation = await _leaveMasterRepository.Getleavewithemployee(companyid, branchid, EmployeeId, MonthId, YearId);
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
            else
            {
                return Unauthorized("Invalid MdHash");
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
                var department = await _leaveMasterRepository.GetByID(id);
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



        [HttpPost]
        [Authorize]

        public async Task<IActionResult> Post([FromBody] LeaveMaster leaveMaster, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var leave = await _leaveMasterRepository.Insert(leaveMaster);
                    scope.Complete();
                    return new OkObjectResult(leave);
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




        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] LeaveMaster leaveMaster, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (leaveMaster != null)
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var leave = await _leaveMasterRepository.Update(leaveMaster);
                        scope.Complete();
                        return new OkObjectResult(leave);
                    }
                }
                return new NoContentResult();
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



        [HttpDelete("{id}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var delete = await _leaveMasterRepository.Delete(id, UserId);
                return new OkObjectResult(delete);
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

        [HttpPost("SalaryPerDay")]
        [Authorize]
        public async Task<IActionResult> SalaryPerDay([FromBody] LeaveSettingsDet leaveSettings, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var delete = await _leaveMasterRepository.SalaryPerDay(leaveSettings);
                return new OkObjectResult(delete);
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
        [HttpGet("EditDelete/{id}/{branchId}")]
        [Authorize]
        public async Task<IActionResult> EditDelete(int id, int branchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _leaveMasterRepository.CheckEditDelete(id, branchId);
                return new OkObjectResult(result);
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

        [HttpGet("AutoFetch/{branchId}")]
        [Authorize]
        public async Task<IActionResult> AutoFetch(int branchId,[FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var result = await _leaveMasterRepository.AutoFetch( branchId);
                    return new OkObjectResult(result);
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


        [HttpGet("MonthlyMobile/{companyid}/{Branchid}/{EmployeeId}")]
        public async Task<IActionResult> GetwithemployeeMobileApp(int companyid, int branchid, int EmployeeId)
        {
            try
            {
                var designation = await _leaveMasterRepository.GetleavewithemployeeMobile(companyid, branchid, EmployeeId, 0, 0);

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




        [HttpPost("SalaryPerDayMobile")]
        public async Task<IActionResult> SalaryPerDayMobile([FromBody] LeaveSettingsDet leaveSettings)
        {
            try
            {
                var delete = await _leaveMasterRepository.SalaryPerDayMobile(leaveSettings);
                return new OkObjectResult(delete);
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







    }
}
