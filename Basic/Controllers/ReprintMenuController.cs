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
using Microsoft.AspNetCore.Authorization;
using BuildExeBasic.Library;
using System.Security.Policy;

namespace BuildExeBasic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReprintMenuController : ControllerBase
    {
        private readonly IPrintableReportConfigurationRepository _printableReportConfigurationRepository;
        private readonly IPrintableReportFieldRepository _printableReportFieldRepository;
        private readonly IFlatDictionaryProvider _flatDictionaryProvider;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public ReprintMenuController(
            IPrintableReportConfigurationRepository printableReportConfigurationRepository,
            IPrintableReportFieldRepository printableReportFieldRepository,
            IFlatDictionaryProvider flatDictionaryProvider, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _printableReportConfigurationRepository = printableReportConfigurationRepository;
            _printableReportFieldRepository = printableReportFieldRepository;
            _flatDictionaryProvider = flatDictionaryProvider;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] BasicSearch basicSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (basicSearch != null)
                {
                    var product = await _printableReportConfigurationRepository.Getreprintmenu(basicSearch);
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