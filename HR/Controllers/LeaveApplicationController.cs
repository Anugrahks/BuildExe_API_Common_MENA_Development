using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Repository;
using BuildExeHR.Models;
using System.Transactions;
using System.Data;
using Microsoft.IdentityModel.Tokens;
using BuildExeHR.Common;
using Microsoft.AspNetCore.Authorization;
using BuildExeHR.Library;

namespace BuildExeHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveApplicationController : ControllerBase
    {
        private readonly ILeaveApplicationRepository _leaveApplicationRepository ;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public LeaveApplicationController(ILeaveApplicationRepository leaveApplicationRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _leaveApplicationRepository = leaveApplicationRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }
        [HttpGet("GetUser/{companyid}/{Branchid}/{UserId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> GetByUser(int companyid, int Branchid, int UserId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _leaveApplicationRepository.GetforEdit(companyid, Branchid, UserId, FinancialYearId);
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

        [HttpGet("Document/{id}")]
        [Authorize]
        public async Task<IActionResult> GetDocument(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _leaveApplicationRepository.GetleaveDocuments (id);

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
        [HttpGet("Employee/{id}")]
        [Authorize]
        public async Task<IActionResult> GetLastLeave(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _leaveApplicationRepository.GetLastLeave(id);

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

        [HttpGet("AccountClearence/{companyid}/{Branchid}")]
        [Authorize]
        public async Task<IActionResult> GetAccountClearence(int companyid, int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _leaveApplicationRepository.GetforAccountClearence (companyid, Branchid);
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

        [HttpGet("{companyid}/{Branchid}/{UserId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> Get(int companyid, int Branchid, int UserId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _leaveApplicationRepository.GetforApproval(companyid, Branchid, UserId, FinancialYearId);
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

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _leaveApplicationRepository.GetbyID(id);

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

        [HttpGet("details/{id}")]
        [Authorize]
        public async Task<IActionResult> Getdetailsbyid(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _leaveApplicationRepository.GetdetailsbyID(id);

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
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] IEnumerable<LeaveApplication> LeaveApplication, [FromHeader] string mdhash, [FromHeader] int User)
        //   public  IActionResult Post([FromBody] LeaveApplication LeaveApplication)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                /*

                FromDTToList fromtolist = new FromDTToList();
                DataTable dt = fromtolist.ConvertToList(LeaveApplication);
                

                utitlity util = new utitlity();
                List<VaryingHeads> lstVaryingHeads = util.ConvertDataTable<VaryingHeads>(dt);

                LeaveApplication.VaryingHeads = lstVaryingHeads;
                */


                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    
                    var department = await _leaveApplicationRepository.Insert(LeaveApplication);
                    scope.Complete();
                    //return Ok();
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

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] IEnumerable<LeaveApplication> leaveApplications, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                
                //

                if (leaveApplications != null)
                {
/*
                    FromDTToList fromtolist = new FromDTToList();
                    DataTable dt = fromtolist.ConvertToList(leaveApplications);


                    utitlity util = new utitlity();
                    List<VaryingHeads> lstVaryingHeads = util.ConvertDataTable<VaryingHeads>(dt);

                    leaveApplications.VaryingHeads = lstVaryingHeads;

*/
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var department = await _leaveApplicationRepository.Update(leaveApplications);
                        scope.Complete();
                        return new OkObjectResult(department);
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
                var department = await _leaveApplicationRepository.Delete(id, UserId);
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


        [HttpGet("datevalidation/{Fromdate}/{employeeid}")]
        [Authorize]
        public async Task<IActionResult> Datevalidationnew(DateTime fromdate, int employeeid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var department = await _leaveApplicationRepository.Datevalidation(fromdate, employeeid);
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

        [HttpGet("leavevalidation/{Fromdate}/{employeeid}/{leaveid}/{financialYearId}")]
        [Authorize]
        public async Task<IActionResult> Leavevalidationnew(DateTime fromdate, int employeeid, int leaveid, int financialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var department = await _leaveApplicationRepository.LeaveValidation(fromdate, employeeid,leaveid, financialYearId);
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


        [HttpGet("leavevalidationDuration/{Fromdate}/{employeeid}/{leaveid}/{financialYearId}/{DurationId}")]
        [Authorize]
        public async Task<IActionResult> LeaveValidationDuration(DateTime fromdate, int employeeid, int leaveid, int financialYearId,int DurationId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var department = await _leaveApplicationRepository.LeaveValidationDuration(fromdate, employeeid, leaveid, financialYearId, DurationId);
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



        [HttpGet("Status/{CompanyId}/{Branchid}/{Category}/{status}")]
        [Authorize]
        public async Task<IActionResult> Status(int CompanyId, int BranchId, int Category, string status, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var department = await _leaveApplicationRepository.Status(CompanyId, BranchId, Category, status);
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

        [HttpGet("leavevalidationinmonthly/{monthid}/{yearid}/{employeeid}/{leaveid}/{financialYearId}")]
        [Authorize]
        public async Task<IActionResult> Leavevalidationnewmonthly(int monthid, int yearid, int employeeid, int leaveid, int financialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var department = await _leaveApplicationRepository.LeaveValidationmonthly(monthid, yearid, employeeid, leaveid, financialYearId);
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


        [HttpGet("leavevalidationinmonthlyduration/{monthid}/{yearid}/{employeeid}/{leaveid}/{financialYearId}/{FromDate}/{ToDate}")]
        [Authorize]
        public async Task<IActionResult> leavevalidationinmonthlyduration(int monthid, int yearid, int employeeid, int leaveid, int financialYearId,DateTime FromDate , DateTime ToDate, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var department = await _leaveApplicationRepository.LeaveValidationmonthlyDuration(monthid, yearid, employeeid, leaveid, financialYearId,FromDate , ToDate);
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

        [HttpGet("leaveApplication/{companyid}/{Branchid}/")]
        [Authorize]
        public async Task<IActionResult> leaveApplication(int companyid, int Branchid ,[FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _leaveApplicationRepository.LeaveApplication(companyid, Branchid);
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




        [HttpGet("GetUserMobile/{companyid}/{Branchid}/{UserId}/{FinancialYearId}")]
        public async Task<IActionResult> GetByUserforMobile(int companyid, int Branchid, int UserId, int FinancialYearId)
        {
            try
            {
                var purchase = await _leaveApplicationRepository.GetforEditMobile(companyid, Branchid, UserId, FinancialYearId);
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



        [HttpGet("datevalidationMobile/{Fromdate}/{employeeid}")]
        public async Task<IActionResult> DatevalidationnewMobile(DateTime fromdate, int employeeid)
        {
            try
            {
                var department = await _leaveApplicationRepository.DatevalidationMobile(fromdate, employeeid);
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



        [HttpPost("LeaveapplicationMobile")]
        public async Task<IActionResult> LeaveapplicationMobile([FromBody] IEnumerable<LeaveApplication> LeaveApplication)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var result = await _leaveApplicationRepository.InsertMobile(LeaveApplication);
                    scope.Complete();
                    return Ok(result);
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




        [HttpGet("leavevalidationDurationMobile/{Fromdate}/{employeeid}/{leaveid}/{financialYearId}/{DurationId}")]
        public async Task<IActionResult> LeaveValidationDurationMobile(DateTime fromdate,int employeeid,int leaveid,  int financialYearId,int DurationId)
        {
            try
            {
                var result = await _leaveApplicationRepository.LeaveValidationDurationMobile(fromdate, employeeid, leaveid, financialYearId, DurationId);

                return Ok(result);
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


        #region Annual Leave

        [HttpPost("AppliedAnnualLeaves")]
        [Authorize]
        public async Task<IActionResult> AppliedAnnualLeaves([FromBody] HRSearch hRSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _leaveApplicationRepository.AppliedAnnualLeaves(hRSearch);
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

        [HttpGet("DetailsForAnnLv/{Id}")]
        [Authorize]
        public async Task<IActionResult> DetailsForAnnLv(int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _leaveApplicationRepository.DetailsForAnnLv(Id);
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

        [HttpPost("GetEmployeeSettlements")]
        [Authorize]
        public async Task<IActionResult> GetEmployeeSettlements([FromBody] HRSearch hRSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _leaveApplicationRepository.GetEmployeeSettlements(hRSearch);
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

        [HttpPost("GetSalaryHeads")]
        [Authorize]
        public async Task<IActionResult> GetSalaryHeads([FromBody] HRSearch hRSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _leaveApplicationRepository.GetSalaryHeads(hRSearch);
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

        #endregion


    }
}
