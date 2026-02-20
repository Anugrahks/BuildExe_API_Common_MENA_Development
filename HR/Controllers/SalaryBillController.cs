using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Repository;
using BuildExeHR.Models;
using System.Transactions;
using Microsoft.IdentityModel.Tokens;
using BuildExeHR.Common;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using BuildExeHR.Library;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryBillController : ControllerBase
    {
        private readonly ISalaryBillRepository _salaryBillRepository ;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public SalaryBillController(ISalaryBillRepository salaryBillRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _salaryBillRepository = salaryBillRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpGet("{CompanyId}/{BranchId}/{UserId}/{MonthId}/{YearId}/{FinancialYearId}/{Date}")]
        [Authorize]
        public async Task<IActionResult> GetSalaryBill(int CompanyId, int Branchid, int UserId, int MonthId, int YearId,int FinancialYearId, DateTime Date, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _salaryBillRepository.GetSalaryBill(CompanyId, Branchid, UserId, MonthId, YearId,FinancialYearId, Date,0,0, DateTime.Now, DateTime.Now,0);
                return new OkObjectResult(purchase);
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


        [HttpGet("EmployeeList/{Id}/{CompanyId}/{BranchId}/{UserId}/{MonthId}/{YearId}/{FinancialYearId}/{Date}")]
        [Authorize]
        public async Task<IActionResult> EmployeeList(int Id, int CompanyId, int Branchid, int UserId, int MonthId, int YearId, int FinancialYearId, DateTime Date, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _salaryBillRepository.EmployeeList(Id, CompanyId, Branchid, UserId, MonthId, YearId, FinancialYearId, Date);
                    return new OkObjectResult(purchase);
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
        

        [HttpGet("SalaryBillForEmployee/{CompanyId}/{BranchId}/{UserId}/{MonthId}/{YearId}/{FinancialYearId}/{Date}/{EmployeeId}/{FromDate}/{ToDate}")]
        [Authorize]
        public async Task<IActionResult> GetSalaryBill(int CompanyId, int Branchid, int UserId, int MonthId, int YearId, int FinancialYearId, DateTime Date, int EmployeeId,DateTime FromDate,DateTime ToDate,  [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _salaryBillRepository.GetSalaryBill(CompanyId, Branchid, UserId, MonthId, YearId, FinancialYearId, Date, EmployeeId, 0, FromDate, ToDate,1);
                    return new OkObjectResult(purchase);
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

        [HttpGet("SalaryBillForDuration/{CompanyId}/{BranchId}/{UserId}/{MonthId}/{YearId}/{FinancialYearId}/{Date}/{DurationId}/{FromDate}/{ToDate}")]
        [Authorize]
        public async Task<IActionResult> GetSalaryBillDuration(int CompanyId, int Branchid, int UserId, int MonthId, int YearId, int FinancialYearId, DateTime Date,int DurationId, DateTime FromDate,DateTime ToDate, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _salaryBillRepository.GetSalaryBill(CompanyId, Branchid, UserId, MonthId, YearId, FinancialYearId, Date, 0, DurationId, FromDate,ToDate,1);
                    return new OkObjectResult(purchase);
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



        [HttpPost("SalaryBillGenerator")]
        [Authorize]
        public async Task<IActionResult> SalaryBillGenerator([FromBody] HRSearch hRSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _salaryBillRepository.SalaryBillGenerator(hRSearch);
                    return new OkObjectResult(purchase);
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

        [HttpGet("SalaryBillReprint/{Id}")]
        [Authorize]
        public async Task<IActionResult> SalaryBillReprint(int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _salaryBillRepository.SalaryBillReprint(Id);
                    return new OkObjectResult(purchase);
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


        [HttpGet("getforvalidation/{CompanyId}/{BranchId}/{UserId}/{MonthId}/{YearId}")]
        [Authorize]
        public async Task<IActionResult> getforvalidation(int CompanyId, int Branchid, int UserId, int MonthId, int YearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                var purchase = await _salaryBillRepository.GetSalaryValidation(CompanyId, Branchid, UserId, MonthId, YearId,0, 0, DateTime.Now, DateTime.Now);
                return new OkObjectResult(purchase);
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



        [HttpGet("getforvalidation/{CompanyId}/{BranchId}/{UserId}/{MonthId}/{YearId}/{EmployeeId}/{DurationId}/{fromDate}/{toDate}")]
        [Authorize]
        public async Task<IActionResult> getforvalidation(int CompanyId, int Branchid, int UserId, int MonthId, int YearId, int EmployeeId,int DurationId, DateTime fromDate, DateTime toDate, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _salaryBillRepository.GetSalaryValidation(CompanyId, Branchid, UserId, MonthId, YearId, EmployeeId, DurationId, fromDate, toDate);
                    return new OkObjectResult(purchase);
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


        [HttpPost("Validation")]
        [Authorize]
        public async Task<IActionResult> Validation([FromBody] HRSearch hRSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    if (hRSearch != null)
                    {
                        var product = await _salaryBillRepository.Validation(hRSearch);
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

        [HttpPost("Report")]
        [Authorize]
        public async Task<IActionResult> Report([FromBody] HRSearch hRSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (hRSearch != null)
                {
                    var product = await _salaryBillRepository.Getjson(hRSearch);
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


        [HttpPost("WPS")]
        [Authorize]
        public async Task<IActionResult> WPS([FromBody] HRSearch hRSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    if (hRSearch != null)
                    {
                        var product = await _salaryBillRepository.WPS(hRSearch);
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



        [HttpPost("WPSSearch")]
        [Authorize]
        public async Task<IActionResult> WPSSearch([FromBody] HRSearch hRSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    if (hRSearch != null)
                    {
                        var product = await _salaryBillRepository.WPSSearch(hRSearch);
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


        [HttpGet("{CompanyId}/{BranchId}/{UserId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> GetforApproval(int CompanyId, int BranchId, int UserId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase =await _salaryBillRepository.GetForApproval(CompanyId, BranchId, UserId, FinancialYearId);
                return new OkObjectResult(purchase);
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

        [HttpGet("GetUser/{CompanyId}/{BranchId}/{UserId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> GetUser(int CompanyId, int Branchid, int UserId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _salaryBillRepository.GetByUser(CompanyId, Branchid, UserId, FinancialYearId);
                return new OkObjectResult(purchase);
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

        [HttpPost()]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] IEnumerable<SalaryBill> salaryBill, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var department = await _salaryBillRepository.Insert(salaryBill);
                    scope.Complete();
                    return new OkObjectResult(department);

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

        [HttpPut()]
        [Authorize]
        public async Task<IActionResult> PUT([FromBody] IEnumerable<SalaryBill> salaryBill, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var department = await _salaryBillRepository.Update(salaryBill);
                    scope.Complete();
                    return new OkObjectResult(department);

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

        [HttpDelete("{Id}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int Id, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                await _salaryBillRepository.Delete(Id, UserId);
                return new OkResult();
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


        [HttpGet("getforview/{employeeid}/{monthid}/{yearId}/{companyid}/{branchid}/{financialYearId}")]
        [Authorize]
        public async Task<IActionResult> Getdetailsforviews(int employeeid, int monthid, int yearId, int companyid, int branchid, int financialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _salaryBillRepository.Getdetailsforview( employeeid,  monthid, yearId,  companyid,  branchid,  financialYearId);
                return new OkObjectResult(purchase);
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



        [HttpGet("GetEmployeeList/{CompanyId}/{BranchId}/{CategoryId}")]
        [Authorize]
        public async Task<IActionResult> GetEmployeeList(int CompanyId, int BranchId, int CategoryId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _salaryBillRepository.GetEmployeeList(CompanyId, BranchId, CategoryId);
                    return new OkObjectResult(purchase);
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

        [HttpGet("getforview/{employeeid}/{monthid}/{yearId}/{companyid}/{branchid}/{financialYearId}/{FromDate}/{ToDate}")]
        [Authorize]
        public async Task<IActionResult> Getdetailsforviewsduration(int employeeid, int monthid, int yearId, int companyid, int branchid, int financialYearId, DateTime FromDate, DateTime ToDate, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _salaryBillRepository.Getdetailsforviewduration(employeeid, monthid,yearId, companyid, branchid, financialYearId, FromDate, ToDate);
                    return new OkObjectResult(purchase);
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
