using BuildExeBasic.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
using System.Transactions;
using BuildExeBasic.Library;
using System.Security.Policy;
namespace BuildExeBasic.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralAlertController : ControllerBase
    {
        private readonly IGeneralAlertRepository _generalAlertRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public GeneralAlertController(IGeneralAlertRepository generalAlertRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {

            _generalAlertRepository = generalAlertRepository;
            _userLogRepository = userLogRepository;
                _mdHashValidator = mdHashValidator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] IEnumerable<GeneralAlert> generalAlert, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var item = await _generalAlertRepository.Insert(generalAlert);
                    scope.Complete();

                    return new OkObjectResult(item);
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


        [HttpGet("{CompanyId}/{Branchid}/{UserId}")]
        [Authorize]

        public async Task<IActionResult> Get(int CompanyId, int BranchId, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
            {
                if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                {
                    try
            {
                var val = await _generalAlertRepository.Get(CompanyId, BranchId, UserId);
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

        [HttpGet("{Id}")]
        [Authorize]

        public async Task<IActionResult> Get(int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
            try
            {
                var val = await _generalAlertRepository.GetById(Id);
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


        [HttpGet("Alertdoc/{Id}")]
        [Authorize]

        public async Task<IActionResult> GetDoc(int Id, [FromHeader] string mdhash, [FromHeader] int User)
                {
                    if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                    {
                        try
            {
                var val = await _generalAlertRepository.Getdoc(Id);
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


        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] IEnumerable<GeneralAlert> generalAlert, [FromHeader] string mdhash, [FromHeader] int User)
                    {
                        if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                        {
                            try
            {
                if (generalAlert != null)
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var item = await _generalAlertRepository.Update(generalAlert);
                        scope.Complete();
                        return new OkObjectResult(item);
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


        [HttpDelete("{Id}/{CompanyId}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int Id, int CompanyId, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
                        {
                            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                            {
                                try
            {
                var val = await _generalAlertRepository.Delete(Id, CompanyId, BranchId);
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

