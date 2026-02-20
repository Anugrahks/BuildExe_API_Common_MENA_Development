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
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabourAdvanceMasterController : ControllerBase
    {
        private readonly ILabourAdvanceMasterRepository _labourAdvanceMasterRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public LabourAdvanceMasterController(ILabourAdvanceMasterRepository labourAdvanceMasterRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _labourAdvanceMasterRepository = labourAdvanceMasterRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        // GET: api/<TeamController>
        [HttpGet("{Companyid}/{Branchid}")]
        [Authorize]
        public async Task<IActionResult> Get(int Companyid, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _labourAdvanceMasterRepository.Get(Companyid, BranchId);
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
        [HttpGet("{Companyid}/{Branchid}/{MenuID}")]
        [Authorize]
        public async Task<IActionResult> Get(int Companyid, int BranchId, int MenuID, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _labourAdvanceMasterRepository.GetforEdit(Companyid, BranchId, MenuID);
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


        [HttpGet("getuser/{Companyid}/{Branchid}/{UserId}/{MenuID}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> Getuser(int Companyid, int BranchId, int UserId, int MenuID, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _labourAdvanceMasterRepository.GetforEdit(Companyid, BranchId, UserId, MenuID, FinancialYearId);
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



        [HttpGet("{Companyid}/{Branchid}/{UserId}/{MenuID}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> Get(int Companyid, int BranchId, int UserId, int MenuID, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _labourAdvanceMasterRepository.GetforApproval(Companyid, BranchId, UserId, MenuID, FinancialYearId);
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
                var purchase = await _labourAdvanceMasterRepository.GetByID(id);

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
        public async Task<IActionResult> Post([FromBody] LabourAdvanceMaster labourAdvanceMasters, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var val = await _labourAdvanceMasterRepository.Insert(labourAdvanceMasters);
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

        [HttpPost("Report")]
        [Authorize]
        public async Task<IActionResult> PostReport([FromBody] HRSearch search, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {

                    var val = await _labourAdvanceMasterRepository.Report(search);
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

        [HttpGet("GetAdvanceAdjustment/{BranchId}/{ProjectId}/{EmployeeCategoryId}/{EmployeeId}/{SupplierId}")]
        [Authorize]
        public async Task<IActionResult> GetAdvanceAdjustment(int BranchId, int ProjectId, int EmployeeCategoryId, int EmployeeId, int SupplierId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _labourAdvanceMasterRepository.GetAdvanceAdjustment( BranchId,  ProjectId,  EmployeeCategoryId,  EmployeeId,  SupplierId);

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
        

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] LabourAdvanceMaster labourAdvanceMasters, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (labourAdvanceMasters != null)
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var val = await _labourAdvanceMasterRepository.Update(labourAdvanceMasters);
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
        [Authorize]
        public async Task<IActionResult> Delete(int id, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var val = await _labourAdvanceMasterRepository.Delete(id, UserId);
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

        [HttpPost("AdvanceReport")]
        [Authorize]
        public async Task<IActionResult> ForReport([FromBody] HRSearch search, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {

                var val = await _labourAdvanceMasterRepository.ForReport(search);
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
