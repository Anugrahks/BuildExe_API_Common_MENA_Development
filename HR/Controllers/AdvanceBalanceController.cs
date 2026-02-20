using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
using BuildExeHR.Repository;
using System.Transactions;
using Microsoft.Extensions.Logging;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using BuildExeHR.Library;
using System.Security.Policy;

namespace BuildExeHR.Controllers
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
        [HttpGet("{ProjectId}/{UnitId}/{BlockId}/{FloorId}/{CompanyId}/{Branchid}/{EmployeeId}")]
        [Authorize]
        public async Task<IActionResult> Get(int ProjectId, int UnitId, int BlockId, int FloorId,int CompanyId, int Branchid, int EmployeeId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var brand = await _advanceBalanceRepository.Get(ProjectId, UnitId, BlockId, FloorId, CompanyId, Branchid, EmployeeId);
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
        

        [HttpGet("Balance/{ProjectId}/{UnitId}/{BlockId}/{FloorId}/{CompanyId}/{Branchid}/{EmployeeId}")]
        [Authorize]
        public async Task<IActionResult> GetBalance(int ProjectId, int UnitId, int BlockId, int FloorId,  int CompanyId,int Branchid, int EmployeeId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var brand = await _advanceBalanceRepository.GetBalance(ProjectId, UnitId, BlockId, FloorId, CompanyId, Branchid, EmployeeId);
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

        [HttpGet("{CompanyId}/{Branchid}/{EmployeeId}")]
        [Authorize]
        public async Task<IActionResult> Get(int CompanyId, int Branchid, int EmployeeId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                int ProjectId = 0, UnitId = 0, BlockId = 0, FloorId = 0;
            var brand = await _advanceBalanceRepository.Get(ProjectId, UnitId, BlockId, FloorId, CompanyId, Branchid, EmployeeId);
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
        [HttpGet("Balance/{CompanyId}/{Branchid}/{EmployeeId}")]
        [Authorize]
        public async Task<IActionResult> GetBalance(int CompanyId, int Branchid, int EmployeeId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                int ProjectId = 0, UnitId = 0, BlockId = 0, FloorId = 0;
            var brand = await _advanceBalanceRepository.GetBalance(ProjectId, UnitId, BlockId, FloorId, CompanyId, Branchid, EmployeeId);
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
