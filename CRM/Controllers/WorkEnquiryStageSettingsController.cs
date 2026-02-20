using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using BuildExeServices.Repository;
using BuildExeServices.Models;
using Microsoft.AspNetCore.Authorization;
using BuildExeServices.Library;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkEnquiryStageSettingController : ControllerBase
    {
        private readonly IWorkEnquiryStageSettingsRepository _workEnquiryStageSettingsRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public WorkEnquiryStageSettingController(IWorkEnquiryStageSettingsRepository workEnquiryStageSettings, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _workEnquiryStageSettingsRepository = workEnquiryStageSettings;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] IEnumerable<WorkEnquiryStageSettings> workEnquiryStageSettings, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _workEnquiryStageSettingsRepository.Insert(workEnquiryStageSettings);
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
        public async Task<IActionResult> Put([FromBody] IEnumerable<WorkEnquiryStageSettings> workEnquiryStageSettings, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {

                var result = await _workEnquiryStageSettingsRepository.Update(workEnquiryStageSettings);
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
                var result = await _workEnquiryStageSettingsRepository.Delete(id, userId);
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

        [HttpGet("getuser/{CompanyId}/{BranchId}/{UserId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> Getforedit(int companyId, int BranchId, int userId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var department = await _workEnquiryStageSettingsRepository.Getforedit(companyId, BranchId, userId, FinancialYearId);
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

        [HttpPost("Dashboard")]
        [Authorize]
        public async Task<IActionResult> Dashboard([FromBody] WorkEnquiryStageSettingsDashBoard workEnquiryStageSettingsDashBoard, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _workEnquiryStageSettingsRepository.Dashboard(workEnquiryStageSettingsDashBoard);
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

        [HttpPost("JobDashboard")]
        [Authorize]
        public async Task<IActionResult> JobDashboard([FromBody] WorkEnquiryStageSettingsDashBoard workEnquiryStageSettingsDashBoard, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _workEnquiryStageSettingsRepository.JobDashboard(workEnquiryStageSettingsDashBoard);
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

        [HttpGet("getbyId/{Id}")]
        [Authorize]
        public async Task<IActionResult> getbyId(int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var brand = await _workEnquiryStageSettingsRepository.GetById(Id);
                return new OkObjectResult(brand);
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
        [HttpGet("getNormalProject/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> getNormalProject(int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var brand = await _workEnquiryStageSettingsRepository.getNormalProject(BranchId);
                return new OkObjectResult(brand);
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


        [HttpPost("getDataForDashboard")]
        [Authorize]
        public async Task<IActionResult> getDataForDashboard([FromBody] EnquiryReportSearch billSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var brand = await _workEnquiryStageSettingsRepository.getDataForDashboard(billSearch);
                return new OkObjectResult(brand);
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

        [HttpGet("EditDelete/{id}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> EditDelete(int id, int userId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _workEnquiryStageSettingsRepository.EditDelete(id, userId);
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

        [HttpGet("GetDataForSync/{TableName}/{LastSyncDate}")]
        public async Task<IActionResult> GetDataForSync(string TableName, DateTime LastSyncDate)
        {
            //Please don't add MDHASH validation.
            try
            {
                var syncData = await _workEnquiryStageSettingsRepository.GetDataForSync(TableName, LastSyncDate);
                return new OkObjectResult(syncData);
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
    }
}
