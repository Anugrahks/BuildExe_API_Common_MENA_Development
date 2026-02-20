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
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeBasic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SitemanagerController : ControllerBase
    {
        private readonly ISitemanagerRepository _sitemanagerRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public SitemanagerController(ISitemanagerRepository sitemanagerRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _sitemanagerRepository = sitemanagerRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpGet("{CompanyId}/{Branchid}")]
        [Authorize]

        public async Task<IActionResult> Get(int CompanyId, int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var designation = await _sitemanagerRepository.Get(CompanyId, Branchid);
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
        [HttpGet("{CompanyId}/{Branchid}/{Transactiontype}")]
        [Authorize]

        public async Task<IActionResult> Get(int CompanyId, int Branchid, int Transactiontype, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var designation = await _sitemanagerRepository.GetForEdit(CompanyId, Branchid, Transactiontype);
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

        [HttpGet("getuser/{CompanyId}/{Branchid}/{Transactiontype}/{UserId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> Getbyid(int CompanyId, int Branchid, int Transactiontype, int UserId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var designation = await _sitemanagerRepository.GetForEdituser(CompanyId, Branchid, Transactiontype, UserId, FinancialYearId);
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

        [HttpGet("getforApproval/{CompanyId}/{Branchid}/{Transactiontype}/{userid}/{FinancialYearId}")]
        [Authorize]

        public async Task<IActionResult> Get(int CompanyId, int Branchid, int Transactiontype,int userid, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try { 
            var designation = await _sitemanagerRepository.GetForapproval(CompanyId, Branchid, Transactiontype,userid, FinancialYearId);
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
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try { 
            var department = await  _sitemanagerRepository.GetByID(id);
            return new OkObjectResult(department);
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

        public async Task<IActionResult> Post([FromBody] Sitemanager  sitemanager, [FromHeader] string mdhash, [FromHeader] int User)
        {
            //if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try { 
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var val =await _sitemanagerRepository.Insert(sitemanager);
                scope.Complete();
                return new OkObjectResult(val);
              //  return CreatedAtAction(nameof(Get), new { Id = sitemanager.Id }, sitemanager);
            }
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
            //else
            //{
            //    return Unauthorized("Invalid MdHash");
            //}
        }




        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Sitemanager sitemanager, [FromHeader] string mdhash, [FromHeader] int User)
        {
           // if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try { 
            if (sitemanager != null)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var val =await _sitemanagerRepository.Update(sitemanager);
                    scope.Complete();
                    return new OkObjectResult(val);
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
            //else
            //{
            //    return Unauthorized("Invalid MdHash");
            //}
        }



        [HttpDelete("{id}/{UserId}")]
        public async Task<IActionResult> Delete(int id, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
               var val = await  _sitemanagerRepository.Delete(id, UserId);
                return new OkObjectResult(val);
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


        [HttpPost("Ledger")]
        public async Task<IActionResult> LedgerPost([FromBody] BasicSearch basicSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            //if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (basicSearch != null)
                {
                    var product = await _sitemanagerRepository.GetLedger(basicSearch);
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
            //else
            //{
            //    return Unauthorized("Invalid MdHash");
            //}
        }
        [HttpPost("Report")]
        public async Task<IActionResult> ReportPost([FromBody] BasicSearch basicSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (basicSearch != null)
                {
                    var product = await _sitemanagerRepository.GetReport(basicSearch);
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

        [HttpPost("AdvanceLedger")]
        public async Task<IActionResult> AdvanceLedgerPost([FromBody] BasicSearch basicSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (basicSearch != null)
                {
                    var product = await _sitemanagerRepository.GetAdvanceLedger(basicSearch);
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

        [HttpPost("LoanLedger")]
        public async Task<IActionResult> LoanLedgerPost([FromBody] BasicSearch basicSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (basicSearch != null)
                {
                    var product = await _sitemanagerRepository.GetLoanLedger(basicSearch);
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
