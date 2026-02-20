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
    public class PendingClientBillsController : ControllerBase
    {
        private readonly IPendingClientBillsRepository _pendingClientBillsRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public PendingClientBillsController(IPendingClientBillsRepository pendingClientBillsRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _pendingClientBillsRepository = pendingClientBillsRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;

        }
        [HttpGet("{TypeId}/{ProjectId}/{UnitId}/{blockid}/{Floorid}/{DivisionId}")]
        [Authorize]
        public async Task<IActionResult> Get(int TypeId,int ProjectId,int UnitId,int blockid,int Floorid, int DivisionId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var products = await _pendingClientBillsRepository.GetPendingClientBills(TypeId, ProjectId, UnitId, blockid, Floorid, DivisionId);
            return new OkObjectResult(products);
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

        [HttpGet("{TypeId}/{ProjectId}/{UnitId}/{blockid}/{Floorid}/{DivisionId}/{Id}")]
        [Authorize]
        public async Task<IActionResult> Get(int TypeId, int ProjectId, int UnitId, int blockid, int Floorid, int DivisionId,int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var products = await _pendingClientBillsRepository.GetPendingClientBillsEdit(TypeId, ProjectId, UnitId, blockid, Floorid, DivisionId, Id);
                return new OkObjectResult(products);
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
    