using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
using System.Transactions;
using BuildExeHR.Repository;
using Microsoft.AspNetCore.Authorization;
using BuildExeHR.Library;

namespace BuildExeHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaboursInProjectDetailController : ControllerBase
    {

        private readonly ILaboursInProjectRepository _laboursInProjectRepository   ;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public LaboursInProjectDetailController(ILaboursInProjectRepository laboursInProjectRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _laboursInProjectRepository = laboursInProjectRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] LaboursInProject laboursInProject, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (laboursInProject != null)
                {
                    var product = await _laboursInProjectRepository.GetEmplyeeInProject(laboursInProject);
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

        [HttpPost("EmployeeWise")]
        [Authorize]
        public async Task<IActionResult> EmployeeWise([FromBody] HRSearch laboursInProject, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    if (laboursInProject != null)
                    {
                        var product = await _laboursInProjectRepository.EmployeeWise(laboursInProject);
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
    }
}
