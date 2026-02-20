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
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeMaterialServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialissueListController : ControllerBase
    {
        private readonly IMaterialUsageRepository  _materialUsageRepository  ;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public MaterialissueListController(IMaterialUsageRepository materialUsageRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _materialUsageRepository = materialUsageRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }
        [HttpGet("{IndentId}")]
        [Authorize]
        public async Task<IActionResult> Get(int IndentId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _materialUsageRepository.GetDetailsbyid(IndentId);
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

        [HttpGet("{CompanyId}/{Branchid}")]
        [Authorize]
        public async Task<IActionResult> Get(int CompanyId, int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase =await _materialUsageRepository.GetforEdit(CompanyId, Branchid);
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
        [HttpGet("GetUser/{CompanyId}/{Branchid}/{UserId}/{FinancialYearId}/{bulkentry}")]
        [Authorize]
        public async Task<IActionResult> GetByUser(int CompanyId, int Branchid, int UserId,int FinancialYearId, int bulkentry, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _materialUsageRepository.GetforEdit(CompanyId, Branchid, UserId, FinancialYearId, bulkentry);
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


        [HttpGet("{CompanyId}/{Branchid}/{UserID}/{FinancialYearId}/{bulkentry}")]
        [Authorize]
        public async Task<IActionResult> Get(int CompanyId, int Branchid, int UserID,int FinancialYearId,int bulkentry, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _materialUsageRepository.GetforApproval(CompanyId, Branchid, UserID, FinancialYearId, bulkentry);
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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] MaterialSearch materialSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (materialSearch != null)
                {
                    var product = await _materialUsageRepository.GetforReport(materialSearch);
                    return new OkObjectResult(product);
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


        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] MaterialSearch materialSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (materialSearch != null)
                {
                    var product = await _materialUsageRepository.Getforview(materialSearch);
                    return new OkObjectResult(product);
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
