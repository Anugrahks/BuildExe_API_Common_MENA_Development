using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Repository;
using BuildExeMaterialServices.Models;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using BuildExeMaterialServices.Library;

namespace BuildExeMaterialServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueReturnController : ControllerBase
    {
        private readonly IIssueReturnRepository _issueReturnRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public IssueReturnController(IIssueReturnRepository issueReturnRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _issueReturnRepository = issueReturnRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpPost("Issue")]
        [Authorize]
        public async Task<IActionResult> PostIssue([FromBody] IEnumerable<MaterialIssue> issue, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var val = await _issueReturnRepository.PostIssue(issue);
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

        [HttpPut("Issue")]
        [Authorize]
        public async Task<IActionResult> PutIssue([FromBody] IEnumerable<MaterialIssue> issue, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var val = await _issueReturnRepository.PutIssue(issue);
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

        [HttpGet("IssueGet/{CompanyId}/{Branchid}/{FinacialYearId}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> GetIssue(int CompanyId, int Branchid, int FinacialYearId, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _issueReturnRepository.GetIssue(CompanyId, Branchid, FinacialYearId, UserId);
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

        [HttpGet("IssueGetType/{CompanyId}/{Branchid}/{TypeId}/{ProjectId}/{DivisionId}/{MaterialTypeId}/{Id}/{ReturnDate}")]
        [Authorize]
        public async Task<IActionResult> GetIssueType(int CompanyId, int Branchid, int TypeId, int ProjectId, int DivisionId , int MaterialTypeId, int Id,DateTime ReturnDate, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _issueReturnRepository.GetIssueType(CompanyId, Branchid, TypeId, ProjectId, DivisionId, MaterialTypeId, Id, ReturnDate);
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

        [HttpGet("IssueGetType/{CompanyId}/{Branchid}/{TypeId}/{ProjectId}/{DivisionId}/{MaterialTypeId}/{Id}")]
        [Authorize]
        public async Task<IActionResult> GetIssueType(int CompanyId, int Branchid, int TypeId, int ProjectId, int DivisionId, int MaterialTypeId, int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    DateTime dateWorked = new DateTime(2028, 1, 1);
                    var purchase = await _issueReturnRepository.GetIssueType(CompanyId, Branchid, TypeId, ProjectId, DivisionId, MaterialTypeId, Id, dateWorked);
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

        [HttpGet("IssueGetApproval/{CompanyId}/{Branchid}/{FinacialYearId}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> GetApprovalIssue(int CompanyId, int Branchid, int FinacialYearId, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _issueReturnRepository.GetApprovalIssue(CompanyId, Branchid, FinacialYearId, UserId);
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

        [HttpGet("IssueGetById/{Id}")]
        [Authorize]
        public async Task<IActionResult> GetByIdIssue(int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _issueReturnRepository.GetByIdIssue(Id);
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

        [HttpDelete("Issue/{Id}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> GetDeleteIssue(int Id, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var bb = await _issueReturnRepository.GetDeleteIssue(Id, UserId);
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

        [HttpPost("Return")]
        [Authorize]
        public async Task<IActionResult> PostReturn([FromBody] IEnumerable<MaterialReturn> issue, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var val = await _issueReturnRepository.PostReturn(issue);
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

        [HttpPut("Return")]
        [Authorize]
        public async Task<IActionResult> PutReturn([FromBody] IEnumerable<MaterialReturn> issue, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var val = await _issueReturnRepository.PutReturn(issue);
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

        [HttpGet("ReturnGet/{CompanyId}/{Branchid}/{FinacialYearId}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> GetReturn(int CompanyId, int Branchid, int FinacialYearId, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _issueReturnRepository.GetReturn(CompanyId, Branchid, FinacialYearId, UserId);
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

        [HttpGet("ReturnApproval/{CompanyId}/{Branchid}/{FinacialYearId}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> GetApprovalReturn(int CompanyId, int Branchid, int FinacialYearId, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _issueReturnRepository.GetApprovalReturn(CompanyId, Branchid, FinacialYearId, UserId);
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

        [HttpDelete("Return/{Id}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> GetDeleteReturn(int Id, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try 
                {
                    var bb = await _issueReturnRepository.GetDeleteReturn(Id, UserId);
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

        [HttpGet("getorderno/{CompanyId}/{BranchId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> GetOrderNo(int CompanyId, int BranchId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var material = await _issueReturnRepository.Getorderno(CompanyId, BranchId, FinancialYearId);
                return new OkObjectResult(material);
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

        [HttpGet("getordernoReturn/{CompanyId}/{BranchId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> GetOrderNoReturn(int CompanyId, int BranchId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var material = await _issueReturnRepository.GetordernoReturn(CompanyId, BranchId, FinancialYearId);
                return new OkObjectResult(material);
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

        [HttpGet("StockProjectDash/{ProjectId}/{CompanyId}/{Branchid}/{DivisionId}/{FinancialYearId}/{UnitId}")]
        [Authorize]
        public async Task<IActionResult> GetStockProjectDash(int ProjectId, int CompanyId, int Branchid, int DivisionId,int FinancialYearId, int UnitId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _issueReturnRepository.GetStockProjectDash(ProjectId, CompanyId, Branchid, DivisionId, FinancialYearId, UnitId);
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

        [HttpGet("StockMaterialDash/{MaterialId}/{CompanyId}/{Branchid}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> GetStockMaterialDash(int MaterialId, int CompanyId, int Branchid,int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _issueReturnRepository.GetStockMaterialDash(MaterialId, CompanyId, Branchid, FinancialYearId);
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

        [HttpGet("StockDash/{ProjectId}/{CategoryId}/{TypeId}/{DivisionId}/{Branchid}/{Id}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> GetStockDash(int ProjectId, int CategoryId, int TypeId, int DivisionId, int Branchid, int Id,int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _issueReturnRepository.GetStockDash(ProjectId, CategoryId, TypeId, DivisionId, Branchid, Id, FinancialYearId);
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


        [HttpGet("ReturnGetById/{Id}")]
        [Authorize]
        public async Task<IActionResult> GetByIdReturn(int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _issueReturnRepository.GetByIdReturn(Id);
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

        [HttpPost("Report")]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] MaterialSearch materialSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    if (materialSearch != null)
                    {
                        var details = await _issueReturnRepository.GetforReport(materialSearch);
                        return new OkObjectResult(details);
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
