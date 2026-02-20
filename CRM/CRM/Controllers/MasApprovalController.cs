using Microsoft.AspNetCore.Http;
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
namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasApprovalController : ControllerBase
    {
        private readonly IBoqMasterRepository _boqMasterRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public MasApprovalController(IBoqMasterRepository boqMasterRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _boqMasterRepository = boqMasterRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }



        [HttpGet("{Porjectid}")]
        [Authorize]
        public async Task<IActionResult> Get(int Porjectid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            try
            {
                var product = await _boqMasterRepository.GetFor_MasApproval(Porjectid);
                return new OkObjectResult(product);
            }
            catch (Exception)
            { throw; }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] BillSearch billSearches, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (billSearches != null)
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        await _boqMasterRepository.Update_MAsStatus(billSearches);
                        scope.Complete();
                        return new OkResult();
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
    }
}
