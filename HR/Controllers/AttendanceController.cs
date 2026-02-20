using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using BuildExeHR.Models;
using BuildExeHR.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using BuildExeHR.Library;
using Newtonsoft.Json;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public AttendanceController(IAttendanceRepository attendanceRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _attendanceRepository = attendanceRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpGet]
        [Authorize]

        public async Task<IActionResult> Get()
        {
            try
            {
                var designation = await _attendanceRepository.Get();
                return new OkObjectResult(designation);
            }
            catch (Exception)
            { throw; }
        }

        [HttpGet("{companyid}/{branchid}/{Menuid}")]
        [Authorize]

        public async Task<IActionResult> Get(int companyid, int branchid, int Menuid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var department = await _attendanceRepository.GetForEdit(companyid, branchid, Menuid);
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

        [HttpGet("getuser/{companyid}/{branchid}/{Menuid}/{UserId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> Getbyuser(int companyid, int branchid, int Menuid, int UserId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var department = await _attendanceRepository.GetForEdituser(companyid, branchid, Menuid, UserId, FinancialYearId);
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

        [HttpGet("{companyid}/{branchid}/{Userid}/{Menuid}/{FinancialYearId}")]
        [Authorize]

        public async Task<IActionResult> Get(int companyid, int branchid, int Userid, int Menuid, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var department = await _attendanceRepository.GetForApprovals(companyid, branchid, Userid, Menuid, FinancialYearId);
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

        public async Task<IActionResult> Post([FromBody] IEnumerable<Attendance> attendance, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {

                    var department = await _attendanceRepository.Insert(attendance);
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




        [HttpPut]
        [Authorize]

        public async Task<IActionResult> Put([FromBody] IEnumerable<Attendance> attendance, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    //       if (attendance != null)
                    // {

                    var department = await _attendanceRepository.Update(attendance);
                    return new OkObjectResult(department);
                    //}
                    //       return new NoContentResult();
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
                    await _attendanceRepository.Delete(id, UserId);
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

        [HttpPost("{Report}")]
        [Authorize]

        public async Task<IActionResult> Post([FromBody] HRSearch hRSearchs, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    if (hRSearchs != null)
                    {
                        var product = await _attendanceRepository.Getjson(hRSearchs);
                        return new OkObjectResult(product);
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

        [HttpPost("punching")]
        public async Task<IActionResult> Punchings(
            [FromBody] AttendancePunching attendancePunching,
            [FromHeader] string mdhash,
            [FromHeader] int User)
        {
            try
            {
                // Ensure API call is logged properly
                Logger.InfoLog(nameof(Punchings), "punching", "API called successfully");

                if (attendancePunching == null)
                {
                    Logger.InfoLog(nameof(Punchings), "punching", "Received NULL payload. Model binding failed.");
                    return BadRequest(new { message = "Invalid request payload", statusCode = 400 });
                }

                // Log full payload for debugging
                Logger.InfoLog(nameof(Punchings), "punching", $"Payload: {JsonConvert.SerializeObject(attendancePunching)}");

                var res = await _attendanceRepository.punching(attendancePunching);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(nameof(Punchings), "punching", ex);
                return StatusCode(500, new
                {
                    message = "An internal server error occurred. Please try again later.",
                    statusCode = 0
                });
            }
        }


        [HttpGet("TADetails/{EmployeeId}")]

        public async Task<IActionResult> TADetails(int EmployeeId)
        {
                try
                {
                    var department = await _attendanceRepository.TADetails(EmployeeId);
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


        [HttpGet("TAByMonth/{EmployeeId}/{MonthId}/{YearId}")]
        [Authorize]

        public async Task<IActionResult> TAByMonth(int EmployeeId,int MonthId, int YearId,  [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var department = await _attendanceRepository.TAByMonth(EmployeeId, MonthId, YearId);
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


        [HttpPost("biomatrix")]
        public async Task<IActionResult> BiomatrixBulkEntry(
            [FromBody] IEnumerable<AttendancePunching> attendancePunchings,
            [FromHeader] string mdhash,
            [FromHeader] int User)
        {
            try
            {
                // Log API call
                Logger.InfoLog(nameof(BiomatrixBulkEntry), "biomatrix", "API called successfully");

                if (attendancePunchings == null || !attendancePunchings.Any())
                {
                    Logger.InfoLog(nameof(BiomatrixBulkEntry), "biomatrix", "Received NULL or empty payload. Model binding failed.");
                    return BadRequest(new { message = "Invalid request payload", statusCode = 400 });
                }

                // Log full payload for debugging
                Logger.InfoLog(nameof(BiomatrixBulkEntry), "biomatrix", $"Payload: {JsonConvert.SerializeObject(attendancePunchings)}");

                var res = await _attendanceRepository.BiomatrixBulkEntry(attendancePunchings);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(nameof(BiomatrixBulkEntry), "biomatrix", ex);
                return StatusCode(500, new
                {
                    message = "An internal server error occurred. Please try again later.",
                    statusCode = 0
                });
            }
        }

        [HttpPost("NewBiomatrix")]

        public async Task<IActionResult> InsertPunching([FromBody] AttendancePunching reportInput)
        {

            try
            {

                var val = await _attendanceRepository.NewBiomatrix(reportInput);
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


        private static readonly string[] supportedContentTypes = new[]
{
        "image/jpeg", "image/jpg", "image/png", "image/gif", "image/bmp",
        "image/tiff", "image/tif", "image/webp", "image/svg+xml",
        "image/heic", "image/heif", "image/x-icon", "image/vnd.microsoft.icon",
        "image/apng", "application/pdf"
    };


        [HttpPost("punchingupload"), DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var contentType = Request.ContentType;
                Logger.InfoLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Upload method called with contentType: " + contentType);

                if (contentType.StartsWith("multipart/form-data"))
                {
                    var formCollection = await Request.ReadFormAsync();
                    var file = formCollection.Files.FirstOrDefault();
                    var employeeUserName = SanitizeFileName(formCollection["EmployeeUserName"].FirstOrDefault());

                    if (string.IsNullOrEmpty(employeeUserName))
                    {
                        Logger.InfoLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Missing EmployeeUserName");
                    }

                    if (file != null && file.Length > 0 && supportedContentTypes.Contains(file.ContentType) && !string.IsNullOrEmpty(employeeUserName))
                    {
                        return await SaveFile(file, employeeUserName);
                    }
                    else
                    {
                        var errorResponse = "No file uploaded, unsupported content type.";
                        Logger.InfoLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, errorResponse);
                        return BadRequest(errorResponse);
                    }
                }
                else if (supportedContentTypes.Any(ct => contentType.StartsWith(ct)))
                {
                    var employeeUserName = SanitizeFileName(Request.Headers["EmployeeUserName"].ToString());
                    if (!string.IsNullOrEmpty(employeeUserName) && Request.Body != null)
                    {
                        var extension = contentType.Split('/').Last();
                        var fileName = $"{employeeUserName}_{Guid.NewGuid():N}.{extension}";
                        var folderName = Path.Combine("Upload", "Punching");
                        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                        var fullPath = Path.Combine(pathToSave, fileName);

                        if (!Directory.Exists(pathToSave))
                        {
                            Directory.CreateDirectory(pathToSave);
                        }

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await Request.Body.CopyToAsync(stream);
                        }

                        var successResponse = new { dbPath = Path.Combine(folderName, fileName).Replace("\\", "/"), fileName };
                        Logger.InfoLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Response: " + Newtonsoft.Json.JsonConvert.SerializeObject(successResponse));

                        return Ok(successResponse);
                    }
                    else
                    {
                        var errorResponse = "No file uploaded or missing EmployeeUserName.";
                        Logger.InfoLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, errorResponse);
                        return BadRequest(errorResponse);
                    }
                }
                else
                {
                    var errorResponse = "Unsupported content type.";
                    Logger.InfoLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, errorResponse);
                    return BadRequest(errorResponse);
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        private async Task<IActionResult> SaveFile(IFormFile file, string employeeUserName)
        {
            try
            {
                Logger.InfoLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "SaveFile method called");

                var folderName = Path.Combine("Upload", "Punching");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                var extension = Path.GetExtension(file.FileName);
                var fileName = $"{SanitizeFileName(employeeUserName)}_{Guid.NewGuid():N}{extension}";
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName).Replace("\\", "/");

                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                }

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var successResponse = new { dbPath, fileName };
                Logger.InfoLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Response: " + Newtonsoft.Json.JsonConvert.SerializeObject(successResponse));

                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                return StatusCode(500, new { message = "Error saving file", error = ex.Message });
            }
        }

        private string SanitizeFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return string.Empty;

            return Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), "_"));
        }


        [HttpPost("bulk")]
        [Authorize]

        public async Task<IActionResult> BulkAttendanceEntry([FromBody] List<Attendance> attendances, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    await _attendanceRepository.BulkAttendanceEntry(attendances);
                    return Ok();

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

        [HttpPost("DateWiseReport")]
        [Authorize]

        public async Task<IActionResult> DateWiseReports([FromBody] HRSearch hRSearchs, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    if (hRSearchs != null)
                    {
                        var product = await _attendanceRepository.DateWiseReport(hRSearchs);
                        return new OkObjectResult(product);
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

        [HttpPut("bulk/edit")]
        [Authorize]

        public async Task<IActionResult> Edit([FromBody] List<Attendance> attendances, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    await _attendanceRepository.Edit(attendances);
                    return Ok();
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

        [HttpPost("Details")]
        [Authorize]
        public async Task<IActionResult> GetAttendanceDetails([FromBody] HRSearch attendanceDetailsSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var attendanceDetails = await _attendanceRepository.GetAttendanceDetails(attendanceDetailsSearch);
                    return new OkObjectResult(attendanceDetails);
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

        [HttpPost("NextAttendanceDate")]
        [Authorize]
        public async Task<IActionResult> GetDefaultDate([FromBody] HRSearch attendanceDetailsSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var date = await _attendanceRepository.GetDefaultDate(attendanceDetailsSearch);
                    return new OkObjectResult(date);
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
