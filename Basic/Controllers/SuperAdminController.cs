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

namespace BuildExeBasic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperAdminController : ControllerBase
    {
        private readonly ISuperAdminRepository _superadminrepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public SuperAdminController(ISuperAdminRepository superAdminRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _superadminrepository = superAdminRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;

        }

        [HttpGet("get/{MenuId}/{BranchId}/{FinancialYearId}/{FromDate}/{ToDate}")]
        [Authorize]
        public async Task<IActionResult> Get(int MenuId, int BranchId, int FinancialYearId, DateTime FromDate, DateTime ToDate, int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var res = await _superadminrepository.Insert(MenuId, BranchId, FinancialYearId, FromDate,ToDate);
                return new OkObjectResult(res);
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

        [HttpDelete("{MenuId}/{Id}")]
        [Authorize]
        public async Task<IActionResult> deleted(int MenuId, int Id, int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var res = await _superadminrepository.Delete(MenuId, Id);
                return new OkObjectResult(res);
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

        [HttpGet("check/{MenuId}/{Id}")]
        [Authorize]
        public async Task<IActionResult> Get(int MenuId, int Id, int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var res = await _superadminrepository.Check(MenuId, Id);
                return new OkObjectResult(res);
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

        [HttpGet("approvechange/{MenuId}/{Id}")]
        [Authorize]
        public async Task<IActionResult> approvechanges(int MenuId, int Id, int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var res = await _superadminrepository.approvechange(MenuId, Id);
                return new OkObjectResult(res);
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
