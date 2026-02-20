using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Repository;
using BuildExeMaterialServices.Models;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using BuildExeMaterialServices.Library;
namespace BuildExeMaterialServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierPaymentListController : ControllerBase
    {
        private readonly ISupplierPaymentRepository _supplierPaymentRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public SupplierPaymentListController(ISupplierPaymentRepository supplierPaymentRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _supplierPaymentRepository = supplierPaymentRepository;
            _userLogRepository = userLogRepository;
                _mdHashValidator = mdHashValidator;
        }
        [HttpGet("{Id}")]
        [Authorize]
        public async Task<IActionResult> Get(int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var material =await _supplierPaymentRepository.GetDetailsbyid (Id);
            return new OkObjectResult(material);
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
        // GET: api/<TeamController>
        [HttpGet("{CompanyId}/{Branchid}")]
        [Authorize]
        public async Task<IActionResult> Get(int CompanyId, int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var material = await _supplierPaymentRepository.GetForEdit (CompanyId, Branchid);
            return new OkObjectResult(material);
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

        [HttpGet("getuser/{CompanyId}/{Branchid}/{UserId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> Getbyid(int CompanyId, int Branchid, int UserId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var material = await _supplierPaymentRepository.GetForEdituser(CompanyId, Branchid, UserId, FinancialYearId);
                return new OkObjectResult(material);
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

        [HttpGet("{CompanyId}/{Branchid}/{UserId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> Get(int CompanyId, int Branchid, int UserId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var material =await _supplierPaymentRepository.GetForApproval(CompanyId, Branchid, UserId, FinancialYearId);
            return new OkObjectResult(material);
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
        public async Task<IActionResult> Post([FromBody] MaterialSearch materialSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (materialSearch != null)
            {
                var product =await _supplierPaymentRepository.GetforReport(materialSearch);
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
