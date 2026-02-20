using BuildExeBasic.Models;
using BuildExeBasic.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using BuildExeBasic.Library;
using System.Security.Policy;


namespace BuildExeBasic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WhatsAppSmsFetchController : Controller
    {
        private readonly IEmailConfigurationRepository _emailConfigurationRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public WhatsAppSmsFetchController(IEmailConfigurationRepository emailConfigurationRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _emailConfigurationRepository = emailConfigurationRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

       
        [HttpGet("{Id}/{MenuId}")]
        [Authorize]
        public async Task<IActionResult> WhatsappSmsFetch(int Id, int MenuId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var entity = await _emailConfigurationRepository.WhatsappSmsFetch(Id, MenuId);
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

