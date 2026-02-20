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
    public class MobileDailyNotesController : ControllerBase
    {
        private readonly ITAMasterRepository _attendanceMonthlyRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public MobileDailyNotesController(ITAMasterRepository attendanceMonthlyRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _attendanceMonthlyRepository = attendanceMonthlyRepository;
            _mdHashValidator = mdHashValidator;

        }


        [HttpGet("{CompanyId}/{BranchId}/{UserId}/{FinancialYearId}")]
        public async Task<IActionResult> GetDailyNotes(int CompanyId, int BranchId, int UserId, int FinancialYearId)
        {

                try
                {
                    var department = await _attendanceMonthlyRepository.GetDailyNotes(CompanyId, BranchId, UserId, FinancialYearId);
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
        public async Task<IActionResult> Post([FromBody] IEnumerable<DailyNotesMaster> attendance)
        {

                try
                {
                    var result = await _attendanceMonthlyRepository.InsertDailyNotes(attendance);
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




        [HttpPut]
        public async Task<IActionResult> Put([FromBody] IEnumerable<DailyNotesMaster> attendance)
        {
                try
                {

                    var result = await _attendanceMonthlyRepository.UpdateDailyNotes(attendance);
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
                    await _attendanceMonthlyRepository.DeleteDailyNotes(id, userId);
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
