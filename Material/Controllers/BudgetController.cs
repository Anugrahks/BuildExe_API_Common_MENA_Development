using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
using BuildExeMaterialServices.Repository;

using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using BuildExeMaterialServices.Library;
using System.Security.Cryptography;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly IAssetDetailRepository _projectSpecificationRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public BudgetController(IAssetDetailRepository projectSpecificationRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _projectSpecificationRepository = projectSpecificationRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;

        }


        [HttpPost("Material")]
        [Authorize]
        public async Task<IActionResult> Material([FromBody] MaterialSearch materialSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var val = await _projectSpecificationRepository.Material(materialSearch);

                        scope.Complete();
                        return new OkObjectResult(val);
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


        [HttpPost("Report")]
        [Authorize]
        public async Task<IActionResult> Report([FromBody] MaterialSearch materialSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var val = await _projectSpecificationRepository.Report(materialSearch);

                        scope.Complete();
                        return new OkObjectResult(val);
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

    }
}
