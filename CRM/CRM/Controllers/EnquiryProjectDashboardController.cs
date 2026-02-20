
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using BuildExeServices.Repository;
using BuildExeServices.Models;
using Microsoft.AspNetCore.Authorization;
using BuildExeServices.Library;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnquiryProjectDashboardController : ControllerBase
    {
        private readonly IWorkEnquiryStageSettingsRepository _workEnquiryStageSettingsRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public EnquiryProjectDashboardController(IWorkEnquiryStageSettingsRepository workEnquiryStageSettings, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _workEnquiryStageSettingsRepository = workEnquiryStageSettings;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpPost("getEnquiryByMonth")]
        [Authorize]
        public async Task<IActionResult> getEnquiryByMonth([FromBody] EnquiryReportSearch billSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var brand = await _workEnquiryStageSettingsRepository.getEnquiryByMonth(billSearch);
                return new OkObjectResult(brand);
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
