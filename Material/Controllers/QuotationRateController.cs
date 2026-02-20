using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
using BuildExeMaterialServices.Repository;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using BuildExeMaterialServices.Library;

namespace BuildExeMaterialServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotationRateController : ControllerBase
    {
        private readonly IQuotationRepository _QuotationRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public QuotationRateController(IQuotationRepository quotationRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _QuotationRepository = quotationRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;

        }

        [HttpGet("{ProjectID}/{MaterialId}/{Brandid}")]
        [Authorize]
        public async Task<IActionResult> Get(int ProjectID, int MaterialId, int Brandid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var brand = await _QuotationRepository.Getsuppliers (ProjectID, MaterialId, Brandid);
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

        [HttpGet("{CompanyId}/{Branchid}")]
        [Authorize]
        public async Task<IActionResult> Get(int CompanyId, int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var brand = await _QuotationRepository.Getfor_Edit_Rate (CompanyId, Branchid);
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

        [HttpGet("GetUser/{CompanyId}/{Branchid}/{UserId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> GetByUser(int CompanyId, int Branchid, int UserId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var brand = await _QuotationRepository.Getfor_Edit_Rate(CompanyId, Branchid, UserId, FinancialYearId);
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
        [HttpGet("Comparison/{CompanyId}/{Branchid}")]
        [Authorize]
        public async Task<IActionResult> GetComaparison(int CompanyId, int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var brand = await _QuotationRepository.Getfor_CompasisonStatement (CompanyId, Branchid);
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

        [HttpGet("Comparison/GetUser/{CompanyId}/{Branchid}/{UserId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> GetComaparisonByUser(int CompanyId, int Branchid, int UserId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var brand = await _QuotationRepository.Getfor_CompasisonStatement(CompanyId, Branchid, UserId, FinancialYearId);
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

        [HttpGet("Comparison/{CompanyId}/{Branchid}/{UserId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> GetComaparison(int CompanyId, int Branchid, int UserId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var brand = await _QuotationRepository.GetforApprovalCompasisonStatement(CompanyId, Branchid, UserId, FinancialYearId);
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

        [HttpGet("Supplier/{id}")]
        [Authorize]
        public async Task<IActionResult> GetSupplier(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var brand = await _QuotationRepository.GetsupplierswithItem(id);

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
        [HttpGet("Item/{id}")]
        [Authorize]
        public async Task<IActionResult> GetItem(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var brand = await _QuotationRepository.GetItemwithSupplier (id);

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
        [HttpGet("Total/{id}")]
        [Authorize]
        public async Task<IActionResult> Gettotal(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var brand = await _QuotationRepository.GetSupplierTotal(id);

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


        [HttpGet("Project/{id}")]
        [Authorize]
        public async Task<IActionResult> Getbyproject(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var brand = await _QuotationRepository.GetBy_Project (id);

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
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var brand = await _QuotationRepository.GetRateDetailsbyid(id);

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
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] IEnumerable<QuotationRate> quotations, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var pur = await _QuotationRepository.Insert_QuotationRate(quotations);
                    scope.Complete();

                    return new OkObjectResult(pur);
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
        [Authorize]
        public async Task<IActionResult> Put([FromBody] IEnumerable<QuotationRate> quotations, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (quotations != null)
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var pur = await _QuotationRepository.Update_QuotationRate(quotations);
                        scope.Complete();
                        return new OkObjectResult(pur);
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

        [HttpPut("Approve")]
        [Authorize]
        public async Task<IActionResult> Approve([FromBody] IEnumerable<Quotation> quotations, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (quotations != null)
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var pur = await _QuotationRepository.Approve_QuotationRate(quotations);
                        scope.Complete();
                        return new OkObjectResult(pur);
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

        [HttpDelete("{id}/{UserID}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id, int UserID, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var pur = await _QuotationRepository.Delete_QuotationRate(id,  UserID);
                return new OkObjectResult(pur);
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

        [HttpGet("ApprovedSupplierRate/{CompanyId}/{BranchId}/{ProjectId}/{MaterialId}")]
        [Authorize]
        public async Task<IActionResult> GetApprovedData(int CompanyId, int BranchId, int ProjectId, int MaterialId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var item = await _QuotationRepository.GetApprovedData(CompanyId, BranchId, ProjectId, MaterialId);

                return new OkObjectResult(item);
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
