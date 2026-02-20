using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using BuildExeServices.Repository;
using BuildExeServices.Models;
using Microsoft.AspNetCore.Authorization;
using BuildExeServices.Library;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnquiryStatusController : ControllerBase
    {
        private readonly IEnquiryStatusRepository _enquiryStatusRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public EnquiryStatusController(IEnquiryStatusRepository EnquiryStatusRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _enquiryStatusRepository = EnquiryStatusRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var products = await _enquiryStatusRepository.Get();
            return new OkObjectResult(products);
            }
            catch (Exception)
            { throw; }
        }
        [HttpGet("{companyid}/{Branchid}")]
        [Authorize]
        public async Task<IActionResult> Get(int companyid,int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var products = await _enquiryStatusRepository.Get(companyid, Branchid);
                return new OkObjectResult(products);
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
                var product = await _enquiryStatusRepository.GetByID(id);
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

        [HttpGet("getuser/{companyid}/{Branchid}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> Getbyid(int companyid, int Branchid, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var products = await _enquiryStatusRepository.Getuser(companyid, Branchid, UserId);
                return new OkObjectResult(products);
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
        public async Task<IActionResult> Post([FromBody] EnquiryStatus enquiryStatus, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                    var val = await _enquiryStatusRepository.Insert(enquiryStatus);
                    //_userLogRepository.Insert(enquiryMode.UserId, enquiryMode.Id, "ENQUIRY MODE",2);
                    scope.Complete();
                    return new OkObjectResult(val);
                //    await _enquiryStatusRepository.Insert(enquiryStatus);
                ////_userLogRepository.Insert(enquiryStatus.UserId, enquiryStatus.EnquiryStatusId, "ENQUIRY STATUS", 1);
                //scope.Complete();
                //return CreatedAtAction(nameof(Get), new { id = enquiryStatus.EnquiryStatusId}, enquiryStatus);
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
        public async Task<IActionResult> Put([FromBody] EnquiryStatus enquiryStatus, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (enquiryStatus != null)
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var val = await _enquiryStatusRepository.Update(enquiryStatus);
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
        public async Task< IActionResult> Delete(int id,int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
               var val =await _enquiryStatusRepository.Delete(id, UserId);
           // _userLogRepository.Insert(UserId, id, "ENQUIRY STATUS", 3);
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
