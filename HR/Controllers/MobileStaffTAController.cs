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
    public class MobileStaffTAController : ControllerBase
    {
        private readonly ITAMasterRepository _attendanceMonthlyRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public MobileStaffTAController(ITAMasterRepository attendanceMonthlyRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _attendanceMonthlyRepository = attendanceMonthlyRepository;
            _mdHashValidator = mdHashValidator;

        }


        [HttpGet("GetById/{Id}")]

        public async Task<IActionResult> Get(int Id)
        {
                try
                {
                    var department = await _attendanceMonthlyRepository.GetById(Id);
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


        [HttpPost]

        public async Task<IActionResult> Post([FromBody] IEnumerable<StaffTA> attendance)
        {
                try
                {
                    var result = await _attendanceMonthlyRepository.InsertStaffTA(attendance);
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


        [HttpGet("GetforEdit/{CompanyId}/{BranchId}/{UserId}/{FinancialYearId}")]

        public async Task<IActionResult> GetforEdit(int companyId, int BranchId, int userID, int FinancialYearId)
        {
                try
                {
                    var department = await _attendanceMonthlyRepository.GetforEdit(companyId, BranchId, userID, FinancialYearId);
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

        [HttpGet("GetforApproval/{CompanyId}/{BranchId}/{UserId}/{FinancialYearId}")]

        public async Task<IActionResult> GetforApproval(int companyId, int BranchId, int userID, int FinancialYearId)
        {

                try
                {
                    var department = await _attendanceMonthlyRepository.GetforApproval(companyId, BranchId, userID, FinancialYearId);
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



        [HttpPut]

        public async Task<IActionResult> Put([FromBody] IEnumerable<StaffTA> attendance)
        {

                try
                {

                    var result = await _attendanceMonthlyRepository.UpdateStaffTA(attendance);
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



        [HttpDelete("{id}/{UserID}")]

        public async Task<IActionResult> Delete(int id, int userId)
        {

                try
                {
                    await _attendanceMonthlyRepository.DeleteStaffTA(id, userId);
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

    }
}
