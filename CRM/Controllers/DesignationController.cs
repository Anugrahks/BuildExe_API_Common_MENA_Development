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
    public class DesignationController : ControllerBase
    {

        private readonly IDesignationRepository _DesignationRepository;

        public DesignationController(IDesignationRepository designationRepository)
        {
            _DesignationRepository = designationRepository;
        }

        [HttpGet]
        [Authorize]

        public IActionResult Get()
        {
            var designation = _DesignationRepository.GetDesignation();
            return new OkObjectResult(designation);
        }


        [HttpGet("{id}")]
        [Authorize]

        public IActionResult Get(int id)
        {
            var designation = _DesignationRepository.GetDesignationByID(id);
            return new OkObjectResult(designation);
        }



        [HttpPost]
        [Authorize]

        public IActionResult Post([FromBody] EmployeeDesignation designation)
        {
            using (var scope = new TransactionScope())
            {
                _DesignationRepository.InsertDesignation(designation);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = designation.EmployeeDesignationId }, designation);
            }
        }

       


        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody] EmployeeDesignation designation)
        {
            if (designation != null)
            {
                using (var scope = new TransactionScope())
                {
                    _DesignationRepository.UpdateDesignation(designation);
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
            _DesignationRepository.DeleteDesignation(id);
            return new OkResult();
        }
    }
}
