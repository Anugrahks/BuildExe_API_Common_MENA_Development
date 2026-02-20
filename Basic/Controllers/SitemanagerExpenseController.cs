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
    public class SitemanagerExpenseController : ControllerBase
    {
        private readonly ISitemanagerRepository _sitemanagerRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public SitemanagerExpenseController(ISitemanagerRepository sitemanagerRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _sitemanagerRepository = sitemanagerRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }


        [HttpGet("getuser/{CompanyId}/{Branchid}/{UserId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> getusersiteexpense(int CompanyId, int Branchid, int UserId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var designation = await _sitemanagerRepository.getusersiteexpense(CompanyId, Branchid, UserId, FinancialYearId);
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

        [HttpGet("getforApproval/{CompanyId}/{Branchid}/{userid}/{FinancialYearId}")]
        [Authorize]

        public async Task<IActionResult> getforApprovalsiteexpense(int CompanyId, int Branchid, int userid, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var designation = await _sitemanagerRepository.getforApprovalsiteexpense(CompanyId, Branchid, userid, FinancialYearId);
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
        public async Task<IActionResult> Getbyidsiteexpense(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var department = await _sitemanagerRepository.Getbyidsiteexpense(id);
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

        public async Task<IActionResult> Post([FromBody] IEnumerable<SiteExpense> sitemanager, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var val = await _sitemanagerRepository.InsertSiteExpense(sitemanager);
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
            else
            {
                return Unauthorized("Invalid MdHash");
            }
        }




        [HttpPut]
        public async Task<IActionResult> Put([FromBody] IEnumerable<SiteExpense> sitemanager, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    if (sitemanager != null)
                    {
                        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                        {
                            var val = await _sitemanagerRepository.UpdateSiteExpense(sitemanager);
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
            else
            {
                return Unauthorized("Invalid MdHash");
            }
        }



        [HttpDelete("{id}/{UserId}")]
        public async Task<IActionResult> Delete(int id, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var val = await _sitemanagerRepository.DeleteSiteExpense(id, UserId);
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

    }
}
