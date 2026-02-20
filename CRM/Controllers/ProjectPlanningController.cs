using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BuildExeServices.Repository;
using BuildExeServices.Models;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using BuildExeServices.Library;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectPlanningController : ControllerBase
    {
        private readonly IProjectWorkSettingRepository _projectWorkSettingRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public ProjectPlanningController(IProjectWorkSettingRepository projectWorkSettingRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _projectWorkSettingRepository = projectWorkSettingRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] IEnumerable<ProjectStagePlanning> workEnquiryStageSettings, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _projectWorkSettingRepository.InsertPlan(workEnquiryStageSettings);
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

        [HttpPost("PlanActivity")]
        [Authorize]
        public async Task<IActionResult> PostPlanActivity([FromBody] IEnumerable<StageActivityDetails> workEnquiryStageSettings, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _projectWorkSettingRepository.InsertPlanActivity(workEnquiryStageSettings);
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

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] IEnumerable<ProjectStagePlanning> workEnquiryStageSettings, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {

                var result = await _projectWorkSettingRepository.Update(workEnquiryStageSettings);
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

        [HttpDelete("{id}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id, int userId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _projectWorkSettingRepository.DeletePlan(id, userId);
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




        [HttpGet("GetList/{CompanyId}/{BranchId}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> ViewNotificationsProject(int CompanyId, int BranchId, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _projectWorkSettingRepository.GetListPlanning(CompanyId, BranchId, UserId);
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

        [HttpGet("GetProjectList/{CompanyId}/{BranchId}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> GetProjectList(int CompanyId, int BranchId, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var result = await _projectWorkSettingRepository.GetProjectList(CompanyId, BranchId, UserId);
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

        [HttpGet("GetPlanById/{Id}")]
        [Authorize]
        public async Task<IActionResult> ViewNotificationsProject(int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _projectWorkSettingRepository.GetPlanById(Id);
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


        [HttpGet("GetPlan/{ProjecId}/{DivisionId}/{OrderId}/{JobId}")]
        [Authorize]
        public async Task<IActionResult> GetPlan(int ProjecId, int DivisionId, int OrderId, int JobId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _projectWorkSettingRepository.GetPlan(ProjecId, DivisionId, OrderId, JobId);
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

        //---------------------------------------------------------------------------------------------------------------------------------------//

        [HttpGet("GetPlan/{ProjecId}/{DivisionId}/{OrderId}")]
        [Authorize]
        public async Task<IActionResult> GetPlan(int ProjecId, int DivisionId, int OrderId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _projectWorkSettingRepository.GetPlan(ProjecId, DivisionId, OrderId, 0);
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


        [HttpGet("GetPlanActivities/{ProjecId}/{DivisionId}/{OrderId}/{JobId}")]
        [Authorize]
        public async Task<IActionResult> GetPlanActivities(int ProjecId, int DivisionId, int OrderId, int JobId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _projectWorkSettingRepository.GetPlanActivities(ProjecId, DivisionId, OrderId, JobId);
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

        //---------------------------------------------------------------------------------------------------------------------------------------//


        [HttpGet("GetPlanActivities/{ProjecId}/{DivisionId}/{OrderId}")]
        [Authorize]
        public async Task<IActionResult> GetPlanActivities(int ProjecId, int DivisionId, int OrderId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _projectWorkSettingRepository.GetPlanActivities(ProjecId, DivisionId, OrderId, 0);
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

        [HttpGet("ValidationDuringShow/{ProjectId}/{DivisionId}/{JobId}")]
        [Authorize]
        public async Task<IActionResult> ValidationDuringShow(int ProjectId, int DivisionId, int JobId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _projectWorkSettingRepository.ValidationDuringShow(ProjectId, DivisionId, JobId);
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

        //---------------------------------------------------------------------------------------------------------------------------------------//

        [HttpGet("ValidationDuringShow/{ProjectId}/{DivisionId}")]
        [Authorize]
        public async Task<IActionResult> ValidationDuringShow(int ProjectId, int DivisionId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _projectWorkSettingRepository.ValidationDuringShow(ProjectId, DivisionId, 0);
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


        [HttpGet("GetStages/{ProjectId}/{DivisionId}/{JobId}")]
        [Authorize]
        public async Task<IActionResult> GetStages(int ProjectId, int DivisionId, int JobId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _projectWorkSettingRepository.GetStages(ProjectId, DivisionId, JobId);
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

        //---------------------------------------------------------------------------------------------------------------------------------------//


        [HttpGet("GetStages/{ProjectId}/{DivisionId}")]
        [Authorize]
        public async Task<IActionResult> GetStages(int ProjectId, int DivisionId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _projectWorkSettingRepository.GetStages(ProjectId, DivisionId, 0);
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

        [HttpGet("PlanningDashboard/{ProjectId}/{DivisionId}/{JobId}")]
        [Authorize]
        public async Task<IActionResult> PlanningDashboard(int ProjectId, int DivisionId, int JobId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var result = await _projectWorkSettingRepository.PlanningDashboard(ProjectId, DivisionId, JobId, 0, 0, 0);
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


        [HttpGet("PlanningDashboard/{ProjectId}/{DivisionId}/{JobId}/{PageNumber}/{PageSize}/{UnitId}")]
        [Authorize]
        public async Task<IActionResult> PlanningDashboard(int ProjectId, int DivisionId, int JobId, int PageNumber, int PageSize, int UnitId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _projectWorkSettingRepository.PlanningDashboard(ProjectId, DivisionId, JobId, PageNumber, PageSize, UnitId);
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
    }
}
