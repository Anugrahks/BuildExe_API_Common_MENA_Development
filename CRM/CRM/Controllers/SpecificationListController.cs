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
using System.Security.Policy;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecificationListController : ControllerBase
    {
        private readonly ISpecificationMasterRepository _specificationMasterRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public SpecificationListController(ISpecificationMasterRepository specificationMasterRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _specificationMasterRepository = specificationMasterRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;

        }
        [HttpGet("{specId}")]
        [Authorize]
        public async Task<IActionResult> Get(int specId)
        {
        
                try
            {
                var product =await _specificationMasterRepository.Getspecdetails (specId);
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
        [HttpGet("{companyid}/{BranchId}/{DepartmentId}")]
        [Authorize]
        public async Task<IActionResult> Get(int companyid, int BranchId,int DepartmentId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product =await _specificationMasterRepository.GetforEdit(companyid, BranchId, DepartmentId);
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
        [HttpGet("{companyid}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> Get(int companyid, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product = await _specificationMasterRepository.GetforEdit(companyid, BranchId);
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
        [HttpGet("getuser/{companyid}/{BranchId}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> Getbyid(int companyid, int BranchId, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product = await _specificationMasterRepository.GetforEdituser(companyid, BranchId, UserId);
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
    }
}
