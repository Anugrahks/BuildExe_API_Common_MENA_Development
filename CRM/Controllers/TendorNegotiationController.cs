using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Repository;
using BuildExeServices.Models;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using BuildExeServices.Library;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TendorNegotiationController : ControllerBase
    {
        private readonly ITendorNegotiationRepository _tendorNegotiationRepository  ;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        //private readonly IUserLogRepository _userLogRepository;
        public TendorNegotiationController(ITendorNegotiationRepository tendorNegotiationRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _tendorNegotiationRepository = tendorNegotiationRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
            //_userLogRepository = userLogRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var floor = await _tendorNegotiationRepository.Get();
            return new OkObjectResult(floor);
            }
            catch (Exception)
            { throw; }
        }


        [HttpGet("{projectid}")]
        [Authorize]
        public async Task<IActionResult> Get(int projectid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var floor = await _tendorNegotiationRepository.GetByID(projectid);

            return new OkObjectResult(floor);
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
        public async Task<IActionResult> Post([FromBody] TendorNegotiation tendorNegotiation, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _tendorNegotiationRepository.Insert(tendorNegotiation);

                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = tendorNegotiation.Id }, tendorNegotiation);
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
        public async Task<IActionResult> Put([FromBody] TendorNegotiation tendorNegotiation, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (tendorNegotiation != null)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                   await _tendorNegotiationRepository.Update(tendorNegotiation);

                    scope.Complete();
                    return new OkResult();
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


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
               await _tendorNegotiationRepository.Delete(id);

                return new OkResult();
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
