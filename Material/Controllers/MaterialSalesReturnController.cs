using BuildExeMaterialServices.Library;
using BuildExeMaterialServices.Models;
using BuildExeMaterialServices.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Threading.Tasks;


namespace BuildExeMaterialServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialSalesReturnController : Controller
    {
        private readonly IMaterialSalesReturnRepository _materialSalesReturnRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public MaterialSalesReturnController(IMaterialSalesReturnRepository materialSalesReturnRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _materialSalesReturnRepository = materialSalesReturnRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        #region Data Manipulation

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] IEnumerable<MaterialSalesReturn> salesReturns, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var val = await _materialSalesReturnRepository.Insert(salesReturns);
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
        public async Task<IActionResult> Put([FromBody] IEnumerable<MaterialSalesReturn> salesReturns, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var val = await _materialSalesReturnRepository.Update(salesReturns);
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
                    var bb = await _materialSalesReturnRepository.Delete(id);
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
                    var returnList = await _materialSalesReturnRepository.GetForEdit(CompanyId, BranchId, UserId, FinancialYearId);
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
                    var returnList = await _materialSalesReturnRepository.GetForApproval(CompanyId, BranchId, UserId, FinancialYearId);
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
                    var returnList = await _materialSalesReturnRepository.GetById(id);
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
        public async Task<IActionResult> GetForReport([FromBody] MaterialSearch materialSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var returnList = await _materialSalesReturnRepository.GetForReport(materialSearch);
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
                var returnList = _materialSalesReturnRepository.GetLatestCreditNote(CompanyId, BranchId, FinancialYearId);
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

        #endregion
    }
}
