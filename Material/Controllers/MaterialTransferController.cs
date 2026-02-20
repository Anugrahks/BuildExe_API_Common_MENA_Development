using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Repository;
using BuildExeMaterialServices.Models;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using BuildExeMaterialServices.Library;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeMaterialServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialTransferController : ControllerBase
    {
        private readonly IMaterialTransferRepository _materialTransferRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public MaterialTransferController(IMaterialTransferRepository materialTransferRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _materialTransferRepository = materialTransferRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        // GET: api/<TeamController>
        [HttpGet("{CompanyId}/{Branchid}")]
        [Authorize]
        public async Task<IActionResult> Get(int CompanyId, int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase =await  _materialTransferRepository.Get(CompanyId, Branchid);
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
        [HttpGet("{CompanyId}/{Branchid}/{ProjectId}/{ReceiveDate}")]
        [Authorize]
        public async Task<IActionResult> Get(int CompanyId, int Branchid,int ProjectId, DateTime ReceiveDate, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _materialTransferRepository.GetforRecieve(CompanyId, Branchid,ProjectId, ReceiveDate);
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

        // GET api/<TeamController>/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _materialTransferRepository.GetbyID(id);

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

        // POST api/<TeamController>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] IEnumerable<MaterialTransfer> materialTransfer, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var val = await _materialTransferRepository.Insert(materialTransfer);
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
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] IEnumerable<MaterialTransfer > materialTransfer, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (materialTransfer != null)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var val = await _materialTransferRepository.Update(materialTransfer);
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
        public async Task<IActionResult> Delete(int id, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
             var li=  await _materialTransferRepository.Delete(id, UserId);
                return new OkObjectResult(li);
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
