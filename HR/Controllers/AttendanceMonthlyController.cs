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
    public class AttendanceMonthlyController : ControllerBase
    {
        private readonly IAttendanceMonthlyRepository _attendanceMonthlyRepository  ;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public AttendanceMonthlyController(IAttendanceMonthlyRepository attendanceMonthlyRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _attendanceMonthlyRepository = attendanceMonthlyRepository;
            _mdHashValidator = mdHashValidator;

        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try { 
            var designation = await _attendanceMonthlyRepository.Get();
            return new OkObjectResult(designation);
            }
            catch (Exception)
            { throw; }
        }


        [HttpGet("detailsbyid/{id}/{employeeid}/{financialyearid}")]
        [Authorize]
        public async Task<IActionResult> Get(int id, int employeeid, int financialyearid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var department =await _attendanceMonthlyRepository.GetDetailsbyid (id, employeeid, financialyearid);
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

        [HttpGet("leavedetails/{id}/{employeeid}")]
        [Authorize]
        public async Task<IActionResult> Getleave(int id, int employeeid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var department = await _attendanceMonthlyRepository.Getleavebyid(id, employeeid);
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

        [HttpGet("show/{monthid}/{companyid}/{branchid}/{financialyearid}")]
        [Authorize]
        public async Task<IActionResult> Show(int monthid, int companyid, int branchid, int financialyearid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var department = await _attendanceMonthlyRepository.Showdetails( monthid,2025,  companyid,  branchid, financialyearid,0,DateTime.Now,DateTime.Now, 0);
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


        [HttpGet("show/{monthid}/{yearId}/{companyid}/{branchid}/{financialyearid}")]
        [Authorize]
        public async Task<IActionResult> Show(int monthid,int yearId, int companyid, int branchid, int financialyearid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var department = await _attendanceMonthlyRepository.Showdetails(monthid, yearId, companyid, branchid, financialyearid,0,DateTime.Now,DateTime.Now,0);
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

        [HttpGet("show/{monthid}/{yearId}/{companyid}/{branchid}/{financialyearid}/{DurationId}/{FromDate}/{ToDate}/{EmployeeId}")]
        [Authorize]
        public async Task<IActionResult> Show(int monthid, int yearId, int companyid, int branchid, int financialyearid,int DurationId,DateTime FromDate,DateTime ToDate, int EmployeeId , [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var department = await _attendanceMonthlyRepository.Showdetails(monthid, yearId, companyid, branchid, financialyearid, DurationId, FromDate, ToDate, EmployeeId);
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

        [HttpGet("getuser/{CompanyId}/{BranchId}/{MenuId}/{UserId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> Getforedit(int companyId, int BranchId ,int MenuId, int userID, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var department =await _attendanceMonthlyRepository.GetforEdit (companyId, BranchId, MenuId, userID, FinancialYearId);
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

        [HttpGet("{CompanyId}/{BranchId}/{UserId}/{MenuId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> Get(int companyId, int BranchId,int userID, int MenuId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var department = await _attendanceMonthlyRepository.GetforApproval(companyId, BranchId, userID, MenuId, FinancialYearId);
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
        public async Task<IActionResult> Post([FromBody] IEnumerable<AttendanceMonthly > attendance, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                   var result= await _attendanceMonthlyRepository.Insert(attendance);
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




        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] IEnumerable<AttendanceMonthly > attendance, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {

                       var result = await _attendanceMonthlyRepository.Update(attendance);
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



        [HttpDelete("{id}/{UserID}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id,int userId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
               await _attendanceMonthlyRepository.Delete(id, userId);
            return new OkResult();
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


        [HttpGet("validation/{monthid}/{financialyearid}/{branchid}/{Durationid}/{FromDate}/{ToDate}/{employeeId}")]
        [Authorize]
        public async Task<IActionResult> validatioons(int monthid, int financialyearid, int branchid, int DurationId, DateTime FromDate, DateTime ToDate, int employeeId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result= await _attendanceMonthlyRepository.Datevalidation(monthid, financialyearid, branchid, DurationId, FromDate, ToDate, employeeId);
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


        [HttpGet("validation/{monthid}/{financialyearid}/{branchid}")]
        [Authorize]
        public async Task<IActionResult> validatioons(int monthid, int financialyearid, int branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var result = await _attendanceMonthlyRepository.Datevalidation(monthid, financialyearid, branchid,0,DateTime.Now,DateTime.Now,0);
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
    }
}
