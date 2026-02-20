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
    public class ReceiptListController : ControllerBase
    {
        private readonly IRecieptsRepository _recieptsRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public ReceiptListController(IRecieptsRepository recieptsRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _recieptsRepository = recieptsRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;

        }
        [HttpGet("{companyid}/{BranchId}/{MenuId}")]
        [Authorize]
        public async Task<IActionResult> Get(int companyid, int BranchId,int MenuId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product = await _recieptsRepository.GetforEdit (companyid, BranchId, MenuId);
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

        [HttpGet("getuser/{companyid}/{BranchId}/{MenuId}/{UserId}/{FinancialYearId}")]
       // [Authorize]
        public async Task<IActionResult> Getbyid(int companyid, int BranchId, int MenuId, int UserId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
           // if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product = await _recieptsRepository.GetforEdituser(companyid, BranchId, MenuId, UserId, FinancialYearId);
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
            //else
            //{
            //    return Unauthorized("Invalid MdHash");
            //}
        }

        [HttpGet("{companyid}/{BranchId}/{UserId}/{MenuId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> Get(int companyid, int BranchId,int UserId, int MenuId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product = await _recieptsRepository.Getforapproval (companyid, BranchId, UserId, MenuId, FinancialYearId);
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

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product =await _recieptsRepository.getRecieptDetails(id);
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
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] BillSearch billSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (billSearch != null)
                {
                    var product = await _recieptsRepository.GetforView(billSearch);

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
