using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
using BuildExeServices.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialYearController : ControllerBase
    {
        private readonly IFinancialYearRepository  _financialYearController;
        public FinancialYearController(IFinancialYearRepository FinancialYearRepository)
        {
            _financialYearController = FinancialYearRepository;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var products = _financialYearController.GetFinancilaYear ();
            return new OkObjectResult(products);
            
        }


        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            var product = _financialYearController.GetFinancilaYearByID (id);
            return new OkObjectResult(product);
        }
        [HttpGet("Active/{Companyid}/{BranchId}")]
        [Authorize]
        public IActionResult Get(int Companyid, int BranchId)
        {
            var product = _financialYearController.GetActiveFinancialYear(Companyid, BranchId);
            return new OkObjectResult(product);
        }
        [HttpGet("{Companyid}/{BranchId}")]
        [Authorize]
        public IActionResult Getfinacialyear(int Companyid, int BranchId )
        {
            var product = _financialYearController.GetFinancialYear(Companyid, BranchId);
            return new OkObjectResult(product);
        }


        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] FinancialYear financialYear )
        {
            using (var scope = new TransactionScope())
            {
                _financialYearController.InsertFinancilaYear(financialYear);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = financialYear.FinancialYearId }, financialYear);
            }
        }


        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody] FinancialYear financialYear)
        {
            if (financialYear != null)
            {
                using (var scope = new TransactionScope())
                {
                    _financialYearController.UpdateFinancilaYear(financialYear);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }


        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            _financialYearController.DeleteFinancilaYear(id);
            return new OkResult();
        }
    }
}
