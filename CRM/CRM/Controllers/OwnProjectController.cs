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
    public class OwnProjectController : ControllerBase
    {
       private readonly IOwnProjectRepository _OwnProjectRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public OwnProjectController(IOwnProjectRepository ownProjectRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _OwnProjectRepository = ownProjectRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var products = await _OwnProjectRepository.Get();
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
                var product = await _OwnProjectRepository.GetByID(id);
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

        [HttpGet("Unit/{id}")]
        [Authorize]
        public async Task<IActionResult> GetUnits(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product = await _OwnProjectRepository.GetUnitByID(id);
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
        public async Task<IActionResult> Post([FromBody] IEnumerable<OwnProject > ownProject, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _OwnProjectRepository.Insert(ownProject);
                scope.Complete();
                return new OkResult();
                //return CreatedAtAction(nameof(Get), new { id = ownProject.Id }, ownProject);
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
        public async Task<IActionResult> Put([FromBody] IEnumerable<OwnProject> ownProject, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (ownProject != null)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                   await _OwnProjectRepository.Update(ownProject);
                    scope.Complete();
                    return new OkResult();
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


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                await _OwnProjectRepository.Delete(id);
            return new OkResult();
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

        #region UnitFilter

        [HttpGet("GeneralInvoice/{projectId}")]
        [Authorize]
        public async Task<IActionResult> GetUnitsForGeneralInvoice(int projectId)
        {
            try
            {
                var Project = await _OwnProjectRepository.GetUnitsForGeneralInvoice(projectId);
                return new OkObjectResult(Project);
            }
            catch (Exception)
            { throw; }
        }

        [HttpGet("StageInvoice/{projectId}")]
        [Authorize]
        public async Task<IActionResult> GetUnitsForStageInvoice(int projectId)
        {
            try
            {
                var Project = await _OwnProjectRepository.GetUnitsForStageInvoice(projectId);
                return new OkObjectResult(Project);
            }
            catch (Exception)
            { throw; }
        }

        [HttpGet("ClientAdvance/{projectId}")]
        [Authorize]
        public async Task<IActionResult> GetUnitsForClientAdvance(int projectId)
        {
            try
            {
                var Project = await _OwnProjectRepository.GetUnitsForClientAdvance(projectId);
                return new OkObjectResult(Project);
            }
            catch (Exception)
            { throw; }
        }

        [HttpGet("StageReceipt/{projectId}")]
        [Authorize]
        public async Task<IActionResult> GetUnitsForStageReceipt(int projectId)
        {
            try
            {
                var Project = await _OwnProjectRepository.GetUnitsForStageReceipt(projectId);
                return new OkObjectResult(Project);
            }
            catch (Exception)
            { throw; }
        }

        #endregion


    }
}
