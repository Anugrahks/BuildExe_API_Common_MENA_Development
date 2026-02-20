using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
using System.Transactions;
using BuildExeBasic.Repository;
using Microsoft.AspNetCore.Authorization;
using BuildExeBasic.Library;
using System.Security.Policy;

namespace BuildExeBasic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralLedgerController : ControllerBase
    {
        private readonly IGeneralLedgerRepository _generalLedgerRepository  ;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public GeneralLedgerController(IGeneralLedgerRepository generalLedgerRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _generalLedgerRepository = generalLedgerRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;

        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] BasicSearch basicSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (basicSearch != null)
                {
                    var product = await _generalLedgerRepository.GeneralLedger(basicSearch);
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
