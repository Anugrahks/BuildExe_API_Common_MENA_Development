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
    public class ReportConfigurationController : ControllerBase
    {
        private readonly IReportConfigurationRepository _reportConfigurationRepository  ;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public ReportConfigurationController(IReportConfigurationRepository reportConfigurationRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _reportConfigurationRepository = reportConfigurationRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }
        [HttpGet("{id}/{CompanyId}/{BranchID}")]
        [Authorize]
        public async Task<IActionResult> Get(int id,int CompanyId,int BranchID, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try {
                var department = await  _reportConfigurationRepository.GetByID(id, CompanyId, BranchID);
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

        [HttpGet("Filter/{id}/{CompanyId}/{BranchID}")]
        [Authorize]
        public async Task<IActionResult> GetFilter(int id, int CompanyId, int BranchID, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var department = await _reportConfigurationRepository.GetFilterByID(id, CompanyId, BranchID);
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
        [HttpGet("Field/{id}/{CompanyId}/{BranchID}")]
        [Authorize]
        public async Task<IActionResult> GetField(int id, int CompanyId, int BranchID, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var department = await _reportConfigurationRepository.GetFieldByID(id, CompanyId, BranchID);
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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] IEnumerable<ReportConfiguration > reportConfigurations, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var val = await _reportConfigurationRepository.Insert(reportConfigurations);
                scope.Complete();
                return new OkObjectResult(val);
                //return CreatedAtAction(nameof(Get), new { Id = journalEntry.Id }, journalEntry);
            }
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
        public async Task<IActionResult> Put([FromBody] IEnumerable<ReportConfiguration> reportConfigurations, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (reportConfigurations != null)
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        await _reportConfigurationRepository.Update(reportConfigurations);
                        scope.Complete();
                        return new OkResult();
                    }
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



        [HttpDelete("{id}/{UserID}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id, int UserID, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var val = await _reportConfigurationRepository.Delete(id, UserID);
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

        [HttpPost("Printable")]
        [Authorize]
        public async Task<IActionResult> PostPrint([FromBody] IEnumerable<PrintableTemplate> templates, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var val = await _reportConfigurationRepository.InsertPrintable(templates);
                    scope.Complete();
                    return new OkObjectResult(val);
                    //return CreatedAtAction(nameof(Get), new { Id = journalEntry.Id }, journalEntry);
                }
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

        [HttpGet("Printable/{id}/{CompanyId}/{BranchID}")]
        [Authorize]

        public async Task<IActionResult> GetPrintable(int id, int CompanyId, int BranchID, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var department = await _reportConfigurationRepository.GetPrintableTemplate(id, CompanyId, BranchID);
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

        [HttpGet("{MenuId}/{BranchID}")]
        [Authorize]

        public async Task<IActionResult> List(int MenuId,int BranchID, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var department = await _reportConfigurationRepository.GetList(MenuId,BranchID);
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

        [HttpGet("reportname")]
        [Authorize]
        public async Task<IEnumerable<ListReportName>> ListReportNames()
        {
            try
            {
                var reportNames = await _reportConfigurationRepository.ListReportNames();
                return reportNames;
            }
            catch(Exception) { throw; }

        }
    }
}
