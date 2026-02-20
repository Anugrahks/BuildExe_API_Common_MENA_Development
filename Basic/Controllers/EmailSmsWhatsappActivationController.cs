using BuildExeBasic.Models;
using BuildExeBasic.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Transactions;
using System.Reflection;
using BuildExeBasic.Library;


namespace BuildExeBasic.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailSmsWhatsappActivationController : Controller
    {
        private readonly IEmailSmsWhatsappActivationRepository _emailSmsWhatsappActivationRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public EmailSmsWhatsappActivationController(IEmailSmsWhatsappActivationRepository emailSmsWhatsappActivationRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _emailSmsWhatsappActivationRepository = emailSmsWhatsappActivationRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }


        [HttpGet("{CompanyId}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> EmailSmsWhatsappActivation(int CompanyId, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var entity = await _emailSmsWhatsappActivationRepository.EmailSmsWhatsappActivation(CompanyId, BranchId);
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

        [HttpPost("Upload"), DisableRequestSizeLimit]
        [Authorize]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                var folderName = Path.Combine("Upload", "Documents");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    fileName = AppendTimeStamp(fileName);
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath, fileName });
                }
                else
                {
                    return BadRequest();
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
        public string AppendTimeStamp(string fileName)
        {
            return Path.Combine(Path.GetDirectoryName(fileName), string.Concat(Path.GetFileNameWithoutExtension(fileName),
                                                                               DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss_fff"),
                                                                               Path.GetExtension(fileName))
                );
        }

        public class FileRequestModel
        {
            public string File { get; set; } // Base64 string of the file content
        }

        [HttpPost("Pdf")]
        [Authorize]
        public async Task<IActionResult> UploadFileAsPdf([FromBody] FileRequestModel request, [FromHeader] string mdhash, [FromHeader] int User)
        {
            try
            {
                if (string.IsNullOrEmpty(request.File))
                {
                    return BadRequest("File content cannot be null or empty.");
                }

                // Call the SavePath method to save the PDF and get the file path
                string savedFilePath = await _emailSmsWhatsappActivationRepository.SavePath(request.File);

                // Return the file path in the response
                return Ok(new { FilePath = savedFilePath });
            }
            catch (Exception ex)
            {
                // Log the error and return an internal server error response
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }


        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] IEnumerable<EmailSmsWhatsappActivation> emailSmsWhatsappActivation, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var val = await _emailSmsWhatsappActivationRepository.Update(emailSmsWhatsappActivation);
                return new OkObjectResult(val);
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




        [HttpPost("Whatsapp")]
        [Authorize]
        public async Task<IActionResult> Post(EmailSmsWhatsappActivation emailSmsWhatsappActivation, [FromHeader] string mdhash, [FromHeader] int User)

        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var val = await _emailSmsWhatsappActivationRepository.Post(emailSmsWhatsappActivation);
                return new OkObjectResult(val);
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

        [HttpPut("Whatsapp")]
        [Authorize]
        public async Task<IActionResult> PutWhatsapp(EmailSmsWhatsappActivation emailSmsWhatsappActivation, [FromHeader] string mdhash, [FromHeader] int User)

        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var val = await _emailSmsWhatsappActivationRepository.PutWhatsapp(emailSmsWhatsappActivation);
                return new OkObjectResult(val);
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


        [HttpPut("Status")]
        [Authorize]
        public async Task<IActionResult> PutStatus(EmailSmsWhatsappActivation emailSmsWhatsappActivation, [FromHeader] string mdhash, [FromHeader] int User)

        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var val = await _emailSmsWhatsappActivationRepository.PutStatus(emailSmsWhatsappActivation);
                return new OkObjectResult(val);
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



        [HttpGet("WhatsAppConfiguration/{CompanyId}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> WhatsAppConfiguration(int CompanyId, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var entity = await _emailSmsWhatsappActivationRepository.WhatsAppConfiguration(CompanyId, BranchId);
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




        [HttpGet("{MenuId}/{CompanyId}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> GetByMenu(int MenuId,int CompanyId, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var entity = await _emailSmsWhatsappActivationRepository.GetByMenu(MenuId, CompanyId, BranchId);
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
