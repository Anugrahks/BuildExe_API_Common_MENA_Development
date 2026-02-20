using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
using BuildExeHR.Repository;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using BuildExeHR.Library;

namespace BuildExeHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanBalanceController : ControllerBase
    {
        private readonly ILoanBalanceRepository _loanBalanceRepository  ;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public LoanBalanceController(ILoanBalanceRepository loanBalanceRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _loanBalanceRepository = loanBalanceRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }
        [HttpGet("{CompanyId}/{Branchid}/{EmployeeId}")]
        [Authorize]
        public async Task<IActionResult> Get(int CompanyId, int Branchid, int EmployeeId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {

                var brand =await _loanBalanceRepository.Get(CompanyId, Branchid, EmployeeId);
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
        [HttpGet("Balance/{CompanyId}/{Branchid}/{EmployeeId}")]
        [Authorize]
        public async Task<IActionResult> GetBalance(int CompanyId, int Branchid, int EmployeeId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {


                var brand = await _loanBalanceRepository.GetBalance( CompanyId, Branchid, EmployeeId);
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
