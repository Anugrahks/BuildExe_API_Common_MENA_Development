using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
using System.Transactions;
using BuildExeBasic.Repository;
using BuildExeBasic.Helper;
using System.IO;
using System.Net.Http.Headers;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using BuildExeBasic.Library;

namespace BuildExeBasic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndividualEntryPrintableController : ControllerBase
    {
        private readonly IPrintableReportConfigurationRepository _printableReportConfigurationRepository;
        private readonly IPrintableReportFieldRepository _printableReportFieldRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public IndividualEntryPrintableController(
            IPrintableReportFieldRepository printableReportFieldRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _printableReportFieldRepository = printableReportFieldRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpGet("{id}/{CompanyId}/{BranchID}")]
        [Authorize]


        [HttpPost]
        [Authorize]

        public async Task<IActionResult> Post([FromBody] IndividualPrintable individual, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                PrintableReportViewModel printableReportViewModel = new PrintableReportViewModel();
                var printable = await _printableReportFieldRepository.IndividualEntryPrint(individual);
                if (!printable.Equals("[]"))
                {
                    printableReportViewModel = JsonConvert.DeserializeObject<List<PrintableReportViewModel>>(printable).FirstOrDefault();
                }
                return new OkObjectResult(printableReportViewModel);
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
