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
    public class EmailConfigurationController : Controller
    {
        private readonly IEmailConfigurationRepository _emailConfigurationRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public EmailConfigurationController(IEmailConfigurationRepository emailConfigurationRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _emailConfigurationRepository = emailConfigurationRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(EmailConfiguration configuration, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var entity = await _emailConfigurationRepository.Post(configuration);
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

        [HttpPost("send")]
        [Authorize]
        public async Task<IActionResult> SendAsyncMail(EmailRequestModel emailRequestModel, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {


                var entity = await _emailConfigurationRepository.SendEmailAsync(emailRequestModel.CompanyId, emailRequestModel.BranchId, emailRequestModel.MenuId, emailRequestModel.Id, emailRequestModel.file, emailRequestModel.Content);
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

        [HttpPost("sendbulk")]
        [Authorize]
        public async Task<IActionResult> SendBulkAsync([FromBody] BulkEmailRequest request, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _emailConfigurationRepository.SendBulkEmailAsync(request);
                return Ok(new { statusCode = 1, message = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    statusCode = 0,
                    message = $"Error sending bulk email: {ex.Message}"
                });
            }
            }
            else
            {
                return Unauthorized("Invalid MdHash");
            }
        }


        [HttpGet("{MenuId}/{CompanyId}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> GetContent(int MenuId, int CompanyId, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var entity = await _emailConfigurationRepository.GetContent(MenuId, CompanyId, BranchId);
                return Json(new { content = entity });
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
                var entity = await _emailConfigurationRepository.GetList(CompanyId, BranchId);
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

        [HttpPost("status")]
        [Authorize]
        public async Task<IActionResult> ChangeStatus(EmailStatusModel emailStatusModel, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var entity = await _emailConfigurationRepository.ChangeStatus(emailStatusModel.id, emailStatusModel.status);
                if (entity == "success")
                {
                    return Ok();
                }
                return BadRequest();
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

        [HttpGet("status/{MenuId}/{CompanyId}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> CheckStatus(int MenuId, int CompanyId, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var entity = await _emailConfigurationRepository.CheckStatus(MenuId, CompanyId, BranchId);
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
        public async Task<IActionResult> Edit(EmailConfiguration emailConfiguration, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                EmailConfiguration entity = await _emailConfigurationRepository.EditEmailConfiguration(emailConfiguration);

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

        [HttpGet("template/{MenuId}/{CompanyId}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> GetTemplate(int MenuId, int CompanyId, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var template = await _emailConfigurationRepository.GetTemplate(MenuId, CompanyId, BranchId);
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
