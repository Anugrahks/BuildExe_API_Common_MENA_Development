using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Repository;
using BuildExeHR.Models;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using BuildExeHR.Library;

namespace BuildExeHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabourWagePaymentController : ControllerBase
    {
        private readonly ILabourWagePaymentRepository _labourWagePaymentRepository ;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public LabourWagePaymentController(ILabourWagePaymentRepository labourWagePaymentRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _labourWagePaymentRepository = labourWagePaymentRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }
        [HttpGet("{companyid}/{branchid}")]
        [Authorize]
        public async Task<IActionResult> Get(int companyid, int branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _labourWagePaymentRepository.Get(companyid, branchid);
                return new OkObjectResult(purchase);
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



        [HttpPost("GetForPaymentList")]
        [Authorize]
        public async Task<IActionResult> Get([FromBody] LabourForPayment labourForPayment, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase =await _labourWagePaymentRepository.GetForPaymentList(labourForPayment);
            return new OkObjectResult(purchase);
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

        [HttpGet("{CompanyId}/{BranchId}/{EmployeeId}/{SitemanagerId}/{FinancialyearId}/{Dateto}")]
        [Authorize]
        public async Task<IActionResult> Get(int CompanyId, int BranchId, int EmployeeId, int SitemanagerId, int FinancialyearId, DateTime Dateto, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _labourWagePaymentRepository.GetForPayment(CompanyId, BranchId, EmployeeId, SitemanagerId, FinancialyearId, Dateto);
                    return new OkObjectResult(purchase);
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


        [HttpPost("PendingBillsClearList")]
        [Authorize]
        public async Task<IActionResult> PendingBillsClear([FromBody] LabourForPayment labourForPayment, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _labourWagePaymentRepository.PendingBillsClearList(labourForPayment);
                    return new OkObjectResult(purchase);
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


        [HttpGet("PendingBillsClear/{CompanyId}/{BranchId}/{EmployeeId}/{SitemanagerId}/{FinancialyearId}/{Dateto}")]
        [Authorize]
        public async Task<IActionResult> PendingBillsClear(int CompanyId, int BranchId, int EmployeeId, int SitemanagerId, int FinancialyearId, DateTime Dateto, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _labourWagePaymentRepository.PendingBillsClear(CompanyId, BranchId, EmployeeId, SitemanagerId, FinancialyearId, Dateto);
                return new OkObjectResult(purchase);
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
                var purchase =await _labourWagePaymentRepository.GetbyID(id);

            return new OkObjectResult(purchase);
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
        public async Task<IActionResult> Post([FromBody] IEnumerable<LabourWagePayment> labourWagePayments, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var result = await _labourWagePaymentRepository.Insert(labourWagePayments);
                    scope.Complete();

                    return new OkObjectResult(result);
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
        public async Task<IActionResult> Put([FromBody] IEnumerable<LabourWagePayment > labourWagePayments, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (labourWagePayments != null)
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var result = await _labourWagePaymentRepository.Update(labourWagePayments);
                        scope.Complete();
                        return new OkObjectResult(result);
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
        [Authorize]
        public async Task<IActionResult> Delete(int id, int userid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await  _labourWagePaymentRepository.Delete(id, userid);
            return new OkObjectResult(result);
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

        [HttpGet("Validation/{CompanyId}/{BranchId}/{EmployeeId}/{FinancialyearId}/{Dateto}/{UserId}/{EmployeeLabourGroupId}")]
        [Authorize]
        public async Task<IActionResult> ValidationCheck(int CompanyId, int BranchId, int EmployeeId, int FinancialyearId, DateTime Dateto, int UserId, int @EmployeeLabourGroupId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _labourWagePaymentRepository.ValidationCheck(CompanyId, BranchId, EmployeeId, FinancialyearId, Dateto, UserId, @EmployeeLabourGroupId);
                return new OkObjectResult(purchase);
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

        [HttpGet("ValidationforApproval/{CompanyId}/{BranchId}/{EmployeeId}/{FinancialyearId}/{Dateto}/{UserId}/{EmployeeLabourGroupId}")]
        [Authorize]
        public async Task<IActionResult> ValidationChecknew(int CompanyId, int BranchId, int EmployeeId, int FinancialyearId, DateTime Dateto, int UserId, int @EmployeeLabourGroupId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _labourWagePaymentRepository.ValidationChecknew(CompanyId, BranchId, EmployeeId, FinancialyearId, Dateto, UserId, @EmployeeLabourGroupId);
                return new OkObjectResult(purchase);
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

        [HttpGet("project/{CompanyId}/{BranchId}/{ToDate}")]
        [Authorize]
        public async Task<IEnumerable<ListLabour>> GetLabours(int CompanyId, int BranchId, string ToDate, [FromHeader] string mdhash, [FromHeader] int User)
        {
                try
                {
                var projectAllocationDate = DateTime.Parse(ToDate);
                var entity = await _labourWagePaymentRepository.GetLabours(CompanyId, BranchId, projectAllocationDate);
                return entity;
                }
            catch (Exception)
            { throw; }
        }
    }
}
