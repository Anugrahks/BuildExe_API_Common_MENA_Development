using BuildExeServices.Library;
using BuildExeServices.Models;
using BuildExeServices.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Transactions;

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectBookingCancellationController : ControllerBase
    {
        private readonly IProjectBookingCancellationRepository _projectBookingCancellationRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public ProjectBookingCancellationController(IProjectBookingCancellationRepository projectBookingCancellationRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _projectBookingCancellationRepository = projectBookingCancellationRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;

        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] ProjectBookingCancellation projectBookingCancellations, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await _projectBookingCancellationRepository.Insert(projectBookingCancellations);

                    scope.Complete();
                    return new OkResult();
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

        [HttpGet("GetDeletedClients")]
        [Authorize]
        public async Task<IActionResult> GetDeletedClients([FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var product = await _projectBookingCancellationRepository.GetDeletedClients();

                    return new OkObjectResult(product);
                    
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

        [HttpPost("ProjectCancelReport")]
        [Authorize]
        public async Task<IActionResult> ProjectCancelReport([FromBody] ProjectBookingCancelFilter filter, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var product = await _projectBookingCancellationRepository.ProjectCancelReport(filter);

                    return new OkObjectResult(product);

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
