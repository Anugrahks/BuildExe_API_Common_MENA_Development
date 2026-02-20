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
    public class AttendancePayRollController : ControllerBase
    {
        private readonly IAttendancePayRollRepository _attendancePayrollRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public AttendancePayRollController(IAttendancePayRollRepository attendancepayrollRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _attendancePayrollRepository = attendancepayrollRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }


        [HttpPost]
        [Authorize]

        public async Task<IActionResult> Post([FromBody] IEnumerable<AttendancePayroll> attendance, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {

                var department = await _attendancePayrollRepository.Insert(attendance);
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




        [HttpPut]
        [Authorize]

        public async Task<IActionResult> Put([FromBody] IEnumerable<AttendancePayroll> attendance, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (attendance != null)
                {

                    var department = await _attendancePayrollRepository.Update(attendance);
                    return new OkObjectResult(department);
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
                await _attendancePayrollRepository.Delete(id, UserId);
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


        [HttpGet("{id}/{EmployeeId}/{Isdetail}")]
        [Authorize]

        public async Task<IActionResult> getforedit(int id,int EmployeeId, int Isdetail, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
               var attendanc= await _attendancePayrollRepository.GetPayRollAttendanceDetail(id, EmployeeId, Isdetail);
                return new OkObjectResult(attendanc);
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

        [HttpPost("Payroll")]
        [Authorize]
        public async Task<IActionResult> PayrollPost([FromBody] AttendancePayrollList attendanceList, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (attendanceList != null)
                {
                    var product = await _attendancePayrollRepository.GetPayRollAttendancebyid(attendanceList);
                    return new OkObjectResult(product);
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

        [HttpGet("validation/{dateworked}/{companyid}/{branchid}/{departmentId}")]
        [Authorize]

        public async Task<IActionResult> getforedit(DateTime dateworked, int companyid, int branchid, int departmentId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var attendanc = await _attendancePayrollRepository.Getvalidationpayroll(dateworked, companyid, branchid, departmentId);
                return new OkObjectResult(attendanc);
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


        [HttpGet("validation/{dateworked}/{companyid}/{branchid}")]
        [Authorize]

        public async Task<IActionResult> getforedit(DateTime dateworked, int companyid, int branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var attendanc = await _attendancePayrollRepository.Getvalidationpayroll(dateworked, companyid, branchid, 0);
                    return new OkObjectResult(attendanc);
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

        [HttpGet("LatePenaltyDetails/{dateworked}/{EmployeeId}")]
        [Authorize]

        public async Task<IActionResult> LatePenaltyDetails(DateTime dateworked, int EmployeeId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var attendanc = await _attendancePayrollRepository.LatePenaltyDetails(dateworked, EmployeeId);
                    return new OkObjectResult(attendanc);
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
