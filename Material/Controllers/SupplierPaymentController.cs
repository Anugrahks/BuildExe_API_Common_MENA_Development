using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BuildExeMaterialServices.Repository;
using BuildExeMaterialServices.Models;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using BuildExeMaterialServices.Library;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeMaterialServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierPaymentController : ControllerBase
    {
        private readonly ISupplierPaymentRepository _supplierPaymentRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public SupplierPaymentController(ISupplierPaymentRepository supplierPaymentRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _supplierPaymentRepository = supplierPaymentRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
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
                var material = await _supplierPaymentRepository.Get(CompanyId, Branchid);
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

        // GET api/<TeamController>/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var material = await _supplierPaymentRepository.GetbyID(id);

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

        // POST api/<TeamController>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] IEnumerable<SupplierPayment> supplierPayments, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var val = await _supplierPaymentRepository.Insert(supplierPayments);
                    scope.Complete();
                    return new OkObjectResult(val);
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
        public async Task<IActionResult> Put([FromBody] IEnumerable<SupplierPayment> supplierPayments, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (supplierPayments != null)
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var val = await _supplierPaymentRepository.Update(supplierPayments);
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

        // DELETE api/<TeamController>/5
        [HttpDelete("{id}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var val = await _supplierPaymentRepository.Delete(id, UserId);
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


        [HttpGet("DebitNote/{SupplierId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> getDebit(int SupplierId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {

                try
                {
                    var val = await _supplierPaymentRepository.getDebit(SupplierId, FinancialYearId);
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
        [HttpPost("DebitNote")]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] IEnumerable<DebitNote> debitNote, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {

                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var val = await _supplierPaymentRepository.Save(debitNote);
                    scope.Complete();
                    return new OkObjectResult(val);

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

        [HttpGet("DebitNoteEdit/{SupplierId}/{FinancialYearId}/{SupplierPaymentId}")]
        [Authorize]
        public async Task<IActionResult> getDebitEdit(int SupplierId, int FinancialYearId, int SupplierPaymentId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {

                try
                {
                    var val = await _supplierPaymentRepository.getDebitEdit(SupplierId, FinancialYearId, SupplierPaymentId);
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

        [HttpPost("DebitNoteReport")]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] MaterialSearch materialSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    if (materialSearch != null)
                    {
                        var product = await _supplierPaymentRepository.GetforDebitNoteReport(materialSearch);
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
