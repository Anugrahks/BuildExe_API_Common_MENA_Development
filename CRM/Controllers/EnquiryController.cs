using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BuildExeServices.Models;
using BuildExeServices.Repository;

using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using BuildExeServices.Library;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnquiryController : ControllerBase
    {
        private readonly IEnquiryRepository _enquiryRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public EnquiryController(IEnquiryRepository enquiryRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _enquiryRepository = enquiryRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try { 
            var branch = await _enquiryRepository.GetEnquiry ();
            return new OkObjectResult(branch);
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

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var branch = await _enquiryRepository.GetEnquiryByID(id);

                return new OkObjectResult(branch);
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
        [HttpGet("{Companyid}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> Get(int Companyid, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var product = await _enquiryRepository.GetEnquiry(Companyid, BranchId);
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
            else
            {
                return Unauthorized("Invalid MdHash");
            }

        }

        
        [HttpGet("{id}/{enquiryid}/{Companyid}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> Get(int id, string enquiryid,int Companyid, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product = await _enquiryRepository.GetEnquiryIdValidation(id, enquiryid, Companyid, BranchId);
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


        [HttpPost("Validation")]
        [Authorize]
        public async Task<IActionResult> PostValidation([FromBody] EnquiryValidation enquiryValidation, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product = await _enquiryRepository.GetEnquiryIdValidation(enquiryValidation.Id, enquiryValidation.EnquiryId, 
                    enquiryValidation.CompanyId, enquiryValidation.BranchId);
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
        public async Task<IActionResult> Post([FromBody] Enquiry enquiry, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try { 
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled ))
            {
                    var val = await _enquiryRepository.InsertEnquiry(enquiry);
               // _userLogRepository.Insert(enquiry.UserId, enquiry.EnquiryId, "ENQUIRY", 1);
                scope.Complete();
                    return new OkObjectResult(val); //CreatedAtAction(nameof(Get), new { id = enquiry.EnquiryId  }, enquiry);
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

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] Enquiry enquiry, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try {
                if (enquiry != null)
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var val = await _enquiryRepository.UpdateEnquiry(enquiry);
                        // _userLogRepository.Insert(enquiry.UserId, enquiry.EnquiryId, "ENQUIRY", 2);
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
        public async Task <IActionResult> Delete(int id,int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try { 
            var val =await _enquiryRepository.DeleteEnquiry(id, UserId);
                //_userLogRepository.Insert(UserId, id, "ENQUIRY", 3);
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
        [HttpPost("import")]
        public async Task<IActionResult> ImportEnquiriesFromCsv([FromForm] string csvFilePath, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                await _enquiryRepository.ImportEnquiriesFromCsv(csvFilePath);
            return Ok("CSV data imported successfully.");
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



        #region Enquiry Related EditDelete

        #endregion

        [HttpGet("EditDelete/{id}/{type}")]
        [Authorize]
        public async Task<IActionResult> EditDelete(int id, int type, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _enquiryRepository.CheckEditDelete(id, type);
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

        [HttpDelete("DeleteMessage/{id}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> DeleteMessage(int id, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var val = await _enquiryRepository.DeleteMessage(id, UserId);
                    //_userLogRepository.Insert(UserId, id, "ENQUIRY", 3);
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


        [HttpGet("LocationData")]
        [Authorize]
        public async Task<IActionResult> GetLocationData()
        {
            try
            {
                var branch = await _enquiryRepository.GetLocationData();
                return new OkObjectResult(branch);
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

        [HttpGet("UnqProspectName/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> UnqProspectName(int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var result = await _enquiryRepository.UnqProspectName(BranchId);
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

        
    }
}
