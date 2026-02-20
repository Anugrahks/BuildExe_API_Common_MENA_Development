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
    public class FinancialYearController : ControllerBase
    {
        private readonly IFinancialYearRepository _financialYearController;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public FinancialYearController(IFinancialYearRepository FinancialYearRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            
            _financialYearController = FinancialYearRepository;
                _userLogRepository = userLogRepository;
                _mdHashValidator = mdHashValidator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var products = await _financialYearController.GetFinancilaYear();
                return new OkObjectResult(products);
            }
            catch (Exception)
            { throw; }

        }


        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id, [FromHeader] string mdhash, [FromHeader] int User)
            {
                if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                {
                    try
            {
                var product = await _financialYearController.GetFinancilaYearByID(id);
                return new OkObjectResult(product);
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
        [HttpGet("Active/{Companyid}/{BranchId}")]
        public async Task<IActionResult> GetActive(int Companyid, int BranchId)

            {
                try
            {
                var product = await _financialYearController.GetActiveFinancialYear(Companyid, BranchId);
                return new OkObjectResult(product);
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

        [HttpGet("Active/{Companyid}/{BranchId}/mobile")]
        public async Task<IActionResult> GetActivemobile(int Companyid, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
            {

                    try
            {
                var product = await _financialYearController.GetActiveFinancialYear(Companyid, BranchId);
                return new OkObjectResult(product);
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

        [HttpGet("{Companyid}/{branchId}")]
        [Authorize]
        public async Task<IActionResult> Get(int Companyid, int branchId, [FromHeader] string mdhash, [FromHeader] int User)
            {
                if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                {
                    try
            {
                var product = await _financialYearController.Get(Companyid, branchId);
                return new OkObjectResult(product);
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

        [HttpGet("RegenerateOpeningBalance/{companyid}/{branchid}/{financialyearid}")]
        [Authorize]
        public async Task<IActionResult> Getregenerate(int companyid, int branchid, int financialyearid, [FromHeader] string mdhash, [FromHeader] int User)
            {
                if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                {
                    try
            {
                var product = await _financialYearController.Getregeneratebalance(companyid, branchid, financialyearid);
                return new OkObjectResult(product);
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
        public async Task<IActionResult> Post([FromBody] FinancialYear financialYear, [FromHeader] string mdhash, [FromHeader] int User)
            {
                if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                {
                    try
            {
               var validation= await _financialYearController.InsertFinancilaYear(financialYear);
                return new OkObjectResult(validation);
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
        public async Task<IActionResult> Put([FromBody] FinancialYear financialYear, [FromHeader] string mdhash, [FromHeader] int User)
            {
                if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                {
                    try
            {

                if (financialYear != null)
                {

                    var validation = await _financialYearController.getvalidation2(financialYear);

                    foreach (var error in validation)
                    {
                        if (error.StatusCode == 0)
                            return new OkObjectResult(validation);

                    }
                    //using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled ))
                    //{
                    await _financialYearController.UpdateFinancilaYear(financialYear);
                    //    scope.Complete();
                    //return new OkResult();
                    return new OkObjectResult(validation);
                    //}
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


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id, [FromHeader] string mdhash, [FromHeader] int User)
            {
                if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                {
                    try
            {
                await _financialYearController.DeleteFinancilaYear(id);
                return new OkResult();
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

        #region Finacial Year Process

        [HttpGet("StatusChange/{Companyid}/{BranchId}/{FinancialYearId}/{Type}")]
        [Authorize]
        public async Task<IActionResult> StatusChange(int Companyid, int BranchId, int FinancialYearId, int Type)
        {
            try
            {
                var product = await _financialYearController.FinancialYearStatusChange(Companyid, BranchId, FinancialYearId, Type);
                return new OkObjectResult(product);
            }
            catch (Exception)
            { throw; }
        }
        [HttpGet("Validation/{Companyid}/{BranchId}/{Type}")]
        [Authorize]
        public async Task<IActionResult> FinancialYearValidation(int Companyid, int BranchId, int Type)
        {
            try
            {
                var product = await _financialYearController.FinancialYearValidation(Companyid, BranchId, Type);
                return new OkObjectResult(product);
            }
            catch (Exception)
            { throw; }
        }

        #endregion

    }
}
