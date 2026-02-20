using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
using System.Transactions;
using BuildExeBasic.Repository;
using Microsoft.AspNetCore.Authorization;
using BuildExeBasic.Library;

namespace BuildExeBasic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportFieldController : ControllerBase
    {
        private readonly IReportFieldRepository _reportFieldRepository  ;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public ReportFieldController(IReportFieldRepository reportFieldRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _reportFieldRepository = reportFieldRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;

        }

        [HttpGet()]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var designation = await _reportFieldRepository.Get();
            return new OkObjectResult(designation);
            }
            catch (Exception)
            { throw; }
        }


        [HttpGet("{id}")]
        [Authorize]

        public async Task<IActionResult> Get(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var department = await _reportFieldRepository.GetByID(id);
            return new OkObjectResult(department);
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


        [HttpGet("Printable/{id}")]
        [Authorize]
        public async Task<IActionResult> PrintableGet(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var department = await _reportFieldRepository.GetPrintableByID(id);
                return new OkObjectResult(department);
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
