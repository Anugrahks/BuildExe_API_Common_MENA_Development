using BuildExeServices.Library;
using BuildExeServices.Models;
using BuildExeServices.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecificationMasterController : ControllerBase
    {
        private readonly ISpecificationMasterRepository _specificationMasterRepository ;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public SpecificationMasterController(ISpecificationMasterRepository specificationMasterRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _specificationMasterRepository = specificationMasterRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
            
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var products = await _specificationMasterRepository.Get();
            return new OkObjectResult(products);
            }
            catch (Exception)
            { throw; }
        }
        [HttpGet("{companyid}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> Get(int companyid, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product = await _specificationMasterRepository.Get(companyid, BranchId);
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

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product = await _specificationMasterRepository.GetbyID(id);
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


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] IEnumerable<SpecificationMaster> specificationMasters, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
               var val = await _specificationMasterRepository.Insert (specificationMasters);
              
                scope.Complete();
                return new OkObjectResult(val);
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


        [HttpPost("ImageUpload"), DisableRequestSizeLimit]

        public async Task<IActionResult> Upload()
        {
            try
            {
                var contentType = Request.ContentType;
                var supportedContentTypes = new[] { "image/jpeg", "image/jpg", "image/png", "application/pdf", "image/heic" };
                var guid = Guid.NewGuid();
                var shortGuid = Convert.ToBase64String(guid.ToByteArray())
                .Replace("/", "_")
                .Replace("+", "-")
                .Substring(0, 22);
                if (contentType.StartsWith("multipart/form-data"))
                {
                    var formCollection = await Request.ReadFormAsync();
                    var file = formCollection.Files.FirstOrDefault();
                    string rawValue = formCollection["SpecName"].FirstOrDefault();

                    // Remove bullets, punctuation, and symbols, keep only letters and digits
                    string employeeUserName = Regex.Replace(rawValue, @"[^a-zA-Z0-9]", "");

                    if (employeeUserName.Length > 50)
                    {
                        employeeUserName = employeeUserName.Substring(0, 50);
                    }

                    if (file != null && file.Length > 0 && supportedContentTypes.Contains(file.ContentType) && !string.IsNullOrEmpty(employeeUserName))
                    {
                        // Use EmployeeUserName to rename the file
                        var extension = Path.GetExtension(file.FileName);
                        var fileName = $"{employeeUserName}_{shortGuid}{extension}";
                        var folderName = Path.Combine("Upload", "Images");
                        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                        var fullPath = Path.Combine(pathToSave, fileName);

                        if (!Directory.Exists(pathToSave))
                        {
                            Directory.CreateDirectory(pathToSave);
                        }

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        return Ok(new { dbPath = Path.Combine(folderName, fileName).Replace("\\", "/"), fileName });
                    }
                    else
                    {
                        return BadRequest("No file uploaded, unsupported content type, or missing SpecName.");
                    }
                }
                else if (supportedContentTypes.Contains(contentType))
                {

                    string rawValue = Request.Headers["SpecName"].FirstOrDefault() ?? string.Empty;

                    // Remove bullets, punctuation, and symbols, keep only letters and digits
                    string employeeUserName = Regex.Replace(rawValue, @"[^a-zA-Z0-9]", "");

                    if (employeeUserName.Length > 50)
                    {
                        employeeUserName = employeeUserName.Substring(0, 50);
                    }

                    if (!string.IsNullOrEmpty(employeeUserName) && Request.Body != null)
                    {
                        var extension = contentType.Split('/').Last();
                        var fileName = $"{employeeUserName}_{Guid.NewGuid()}.{extension}";
                        var folderName = Path.Combine("Upload", "Images");
                        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                        var fullPath = Path.Combine(pathToSave, fileName);

                        if (!Directory.Exists(pathToSave))
                        {
                            Directory.CreateDirectory(pathToSave);
                        }

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await Request.Body.CopyToAsync(stream);
                        }

                        return Ok(new { dbPath = Path.Combine(folderName, fileName).Replace("\\", "/"), fileName });
                    }
                    else
                    {
                        return BadRequest("No file uploaded or missing SpecName.");
                    }
                }
                else
                {
                    return BadRequest("Unsupported content type.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        public string AppendTimeStamp(string fileName)
        {
            return string.Concat(
                Path.GetFileNameWithoutExtension(fileName),
                DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss_fff"),
                Path.GetExtension(fileName)
            );
        }


        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] IEnumerable<SpecificationMaster> specificationMasters, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (specificationMasters != null)
                {
                    var val = await _specificationMasterRepository.Update(specificationMasters);
                    return new OkObjectResult(val);

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


        [HttpDelete("{id}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
               var val=await _specificationMasterRepository.Delete(id, UserId);
           
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

        [HttpGet("EditDelete/{id}")]
        [Authorize]
        public async Task<IActionResult> EditDelete(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _specificationMasterRepository.CheckEditDelete(id);
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

        [HttpPost("BudgetForcasting")]
        [Authorize]
        public async Task<IActionResult> PostBudgetForcasting([FromBody] BudgetForcasting budgetForcasting, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    if (budgetForcasting != null)
                    {
                        var details = await _specificationMasterRepository.PostBudgetForcasting(budgetForcasting);
                        return new OkObjectResult(details);
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
