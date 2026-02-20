
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
using BuildExeMaterialServices.Repository;

using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using BuildExeMaterialServices.Library;
using System.Security.Cryptography;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeMaterialServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetAppreciationController : ControllerBase
    {
        private readonly IAssetAppreciationRepository _projectSpecificationRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public AssetAppreciationController(IAssetAppreciationRepository projectSpecificationRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _projectSpecificationRepository = projectSpecificationRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;

        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] IEnumerable<AssetAppreciation> timeSchedulerMaster, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var val = await _projectSpecificationRepository.Insert(timeSchedulerMaster);

                        scope.Complete();
                        return new OkObjectResult(val);
                        // return CreatedAtAction(nameof(Get), new { id = specificationMasters. }, specificationMasters);
                    }
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



        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] IEnumerable<AssetAppreciation> timeSchedulerMaster, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    if (timeSchedulerMaster != null)
                    {
                        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                        {
                            var val = await _projectSpecificationRepository.Update(timeSchedulerMaster);

                            scope.Complete();
                            return new OkObjectResult(val);
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





        [HttpDelete("{id}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var val = await _projectSpecificationRepository.Delete(id, UserId);

                    return new OkObjectResult(val);
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


        [HttpGet("GetGrid/{BranchId}/{FinancialYearId}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> GetGrid(int BranchId, int FinancialYearId, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var val = await _projectSpecificationRepository.GetGrid(BranchId, FinancialYearId, UserId);

                    return new OkObjectResult(val);
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


        [HttpGet("GetApproval/{BranchId}/{FinancialYearId}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> GetApproval(int BranchId, int FinancialYearId, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var val = await _projectSpecificationRepository.GetApproval(BranchId, FinancialYearId, UserId);

                    return new OkObjectResult(val);
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


        [HttpGet("GetApprovedData/{BranchId}/{FinancialYearId}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> GetApprovedData(int BranchId, int FinancialYearId, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var val = await _projectSpecificationRepository.GetApprovedData(BranchId, FinancialYearId, UserId);

                    return new OkObjectResult(val);
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

        [HttpGet("GetById/{Id}/{CompanyId}/{BranchId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> GetById(int Id,int CompanyId, int BranchId, int FinancialYearId,  [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var val = await _projectSpecificationRepository.GetById(Id, CompanyId, BranchId, FinancialYearId);

                    return new OkObjectResult(val);
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

