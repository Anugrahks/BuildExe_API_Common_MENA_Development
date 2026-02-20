using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Repository;
using BuildExeServices.Models;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using BuildExeServices.Library;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TendorWorkOrderController : ControllerBase
    {
        private readonly ITendorWorkOrderRepository _tendorWorkOrderRepository  ;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        //private readonly IUserLogRepository _userLogRepository;
        public TendorWorkOrderController(ITendorWorkOrderRepository tendorWorkOrderRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _tendorWorkOrderRepository = tendorWorkOrderRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
            //_userLogRepository = userLogRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var floor = await _tendorWorkOrderRepository.Get();
            return new OkObjectResult(floor);
            }
            catch (Exception)
            { throw; }
        }


        [HttpGet("{projectid}")]
        [Authorize]
        public async Task<IActionResult> Get(int projectid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var floor =await _tendorWorkOrderRepository.GetByID(projectid);

                return new OkObjectResult(floor);
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


        [HttpGet("get/{projectid}")]
        [Authorize]
        public async Task<IActionResult> Getworkorder(int projectid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var floor = await _tendorWorkOrderRepository.GetByID2(projectid);

                return new OkObjectResult(floor);
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
        public async Task<IActionResult> Post([FromBody] TendorWorkOrderMaster tendorWorkOrder, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {

                    var val = await _tendorWorkOrderRepository.Insert(tendorWorkOrder);
                    return new OkObjectResult(val);
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
        public async Task<IActionResult> Put([FromBody] TendorWorkOrderMaster tendorWorkOrder, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (tendorWorkOrder != null)
            {
                    var val = await _tendorWorkOrderRepository.Update(tendorWorkOrder);

                    return new OkObjectResult(val);
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


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                await _tendorWorkOrderRepository.Delete(id);

            return new OkResult();
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

        [HttpGet("getTender/{Id}/{DivisionId}")]
        [Authorize]
        public async Task<IActionResult> getBudgetedAmount(int id,int divisionId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var floor = await _tendorWorkOrderRepository.getBudgetedAmount(id, divisionId);

                    return new OkObjectResult(floor);
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
