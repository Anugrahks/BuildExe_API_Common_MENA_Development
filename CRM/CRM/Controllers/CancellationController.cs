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
    public class CancellationController : ControllerBase
    {
        private readonly ICancellationRepository _cancellationRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public CancellationController(ICancellationRepository cancellationRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _cancellationRepository = cancellationRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;

        }

        [HttpGet("Indent/{ProjectId}/{UnitId}/{BlockId}/{DivisionId}/{FloorId}/{Type}")]
        [Authorize]
        public async Task<IActionResult> Indent(int ProjectId, int UnitId, int BlockId, int DivisionId, int FloorId, int Type, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product = await _cancellationRepository.Indent(ProjectId, UnitId, BlockId, DivisionId, FloorId, Type);
                return new OkObjectResult(product);
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

        [HttpGet("PurchaseOrder/{ProjectId}/{UnitId}/{BlockId}/{DivisionId}/{FloorId}/{Type}")]
        [Authorize]
        public async Task<IActionResult> PurchaseOrder(int ProjectId, int UnitId, int BlockId, int DivisionId, int FloorId, int Type, [FromHeader] string mdhash, [FromHeader] int User)
            {
                if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                {
                    try
            {
                var product = await _cancellationRepository.PurchaseOrder(ProjectId, UnitId, BlockId, DivisionId, FloorId, Type);
                return new OkObjectResult(product);
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


        [HttpGet("Project/{CompanyId}/{BranchId}/{UserId}/{SiteUser}/{TypeId}")]
        [Authorize]
        public async Task<IActionResult> Project(int CompanyId, int BranchId, int UserId, int SiteUser, int TypeId, [FromHeader] string mdhash, [FromHeader] int User)
                {
                    if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                    {
                        try
            {
                var product = await _cancellationRepository.Project(CompanyId, BranchId, UserId, SiteUser, TypeId);
                return new OkObjectResult(product);
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


        [HttpGet("ApprovalView/{BranchId}/{FinancialYearId}/{MenuId}")]
        [Authorize]
        public async Task<IActionResult> ApprovalView(int BranchId, int FinancialYearId, int MenuId, [FromHeader] string mdhash, [FromHeader] int User)
                    {
                        if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                        {
                            try
            {
                var product = await _cancellationRepository.ApprovalView(BranchId, FinancialYearId, MenuId);
                return new OkObjectResult(product);
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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] IEnumerable<Cancellation> cancellation, [FromHeader] string mdhash, [FromHeader] int User)
                        {
                            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                            {
                                try
            {

                    var res = await _cancellationRepository.Insert(cancellation);
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
