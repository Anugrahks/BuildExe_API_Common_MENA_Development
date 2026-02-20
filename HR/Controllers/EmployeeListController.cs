using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
using System.Transactions;
using BuildExeHR.Repository;
using Microsoft.AspNetCore.Authorization;
using BuildExeHR.Library;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeListController : ControllerBase
    {
        private readonly IEmployeeListRepository _employeeListRepository  ;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        private readonly ISalaryIncrementRepository _salaryIncrementrepository;
        public EmployeeListController(IEmployeeListRepository employeeListRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _employeeListRepository = employeeListRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpGet("{CompanyId}/{Branchid}/{CategoryId}")]
        [Authorize]
        public async Task<IActionResult> Get(int CompanyId, int Branchid, int CategoryId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var material = await _employeeListRepository.Get(CompanyId, Branchid, CategoryId);
                return new OkObjectResult(material);
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

        [HttpGet("AdvanceLedger/{CompanyId}/{Branchid}/{CategoryId}/{ProjectId}")]
        [Authorize]
        public async Task<IActionResult> Get(int CompanyId, int Branchid, int CategoryId, int ProjectId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var material = await _employeeListRepository.GetAdvance(CompanyId, Branchid, CategoryId, ProjectId);
                return new OkObjectResult(material);
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


        [HttpGet("siteuser/{CompanyId}/{Branchid}/{CategoryId}/{sitemanager}/{sitemanagerid}")]
        [Authorize]
        public async Task<IActionResult> Getsiteusers(int CompanyId, int Branchid, int CategoryId,int sitemanager, int sitemanagerid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var material = await _employeeListRepository.Getsiteuser(CompanyId, Branchid, CategoryId, sitemanager, sitemanagerid);
                return new OkObjectResult(material);
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

        [HttpGet("siteuser/{CompanyId}/{Branchid}")]
        [Authorize]
        public async Task<IActionResult> Getsiteusers(int CompanyId, int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var material = await _employeeListRepository.Getsiteuser(CompanyId, Branchid);
                return new OkObjectResult(material);
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

            [HttpGet("{CompanyId}/{Branchid}/{CategoryId}/{ProjectId}")]
        [Authorize]
        public async Task<IActionResult> GetSite(int CompanyId, int Branchid, int CategoryId, int ProjectId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var material = await _employeeListRepository.GetEmpByProject(CompanyId, Branchid, CategoryId, ProjectId);
                return new OkObjectResult(material);
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

        [HttpGet("siteuser/{CompanyId}/{Branchid}/{CategoryId}/{ProjectId}/{sitemanger}/{sitemanagerid}")]
        [Authorize]
        public async Task<IActionResult> GetSite(int CompanyId, int Branchid, int CategoryId, int ProjectId, int sitemanger, int sitemanagerid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var material = await _employeeListRepository.GetEmpByProject(CompanyId, Branchid, CategoryId, ProjectId, sitemanger, sitemanagerid);
                return new OkObjectResult(material);
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

        [HttpGet("ByProj/{CompanyId}/{Branchid}/{ProjectId}/{UnitId}/{BlockId}/{FloorId}")]
        [Authorize]
        public async Task<IActionResult> Get(int CompanyId, int Branchid, int ProjectId, int UnitId, int BlockId, int FloorId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var sm = await _employeeListRepository.GetByProj(CompanyId, Branchid, ProjectId, UnitId, BlockId, FloorId);
                return new OkObjectResult(sm);
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


        [HttpGet("ByProj/{CompanyId}/{Branchid}/{ProjectId}/{UnitId}/{BlockId}/{FloorId}/{sitemanager}/{sitemanagerid}")]
        [Authorize]
        public async Task<IActionResult> Get(int CompanyId, int Branchid, int ProjectId, int UnitId, int BlockId, int FloorId, int sitemanager, int sitemanagerid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var sm = await _employeeListRepository.GetByProj(CompanyId, Branchid, ProjectId, UnitId, BlockId, FloorId, sitemanager, sitemanagerid);
                return new OkObjectResult(sm);
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


        [HttpGet("{CompanyId}/{Branchid}/{ProjectId}/{UnitId}/{BlockId}/{FloorId}")]
        [Authorize]
        public async Task<IActionResult> GetSMByProj(int CompanyId, int Branchid, int ProjectId, int UnitId, int BlockId, int FloorId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var material = await _employeeListRepository.Get(CompanyId, Branchid, ProjectId, UnitId, BlockId, FloorId);
                return new OkObjectResult(material);
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

        [HttpGet("{DepartmentId}/{DesinationId}")]
        [Authorize]
        public async Task<IActionResult> Get(int DepartmentId, int DesinationId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
            var material =await _employeeListRepository.Get(DepartmentId, DesinationId);
            return new OkObjectResult(material);
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
        public async Task<IActionResult> Post([FromBody] HRSearch hRSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (hRSearch != null)
            {
                var product =await _employeeListRepository.GetReport(hRSearch);
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

        [HttpGet("category/{EmployeeCategoryId}/{EmployeeLabourGroupId}/{CompanyId}/{BranchId}")]
        [Authorize]

        public async Task<IActionResult> GetEmployeeListById(int employeeCategoryId, int EmployeeLabourGroupId, int CompanyId, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var EmployeeList = await _employeeListRepository.GetEmployeeListById(employeeCategoryId, EmployeeLabourGroupId, CompanyId, BranchId);
                return new OkObjectResult(EmployeeList);
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

        [HttpGet("labourhead/{Id}")]
        [Authorize]

        public async Task<IActionResult> GetEmployeebylabourhead(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var EmployeeList = await _employeeListRepository.Getemployeebylabourheadid(id);
                return new OkObjectResult(EmployeeList);
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

        [HttpGet("NumberofDaysInMonth/{CompanyId}/{BranchId}/{DesignationId}")]
        [Authorize]
        public async Task<IActionResult> NumberofDaysInMonths(int CompanyId, int BranchId, int DesignationId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var EmployeeList = await _employeeListRepository.NumberofDaysInMonth(CompanyId, BranchId, DesignationId);
                return new OkObjectResult(EmployeeList);
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

        [HttpGet("getdetails/{Id}/{EmployeeId}")]
        [Authorize]

        public async Task<IActionResult> GetbyId(int Id, int EmployeeId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var department = await _salaryIncrementrepository.GetdetailbyId(Id, EmployeeId);
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
