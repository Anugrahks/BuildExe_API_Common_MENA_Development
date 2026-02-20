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
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientAdvanceController : ControllerBase
    {
        private readonly IClientAdvanceRepository _clientAdvanceRepository   ;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public ClientAdvanceController(IClientAdvanceRepository clientAdvanceRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _clientAdvanceRepository = clientAdvanceRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        // GET: api/<TeamController>
        [HttpGet("{Companyid}/{Branchid}")]
        [Authorize]
        public async Task<IActionResult> Get(int Companyid, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase =  await _clientAdvanceRepository.Get(Companyid, BranchId);
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
                var purchase = await _clientAdvanceRepository.GetByID(id);

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
        public async Task<IActionResult> Post([FromBody] ClientAdvance  clientAdvance, [FromHeader] string mdhash, [FromHeader] int User)
                {
                    if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                    {
                        try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                    var val = await _clientAdvanceRepository.Insert(clientAdvance);

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
        public async Task<IActionResult> Put([FromBody] ClientAdvance clientAdvance, [FromHeader] string mdhash, [FromHeader] int User)
                    {
                        if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                        {
                            try
            {
                if (clientAdvance != null)
            {
                using (var scope = new TransactionScope( TransactionScopeAsyncFlowOption.Enabled ))
                {
                        var val = await _clientAdvanceRepository.Update(clientAdvance);

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
                var val = await _clientAdvanceRepository.Delete(id, UserId);

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
