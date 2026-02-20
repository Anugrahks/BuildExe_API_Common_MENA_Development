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
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeBasic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivateFinancialYearController : ControllerBase
    {
        private readonly IActivateFinancialYearRepository _activateFinancialYearController;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public ActivateFinancialYearController(IActivateFinancialYearRepository ActivateFinancialYearRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _activateFinancialYearController = ActivateFinancialYearRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }



        [HttpPost("{CompanyId}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> Post(int CompanyId, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {

                await _activateFinancialYearController.ActivateFYI(CompanyId, BranchId);
                return new NoContentResult();

                //string startdate = Convert.ToString(financialYear.start_date.Year);
                //string enddate = Convert.ToString(financialYear.end_date.Year);
                //string totaldate = startdate + "-" + enddate;
                //financialYear.Financial_Year = totaldate;


                //var validation = await _financialYearController.getvalidation1(financialYear);

                //foreach (var error in validation)
                //{
                //    if (error.StatusCode == 0)
                //        return new OkObjectResult(validation);

                //}

                //// using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                ////{
                //await _financialYearController.InsertFinancilaYear(financialYear);
                ////  scope.Complete();
                //return new OkObjectResult(validation);
                //return CreatedAtAction(nameof(Get), new { id = financialYear.FinancialYearId }, financialYear);
                // }
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
