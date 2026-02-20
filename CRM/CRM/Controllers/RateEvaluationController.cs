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
    public class RateEvaluationController : ControllerBase
    {
        private readonly IRateEvaluationRepository _rateEvaluationRepository  ;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public RateEvaluationController(IRateEvaluationRepository rateEvaluationRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _rateEvaluationRepository = rateEvaluationRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;

        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] IEnumerable<ProjectSpecificationMaster > rateEvaluations, [FromHeader] string mdhash, [FromHeader] int User)
        {
            try
            {
                if (rateEvaluations != null)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {

                        var res = await _rateEvaluationRepository.Update(rateEvaluations);

                        scope.Complete();
                        return new OkObjectResult(res);
                }
            }
            return new NoContentResult();
            }
            catch (Exception)
            { throw; }
        }

        [HttpPost("Getbyproject")]
        [Authorize]
        public async Task<IActionResult> Get([FromBody] SpecificationFilters specificationFilters, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product = await _rateEvaluationRepository.Getbyproject(specificationFilters);
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

        [HttpGet("Validation/{Projectid}/{UnitId}/{BlockId}/{FloorId}/{DivisionId}")]
        [Authorize]
        public async Task<IActionResult> GetVal(int Projectid, int UnitId, int BlockId, int FloorId,int DivisionId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product = await _rateEvaluationRepository.GetVal(Projectid, UnitId, BlockId, FloorId, DivisionId,0);
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

        [HttpGet("Validation/{Projectid}/{UnitId}/{BlockId}/{FloorId}/{DivisionId}/{EnquiryId}")]
        [Authorize]
        public async Task<IActionResult> GetVal(int Projectid, int UnitId, int BlockId, int FloorId, int DivisionId, int EnquiryId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var product = await _rateEvaluationRepository.GetVal(Projectid, UnitId, BlockId, FloorId, DivisionId, EnquiryId);
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
    }
}

