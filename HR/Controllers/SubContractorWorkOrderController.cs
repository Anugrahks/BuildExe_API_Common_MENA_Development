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
    public class SubContractorWorkOrderController : ControllerBase
    {
        private readonly ISubContractorWorkOrderRepository _subContractorWorkOrderRepository ;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public SubContractorWorkOrderController(ISubContractorWorkOrderRepository subContractorWorkOrderRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _subContractorWorkOrderRepository = subContractorWorkOrderRepository;
            _userLogRepository = userLogRepository;
                _mdHashValidator = mdHashValidator;
        }

        // GET: api/<TeamController>
        [HttpGet("{companyid}/{branchid}")]
        [Authorize]
        public async Task<IActionResult> Get(int companyid, int branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase =await _subContractorWorkOrderRepository.Get(companyid, branchid);
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
        [HttpGet("GetShow/{ProjectId}/{UnitId}/{BlockId}/{FloorId}/{SubcontractorId}/{DivisionId}")]
        [Authorize]
        public async Task<IActionResult> GetProject(int ProjectId, int UnitId, int BlockId, int FloorId,int SubcontractorId, int DivisionId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase =await _subContractorWorkOrderRepository.Getbyproject(ProjectId, UnitId, BlockId, FloorId, SubcontractorId, DivisionId);
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

        [HttpGet("{ProjectId}/{UnitId}/{BlockId}/{FloorId}/{SubcontractorId}/{workcategoryId}/{workNameId}/{DivisionId}")]
        [Authorize]
        public async Task<IActionResult> Gets(int ProjectId, int UnitId, int BlockId, int FloorId, int SubcontractorId, int workcategoryId, int workNameId, int DivisionId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _subContractorWorkOrderRepository.GetDetails(ProjectId, UnitId, BlockId, FloorId, SubcontractorId, workcategoryId, workNameId, DivisionId);
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

        [HttpGet("RateRevision/{ProjectId}/{UnitId}/{BlockId}/{FloorId}/{SubcontractorId}")]
        [Authorize]
        public async Task<IActionResult> Gets(int ProjectId, int UnitId, int BlockId, int FloorId, int SubcontractorId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _subContractorWorkOrderRepository.GetDetails(ProjectId, UnitId, BlockId, FloorId, SubcontractorId);
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

        [HttpGet("Completed/{ProjectId}/{UnitId}/{BlockId}/{FloorId}/{SubcontractorId}")]
        [Authorize]
        public async Task<IActionResult> GetCompleted(int ProjectId, int UnitId, int BlockId, int FloorId, int SubcontractorId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _subContractorWorkOrderRepository.Getbyproject_Completed(ProjectId, UnitId, BlockId, FloorId, SubcontractorId);
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
        [HttpGet("{ProjectId}/{UnitId}/{BlockId}/{FloorId}/{SubcontractorId}/{AttendanceType}")]
        [Authorize]
        public async Task<IActionResult> Get(int ProjectId, int UnitId, int BlockId, int FloorId, int SubcontractorId,int AttendanceType, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _subContractorWorkOrderRepository.GetbyattendanceType(ProjectId, UnitId, BlockId, FloorId, SubcontractorId, AttendanceType);
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
                var purchase =await _subContractorWorkOrderRepository.GetbyID(id);

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
        public async Task<IActionResult> Post([FromBody] IEnumerable<SubContractorWorkOrder> subContractorWorkOrders, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var validation = await _subContractorWorkOrderRepository.Insert(subContractorWorkOrders);
                    scope.Complete();

                    return new OkObjectResult(validation);
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
        public async Task<IActionResult> Put([FromBody] IEnumerable<SubContractorWorkOrder > subContractorWorkOrders, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (subContractorWorkOrders != null)
            {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var validation = await _subContractorWorkOrderRepository.Update(subContractorWorkOrders);
                        scope.Complete();
                        return new OkObjectResult(validation);
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
        public async Task<IActionResult> Delete(int id,int userid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var validation = await _subContractorWorkOrderRepository.Delete(id, userid);
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
    }
}
