using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
using System.Transactions;
using BuildExeHR.Repository;
using Microsoft.AspNetCore.Authorization;
using BuildExeHR.Library;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndentController : ControllerBase
    {
        private readonly IIndentRepository _indentRepository ;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public IndentController(IIndentRepository indentRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            
            _indentRepository = indentRepository;

            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }
        [HttpGet("{IndentId}")]
        [Authorize]
        public async Task<IActionResult> Get(int IndentId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _indentRepository.GetDetailsbyid(IndentId);
            return new OkObjectResult(purchase);
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

        [HttpGet("{CompanyId}/{Branchid}")]
        [Authorize]
        public async Task<IActionResult> Get(int CompanyId, int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase =await _indentRepository.GetforEdit(CompanyId, Branchid);
            return new OkObjectResult(purchase);
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


        [HttpGet("getuser/{CompanyId}/{Branchid}/{UserId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> Getbyuser(int CompanyId, int Branchid, int userId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _indentRepository.GetforEdituser(CompanyId, Branchid, userId, FinancialYearId);
                return new OkObjectResult(purchase);
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

        [HttpGet("{CompanyId}/{Branchid}/{UserID}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> Get(int CompanyId, int Branchid, int UserID, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase =await _indentRepository.GetforApproval(CompanyId, Branchid, UserID, FinancialYearId);
            return new OkObjectResult(purchase);
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
        [HttpGet("{projectId}/{Unitid}/{blockid}/{floorid}/{subcontractoId}")]
        [Authorize]
        public async Task<IActionResult> Get(int projectId, int Unitid, int blockid, int floorid, int subcontractoId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase =await _indentRepository.GetDetailsForworkorder(projectId, Unitid, blockid, floorid, subcontractoId,0);
            return new OkObjectResult(purchase);
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
        [HttpGet("{projectId}/{Unitid}/{blockid}/{floorid}/{subcontractoId}/{workcategoryId}/{workNameId}")]
        [Authorize]
        public async Task<IActionResult> Gets(int projectId, int Unitid, int blockid, int floorid, int subcontractoId, int workcategoryId, int workNameId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var details = await _indentRepository.GetDetails(projectId, Unitid, blockid, floorid, subcontractoId, workcategoryId, workNameId);
                return new OkObjectResult(details);
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

        [HttpGet("{projectId}/{Unitid}/{blockid}/{floorid}/{subcontractoId}/{workorderid}")]
        [Authorize]
        public async Task<IActionResult> Get(int projectId, int Unitid, int blockid, int floorid, int subcontractoId,int workorderid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _indentRepository.GetDetailsForworkorder(projectId, Unitid, blockid, floorid, subcontractoId, workorderid);
                return new OkObjectResult(purchase);
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
        public async Task<IActionResult> Post([FromBody] IEnumerable<Indent> indent, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {

                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
               var val = await _indentRepository.Insert(indent);

                scope.Complete();
                return new OkObjectResult(val);

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
        public async Task<IActionResult> Put([FromBody] IEnumerable<Indent> indent, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (indent != null)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                        var val = await  _indentRepository.Update(indent);
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


        [HttpDelete("{id}/{UserID}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id, int UserID, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var vv = await _indentRepository.Delete(id, UserID);
                return new OkObjectResult(vv);
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
