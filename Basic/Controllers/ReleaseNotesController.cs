using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
using System.Transactions;
using BuildExeBasic.Repository;
using Microsoft.AspNetCore.Authorization;
using BuildExeBasic.Library;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeBasic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReleaseNotesController : ControllerBase
    {
        private IReleaseNotesRepository _releaseNotesRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public ReleaseNotesController(IReleaseNotesRepository releasenotesRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _releaseNotesRepository = releasenotesRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpGet()]
        [Authorize]

        public async Task<IActionResult> Get()
        {
            try
            {
                var designation = await _releaseNotesRepository.Get();
                return new OkObjectResult(designation);
            }
            catch (Exception)
            { throw; }
        }

        [HttpGet("{VersionNumber}")]
        [Authorize]

        public async Task<IActionResult> Get(string VersionNumber, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var designation = await _releaseNotesRepository.Get(VersionNumber);
                return new OkObjectResult(designation);
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
