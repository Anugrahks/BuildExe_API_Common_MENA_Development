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
    public class EnquiryModeController : ControllerBase
    {
        private readonly IEnquiryModeRepository _EnquiryModeRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public EnquiryModeController(IEnquiryModeRepository EnquiryModeRepository, IUserLogRepository userLogRepository,  IMdHashValidator mdHashValidator)
        {
            _EnquiryModeRepository = EnquiryModeRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var enquirymode = _EnquiryModeRepository.Getenquirymode();
            return new OkObjectResult(enquirymode);
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            var enquirymode = _EnquiryModeRepository.GetenquirymodeByID (id);
            return new OkObjectResult(enquirymode);
        }
        [HttpGet("{Companyid}/{BranchId}")]
        [Authorize]
        public IActionResult Get(int Companyid, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            var product = _EnquiryModeRepository.Getenquirymode(Companyid, BranchId);
            return new OkObjectResult(product);
        }

        [HttpGet("getuser/{Companyid}/{BranchId}/{UserId}")]
        [Authorize]
        public IActionResult Getbyid(int Companyid, int BranchId, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            var product = _EnquiryModeRepository.Getenquirymodeuser(Companyid, BranchId, UserId);
            return new OkObjectResult(product);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] EnquiryMode enquiryMode, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var val = await _EnquiryModeRepository.Insertenquirymode(enquiryMode);
                    //_userLogRepository.Insert(enquiryMode.UserId, enquiryMode.Id, "ENQUIRY MODE", 1);
                    scope.Complete();
                    return new OkObjectResult(val); //CreatedAtAction(nameof(Get), new { id = enquiryMode.Id}, enquiryMode);
                }

                //using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                //{
                //    await _menuRepository.Update(menus);
                //    scope.Complete();
                //    return new OkResult();
                //}
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
        public async Task<IActionResult> Put([FromBody] EnquiryMode enquiryMode, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {

                if (enquiryMode != null)
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var val = await _EnquiryModeRepository.Update_enquirymode(enquiryMode);
                        //_userLogRepository.Insert(enquiryMode.UserId, enquiryMode.Id, "ENQUIRY MODE",2);
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
        public async Task<IActionResult> Delete(int id,int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try { 
            var val =await _EnquiryModeRepository.Delete_enquirymode(id);
            //_userLogRepository.Insert(UserId, id, "ENQUIRY MODE",3);
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
    }
}
