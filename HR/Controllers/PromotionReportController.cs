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
    public class PromotionReportController : ControllerBase
    {
        private readonly IPromotionReportRepository _promotionReportRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public PromotionReportController(IPromotionReportRepository promotionReportRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _promotionReportRepository = promotionReportRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpPost("Promotion")]
        [Authorize]
        public async Task<IActionResult> PostPromotion([FromBody] HRSearch hRSearchs, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    if (hRSearchs != null)
                    {
                        var report = await _promotionReportRepository.Promotion(hRSearchs);
                        return new OkObjectResult(report);
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
    }
}
