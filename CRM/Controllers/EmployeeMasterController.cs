using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using BuildExeServices.Repository;
using BuildExeServices.Models;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeMasterController : ControllerBase
    {

        private readonly IEmployeeMasterRepository _employeeMasterRepository;
        public EmployeeMasterController(IEmployeeMasterRepository employeeMasterRepository)
        {
            _employeeMasterRepository = employeeMasterRepository;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var employee = _employeeMasterRepository.GetEmployee();
            return new OkObjectResult(employee);

        }


        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            var branch = _employeeMasterRepository.GetEmployeeByID (id);

            return new OkObjectResult(branch);
        }



        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] Employee employee)
        {
            using (var scope = new TransactionScope())
            {
                _employeeMasterRepository.InsertEmployee(employee);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = employee.CompanyId }, employee);
            }
        }


        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody] Employee employee)
        {
            if (employee != null)
            {
                using (var scope = new TransactionScope())
                {
                    _employeeMasterRepository.UpdateEmployee(employee);
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
            _employeeMasterRepository.DeleteEmployee(id);
            return new OkResult();
        }
    }
}
