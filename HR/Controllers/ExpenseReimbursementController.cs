using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using BuildExeHR.Library;
using BuildExeHR.Models;
using BuildExeHR.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildExeHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseReimbursementController : ControllerBase
    {
        private readonly IExpenseReimbursementRepository _expenseReimbursementRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public ExpenseReimbursementController(
            IExpenseReimbursementRepository expenseReimbursementRepository,
            IUserLogRepository userLogRepository,
            IMdHashValidator mdHashValidator)
        {
            _expenseReimbursementRepository = expenseReimbursementRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }





        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] IEnumerable<ExpenseReimbursement> items, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (!await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                return Unauthorized("Invalid MdHash");

            try
            {
                using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                var validationResult = await _expenseReimbursementRepository.Insert(items);
                scope.Complete();
                return Ok(validationResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}", statusCode = 0 });
            }
        }



        //[HttpPost()]
        //[Authorize]
        //public async Task<IActionResult> Post([FromBody] IEnumerable<SalaryBill> salaryBill, [FromHeader] string mdhash, [FromHeader] int User)
        //{
        //    if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
        //    {
        //        try
        //        {
        //            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        //            {
        //                var department = await _salaryBillRepository.Insert(salaryBill);
        //                scope.Complete();
        //                return new OkObjectResult(department);

        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return StatusCode(500, new
        //            {
        //                message = $"An error occurred: {ex.Message}",
        //                statusCode = 0
        //            });
        //        }
        //    }
        //    else
        //    {
        //        return Unauthorized("Invalid MdHash");
        //    }
        //}

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] IEnumerable<ExpenseReimbursement> items, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (!await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                return Unauthorized("Invalid MdHash");

            try
            {
                using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                var validationResult = await _expenseReimbursementRepository.Update(items);
                scope.Complete();
                return Ok(validationResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}", statusCode = 0 });
            }
        }



        [HttpDelete("{Id}/{UserId}")]
       [Authorize]
        public async Task<IActionResult> Delete(int Id, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (!await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                return Unauthorized("Invalid MdHash");

            try
            {
                await _expenseReimbursementRepository.Delete(Id, UserId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}", statusCode = 0 });
            }
        }

        [HttpGet("{Id}/{CompanyId}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> GetById(int Id, int CompanyId, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (!await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                return Unauthorized("Invalid MdHash");

            try
            {
                var result = await _expenseReimbursementRepository.GetById(Id, CompanyId, BranchId);
                return Content(result, "application/json");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}", statusCode = 0 });
            }
        }

        [HttpGet("GetForGridView/{CompanyId}/{BranchId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> GetForGridView(int CompanyId, int BranchId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (!await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                return Unauthorized("Invalid MdHash");

            try
            {
                var result = await _expenseReimbursementRepository.GetAll(CompanyId, BranchId, FinancialYearId);
                return Content(result, "application/json");
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



    }
}