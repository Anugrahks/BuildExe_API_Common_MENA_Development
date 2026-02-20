using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
using System.Transactions;
using BuildExeMaterialServices.Repository;
using Microsoft.AspNetCore.Authorization;
using BuildExeMaterialServices.Library;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeMaterialServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DamageStockController : ControllerBase
    {
        private readonly IDamageStockRepository _damageStockRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public DamageStockController(IDamageStockRepository damageStockRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _damageStockRepository = damageStockRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpGet("{CompanyId}/{Branchid}")]
        [Authorize]
        public async Task <IActionResult> Get(int CompanyId, int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _damageStockRepository.Get(CompanyId, Branchid);
                return new OkObjectResult(Project);
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

        // GET api/<ProjectController>/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project =await _damageStockRepository.GetByID(id);
            return new OkObjectResult(Project);
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
        public async Task<IActionResult> Post([FromBody] DamageStock damageStock, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                //projectmaster.GovtProj.GovtProjId = 100;
                try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var val =await _damageStockRepository.Insert(damageStock);
                    scope.Complete();
                    return new OkObjectResult(val);
                    //  return CreatedAtAction(nameof(Get), new { id = damageStock.Id }, damageStock);
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

        [HttpPost("Report")]
        [Authorize]
        public async Task<IActionResult> PostReport([FromBody] DamageStock damageStock, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (damageStock != null)
                {
                    var product = await _damageStockRepository.GetforReport(damageStock);
                    return new OkObjectResult(product);
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


        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] DamageStock damageStock, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (damageStock != null)
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var val =await _damageStockRepository.Update(damageStock);
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

        // DELETE api/<ProjectController>/5
        [HttpDelete("{id}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
               var val = await _damageStockRepository.Delete(id, UserId);
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
