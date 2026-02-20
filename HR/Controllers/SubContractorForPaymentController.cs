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
    public class SubContractorForPaymentController : ControllerBase
    {
        private readonly ISubContractorForPaymentRepository _subContractorPaymentRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public SubContractorForPaymentController(ISubContractorForPaymentRepository subContractorPaymentRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _subContractorPaymentRepository = subContractorPaymentRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        // GET: api/<TeamController>
        [HttpGet("{Employeeid}/{sitemanagerid}/{financialyearId}")]
        [Authorize]
        public async Task<IActionResult> Get(int Employeeid, int sitemanagerid,int financialyearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase =await _subContractorPaymentRepository.Get(Employeeid, sitemanagerid, financialyearId);
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
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase =await _subContractorPaymentRepository.Get(id);
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
