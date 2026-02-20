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
    public class RefundBalanceController : ControllerBase
    {
        private readonly IRefundBalanceRepository _refundBalanceRepository ;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public RefundBalanceController(IRefundBalanceRepository refundBalanceRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _refundBalanceRepository = refundBalanceRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }
        [HttpGet("Detail/{Type}/{ProjectId}/{Unitid}/{BlockID}/{Floorid}")]
        [Authorize]
        public async Task<IActionResult> GetDetail(int Type,int ProjectId, int Unitid, int BlockID, int Floorid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var brand = await _refundBalanceRepository.GetDetail(Type,ProjectId, Unitid, BlockID, Floorid);
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
        [HttpGet("{Type}/{ProjectId}/{Unitid}/{BlockID}/{Floorid}")]
        [Authorize]
        public async Task<IActionResult> Get(int Type,int ProjectId, int Unitid, int BlockID, int Floorid, [FromHeader] string mdhash, [FromHeader] int User)
            {
                if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                {
                    try
                    {
                        var brand = await _refundBalanceRepository.Get(Type, ProjectId, Unitid, BlockID, Floorid);
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
