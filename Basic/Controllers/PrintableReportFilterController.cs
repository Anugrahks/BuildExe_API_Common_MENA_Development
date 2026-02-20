using BuildExeBasic.Library;
using BuildExeBasic.Models;
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
    public class PrintableReportFilterController : ControllerBase
    {
        private readonly IPrintableReportFilter _printableReportFilter;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public PrintableReportFilterController(IPrintableReportFilter printableReportFilter, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _printableReportFilter = printableReportFilter;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpGet("{menuId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PrintableReportFilter>>> GetPrintableReportFilter(int menuId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var printableReportFilters = await _printableReportFilter.GetPrintableReportFilter(menuId);
                return Ok(printableReportFilters);
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
