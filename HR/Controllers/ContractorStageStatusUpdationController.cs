using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
using System.Transactions;
using BuildExeHR.Repository;
using Microsoft.AspNetCore.Authorization;
using BuildExeHR.Library;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractorStageStatusUpdationController : ControllerBase
    {
        private readonly IContractorStageStatusUpdationRepository _ContractorStageStatusUpdationRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public ContractorStageStatusUpdationController(IContractorStageStatusUpdationRepository ContractorStageStatusUpdationRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _ContractorStageStatusUpdationRepository = ContractorStageStatusUpdationRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] IEnumerable<ContractorStageSetting> ContractorStage, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (ContractorStage != null)
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var department = await _ContractorStageStatusUpdationRepository.Update(ContractorStage);
                        scope.Complete();
                        return new OkObjectResult(department);
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



        [HttpPut("ContractorAdditionalBill")]
        [Authorize]
        public async Task<IActionResult> ContractorAdditionalBill([FromBody] IEnumerable<ContractorStageSetting> ContractorStage, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    if (ContractorStage != null)
                    {
                        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                        {
                            var department = await _ContractorStageStatusUpdationRepository.ContractorAdditionalBill(ContractorStage);
                            scope.Complete();
                            return new OkObjectResult(department);
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

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _ContractorStageStatusUpdationRepository.Getbyid(id);
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


        [HttpGet("AdditionalBill/{id}")]
        [Authorize]
        public async Task<IActionResult> GetbyAdditionalBill(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _ContractorStageStatusUpdationRepository.GetbyAdditionalBill(id);
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

        [HttpPost("Additionalbill")]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] IEnumerable<ContractorAdditionalbill> contractorAdditionalbill, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var val = await _ContractorStageStatusUpdationRepository.Insert(contractorAdditionalbill);
                        scope.Complete();

                        return new OkObjectResult(val);
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

        [HttpPut("Additionalbill")]
        [Authorize]
        public async Task<IActionResult> Additionalbill([FromBody] IEnumerable<ContractorAdditionalbill> contractorAdditionalbill, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    if (contractorAdditionalbill != null)
                    {
                        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                        {
                            var val = await _ContractorStageStatusUpdationRepository.Additionalbill(contractorAdditionalbill);
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

        [HttpGet("AdditionalBillGet/{CompanyId}/{Branchid}/{FinacialYearId}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> GetAdditionalBill(int CompanyId, int Branchid, int FinacialYearId, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _ContractorStageStatusUpdationRepository.GetAdditionalBill(CompanyId, Branchid, FinacialYearId, UserId);
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

        [HttpGet("AdditionalBillGetById/{Id}")]
        [Authorize]
        public async Task<IActionResult> GetByIdAdditionalBill(int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _ContractorStageStatusUpdationRepository.GetByIdAdditionalBill(Id);
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

        [HttpGet("AdditionalBillGetApproval/{CompanyId}/{Branchid}/{FinacialYearId}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> GetApprovalAdditionalBill(int CompanyId, int Branchid, int FinacialYearId, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _ContractorStageStatusUpdationRepository.GetApprovalAdditionalBill(CompanyId, Branchid, FinacialYearId, UserId);
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

        [HttpDelete("AdditionalBillDelete/{Id}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> GetDeleteAdditionalBill(int Id, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var bb = await _ContractorStageStatusUpdationRepository.GetDeleteAdditionalBill(Id, UserId);
                    return new OkObjectResult(bb);
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
