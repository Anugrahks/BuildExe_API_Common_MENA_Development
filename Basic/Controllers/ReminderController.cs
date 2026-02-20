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

namespace BuildExeBasic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReminderController : Controller
    {

        private readonly IReportInputRepository _reportInputRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public ReminderController(IReportInputRepository reportInputRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _reportInputRepository = reportInputRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }


        [HttpGet("{CompanyId}/{Branchid}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> Get(int CompanyId, int Branchid, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {

            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                var designation = await _reportInputRepository.GetReminder(CompanyId, Branchid, UserId);
                    return new OkObjectResult(designation);
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
        public async Task<IActionResult> Post([FromBody] IEnumerable<Reminder> reminders, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var val = await _reportInputRepository.InsertReminder(reminders);
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
        public async Task<IActionResult> Put([FromBody] IEnumerable<Reminder> reminders, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    if (reminders != null)
                    {
                        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                        {
                            var val = await _reportInputRepository.UpdateReminder(reminders);
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

        
        [HttpDelete("{id}/{UserID}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id, int UserID, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {

                try
                {
                    var val = await _reportInputRepository.Delete(id, UserID);
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
