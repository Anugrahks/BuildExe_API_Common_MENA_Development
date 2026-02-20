using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
using System.Transactions;
using BuildExeBasic.Repository;
using Microsoft.AspNetCore.Authorization;
using BuildExeBasic.Library;

namespace BuildExeBasic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertfewController : ControllerBase
    {
        private readonly IAlertRepository _alertRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public AlertfewController(IAlertRepository alertRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _alertRepository = alertRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }
        [HttpGet("{userId}/{Todate}/{CompanyID}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> Get(int userId, DateTime Todate, int CompanyID, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var designation = await _alertRepository.Getfew(userId, Todate, CompanyID, BranchId);
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

        [HttpGet("ForClosing/{CompanyId}/{BranchId}/{FinancialYearId}")]
        public async Task<IActionResult> GetAlertForClosing(int CompanyId, int BranchId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
                try
                {
                    var designation = await _alertRepository.GetAlertForClosing(CompanyId, BranchId, FinancialYearId);
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

    }
}
