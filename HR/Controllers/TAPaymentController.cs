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
    public class TAPaymentController : ControllerBase
    {
        private readonly ITAMasterRepository _attendanceMonthlyRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public TAPaymentController(ITAMasterRepository attendanceMonthlyRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _attendanceMonthlyRepository = attendanceMonthlyRepository;
            _mdHashValidator = mdHashValidator;

        }


        [HttpGet("GetShow/{Id}/{EmployeeId}/{BranchId}/{FinancialYearId}/{FromDate}/{ToDate}")]
        [Authorize]
        public async Task<IActionResult> GetShow(int Id,int EmployeeId,int BranchId,int FinancialYearId,DateTime FromDate,DateTime ToDate,  [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var department = await _attendanceMonthlyRepository.GetShowPayment(Id, EmployeeId, BranchId, FinancialYearId, FromDate, ToDate);
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

        [HttpGet("GetShow/{Id}/{EmployeeId}/{BranchId}/{FinancialYearId}/{ToDate}")]
        [Authorize]
        public async Task<IActionResult> GetShow(int Id, int EmployeeId, int BranchId, int FinancialYearId, DateTime ToDate, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var department = await _attendanceMonthlyRepository.GetShowPayment(Id, EmployeeId, BranchId, FinancialYearId, DateTime.Now, ToDate);
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
        public async Task<IActionResult> Post([FromBody] IEnumerable<TAPayment> attendance, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var result = await _attendanceMonthlyRepository.InsertTAPayment(attendance);
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


        [HttpGet("GetforEdit/{CompanyId}/{BranchId}/{UserId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> GetforEdit(int companyId, int BranchId, int userID, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var department = await _attendanceMonthlyRepository.GetforEditTAPayment(companyId, BranchId, userID, FinancialYearId);
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




        [HttpGet("GetforApproval/{CompanyId}/{BranchId}/{UserId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> GetforApproval(int companyId, int BranchId, int userID, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var department = await _attendanceMonthlyRepository.GetforApprovalTAPayment(companyId, BranchId, userID, FinancialYearId);
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


        [HttpPost("Report")]
        [Authorize]
        public async Task<IActionResult> IndividualReport(HRSearch hRSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var department = await _attendanceMonthlyRepository.IndividualReport(hRSearch);
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
        public async Task<IActionResult> Put([FromBody] IEnumerable<TAPayment> attendance, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {

                    var result = await _attendanceMonthlyRepository.UpdateTAPayment(attendance);
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
        public async Task<IActionResult> Delete(int id, int userId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    await _attendanceMonthlyRepository.DeleteTAPayment(id, userId);
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

    }
}
