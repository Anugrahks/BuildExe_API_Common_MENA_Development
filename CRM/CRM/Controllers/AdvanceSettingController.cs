using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
using BuildExeServices.Repository;

using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using BuildExeServices.Library;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvanceSettingController : ControllerBase
    {
        private readonly ICancellationRepository _cancellationRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public AdvanceSettingController(ICancellationRepository cancellationRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _cancellationRepository = cancellationRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;

        }



        [HttpGet("{BranchId}")]
        [Authorize]
        public async Task<IActionResult> AdvanceSetting(int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product = await _cancellationRepository.AdvanceSetting(BranchId);
                return new OkObjectResult(product);
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

        [HttpGet("{BranchId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> AdvanceSettingFinancialYear(int BranchId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product = await _cancellationRepository.AdvanceSettingFinancialYear(BranchId, FinancialYearId);
                return new OkObjectResult(product);
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
        public async Task<IActionResult> Put([FromBody] AdvanceSetting advanceSetting, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {

                var res = await _cancellationRepository.Put(advanceSetting);
                return new OkObjectResult(res);

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
