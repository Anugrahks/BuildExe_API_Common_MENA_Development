using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Repository;
using BuildExeHR.Models;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using BuildExeHR.Library;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RetensionPaymentController : ControllerBase

    {
        private readonly IRetensionPaymentRepository _retensionPaymentRepository ;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public RetensionPaymentController(IRetensionPaymentRepository retensionPaymentRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _retensionPaymentRepository = retensionPaymentRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpGet("{ProjectId}/{UnitId}/{BlockId}/{FloorId}/{SubcontractorId}")]
        [Authorize]
        public async Task<IActionResult> Get(int ProjectId, int UnitId, int BlockId, int FloorId,int SubcontractorId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase =await _retensionPaymentRepository.Getbyproject(ProjectId, UnitId, BlockId, FloorId, SubcontractorId);
            return new OkObjectResult(purchase);
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

        [HttpGet("{Companyid}/{Branchid}/{UserId}/{MenuID}")]
        [Authorize]
        public async Task<IActionResult> Get(int Companyid, int BranchId, int UserId, int MenuID, [FromHeader] string mdhash, [FromHeader] int User)
            {
                if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                {
                    try
            {
                var purchase = await _retensionPaymentRepository.GetAmounts(Companyid, BranchId, UserId, MenuID);
                return new OkObjectResult(purchase);
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
