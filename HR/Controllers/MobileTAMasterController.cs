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
    public class MobileTAMasterController : ControllerBase
    {
        private readonly ITAMasterRepository _attendanceMonthlyRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public MobileTAMasterController(ITAMasterRepository attendanceMonthlyRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _attendanceMonthlyRepository = attendanceMonthlyRepository;
            _mdHashValidator = mdHashValidator;

        }


        [HttpGet("{CompanyId}/{BranchId}")]
        public async Task<IActionResult> Get(int CompanyId, int BranchId)
        {
                try
                {
                    var department = await _attendanceMonthlyRepository.Get(CompanyId, BranchId);
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

    }
}
