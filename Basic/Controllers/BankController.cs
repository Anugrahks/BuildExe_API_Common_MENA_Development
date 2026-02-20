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
    public class BankController : ControllerBase
    {
        private readonly IBankRepository _bankRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public BankController(IBankRepository bankRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _bankRepository = bankRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpGet("{CompanyId}/{Branchid}")]
        [Authorize]
        public async Task<IActionResult> Get(int CompanyId, int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var designation =await  _bankRepository.Get(CompanyId, Branchid);
                return new OkObjectResult(designation);
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


        [HttpGet("getwithfinancial/{CompanyId}/{Branchid}/{financialYearId}")]
        [Authorize]
        public async Task<IActionResult> GetwithFin(int CompanyId, int Branchid, int financialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var designation = await _bankRepository.getwithfinancial(CompanyId, Branchid, financialYearId);
                return new OkObjectResult(designation);
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
                var department = await _bankRepository.GetByID(id);
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



        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] Bank bank, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try { 
            using (var scope = new TransactionScope( TransactionScopeAsyncFlowOption.Enabled ))
            {
               var val =await _bankRepository.Insert(bank);
                scope.Complete();
                return new OkObjectResult(val);
                // return CreatedAtAction(nameof(Get), new { Id = bank.Id  }, bank);
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
        public async Task<IActionResult> Put([FromBody] Bank  bank, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (bank != null)
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled ))
                    {
                        var val = await _bankRepository.Update(bank);
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
        public async Task<IActionResult> Delete(int id, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var val = await _bankRepository.Delete(id, UserId);
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

        [HttpGet("EditDelete/{id}")]
        [Authorize]
        public async Task<IActionResult> EditDelete(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _bankRepository.CheckEditDelete(id);
                return new OkObjectResult(result);
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
