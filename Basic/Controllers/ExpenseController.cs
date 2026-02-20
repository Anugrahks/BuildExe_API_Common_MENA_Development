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
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseIncomeRepository _expenseIncomeRepository  ;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public ExpenseController(IExpenseIncomeRepository expenseIncomeRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _expenseIncomeRepository = expenseIncomeRepository;
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

                    if ((basicSearch.IsDetail is null) || (basicSearch.IsDetail == 1))
                    {
                        var product =await  _expenseIncomeRepository.ExpenseReport(basicSearch);
                        return new OkObjectResult(product);
                    }
                    else
                    {
                        var product = await _expenseIncomeRepository.ExpenseDetailReport(basicSearch);
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
    }
}
