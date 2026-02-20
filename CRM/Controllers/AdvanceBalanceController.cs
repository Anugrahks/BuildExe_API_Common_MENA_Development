using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
using BuildExeServices.Repository;
using Microsoft.AspNetCore.Authorization;
using BuildExeServices.Library;

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvanceBalanceController : ControllerBase
    {
        private readonly IAdvanceBalanceRepository _advanceBalanceRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public AdvanceBalanceController(IAdvanceBalanceRepository advanceBalanceRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _advanceBalanceRepository = advanceBalanceRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }
        [HttpGet("Detail/{ProjectId}/{Unitid}/{BlockID}/{Floorid}")]
        [Authorize]
        public async Task<IActionResult> GetDetail(int ProjectId, int Unitid, int BlockID, int Floorid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var brand = await _advanceBalanceRepository.GetDetail(ProjectId, Unitid, BlockID, Floorid);
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
        [HttpGet("{ProjectId}/{Unitid}/{BlockID}/{Floorid}")]
        [Authorize]
        public async Task<IActionResult> Get(int ProjectId, int Unitid, int BlockID, int Floorid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var brand = await _advanceBalanceRepository.Get(ProjectId, Unitid, BlockID, Floorid);
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
