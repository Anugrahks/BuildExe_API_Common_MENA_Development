
using BuildExeServices.Library;
using BuildExeServices.Models;
using BuildExeServices.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Transactions;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniqueIdController : ControllerBase
    {
        private readonly IUniqueIdRepository _uniqueIdRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public UniqueIdController(IUniqueIdRepository uniqueIdRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _uniqueIdRepository = uniqueIdRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetId([FromBody] GetUniqueId getUniqueId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _uniqueIdRepository.GetId(getUniqueId);
                return new OkObjectResult(result);
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

        [HttpPost("id")]
        [Authorize]
        public async Task<IActionResult> Get([FromBody] GetUniqueId getUniqueId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var result = await _uniqueIdRepository.Get(getUniqueId);
                    return new OkObjectResult(result);
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

        [HttpPost("SNo")]
       [Authorize]
        public async Task<IActionResult> GetSNo([FromBody] GetUniqueId getUniqueId, [FromHeader] string mdhash, [FromHeader] int User)
        {
           if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var json = JsonConvert.SerializeObject(getUniqueId);
                    var result = await _uniqueIdRepository.GetSNo(getUniqueId);
                    return new OkObjectResult(result);
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
