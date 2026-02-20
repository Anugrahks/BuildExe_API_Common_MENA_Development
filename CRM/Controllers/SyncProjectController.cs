using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BuildExeServices.Models;
using System.Transactions;
using BuildExeServices.Repository;
using System.ComponentModel.Design;
using Microsoft.AspNetCore.Authorization;

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SyncProjectController : ControllerBase
    {
        private readonly ISyncProjectRepository _syncProjectRepository;
        public SyncProjectController(ISyncProjectRepository SyncProjectRepository)
        {
            _syncProjectRepository = SyncProjectRepository;
        }
        [HttpPost("ProjectBlockFloorAssign")]
        [Authorize]

        public async Task<IActionResult> PostProjectBlockFloorAssign([FromBody] SyncProject syncProject)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {

                    var val = await _syncProjectRepository.ProjectBlockFloorAssign(syncProject);
                    scope.Complete();
                    return new OkObjectResult(val);
                }
            }
            catch (Exception)
            { throw; }
        }

        [HttpPost("ProjectBookingCancellation")]
        [Authorize]

        public async Task<IActionResult> PostProjectBookingCancellation([FromBody] SyncProject syncProject)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {

                    var val = await _syncProjectRepository.ProjectBookingCancellation(syncProject);
                    scope.Complete();
                    return new OkObjectResult(val);
                }
            }
            catch (Exception)
            { throw; }
        }

        [HttpPost("ProjectConsultancyDetails")]
        [Authorize]

        public async Task<IActionResult> PostProjectConsultancyDetails([FromBody] SyncProject syncProject)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {

                    var val = await _syncProjectRepository.ProjectConsultancyDetails(syncProject);
                    scope.Complete();
                    return new OkObjectResult(val);
                }
            }
            catch (Exception)
            { throw; }
        }

        [HttpPost("ProjectDivision")]
        [Authorize]
        public async Task<IActionResult> PostProjectDivision([FromBody] SyncProject syncProject)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {

                    var val = await _syncProjectRepository.ProjectDivision(syncProject);
                    scope.Complete();
                    return new OkObjectResult(val);
                }
            }
            catch (Exception)
            { throw; }
        }

        [HttpPost("ProjectMaster")]
        [Authorize]
        public async Task<IActionResult> PostProjectMaster([FromBody] SyncProject syncProject)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {

                    var val = await _syncProjectRepository.ProjectMaster(syncProject);
                    scope.Complete();
                    return new OkObjectResult(val);
                }
            }
            catch (Exception)
            { throw; }
        }

        [HttpPost("ProjectPaymentMode")]
        [Authorize]
        public async Task<IActionResult> PostProjectPaymentMode([FromBody] SyncProject syncProject)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {

                    var val = await _syncProjectRepository.ProjectPaymentMode(syncProject);
                    scope.Complete();
                    return new OkObjectResult(val);
                }
            }
            catch (Exception)
            { throw; }
        }

        [HttpPost("ProjectStagePlanning")]
        [Authorize]
        public async Task<IActionResult> PostProjectStagePlanning([FromBody] SyncProject syncProject)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {

                    var val = await _syncProjectRepository.ProjectStagePlanning(syncProject);
                    scope.Complete();
                    return new OkObjectResult(val);
                }
            }
            catch (Exception)
            { throw; }
        }

        [HttpPost("ProjectStagePlanningDetails")]
        [Authorize]
        public async Task<IActionResult> PostProjectStagePlanningDetails([FromBody] SyncProject syncProject)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {

                    var val = await _syncProjectRepository.ProjectStagePlanningDetails(syncProject);
                    scope.Complete();
                    return new OkObjectResult(val);
                }
            }
            catch (Exception)
            { throw; }
        }

        [HttpPost("ProjectStagePlanningUsers")]
        [Authorize]
        public async Task<IActionResult> PostProjectStagePlanningUsers([FromBody] SyncProject syncProject)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {

                    var val = await _syncProjectRepository.ProjectStagePlanningUsers(syncProject);
                    scope.Complete();
                    return new OkObjectResult(val);
                }
            }
            catch (Exception)
            { throw; }
        }

        [HttpPost("ProjectStageSettings")]
        [Authorize]
        public async Task<IActionResult> PostProjectStageSettings([FromBody] SyncProject syncProject)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {

                    var val = await _syncProjectRepository.ProjectStageSettings(syncProject);
                    scope.Complete();
                    return new OkObjectResult(val);
                }
            }
            catch (Exception)
            { throw; }
        }

        [HttpPost("ProjectWorkStage")]
        [Authorize]
        public async Task<IActionResult> PostProjectWorkStage([FromBody] SyncProject syncProject)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {

                    var val = await _syncProjectRepository.ProjectWorkStage(syncProject);
                    scope.Complete();
                    return new OkObjectResult(val);
                }
            }
            catch (Exception)
            { throw; }
        }

        [HttpPost("OwnProjectDetails")]
        [Authorize]
        public async Task<IActionResult> PostOwnProjectDetails([FromBody] SyncProject syncProject)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {

                    var val = await _syncProjectRepository.OwnProjectDetails(syncProject);
                    scope.Complete();
                    return new OkObjectResult(val);
                }
            }
            catch (Exception)
            { throw; }
        }

        [HttpPost("Company")]
        [Authorize]
        public async Task<IActionResult> PostCompany([FromBody] SyncProject syncProject)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {

                    var val = await _syncProjectRepository.Company(syncProject);
                    scope.Complete();
                    return new OkObjectResult(val);
                }
            }
            catch (Exception)
            { throw; }
        }

        [HttpPost("Users")]
        [Authorize]
        public async Task<IActionResult> PostUsers([FromBody] SyncProject syncProject)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {

                    var val = await _syncProjectRepository.Users(syncProject);
                    scope.Complete();
                    return new OkObjectResult(val);
                }
            }
            catch (Exception)
            { throw; }
        }

        [HttpPost("StageActivityDetails")]
        [Authorize]
        public async Task<IActionResult> PostStageActivityDetails([FromBody] SyncProject syncProject)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {

                    var val = await _syncProjectRepository.StageActivityDetails(syncProject);
                    scope.Complete();
                    return new OkObjectResult(val);
                }
            }
            catch (Exception)
            { throw; }
        }

        [HttpPost("WorkEnquiryStageSetting")]
        [Authorize]
        public async Task<IActionResult> PosttWorkEnquiryStageSetting([FromBody] SyncProject syncProject)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {

                    var val = await _syncProjectRepository.WorkEnquiryStageSetting(syncProject);
                    scope.Complete();
                    return new OkObjectResult(val);
                }
            }
            catch (Exception)
            { throw; }
        }

        [HttpPost("WorkEnquiryStageSettingDetails")]
        [Authorize]
        public async Task<IActionResult> PostWorkEnquiryStageSettingDetails([FromBody] SyncProject syncProject)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {

                    var val = await _syncProjectRepository.WorkEnquiryStageSettingDetails(syncProject);
                    scope.Complete();
                    return new OkObjectResult(val);
                }
            }
            catch (Exception)
            { throw; }
        }

        [HttpPost("WorkEnquiryStageSettingUsers")]
        [Authorize]
        public async Task<IActionResult> PostWorkEnquiryStageSettingUsers([FromBody] SyncProject syncProject)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {

                    var val = await _syncProjectRepository.WorkEnquiryStageSettingUsers(syncProject);
                    scope.Complete();
                    return new OkObjectResult(val);
                }
            }
            catch (Exception)
            { throw; }
        }
    }
}
