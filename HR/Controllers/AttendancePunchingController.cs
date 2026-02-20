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
using BuildExeHR.Library;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendancePunchingController : ControllerBase
    {
        private readonly IAttendancePunchingRepository _attendanceRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public AttendancePunchingController(IAttendancePunchingRepository attendanceRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _attendanceRepository = attendanceRepository;
            _userLogRepository = userLogRepository;
                _mdHashValidator = mdHashValidator;
        }


        [HttpGet("Employee/{EmployeeId}/{DateWorked}")]
        [Authorize]

        public async Task<IActionResult> Get(int EmployeeId,DateTime DateWorked, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var department = await _attendanceRepository.GetEmployee(EmployeeId, DateWorked);
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


        [HttpGet("List/{BranchId}")]
        [Authorize]

        public async Task<IActionResult> Get(int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var department = await _attendanceRepository.GetListDetails(BranchId);
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

        [HttpGet("Details/{Id}")]
        [Authorize]

        public async Task<IActionResult> Getdetails(int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var department = await _attendanceRepository.Getdetails(Id);
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

        public async Task<IActionResult> Post([FromBody] IEnumerable<AttendancePunchingConfirm> attendance, [FromHeader] string mdhash, [FromHeader] int User)
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

        [HttpPost("PunchingDetails")]


        public async Task<IActionResult> PunchingDetails([FromBody] IEnumerable<HRSearch> attendance)
        {
                try
            {

                var department = await _attendanceRepository.PunchingDetails(attendance);
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


        [HttpPut]
        [Authorize]

        public async Task<IActionResult> Put([FromBody] IEnumerable<AttendancePunchingConfirm> attendance, [FromHeader] string mdhash, [FromHeader] int User)
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

        [HttpDelete("PunchingDelete")]
        [Authorize]
        public async Task<IActionResult> DeletePunching(string fileName)
        {
            List<Validation> validations = new List<Validation>();
            Validation val = new Validation();
            try
            {

                if (System.IO.File.Exists(fileName))
                {
                    //var fileName = await _documentManagementRepository.GetFileID(id);
                    System.IO.File.Delete(fileName);
                    val.StatusCode = 1;
                    val.Status = "SUCCESS";
                    val.ErrorMessage = "";
                }
                else
                {
                    val.StatusCode = 0;
                    val.Status = "FAILURE";
                    val.ErrorMessage = "File not exists";
                }

            }
            catch (Exception ex)
            {
                val.StatusCode = 0;
                val.Status = "FAILURE";
                val.ErrorMessage = ex.Message;
            }
            validations.Add(val);
            return new OkObjectResult(validations);
        }
    }
}
