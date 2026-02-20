using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
using System.Transactions;
using BuildExeBasic.Repository;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using BuildExeBasic.Library;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeBasic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentManagementController : ControllerBase
    {
        private readonly IDocumentManagementRepository _documentManagementRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public DocumentManagementController(IDocumentManagementRepository documentManagementRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _documentManagementRepository = documentManagementRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpGet("{CompanyId}/{Branchid}")]
        [Authorize]
        public async Task<IActionResult> Get(int CompanyId, int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var designation = await _documentManagementRepository.Get(CompanyId, Branchid);
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


        [HttpGet("GetForDashboard/{Branchid}")]
        [Authorize]
        public async Task<IActionResult> GetForDashboard(int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var designation = await _documentManagementRepository.GetForDashboard(Branchid,0);
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




        [HttpGet("GetForDashboard/{Branchid}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> GetForDashboard(int Branchid,int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var designation = await _documentManagementRepository.GetForDashboard(Branchid, UserId);
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


        [HttpGet("GetUser/{CompanyId}/{Branchid}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> GetUser(int CompanyId, int Branchid, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var designation = await _documentManagementRepository.Get(CompanyId, Branchid, UserId);
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


        [HttpGet("ByForm/{formName}/{masterId}")]
        [Authorize]
        public async Task<IActionResult> Get(string formName, int masterId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var designation = await _documentManagementRepository.Get(formName, masterId);
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


        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var department = await _documentManagementRepository.GetByID(id);
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



        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] DocumentManagement documentManagement, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var val = await _documentManagementRepository.Insert(documentManagement);
                    //_userLogRepository.Insert(documentManagement.UserId, documentManagement.Id, "Document Management", 1);
                    scope.Complete();
                    return new OkObjectResult(val);
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
            // return CreatedAtAction(nameof(Get), new { documentManagement = documentManagement.Id }, documentManagement);
        }





        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] DocumentManagement documentManagement, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (documentManagement != null)
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var val = await _documentManagementRepository.Update(documentManagement);
                        // _userLogRepository.Insert(documentManagement.UserId, documentManagement.Id, "Document Management", 2);
                        scope.Complete();
                        return new OkObjectResult(val);
                    }
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
        public async Task<IActionResult> Delete(int id, int userid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var val = await _documentManagementRepository.Delete(id, userid);
                //_userLogRepository.Insert(userid, id, "Document Management", 3);
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

        [HttpPost("DocumentUpload"), DisableRequestSizeLimit]
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
                    return Ok(new { dbPath , fileName });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }


        [HttpPost("DocumentUploadMobile"), DisableRequestSizeLimit]

        public async Task<IActionResult> UploadMobile()
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
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        public string AppendTimeStamp(string fileName)
        {
            return Path.Combine(Path.GetDirectoryName(fileName), string.Concat(Path.GetFileNameWithoutExtension(fileName),
                                                                               DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss_fff"),
                                                                               Path.GetExtension(fileName))
                );
        }

        [HttpDelete("DocumentDelete")]
        [Authorize]
        public async Task<IActionResult> Delete(string fileName)
        {
            List<Validation> validations = new List<Validation>();
            Validation val = new Validation();
            try
            {

                if (System.IO.File.Exists(fileName))
                {
                    //var fileName = await _documentManagementRepository.GetFileID(id);
                    System.IO.File.Delete(fileName);
                    val.StatusCode = 1;
                    val.Status = "SUCCESS";
                    val.ErrorMessage = "";
                }
                else
                {
                    val.StatusCode = 0;
                    val.Status = "FAILURE";
                    val.ErrorMessage = "File not exists";
                }
               
            }
            catch (Exception ex)
            {
                val.StatusCode = 0;
                val.Status = "FAILURE";
                val.ErrorMessage = ex.Message;
            }
            validations.Add(val);
            return new OkObjectResult(validations);
        }
        


    }
}
