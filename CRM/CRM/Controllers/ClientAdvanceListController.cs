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
    public class ClientAdvanceListController : ControllerBase
    {
        private readonly IClientAdvanceRepository _clientAdvanceRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public ClientAdvanceListController(IClientAdvanceRepository clientAdvanceRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _clientAdvanceRepository = clientAdvanceRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }
        [HttpGet("{Companyid}/{Branchid}")]
        [Authorize]
        public async Task<IActionResult> Get(int Companyid, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase =await  _clientAdvanceRepository.GetforEdit (Companyid, BranchId);
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

        [HttpGet("getuser/{Companyid}/{Branchid}/{UserId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> Getbyid(int Companyid, int BranchId, int UserId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                {
                    try
            {
                var purchase = await _clientAdvanceRepository.GetforEdituser(Companyid, BranchId, UserId, FinancialYearId);
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

        [HttpGet("{Companyid}/{Branchid}/{UserId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> Get(int Companyid, int BranchId,int UserId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
                {
                    if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                    {
                        try
            {

                var purchase = await _clientAdvanceRepository.Getforapproval(Companyid, BranchId, UserId, FinancialYearId);
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
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] BillSearch billSearch, [FromHeader] string mdhash, [FromHeader] int User)
                    {
                        if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                        {
                            try
            {
                if (billSearch != null)
            {
                var product = await _clientAdvanceRepository.GetReport (billSearch);

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
    }
}
