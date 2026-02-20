using BuildExeServices.Library;
using BuildExeServices.Models;
using BuildExeServices.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Security.Policy;
using System.Threading.Tasks;
namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReturnController : Controller
    {
        private readonly IReturnRepository _returnRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public ReturnController(IReturnRepository returnRepository, IUserRepository userRepository, IMdHashValidator mdHashValidator)
        {
            _returnRepository = returnRepository;
            _userRepository = userRepository;
            _mdHashValidator = mdHashValidator;
        }

        #region Data Manipulation

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] IEnumerable<ReturnMaster> returns, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var val = await _returnRepository.Insert(returns);
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

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] IEnumerable<ReturnMaster> returns, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var val = await _returnRepository.Update(returns);
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

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var bb = await _returnRepository.Delete(id);
                    return new OkObjectResult(bb);
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

        #endregion

        #region Grids & Reports

        [HttpGet("GetForEdit/{CompanyId}/{BranchId}/{UserId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> GetForEdit(int CompanyId, int BranchId, int UserId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var returnList = await _returnRepository.GetForEdit(CompanyId, BranchId, UserId, FinancialYearId);
                    return new OkObjectResult(returnList);
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

        [HttpGet("GetForApproval/{CompanyId}/{BranchId}/{UserId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> GetForApproval(int CompanyId, int BranchId, int UserId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var returnList = await _returnRepository.GetForApproval(CompanyId, BranchId, UserId, FinancialYearId);
                    return new OkObjectResult(returnList);
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

        [HttpGet("Details/{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var returnList = await _returnRepository.GetById(id);
                    return new OkObjectResult(returnList);
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
        public async Task<IActionResult> GetForReport([FromBody] BillSearch billSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var returnList = await _returnRepository.GetForReport(billSearch);
                    return new OkObjectResult(returnList);
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

        [HttpGet("LatestCreditNote/{CompanyId}/{BranchId}/{FinancialYearId}")]
        [Authorize]
        public IActionResult GetLatestCreditNote(int CompanyId, int BranchId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            try
            {
                var returnList = _returnRepository.GetLatestCreditNote(CompanyId, BranchId, FinancialYearId);
                return new OkObjectResult(returnList);
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

        [HttpPost("GetSpecificsForReturn")]
        [Authorize]
        public async Task<IActionResult> GetSpecificsForReturn([FromBody] ReturnMaster returnMaster, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var returnList = await _returnRepository.GetSpecificsForReturn(returnMaster);
                    return new OkObjectResult(returnList);
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

        [HttpPost("GetAmountsForReturn")]
        [Authorize]
        public async Task<IActionResult> GetAmountsForReturn([FromBody] ReturnMaster returnMaster, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var returnList = await _returnRepository.GetSpecificsForReturnAmount(returnMaster);
                    return new OkObjectResult(returnList);
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


        #endregion
    }
}
