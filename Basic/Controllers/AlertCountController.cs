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
    public class AlertCountController : ControllerBase
    {
        private readonly IAlertRepository _alertRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public AlertCountController(IAlertRepository alertRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
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
                    var designation = await _alertRepository.Getcount(userId, Todate, CompanyID, BranchId, 0);
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

        [HttpGet("{userId}/{Todate}/{CompanyID}/{BranchId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> Get(int userId, DateTime Todate, int CompanyID, int BranchId,int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var designation = await _alertRepository.Getcount(userId, Todate, CompanyID, BranchId, FinancialYearId);
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

        [HttpGet("IndividualCount/{AlertType}/{userId}/{Todate}/{CompanyID}/{BranchId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> Get(int AlertType,int userId, DateTime Todate, int CompanyID, int BranchId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var designation = await _alertRepository.GetcountIndividualCount(AlertType,userId, Todate, CompanyID, BranchId, FinancialYearId);
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

        [HttpGet("{userId}/{CompanyID}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> Get(int userId, int CompanyID, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var designation = await _alertRepository.GetcountIonic(userId, CompanyID, BranchId);
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


        [HttpGet("TodaysActivity/{userId}/{Todate}/{CompanyID}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> GettodaysActivity(int userId, DateTime Todate, int CompanyID, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var designation = await _alertRepository.GettodaysActivity(userId, Todate, CompanyID, BranchId);
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


        [HttpPost("ActivityStatus")]
        [Authorize]
        public async Task<IActionResult> ActivityStatus(BasicSearch basicSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var designation = await _alertRepository.ActivityStatus(basicSearch);
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

        [HttpPost("ActivityStatusSearch")]
        [Authorize]
        public async Task<IActionResult> ActivityStatusSearch(BasicSearch basicSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var designation = await _alertRepository.ActivityStatusSearch(basicSearch);
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

        

        [HttpGet("TodaysActivityAdmin/{userId}/{CompanyID}/{BranchId}/{fromdate}/{todate}")]
        [Authorize]
        public async Task<IActionResult> GettodaysActivitys(int userId, int CompanyID, int BranchId,DateTime fromdate, DateTime todate, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var designation = await _alertRepository.GettodaysActivityAdmin(userId, CompanyID, BranchId, fromdate, todate);
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
