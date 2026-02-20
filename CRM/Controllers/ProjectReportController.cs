using Microsoft.AspNetCore.Http;
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

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectReportController : ControllerBase
    {
        private readonly IProjectMasterRepository _ProjectMasterRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public ProjectReportController(IProjectMasterRepository ProjectMasterRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _ProjectMasterRepository = ProjectMasterRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpPost]
        [Authorize]
        public async Task <IActionResult> Post([FromBody] ProjectSearch projectSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (projectSearch != null)
                {
                    var product = await _ProjectMasterRepository.GetReport(projectSearch);

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

        [HttpGet("{BranchId}")]
        [Authorize]
        public async Task<IActionResult> GetClient(int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var entity = await _ProjectMasterRepository.GetClient(BranchId);
                return new OkObjectResult(entity);
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
