using BuildExeServices.Library;
using BuildExeServices.Models;
using BuildExeServices.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Transactions;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public ClientController(IClientRepository clientRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _clientRepository = clientRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;

        }
        [HttpGet("{ProjectId}/{UnitId}")]
        [Authorize]
        public async Task<IActionResult> Get( int ProjectId, int UnitId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var products = await  _clientRepository.GetClientMasters ( ProjectId, UnitId);
            return new OkObjectResult(products);
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

        [HttpGet("journal/{ProjectId}/{UnitId}")]
        [Authorize]
        public async Task<IActionResult> Getnew(int ProjectId, int UnitId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var products = await _clientRepository.GetClientMastersnew(ProjectId, UnitId);
                return new OkObjectResult(products);
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

        [HttpGet("Get/{Companyid}/{Branchid}")]
        [Authorize]
        public async Task<IActionResult> GetClinet( int Companyid, int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var products = await _clientRepository.GetClient(Companyid, Branchid);
                return new OkObjectResult(products);
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
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
                try
            {
                var products = await  _clientRepository.Get();
            return new OkObjectResult(products);
            }
            catch (Exception)
            { throw; }
        }

        [HttpGet("GetUniqueNames/{ProjectId}/{CompanyId}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> GetUniqueNames(int ProjectId, int CompanyId, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var products = await _clientRepository.GetUniqueNames(ProjectId, CompanyId, BranchId);
                    return new OkObjectResult(products);
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
