using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
using System.Transactions;
using BuildExeHR.Repository;
using Microsoft.AspNetCore.Authorization;
using BuildExeHR.Library;

namespace BuildExeHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayLeaveController : ControllerBase
    {
        private readonly IHolidayLeaveRepository _holidayLeaveRepository ;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public HolidayLeaveController(IHolidayLeaveRepository holidayLeaveRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _holidayLeaveRepository = holidayLeaveRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpGet("List/{companyid}/{branchid}/{financialyearid}")]
        [Authorize]

        public async Task<IActionResult> Getforedit(int companyid, int branchid, int financialyearid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var department = await _holidayLeaveRepository.GetForEdit(companyid, branchid, financialyearid);
                return new OkObjectResult(department);
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
        [HttpGet("{employeeId}/{monthId}/{financialyearid}")]
        [Authorize]

        public async Task<IActionResult> Get(int employeeId, int monthId, int financialyearid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var department = await _holidayLeaveRepository.Get(employeeId, monthId, financialyearid);
                return new OkObjectResult(department);
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
        public async Task<IActionResult> Post([FromBody] IEnumerable<HolidayLeave> holidayLeaves, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var department = await _holidayLeaveRepository.Insert(holidayLeaves);
                    scope.Complete();
                    return new OkObjectResult(department);

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
        public async Task<IActionResult> Put([FromBody] IEnumerable<HolidayLeave> holidayLeaves, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (holidayLeaves != null)
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var department = await _holidayLeaveRepository.Update(holidayLeaves);
                        scope.Complete();
                        return new OkObjectResult(department);
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

        [HttpDelete("{employeeId}/{monthid}/{finid}/{userid}")]
        [Authorize]
        public async Task<IActionResult> Delete(int employeeId, int monthid, int finid, int userid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var department = await _holidayLeaveRepository.Delete(employeeId, monthid, finid, userid);
                return new OkObjectResult(department);
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
