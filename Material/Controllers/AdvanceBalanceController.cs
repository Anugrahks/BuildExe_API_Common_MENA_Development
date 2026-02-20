using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BuildExeMaterialServices.Models;
using BuildExeMaterialServices.Repository;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using BuildExeMaterialServices.Library;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeMaterialServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvanceBalanceController : ControllerBase
    {
        private readonly IAdvanceBalanceRepository _advanceBalanceRepository ;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public AdvanceBalanceController(IAdvanceBalanceRepository advanceBalanceRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _advanceBalanceRepository = advanceBalanceRepository;
            _userLogRepository = userLogRepository;
                _mdHashValidator = mdHashValidator;
        }
        [HttpGet("Detail/{CompanyId}/{Branchid}/{SupplierId}/{ProjectId}")]
        [Authorize]
        public async Task<IActionResult> GetDetail(int CompanyId, int Branchid,int SupplierId, int ProjectId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var brand =await _advanceBalanceRepository.GetDetail(CompanyId, Branchid, SupplierId, ProjectId);
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
        [HttpGet("WithProject/{CompanyId}/{Branchid}/{SupplierId}/{ProjectId}")]
        [Authorize]
        public async Task<IActionResult> WithProject(int CompanyId, int Branchid, int SupplierId, int ProjectId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var brand = await _advanceBalanceRepository.WithProject(CompanyId, Branchid, SupplierId, ProjectId);
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

        [HttpGet("{CompanyId}/{Branchid}/{SupplierId}/{ProjectId}")]
        [Authorize]
        public async Task<IActionResult> Get(int CompanyId, int Branchid, int SupplierId, int ProjectId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var brand = await _advanceBalanceRepository.Get(CompanyId, Branchid, SupplierId, ProjectId);
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
