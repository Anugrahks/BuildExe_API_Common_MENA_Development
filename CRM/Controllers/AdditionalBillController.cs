using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
using BuildExeServices.Repository;

using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using BuildExeServices.Library;
using System.Security.Policy;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdditionalBillController : ControllerBase
    {
        
        private readonly IAdditionalBillRepository _additionalBillRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public AdditionalBillController(IAdditionalBillRepository additionalBillRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _additionalBillRepository = additionalBillRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;

        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            
                try
                {
                    var products = await _additionalBillRepository.Get();
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
        [HttpGet("{companyid}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> Get(int companyid, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product = await _additionalBillRepository.Get(companyid, BranchId);
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
                var product = await _additionalBillRepository.GetbyID(id);
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
        public async Task<IActionResult> Post([FromBody] IEnumerable<AdditionalBill> additionalBills, [FromHeader] string mdhash, [FromHeader] int User)
        {
                    if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                    {
                        try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                  var res=await  _additionalBillRepository.Insert(additionalBills);

                    scope.Complete();
                    return new OkObjectResult(res);
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


        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] IEnumerable<AdditionalBill> additionalBills, [FromHeader] string mdhash, [FromHeader] int User)
        {
                        if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                        {
                            try
            {
                if (additionalBills != null)
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled ))
                    {
                        var res = await  _additionalBillRepository.Update(additionalBills);

                        scope.Complete();
                        return new OkObjectResult(res);
                    }
                }
                return new NoContentResult();
            }
            catch(Exception ex)
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
        public async Task  <IActionResult> Delete(int id, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
                            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                            {
                                try
            {
                var product = await _additionalBillRepository.Delete(id, UserId);

                return new OkObjectResult (product);
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

        [HttpGet("GetRetention/{id}/{typeId}")]
        [Authorize]
        public async Task<IActionResult> GetRetention(int id, int typeId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _additionalBillRepository.GetRetention(id, typeId);
                    return new OkObjectResult(purchase);
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

        [HttpGet("Getlabel/{Branchid}")]
        [Authorize]
        public async Task<IActionResult> GetLabel(int BranchId , [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _additionalBillRepository.GetLabel(BranchId);
                    return new OkObjectResult(purchase);
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

        [HttpGet("GetRetPer/{Branchid}")]
        [Authorize]
        public async Task<IActionResult> GetRetPer(int BranchId , [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _additionalBillRepository.GetRetPer(BranchId);
                    return new OkObjectResult(purchase);
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
