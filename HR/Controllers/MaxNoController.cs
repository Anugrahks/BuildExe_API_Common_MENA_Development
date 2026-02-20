using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Repository;
using BuildExeHR.Models;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using BuildExeHR.Library;

namespace BuildExeHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaxNoController : ControllerBase
    {
        private readonly IForemanWorkBillRepository _foremanWorkBillRepository  ;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public MaxNoController(IForemanWorkBillRepository foremanWorkBillRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            
            _foremanWorkBillRepository = foremanWorkBillRepository;
                _userLogRepository = userLogRepository;
                _mdHashValidator = mdHashValidator;
        }
        [HttpGet("{Type}/{workOrderId}/{financialYearId}/")]
        [Authorize]
        public async Task< IActionResult> Get(int Type, int workOrderId, int financialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _foremanWorkBillRepository.GetmaxBillNo(Type, workOrderId, financialYearId);
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

    }
}
