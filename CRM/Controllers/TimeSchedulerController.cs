using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
using BuildExeServices.Repository;

using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using BuildExeServices.Library;
using System.Security.Cryptography;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSchedulerController : ControllerBase
    {
        private readonly ITimeSchedulerRepository _projectSpecificationRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public TimeSchedulerController(ITimeSchedulerRepository projectSpecificationRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _projectSpecificationRepository = projectSpecificationRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;

        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] IEnumerable<TimeSchedulerMaster> timeSchedulerMaster, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var val = await _projectSpecificationRepository.Insert(timeSchedulerMaster);

                        scope.Complete();
                        return new OkObjectResult(val);
                        // return CreatedAtAction(nameof(Get), new { id = specificationMasters. }, specificationMasters);
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
        public async Task<IActionResult> Put([FromBody] IEnumerable<TimeSchedulerMaster> timeSchedulerMaster, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    if (timeSchedulerMaster != null)
                    {
                        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                        {
                            var val = await _projectSpecificationRepository.Update(timeSchedulerMaster);

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


        [HttpPut("Reschedule")]
        [Authorize]
        public async Task<IActionResult> Reschedule([FromBody] IEnumerable<TimeSchedulerMaster> timeSchedulerMaster, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    if (timeSchedulerMaster != null)
                    {
                        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                        {
                            var val = await _projectSpecificationRepository.Reschedule(timeSchedulerMaster);

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
                    var val = await _projectSpecificationRepository.Delete(id, UserId);

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


        [HttpGet("GetGrid/{BranchId}/{FinancialYearId}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> GetGrid(int BranchId, int FinancialYearId, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var val = await _projectSpecificationRepository.GetGrid(BranchId, FinancialYearId, UserId);

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

        [HttpGet("GetGridReschedule/{BranchId}/{FinancialYearId}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> GetGridReschedule(int BranchId, int FinancialYearId, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var val = await _projectSpecificationRepository.GetGridReschedule(BranchId, FinancialYearId, UserId);

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


        [HttpGet("GetApproval/{BranchId}/{FinancialYearId}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> GetApproval(int BranchId, int FinancialYearId, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var val = await _projectSpecificationRepository.GetApproval(BranchId, FinancialYearId, UserId);

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


        [HttpGet("GetById/{Id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var val = await _projectSpecificationRepository.GetById(Id);

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


        [HttpGet("GetRescheduleById/{Id}")]
        [Authorize]
        public async Task<IActionResult> GetRescheduleById(int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var val = await _projectSpecificationRepository.GetRescheduleById(Id);

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

        [HttpGet("GetWorkName/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> GetWorkName(int BranchId , [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var val = await _projectSpecificationRepository.GetWorkName(BranchId);

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


        [HttpGet("GetSchedule/{ProjectId}/{DivisionId}")]
        [Authorize]
        public async Task<IActionResult> GetSchedule(int ProjectId, int DivisionId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var val = await _projectSpecificationRepository.GetSchedule(ProjectId, DivisionId);

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


        [HttpGet("GetSubWork/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> GetSubWork(int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var val = await _projectSpecificationRepository.GetSubWork(BranchId);

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

        [HttpGet("GetData/{ProjectId}/{Divisionid}")]
        [Authorize]
        public async Task<IActionResult> GetData(int ProjectId, int DivisionId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var val = await _projectSpecificationRepository.GetData(ProjectId, DivisionId);

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


        [HttpGet("GetDashboardData/{ProjectId}/{Divisionid}")]
        [Authorize]
        public async Task<IActionResult> GetDashboardData(int ProjectId, int DivisionId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var val = await _projectSpecificationRepository.GetDashboardData(ProjectId, DivisionId);

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


        [HttpGet("GetProgressData/{ProjectId}/{Divisionid}/{ScheduleNumber}")]
        [Authorize]
        public async Task<IActionResult> GetProgressData(int ProjectId, int DivisionId, int ScheduleNumber, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var val = await _projectSpecificationRepository.GetProgressData(ProjectId, DivisionId, ScheduleNumber);

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


        [HttpPost("GetProgressDataSubWork")]
        [Authorize]
        public async Task<IActionResult> GetProgressDataSubWork(TimeSchedulerProgress timeSchedulerProgress, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var val = await _projectSpecificationRepository.GetProgressDataSubWork(timeSchedulerProgress);

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


        [HttpGet("GetPlanDashboard/{ProjectId}/{Divisionid}/{ScheduleNumber}/{UnitId}")]
        [Authorize]
        public async Task<IActionResult> GetPlanDashboard(int ProjectId, int DivisionId, int ScheduleNumber, int UnitId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var val = await _projectSpecificationRepository.GetPlanDashboard(ProjectId, DivisionId, ScheduleNumber, UnitId);

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

        [HttpGet("GetPlanDashboardSubWork/{ProjectId}/{Divisionid}/{ScheduleNumber}")]
        [Authorize]
        public async Task<IActionResult> GetPlanDashboardSubWork(int ProjectId, int DivisionId, int ScheduleNumber, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var val = await _projectSpecificationRepository.GetPlanDashboardSubWork(ProjectId, DivisionId, ScheduleNumber);

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


        [HttpGet("GetDashboardDataSubWork/{ProjectId}/{Divisionid}/{ScheduleNumber}")]
        [Authorize]
        public async Task<IActionResult> GetDashboardDataSubWork(int ProjectId, int DivisionId,int ScheduleNumber, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var val = await _projectSpecificationRepository.GetDashboardDataSubWork(ProjectId, DivisionId, ScheduleNumber);

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
