using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BuildExeServices.Repository;
using BuildExeServices.Models;
using Microsoft.AspNetCore.Authorization;
using BuildExeServices.Library;
namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnProjectBookingController : ControllerBase
    {
        private readonly IOwnProjectRepository _OwnProjectRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public  OwnProjectBookingController(IOwnProjectRepository ownProjectRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _OwnProjectRepository = ownProjectRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product = await _OwnProjectRepository.GetForBooking(id);
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

        [HttpGet("Unit/{id}")]
        [Authorize]
        public async Task<IActionResult> GetoneUnit(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product = await _OwnProjectRepository.SelectoneUnit(id);
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
