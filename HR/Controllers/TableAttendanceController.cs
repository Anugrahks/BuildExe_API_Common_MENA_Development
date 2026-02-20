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
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableAttendanceController : ControllerBase
    {
        private readonly ITableAttendanceRepository _tableAttendanceRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public TableAttendanceController(ITableAttendanceRepository tableAttendanceRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _tableAttendanceRepository = tableAttendanceRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpGet("{companyid}/{branchid}/{EmployeeId}/{DateWorked}")]
        [Authorize]

        public async Task<IActionResult> Get(int companyid, int branchid, int EmployeeId, string  DateWorked, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var department = await _tableAttendanceRepository.GetForEdit(companyid, branchid, EmployeeId, DateWorked);
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

        public async Task<IActionResult> Post([FromBody] IEnumerable<TableAttendance> tableAttendance, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var department = await _tableAttendanceRepository.Insert(tableAttendance);
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

        [HttpDelete("{fromdate}/{todate}/{projectid}/{employeeid}/{companyId}/{branchId}/{isgroup}")]
        [Authorize]
        public async Task<IActionResult> Delete(string fromdate, string todate, int projectid, int employeeid, int companyId, int branchId, int isgroup, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var validation = await _tableAttendanceRepository.Delete(fromdate, todate, projectid, employeeid, companyId, branchId, isgroup);
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
