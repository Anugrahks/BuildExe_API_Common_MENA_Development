using BuildExeServices.Models;
using BuildExeServices.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using BuildExeServices.Library;

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportHeaderSettingsController : ControllerBase
    {
        private readonly IReportHeaderSettingsRepository _reportHeaderSettingsRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public ReportHeaderSettingsController(IReportHeaderSettingsRepository reportHeaderSettingsRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _reportHeaderSettingsRepository = reportHeaderSettingsRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;

        }
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var branch = _reportHeaderSettingsRepository.GetHeader();
            return new OkObjectResult(branch);

        }

        [HttpGet("{Companyid}/{BranchID}")]
        [Authorize]
        public async Task<IActionResult> GetByCompanyAndBranch(int Companyid, int BranchID, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var header = _reportHeaderSettingsRepository.GetByCompanyAndBranch(Companyid, BranchID);
                return new OkObjectResult(header);
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

        [HttpGet("{Companyid}/{BranchID}/{id}/{status}")]
        [Authorize]
        public async Task<IActionResult> HeaderUpdate(int Companyid, int BranchID, int id, string status, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var det = await _reportHeaderSettingsRepository.HeaderUpdate(Companyid, BranchID, id, status);
                return new OkObjectResult(det);
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

        [HttpGet("{Companyid}/{BranchID}/{id}/{type}/{status}")]
        [Authorize]
        public async Task<IActionResult> ActiveHeaders(int Companyid, int BranchID,  int id, int type,  string status, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var det = await _reportHeaderSettingsRepository.HeaderStatusByType(Companyid, BranchID, id, type, status);
                return new OkObjectResult(det);
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

        [HttpGet("Validation/{Companyid}/{BranchID}/{id}/{headerName}")]
        [Authorize]
        public async Task<IActionResult> HeaderNameValidation(int Companyid, int BranchID, int id, string headerName, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var det = await _reportHeaderSettingsRepository.HeaderNameValidation(Companyid, BranchID, id, headerName);
                return new OkObjectResult(det);
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


        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var branch = _reportHeaderSettingsRepository.GetByID(id);

                    return new OkObjectResult(branch);
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
        public async Task<IActionResult> Post([FromBody] ReportHeaderSettings reportHeaderSettings, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var val = await _reportHeaderSettingsRepository.Insert(reportHeaderSettings);
                        scope.Complete();
                        return new OkObjectResult(val);
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
        public async Task<IActionResult> Put([FromBody] ReportHeaderSettings reportHeaderSettings, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    if (reportHeaderSettings != null)
                    {
                        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                        {
                            var val = await _reportHeaderSettingsRepository.Update(reportHeaderSettings);
                            scope.Complete();
                            return new OkObjectResult(val);
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


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var val = await _reportHeaderSettingsRepository.Delete(id);
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
    }
}
