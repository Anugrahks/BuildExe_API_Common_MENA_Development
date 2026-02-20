using Microsoft.AspNetCore.Http;
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

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectNegotiationController : ControllerBase
    {
        private readonly ITendorNegotiationRepository _tendorNegotiationRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public ProjectNegotiationController(ITendorNegotiationRepository tendorNegotiationRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _tendorNegotiationRepository = tendorNegotiationRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;

        }

        [HttpGet("{projectid}")]
        [Authorize]
        public async Task<IActionResult> Get(int projectid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var floor = await _tendorNegotiationRepository.GetprojectSpec_Negotiated (projectid);

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
        public async Task<IActionResult> Post([FromBody] IEnumerable<SpecificationNegotiation>  specificationNegotiation, [FromHeader] string mdhash, [FromHeader] int User)
            {
                if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                {
                    try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await _tendorNegotiationRepository.Insert(specificationNegotiation);

                    scope.Complete();
                    return new OkResult();
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
    }
}
