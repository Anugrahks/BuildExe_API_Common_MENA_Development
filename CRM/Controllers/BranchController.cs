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
    public class BranchController : ControllerBase
    {
        private readonly IBranchRepository _branchRepository;
        public BranchController(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var branch= _branchRepository.GetBranch ();
            return new OkObjectResult(branch);
            
        }


        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id)
        {
           var branch = _branchRepository.GetBranchByID (id);
           
            return new OkObjectResult(branch);
        }


        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] Branch branch)
        {
            using (var scope = new TransactionScope())
            {
                _branchRepository.InsertBranch(branch);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = branch.CompanyId }, branch);
            }
        }


        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody] Branch branch)
        {
            if (branch != null)
            {
                using (var scope = new TransactionScope())
                {
                    _branchRepository.UpdateBranch(branch);
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
            _branchRepository.DeleteBranch(id);
            return new OkResult();
        }
    }
}
