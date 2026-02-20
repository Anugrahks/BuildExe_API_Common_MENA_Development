using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
using BuildExeHR.Repository;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using BuildExeHR.Library;

namespace BuildExeHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TdsBalanceController : ControllerBase
    {
        private readonly IAdvanceBalanceRepository _advanceBalanceRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public TdsBalanceController(IAdvanceBalanceRepository advanceBalanceRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _advanceBalanceRepository = advanceBalanceRepository;
           _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpGet("{Employeeid}/{ProjectId}/{Unitid}/{BlockID}/{Floorid}")]
        [Authorize]
        public async Task<IActionResult> Get(int Employeeid,int ProjectId, int Unitid, int BlockID, int Floorid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var brand = await _advanceBalanceRepository.tdsBalance(Employeeid, ProjectId, Unitid, BlockID, Floorid);
                    return new OkObjectResult(brand);
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
