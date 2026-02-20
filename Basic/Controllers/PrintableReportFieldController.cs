using BuildExeBasic.Library;
using BuildExeBasic.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildExeBasic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrintableReportFieldController : ControllerBase
    {

        private readonly IPrintableReportFieldRepository _printableReportFieldRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public PrintableReportFieldController(IPrintableReportFieldRepository printableReportFieldRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _printableReportFieldRepository = printableReportFieldRepository;
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
                var department = await _printableReportFieldRepository.GetByID(id);
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
