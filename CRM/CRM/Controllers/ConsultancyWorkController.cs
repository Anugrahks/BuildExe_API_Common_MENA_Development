using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using BuildExeServices.Repository;
using BuildExeServices.Models;
using Microsoft.AspNetCore.Authorization;
using BuildExeServices.Library;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultancyWorkController : ControllerBase
    {
        private readonly IConsultancyWorkRepository _consultancyWorkRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public ConsultancyWorkController(IConsultancyWorkRepository consultancyWorkRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _consultancyWorkRepository = consultancyWorkRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var products = await _consultancyWorkRepository.Get();
            return new OkObjectResult(products);
            }
            catch (Exception)
            { throw; }
        }


        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product = await _consultancyWorkRepository.GetByID(id);
                return new OkObjectResult(product);
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
        [HttpGet("{companyid}/{Branchid}")]
        [Authorize]
        public async Task<IActionResult> Get(int companyid,int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product = await _consultancyWorkRepository.Getdetails(companyid, Branchid);
                return new OkObjectResult(product);
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

        [HttpGet("getuser/{companyid}/{Branchid}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> Getbyid(int companyid, int Branchid, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product = await _consultancyWorkRepository.Getdetailsuser(companyid, Branchid, UserId);
                return new OkObjectResult(product);
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
        public async Task<IActionResult> Post([FromBody] ConsultancyWork consultancyWork, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
               var val = await  _consultancyWorkRepository.Insert(consultancyWork);
              // _userLogRepository.Insert(consultancyWork.UserId, consultancyWork.Id, "CONSULTANCY WORK", 1);
                scope.Complete();
                    return new OkObjectResult(val); //CreatedAtAction(nameof(Get), new { id = consultancyWork.Id }, consultancyWork);
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
        public async Task<IActionResult> Put([FromBody] ConsultancyWork consultancyWork, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (consultancyWork != null)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                   var val = await  _consultancyWorkRepository.Update(consultancyWork);
                    //_userLogRepository.Insert(consultancyWork.UserId, consultancyWork.Id, "CONSULTANCY WORK", 2);
                    scope.Complete();
                    return new OkObjectResult(val);
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
        public async Task<IActionResult> Delete(int id,int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
              var val =await  _consultancyWorkRepository.Delete(id, UserId);
          //  _userLogRepository.Insert(UserId, id, "CONSULTANCY WORK", 3);
            return new OkObjectResult(val);
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
