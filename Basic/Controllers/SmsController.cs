using BuildExeBasic.Models;
using BuildExeBasic.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Transactions;
using System.Reflection;
using Microsoft.AspNetCore.DataProtection.Repositories;
using BuildExeBasic.Library;


namespace BuildExeBasic.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class SmsController : Controller
    {
        private readonly ISmsRepository _smsRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public SmsController(ISmsRepository smsRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _smsRepository = smsRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpGet("{CompanyId}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> GetSms(int CompanyId, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var entity = await _smsRepository.GetSms(CompanyId, BranchId);
                return new OkObjectResult(entity);
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
        public async Task<IActionResult> Post(Smsmodel smsmodel, [FromHeader] string mdhash, [FromHeader] int User)

        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var val = await _smsRepository.Post(smsmodel);
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
        public async Task<IActionResult> Put(Smsmodel smsmodel, [FromHeader] string mdhash, [FromHeader] int User)

        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var val = await _smsRepository.Put(smsmodel);
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


        [HttpPut("Status")]
        [Authorize]
        public async Task<IActionResult> PutStatus(Smsmodel smsmodel, [FromHeader] string mdhash, [FromHeader] int User)

        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var val = await _smsRepository.PutStatus(smsmodel);
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

        [HttpGet("Fetch/{Id}/{MenuId}")]
        [Authorize]
        public async Task<IActionResult> GetFetch(int Id, int MenuId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var entity = await _smsRepository.GetFetch(Id, MenuId);
                return new OkObjectResult(entity);
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
