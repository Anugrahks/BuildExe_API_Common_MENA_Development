using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Repository;
using BuildExeHR.Models;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using BuildExeHR.Library;

namespace BuildExeHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabourWagePaymentListController : ControllerBase
    {
        private readonly ILabourWagePaymentRepository _labourWagePaymentRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public LabourWagePaymentListController(ILabourWagePaymentRepository labourWagePaymentRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _labourWagePaymentRepository = labourWagePaymentRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }
        [HttpGet("details/{id}/{LabourGroupId}")]
        [Authorize]
        public async Task<IActionResult> Getdetails(int id, int LabourGroupId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _labourWagePaymentRepository.GetDetailsbyid(id, LabourGroupId);
                return new OkObjectResult(purchase);
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
        [HttpGet("{companyid}/{branchid}")]
        [Authorize]
        public async Task<IActionResult> Get(int companyid, int branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _labourWagePaymentRepository.GetforEdit(companyid, branchid);
                return new OkObjectResult(purchase);
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

        [HttpGet("getuser/{companyid}/{branchid}/{UserId}/{IsBulk}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> Getbyuser(int companyid, int branchid, int UserId, int IsBulk, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _labourWagePaymentRepository.GetforEdituser(companyid, branchid, UserId, IsBulk,  FinancialYearId);
                return new OkObjectResult(purchase);
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


        [HttpPost("Report")]
        [Authorize]
        public async Task<IActionResult> Report([FromBody] HRSearch hRSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (hRSearch != null)
                {
                    var product = await _labourWagePaymentRepository.Getjson(hRSearch);
                    return new OkObjectResult(product);
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

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] HRSearch hRSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (hRSearch != null)
                {
                    var product = await _labourWagePaymentRepository.Getforview(hRSearch);
                    return new OkObjectResult(product);
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


        [HttpGet("{companyid}/{branchid}/{Userid}/{IsBulk}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> Get(int companyid, int branchid, int userid, int IsBulk, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _labourWagePaymentRepository.Getforapproval(companyid, branchid, userid, IsBulk, FinancialYearId);
                return new OkObjectResult(purchase);
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
