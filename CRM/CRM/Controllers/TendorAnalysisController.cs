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
    public class TendorAnalysisController : ControllerBase
    {
        private readonly ITendorAnalysisRepository _tendorAnalysisRepository  ;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        //private readonly IUserLogRepository _userLogRepository;
        public TendorAnalysisController(ITendorAnalysisRepository tendorAnalysisRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _tendorAnalysisRepository = tendorAnalysisRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
            //_userLogRepository = userLogRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task <IActionResult> Get()
        {
            try
            {
                var floor = await _tendorAnalysisRepository.Get();
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
                var floor = await _tendorAnalysisRepository.GetByID(projectid);

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
        public async Task<IActionResult> Post([FromBody] IEnumerable<TendorAnalysis>  tendorAnalysis, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var val = await _tendorAnalysisRepository.Insert(tendorAnalysis);

                    scope.Complete();
                    return new OkObjectResult(val);
                    //return CreatedAtAction(nameof(Get), new { id = tendorAnalysis.Id }, tendorAnalysis);
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
        public async Task<IActionResult> Put([FromBody] IEnumerable<TendorAnalysis> tendorAnalysis, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var val=   await _tendorAnalysisRepository.Update(tendorAnalysis);

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


        [HttpDelete("{id}/{userid}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id,int userid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                await _tendorAnalysisRepository.Delete(id, userid);

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

        [HttpPost("Report")]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] BillSearch billSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (billSearch != null)
                {
                    var product = await _tendorAnalysisRepository.GetReport(billSearch);

                    return new OkObjectResult(product);
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
        [HttpPost("GovReport")]
        [Authorize]
        public async Task<IActionResult> PostGetGovReport([FromBody] BillSearch billSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (billSearch != null)
                {
                    var product = await _tendorAnalysisRepository.GetGovReport(billSearch);

                    return new OkObjectResult(product);
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

        [HttpPost("EmdReport")]
        [Authorize]
        public async Task<IActionResult> PostEmdReport([FromBody] BillSearch billSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (billSearch != null)
                {
                    var product = await _tendorAnalysisRepository.GetEmdReport(billSearch);

                    return new OkObjectResult(product);
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

