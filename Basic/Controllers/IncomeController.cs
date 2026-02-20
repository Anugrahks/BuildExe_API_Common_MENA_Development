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
    public class IncomeController : ControllerBase
    {
        private readonly IExpenseIncomeRepository _expenseIncomeRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public IncomeController(IExpenseIncomeRepository expenseIncomeRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _expenseIncomeRepository = expenseIncomeRepository;
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

                    if ((basicSearch.IsDetail is null) || (basicSearch.IsDetail == 0))
                    {
                        var product = await _expenseIncomeRepository.IncomeReport(basicSearch);
                        return new OkObjectResult(product);
                    }
                    else
                    {
                        var product = await _expenseIncomeRepository.IncomeDetailReport(basicSearch);
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

        [HttpGet("Cash/{CompanyId}/{Branchid}")]
        [Authorize]
        public async Task<IActionResult> GetCash(int CompanyId, int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
            {
                if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                {
                    try
            {
                var designation = await _expenseIncomeRepository.cashbalance(CompanyId, Branchid, 0,0);
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



        [HttpGet("Cash/{CompanyId}/{Branchid}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> GetCash(int CompanyId, int Branchid,int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var designation = await _expenseIncomeRepository.cashbalance(CompanyId, Branchid, UserId,0);
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


        [HttpGet("Cash/{CompanyId}/{Branchid}/{UserId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> GetCash(int CompanyId, int Branchid, int UserId,int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var designation = await _expenseIncomeRepository.cashbalance(CompanyId, Branchid, UserId, FinancialYearId);
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

        [HttpGet("Bank/{CompanyId}/{Branchid}/{FinancialYearid}")]
        [Authorize]
        public async Task<IActionResult> GetBank(int CompanyId, int Branchid,int FinancialYearid, [FromHeader] string mdhash, [FromHeader] int User)
                {
                    if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                    {
                        try
            {
                var designation = await _expenseIncomeRepository.bankbalance(CompanyId, Branchid, FinancialYearid);
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

        [HttpGet("Bank/{CompanyId}/{Branchid}")]
        [Authorize]
        public async Task<IActionResult> GetBank(int CompanyId, int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var designation = await _expenseIncomeRepository.bankbalance(CompanyId, Branchid,0);
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


        [HttpGet("Bank/{CompanyId}/{Branchid}/{FinancialYearId}/{FromDate}/{ToDate}")]
        [Authorize]
        public async Task<IActionResult> GetBank(int CompanyId, int Branchid,int FinancialYearId, DateTime FromDate, DateTime ToDate, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var designation = await _expenseIncomeRepository.bankbalancereciept(CompanyId, Branchid, FinancialYearId, FromDate, ToDate);
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


        [HttpGet("OD/{CompanyId}/{Branchid}")]
        [Authorize]
        public async Task<IActionResult> GetOD(int CompanyId, int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
                    {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
              try
            {
                var designation = await _expenseIncomeRepository.odbalance(CompanyId, Branchid,0);
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

        [HttpGet("OD/{CompanyId}/{Branchid}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> GetOD(int CompanyId, int Branchid,int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var designation = await _expenseIncomeRepository.odbalance(CompanyId, Branchid, FinancialYearId);
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


        [HttpPost("Report")]
        [Authorize]
        public async Task<IActionResult> IncomeReportNew([FromBody] BasicSearch basicSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var designation = await _expenseIncomeRepository.IncomeReportNew(basicSearch);
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
