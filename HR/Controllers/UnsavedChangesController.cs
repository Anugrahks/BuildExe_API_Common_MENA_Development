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
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnsavedChangesController : ControllerBase
    {
        private readonly IUnsavedChangesRepository _unsavedChangesRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public UnsavedChangesController(IUnsavedChangesRepository unsavedChangesRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _unsavedChangesRepository = unsavedChangesRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpGet("{id}/{purpose}")]
        [Authorize]

        public async Task<IActionResult> UnsavedChangess(int id, int purpose, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var department = await _unsavedChangesRepository.UnsavedChanges(id, purpose);
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

        [HttpGet("{purpose}/{dateworked}/{projectid}")]
        [Authorize]

        public async Task<IActionResult> UnsavedChangess(int purpose, string dateworked, int projectid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var department = await _unsavedChangesRepository.UnsavedChangesAdd(purpose, dateworked, projectid);
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


        [HttpGet("{purpose}/{fromdate}/{todate}/{branchid}/{isGroup}")]
        [Authorize]

        public async Task<IActionResult> UnsavedChangess(int purpose, DateTime fromdate,DateTime todate, int branchid, int isGroup, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var department = await _unsavedChangesRepository.UnsavedChangesAddnew(purpose, fromdate, todate, branchid,  isGroup);
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

    }
}
