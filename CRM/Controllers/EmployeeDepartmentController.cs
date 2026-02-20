using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BuildExeServices.Models;
using BuildExeServices.Repository;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeDepartmentController : ControllerBase
    {
        private readonly IEmployeeDepartmentRepository _employeeDepartmentRepository;

        public EmployeeDepartmentController(IEmployeeDepartmentRepository employeeDepartmentRepository)
        {
            _employeeDepartmentRepository = employeeDepartmentRepository;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var Department = _employeeDepartmentRepository.GetEmployeeDepartment ();
            return new OkObjectResult(Department);
        }


        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            var Department = _employeeDepartmentRepository.GetEmployeeDepartmentByID(id);
            return new OkObjectResult(Department);
        }


        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] EmployeeDepartment employeeDepartment)
        {
            using (var scope = new TransactionScope())
            {
                _employeeDepartmentRepository.InsertEmployeeDepartment(employeeDepartment);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = employeeDepartment.EmployeeDepartmentId  }, employeeDepartment);
            }
        }


        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody] EmployeeDepartment employeeDepartment)
        {
            if (employeeDepartment != null)
            {
                using (var scope = new TransactionScope())
                {
                    _employeeDepartmentRepository.Update_EmployeeDepartment(employeeDepartment);
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
            _employeeDepartmentRepository.Delete_EmployeeDepartment(id);
            return new OkResult();
        }
    }
}
