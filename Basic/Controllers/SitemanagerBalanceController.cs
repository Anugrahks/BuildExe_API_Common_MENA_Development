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
    public class SitemanagerBalanceController : ControllerBase
    {
        private readonly ISitemanagerRepository _sitemanagerRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public SitemanagerBalanceController(ISitemanagerRepository sitemanagerRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _sitemanagerRepository = sitemanagerRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpGet("{sitemanagerId}/{FinancialYearID}")]
        [Authorize]
        public IActionResult Get(int sitemanagerId, int FinancialYearID, [FromHeader] string mdhash, [FromHeader] int User)
        {
                try
            {
                var designation = _sitemanagerRepository.SitemanagerBalance (sitemanagerId, FinancialYearID);
            return new OkObjectResult(designation);
            }
            catch (Exception)
            { throw; }
        }

        [HttpGet("{sitemanagerId}/{CompanyId}/{BranchId}/{FinancialYearID}")]
        [Authorize]
        public async Task<IActionResult> Get(int sitemanagerId, int CompanyId, int BranchId, int FinancialYearID, [FromHeader] string mdhash, [FromHeader] int User)
         {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
            try
            {
                var designation = await _sitemanagerRepository.SitemanagerBalance_Final(sitemanagerId, CompanyId, BranchId, FinancialYearID);
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

    }
}
