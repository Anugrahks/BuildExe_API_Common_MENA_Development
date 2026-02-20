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
    public class ProjectAnalysisController : ControllerBase
    {
        private readonly IProjectAnalysisRepository _projectAnalysisRepository  ;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public ProjectAnalysisController(IProjectAnalysisRepository projectAnalysisRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _projectAnalysisRepository = projectAnalysisRepository;
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

                if ((basicSearch.IsDetail is null) || (basicSearch.IsDetail == 0))
                {
                    var product = await _projectAnalysisRepository.ProjectAnalysisReport (basicSearch);
                    return new OkObjectResult(product);
                }
                else
                {
                    var product = await _projectAnalysisRepository.ProjectAnalysisReportDetail(basicSearch);
                    return new OkObjectResult(product);
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


        [HttpPost("Print")]
        [Authorize]
        public async Task<IActionResult> Postprint([FromBody] BasicSearch basicSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                        var product = await _projectAnalysisRepository.ProjectAnalysisReportPrint(basicSearch);
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



        [HttpPost("PrintDashboard")]
        [Authorize]
        public async Task<IActionResult> PrintDashboard([FromBody] BasicSearch basicSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var product = await _projectAnalysisRepository.ProjectAnalysisReportPrintDashboard(basicSearch);
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

        

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] BasicSearch basicSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (basicSearch != null)
                {

                    var product = await _projectAnalysisRepository.ProjectAnalysisReportDetail_Datewise(basicSearch);
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
        [HttpGet("{CompanyId}/{Branchid}/{ProjectId}/{PageNumber}/{RowsPerPage}")]
        [Authorize]

        public async Task<IActionResult> Get(int CompanyId, int Branchid, int ProjectId, int PageNumber, int RowsPerPage, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {

                try
            {
                var designation = await _projectAnalysisRepository.GetProjectGraph(CompanyId, Branchid, ProjectId, PageNumber, RowsPerPage,0);
                return new OkObjectResult(designation);
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


        [HttpGet("{CompanyId}/{Branchid}/{ProjectId}/{UnitId}")]
        [Authorize]

        public async Task<IActionResult> Get(int CompanyId, int Branchid, int ProjectId, int UnitId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var designation = await _projectAnalysisRepository.GetProjectGraph(CompanyId, Branchid, ProjectId, 1, 10, UnitId);
                return new OkObjectResult(designation);
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

        [HttpGet("CashFlow/{CompanyId}/{Branchid}/{FinancialYearId}/{ProjectId}/{Unitid}")]
        [Authorize]

        public async Task<IActionResult> Getcash(int CompanyId, int Branchid, int FinancialYearId, int ProjectId,int UnitId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {

                try
            {
                var designation = await _projectAnalysisRepository.CashFlowGraph(CompanyId, Branchid, FinancialYearId,  ProjectId, UnitId);
                return new OkObjectResult(designation);
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



        [HttpPost("DocumentUpload")]
        [Authorize]

        public async Task<IActionResult> PostDocument([FromBody] BasicSearch basicSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var designation = await _projectAnalysisRepository.DocumentUpload(basicSearch);
                return new OkObjectResult(designation);
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
