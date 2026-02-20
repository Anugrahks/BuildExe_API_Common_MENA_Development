using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
using System.Transactions;
using BuildExeServices.Repository;
using Microsoft.AspNetCore.Authorization;
using BuildExeServices.Library;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectBookingController : ControllerBase
    {
        private readonly IProjectBookingRepository _projectBookingRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public ProjectBookingController(IProjectBookingRepository projectBookingRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _projectBookingRepository = projectBookingRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var Project = await _projectBookingRepository.Get ();
            return new OkObjectResult(Project);
                //return new string[] { "value1", "value2" };
            }
            catch (Exception)
            { throw; }
        }

        //GET api/<ProjectController>/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _projectBookingRepository.GetByID(id);
            return new OkObjectResult(Project);
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
        [HttpGet("{companyid}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> Get(int companyid,int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _projectBookingRepository.Get(companyid, BranchId);
            return new OkObjectResult(Project);
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

        [HttpGet("getuser/{companyid}/{BranchId}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> Getbyid(int companyid, int BranchId, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
               
                try
            {
                var Project = await _projectBookingRepository.Getuser(companyid, BranchId, UserId);
                return new OkObjectResult(Project);
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
        public async Task<IActionResult> Post([FromBody] ProjectBooking  projectBooking, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                //projectmaster.GovtProj.GovtProjId = 100;

                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var val = await _projectBookingRepository.Insert(projectBooking);
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
        public async Task<IActionResult> Put([FromBody] ProjectBooking projectBooking, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (projectBooking != null)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var val = await _projectBookingRepository.Update(projectBooking);
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

        // DELETE api/<ProjectController>/5
        [HttpDelete("{id}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id,int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var department = await _projectBookingRepository.Delete(id, UserId);
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

        [HttpGet("EditDelete/{id}")]
        [Authorize]
        public async Task<IActionResult> EditDelete(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _projectBookingRepository.CheckEditDelete(id);
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
