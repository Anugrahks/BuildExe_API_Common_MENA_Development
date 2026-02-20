using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BuildExeServices.Repository;
using Microsoft.AspNetCore.Authorization;
using BuildExeServices.Library;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using BuildExeServices.Models;

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyRentalController : ControllerBase
    {
        private readonly IPropertyRepository _PropertyRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public PropertyRentalController(IPropertyRepository PropertyRepository, IMdHashValidator mdHashValidator)
        {
            _PropertyRepository = PropertyRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] PropertyRentalDTO jsonData, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    
                    string jsonString = JsonConvert.SerializeObject(jsonData);
                    var result = await _PropertyRepository.InsertProperty(jsonString);
                    return new OkObjectResult(result);
                }
                catch (Exception ex)
                {
                    Logger.ErrorLog("PropertyController", "Post Method", ex);
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
        public async Task<IActionResult> Put([FromBody] PropertyRentalDTO jsonData, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    string jsonString = JsonConvert.SerializeObject(jsonData);
                    var result = await _PropertyRepository.UpdateProperty(jsonString);
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

        [HttpGet("RentType/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> GetRentalTypes(int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var result = await _PropertyRepository.GetPropertyRentalCategories(BranchId);
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

        [HttpGet("GetUnits/{ProjectId}")]
        [Authorize]
        public async Task<IActionResult> GetUnits(int ProjectId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var result = await _PropertyRepository.GetPropertyUnitDetails(ProjectId);
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

        [HttpGet("GetActiveTenants/{CompanyId}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> GetActiveTenants(int CompanyId, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var result = await _PropertyRepository.GetActiveTenants(CompanyId, BranchId);
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

        [HttpGet("GetProperties/{CompanyId}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> GetProperties(int CompanyId, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var result = await _PropertyRepository.GetProperties(CompanyId, BranchId);
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

        [HttpGet("GetVacantUnits/{CompanyId}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> GetVacantUnits(int CompanyId, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var result = await _PropertyRepository.GetVacantUnits(CompanyId, BranchId);
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
