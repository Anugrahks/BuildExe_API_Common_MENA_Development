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
    public class SubContractorAttendanceController : ControllerBase
    {
        private readonly ISubContractorAttendanceRepository _subContractorAttendanceRepository  ;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public SubContractorAttendanceController(ISubContractorAttendanceRepository subContractorAttendanceRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _subContractorAttendanceRepository = subContractorAttendanceRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        // GET: api/<TeamController>
        [HttpGet("{companyid}/{Branchid}")]
        [Authorize]
        public async Task<IActionResult> Get(int companyid, int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase =await _subContractorAttendanceRepository.Get(companyid, Branchid);
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
                var purchase =await _subContractorAttendanceRepository.GetbyID(id);

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
        public async Task<IActionResult> Post([FromBody] IEnumerable<SubContractorAttendance > subContractorAttendances, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
               var result = await _subContractorAttendanceRepository.Insert(subContractorAttendances);
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
        public async Task<IActionResult> Put([FromBody] IEnumerable<SubContractorAttendance > subContractorAttendances, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (subContractorAttendances != null)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                        var result = await _subContractorAttendanceRepository.Update(subContractorAttendances);
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


        [HttpDelete("{id}/{Userid}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id,int Userid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
              await  _subContractorAttendanceRepository.Delete(id, Userid);
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

        [HttpGet("BillToDate/{workOrderId}")]
        [Authorize]
        public async Task<IActionResult> GetBillFromDate(int workOrderId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var fromDate = await _subContractorAttendanceRepository.GetBillToDate(workOrderId);
                return new OkObjectResult(fromDate);
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

        [HttpGet("GenerateNextBillNo/{mainWorkOrderNo}")]
        [Authorize]
        public IActionResult GenerateNextBillNo(int mainWorkOrderNo, [FromHeader] string mdhash, [FromHeader] int User)
        {
           
                try
            {
                var material = _subContractorAttendanceRepository.GenerateNextBillNo(mainWorkOrderNo,0 ,0);
                return new OkObjectResult(material);
            }
            catch (Exception)
            { throw; }

        }



        [HttpGet("GenerateNextBillNo/{mainWorkOrderNo}/{SubcontractorId}/{ContractorId}")]
        [Authorize]
        public IActionResult GenerateNextBillNo(int mainWorkOrderNo, int SubcontractorId, int ContractorId, [FromHeader] string mdhash, [FromHeader] int User)
        {

            try
            {
                var material = _subContractorAttendanceRepository.GenerateNextBillNo(mainWorkOrderNo, SubcontractorId, ContractorId);
                return new OkObjectResult(material);
            }
            catch (Exception)
            { throw; }

        }
    }
}
