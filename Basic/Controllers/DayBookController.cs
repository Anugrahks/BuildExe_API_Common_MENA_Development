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
using System.Security.Policy;

namespace BuildExeBasic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DayBookController : ControllerBase
    {
        private readonly IDayBookRepository _dayBookRepository  ;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public DayBookController(IDayBookRepository dayBookRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _dayBookRepository = dayBookRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;

        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] BasicSearch basicSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (basicSearch != null)
                {
                    if(basicSearch.IsDetail == 1)
                    {
                        var product = await _dayBookRepository.GetForSummaryReport(basicSearch);
                        return new OkObjectResult(product);
                    }
                    else if (basicSearch.IsDetail == 2)
                    {
                        var product = await _dayBookRepository.GetForSummaryandDetailReport(basicSearch);
                        return new OkObjectResult(product);
                    }
                    else
                    {
                        var product = await _dayBookRepository.GetForReport(basicSearch);
                        return new OkObjectResult(product);
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

        [HttpPost("FundFlow")]
        [Authorize]
        public async Task<IActionResult> DayBookPost([FromBody] BasicSearch basicSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
                if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                {
                    try
            {
                if (basicSearch != null)
                {
                    if (basicSearch.IsDetail == 1)
                    {
                        var product = await _dayBookRepository.GetForFundFlowReportSummary(basicSearch);
                        return new OkObjectResult(product);
                    }
                    else if(basicSearch.IsDetail ==2)
                        {
                            var product = await _dayBookRepository.GetForFundFlowReportCredit(basicSearch);
                            return new OkObjectResult(product);
                        }    
                    else
                        {
                            var product = await _dayBookRepository.GetForFundFlowReport(basicSearch);
                            return new OkObjectResult(product);
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
    }
}
