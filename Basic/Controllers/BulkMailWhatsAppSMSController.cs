using BuildExeBasic.Models;
using BuildExeBasic.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using BuildExeBasic.Library;


namespace BuildExeBasic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BulkMailWhatsAppSMSController : Controller
    {
        private readonly IEmailConfigurationRepository _emailConfigurationRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public BulkMailWhatsAppSMSController(IEmailConfigurationRepository emailConfigurationRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _emailConfigurationRepository = emailConfigurationRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(BulkMailWhatsAppSMS configuration, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var entity = await _emailConfigurationRepository.PostBulk(configuration);
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


       
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Edit(BulkMailWhatsAppSMS emailConfiguration, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var entity = await _emailConfigurationRepository.EditBulk(emailConfiguration);

                    if (entity != null)
                    {
                        return new OkObjectResult(entity);
                    }
                    else
                    {
                        return new NotFoundObjectResult("No matching entity found.");
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

        [HttpDelete("{id}/{UserID}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id, int UserID, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {

                try
                {
                    await _emailConfigurationRepository.DeleteBulk(id);
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

        [HttpGet("{CompanyId}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> GetList(int CompanyId, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var template = await _emailConfigurationRepository.GetListBulk(CompanyId, BranchId);
                    return new OkObjectResult(template);
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


        [HttpPost("GetData")]
        [Authorize]
        public async Task<IActionResult> GetList([FromBody] BasicSearch basicSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var template = await _emailConfigurationRepository.getDataForEmail(basicSearch.FormType, basicSearch.BranchId, basicSearch.Ids);
                    return new OkObjectResult(template);
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
