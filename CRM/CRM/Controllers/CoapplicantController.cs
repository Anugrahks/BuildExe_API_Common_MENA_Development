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
    public class CoapplicantController : ControllerBase
    {
        private readonly ICoApplicantRepository _coApplicantRepository  ;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public CoapplicantController(ICoApplicantRepository coApplicantRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _coApplicantRepository = coApplicantRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var branch =await _coApplicantRepository.Get();
            return new OkObjectResult(branch);
            }
            catch (Exception)
            { throw; }
        }


        [HttpGet("{unitid}")]
        [Authorize]
        public async Task<IActionResult>  Get(int unitid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var branch =await _coApplicantRepository.GetByID(unitid);

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


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] IEnumerable<CoApplicant> coApplicant, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
               await _coApplicantRepository.Insert(coApplicant);
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


        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] IEnumerable<CoApplicant> coApplicant, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (coApplicant != null)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                   await _coApplicantRepository.Insert(coApplicant);
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
    }
}
