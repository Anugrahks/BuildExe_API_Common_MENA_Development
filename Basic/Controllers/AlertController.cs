using BuildExeBasic.Repository;
using Microsoft.AspNetCore.Authorization;
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
    public class AlertController : ControllerBase
    {
        private readonly IAlertRepository _alertRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public AlertController(IAlertRepository alertRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
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
                    var designation = await _alertRepository.Get(userId, Todate, CompanyID, BranchId);
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
        [HttpGet("{userId}/{Todate}/{CompanyID}/{BranchId}/{Type}")]
        [Authorize]
        public async Task<IActionResult> Get(int userId, DateTime Todate, int CompanyID, int BranchId, int Type, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    object designation;

                    if (Type == 3)
                    {
                        designation = await _alertRepository.GetwithTypeThree(userId, Todate, CompanyID, BranchId, Type,0);
                    }
                    else
                    {
                        designation = await _alertRepository.GetwithType(userId, Todate, CompanyID, BranchId, Type);
                    }

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

        [HttpGet("Reminders/{userId}/{Todate}/{CompanyID}/{BranchId}/{Type}/{Forms}")]
        [Authorize]
        public async Task<IActionResult> Get(int userId, DateTime Todate, int CompanyID, int BranchId, int Type,int Forms, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {

                    var designation = await _alertRepository.GetwithTypeThree(userId, Todate, CompanyID, BranchId, Type, Forms);
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
