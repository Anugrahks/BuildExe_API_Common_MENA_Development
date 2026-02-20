using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Repository;
using BuildExeMaterialServices.Models;
using Microsoft.AspNetCore.Authorization;
using BuildExeMaterialServices.Library;

namespace BuildExeMaterialServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseViewController : ControllerBase
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public PurchaseViewController(IPurchaseRepository purchaseRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _purchaseRepository = purchaseRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
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
                    var product = await _purchaseRepository.Getforview(materialSearch);
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
