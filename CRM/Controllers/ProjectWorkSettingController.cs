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
    public class ProjectWorkSettingController : ControllerBase
    {
        private readonly IProjectWorkSettingRepository _projectWorkSettingRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public ProjectWorkSettingController(IProjectWorkSettingRepository projectWorkSettingRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _projectWorkSettingRepository = projectWorkSettingRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;

        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] IEnumerable<ProjectWorkSetting> workEnquiryStageSettings, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _projectWorkSettingRepository.Insert(workEnquiryStageSettings);
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
                var result = await _projectWorkSettingRepository.Delete(id, userId);
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

        [HttpPost("DocumentUpload"), DisableRequestSizeLimit]
        [Authorize]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                var folderName = Path.Combine("Upload", "Documents");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    fileName = AppendTimeStamp(fileName);
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath, fileName });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        public string AppendTimeStamp(string fileName)
        {
            return Path.Combine(Path.GetDirectoryName(fileName), string.Concat(Path.GetFileNameWithoutExtension(fileName),
                                                                               DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss_fff"),
                                                                               Path.GetExtension(fileName))
                );
        }


        [HttpPost("GetData")]
        [Authorize]
        public async Task<IActionResult> getbyId([FromBody] ProjectWorkSetting projectWorkSetting, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var brand = await _projectWorkSettingRepository.GetById(projectWorkSetting);
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

        [HttpPost("GetDataByBranch")]
        [Authorize]
        public async Task<IActionResult> GetbyBranch([FromBody] ProjectWorkSetting projectWorkSetting, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var brand = await _projectWorkSettingRepository.GetbyBranch(projectWorkSetting);
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
        


        [HttpGet("ViewNotificationsProject/{ProjectId}/{DivisionId}/{UserId}/{JobId}")]
        [Authorize]
        public async Task<IActionResult> ViewNotificationsProject(int ProjectId, int DivisionId, int UserId, int JobId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _projectWorkSettingRepository.ViewNotificationsProject(ProjectId, DivisionId, UserId, JobId);
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
        
        //--------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet("ViewNotificationsProject/{ProjectId}/{DivisionId}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> ViewNotificationsProject(int ProjectId, int DivisionId, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _projectWorkSettingRepository.ViewNotificationsProject(ProjectId, DivisionId, UserId,0);
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

        [HttpGet("ViewNotificationsEnquiry/{Enquiry}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> ViewNotificationsEnquiry(int Enquiry, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _projectWorkSettingRepository.ViewNotificationsEnquiry(Enquiry, UserId);
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

        [HttpGet("ViewNotifications/{BranchId}/{UserId}/{Type}")]
        [Authorize]
        public async Task<IActionResult> ViewNotifications(int BranchId, int UserId, int Type, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _projectWorkSettingRepository.ViewNotifications(BranchId, UserId, Type);
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
