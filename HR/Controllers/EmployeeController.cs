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
using Microsoft.AspNetCore.Mvc.Infrastructure;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeMasterRepository _employeeMasterRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public EmployeeController(IEmployeeMasterRepository employeeMasterRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _employeeMasterRepository = employeeMasterRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpGet("{companyid}/{Branchid}")]
        [Authorize]
        public async Task<IActionResult> Get(int companyid, int branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var designation = await _employeeMasterRepository.Get(companyid, branchid);
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

        [HttpGet("getwithProject/{projectId}/{categoryId}/{labourGroupId}")]
        [Authorize]
        public async Task<IActionResult> EmployeeWithProject(int projectId, int categoryId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var designation = await _employeeMasterRepository.EmployeeWithProject(categoryId, categoryId);
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

        [HttpGet("getforProject/{employeeId}/{categoryId}/{labourGroupId}")]
        [Authorize]
        public async Task<IActionResult> EmployeeforProject(int employeeId, int categoryId)
        {
                try
            {
                var designation = await _employeeMasterRepository.EmployeeforProject(employeeId, categoryId);
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

        [HttpGet("getforProject/{employeeId}/{categoryId}/{labourGroupId}/mobile")]
        public async Task<IActionResult> EmployeeforProjectmobile(int employeeId, int categoryId, [FromHeader] string mdhash, [FromHeader] int User)
        {
                try
            {
                var designation = await _employeeMasterRepository.EmployeeforProject(employeeId, categoryId);
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


        [HttpGet("getuser/{companyid}/{Branchid}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> Getbyuser(int companyid, int branchid, int Userid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var designation = await _employeeMasterRepository.Getuser(companyid, branchid, Userid);
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

        [HttpGet("getuserwithfinancial/{companyid}/{Branchid}/{UserId}/{Financialyearid}")]
        [Authorize]
        public async Task<IActionResult> Getbyuser(int companyid, int branchid, int Userid, int Financialyearid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var designation = await _employeeMasterRepository.Getuser(companyid, branchid, Userid, Financialyearid);
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

        [HttpGet("{CompanyId}/{Branchid}/{CategoryId}")]
        [Authorize]
        public async Task<IActionResult> Get(int CompanyId, int Branchid, int CategoryId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var material = await _employeeMasterRepository.GetbyCategory(CompanyId, Branchid, CategoryId);
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
        [HttpGet("Status/{CompanyId}/{Branchid}/{status}")]
        [Authorize]
        public async Task<IActionResult> Getbystatus(int CompanyId, int Branchid, string status, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var material = await _employeeMasterRepository.GetByStatus(CompanyId, Branchid, status);
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
        [HttpGet("Status/{CompanyId}/{Branchid}/{Category}/{status}")]
        [Authorize]
        public async Task<IActionResult> GetByStatusandCategory(int CompanyId, int Branchid, int Category, string status, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var material = await _employeeMasterRepository.GetByStatusandCategory(CompanyId, Branchid, Category, status);
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


        [HttpGet("StatusRejoining/{CompanyId}/{Branchid}")]
        [Authorize]
        public async Task<IActionResult> StatusRejoining(int CompanyId, int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var material = await _employeeMasterRepository.StatusRejoining(CompanyId, Branchid);
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

        [HttpGet("{CompanyId}/{Branchid}/{CategoryId}/{labourGroup}")]
        [Authorize]
        public async Task<IActionResult> Get(int CompanyId, int Branchid, int CategoryId, int labourGroup, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var material = await _employeeMasterRepository.GetbyCategoryandLabourgroup(CompanyId, Branchid, CategoryId, labourGroup);
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

        [HttpGet("{ProjectId}/{Unitid}/{BlockId}/{FloorId}/{CategoryId}")]
        [Authorize]
        public async Task<IActionResult> Get(int ProjectId, int Unitid, int BlockId, int FloorId, int CategoryId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var material = await _employeeMasterRepository.GetForAttendance(ProjectId, Unitid, BlockId, FloorId, CategoryId);
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

        [HttpGet("{id}")]
        [Authorize]

        public async Task<IActionResult> Get(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var department = await _employeeMasterRepository.GetByID(id);
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


        [HttpGet("{id}/mobile")]

        public async Task<IActionResult> Getmobile(int id)
        {
                try
            {
                var department = await _employeeMasterRepository.GetByID(id);
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

        [HttpGet("GenerateNextEmpNo/{CompanyId}")]
        [Authorize]
        //[Route("GenerateNextEmpNo")]
        public IActionResult GenerateNextEmpNo(int CompanyId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            
                try
            {
                var material = _employeeMasterRepository.GenerateNextEmpNo(CompanyId);
                return new OkObjectResult(material);
            }
            catch (Exception)
            { throw; }

        }

        [HttpGet("ValidationForPunching/{EmployeeId}")]
        //[Route("GenerateNextEmpNo")]
        public IActionResult ValidationForPunching(int EmployeeId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            
                try
            {
                var material = _employeeMasterRepository.ValidationForPunching(EmployeeId);
                return new OkObjectResult(material);
            }
            catch (Exception)
            { throw; }

        }

        [HttpPost]
        [Authorize]

        public async Task<IActionResult> Post([FromBody] EmployeeMaster employeeMaster, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                employeeMaster.Id = 0;
                var validation = await _employeeMasterRepository.getvalidation(employeeMaster);

                foreach (var error in validation)
                {
                    if (error.StatusCode == 0)
                        return new OkObjectResult(validation);

                }

                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var val = await _employeeMasterRepository.Insert(employeeMaster);
                    scope.Complete();
                    return new OkObjectResult(val);
                    //  return CreatedAtAction(nameof(Get), new { id = employeeMaster.Id }, employeeMaster);
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
        public async Task<IActionResult> Put([FromBody] EmployeeMaster employeeMaster, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {

                if (employeeMaster != null)
                {
                    var validation = await _employeeMasterRepository.getvalidation(employeeMaster);

                    foreach (var error in validation)
                    {
                        if (error.StatusCode == 0)
                            return new OkObjectResult(validation);
                    }


                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {

                        var val = await _employeeMasterRepository.Update(employeeMaster);
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



        [HttpDelete("{id}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var validation = await _employeeMasterRepository.Deletevalidation(id);

                foreach (var error in validation)
                {
                    if (error.StatusCode == 0)
                        return new OkObjectResult(validation);
                }
                await _employeeMasterRepository.Delete(id, UserId);
                return new OkObjectResult(validation);
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

        [HttpGet("EditDelete/{id}")]
        [Authorize]
        public async Task<IActionResult> EditDelete(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _employeeMasterRepository.CheckEditDelete(id);
                return new OkObjectResult(result);
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

        [HttpGet("detail/{EmployeeId}")]
        [Authorize]
        public async Task<IActionResult> GetEmployeeById(int EmployeeId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var employee = await _employeeMasterRepository.GetEmployeeById(EmployeeId);
                return new OkObjectResult(employee);

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


        


        [HttpGet("BirthDayReminder/{BranchId}/{UserId}/{Date}")]
        [Authorize]
        public async Task<IActionResult> BirthDayReminder(int BranchId, int UserId, DateTime Date, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var employee = await _employeeMasterRepository.BirthDayReminder(BranchId, UserId, Date);
                    return new OkObjectResult(employee);

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

        [HttpPost("validation")]
        [Authorize]
        public async Task<IActionResult> PostValidation(EmployeeMaster employeeMaster, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var employee = await _employeeMasterRepository.PostValidation(employeeMaster);
                return new OkObjectResult(employee);

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

        [HttpGet("ForSalaryPymt/{CompanyId}/{Branchid}/{CategoryId}")]
        [Authorize]
        public async Task<IActionResult> GetForSalaryPymt(int CompanyId, int Branchid, int CategoryId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var material = await _employeeMasterRepository.GetForSalaryPymt(CompanyId, Branchid, CategoryId);
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

        [HttpGet("ForCheckIn/{CompanyId}/{Branchid}/{CategoryId}")]
        [Authorize]
        public async Task<IActionResult> GetForCheckIn(int CompanyId, int Branchid, int CategoryId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var material = await _employeeMasterRepository.GetForCheckIn(CompanyId, Branchid, CategoryId);
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

        

    }

}
