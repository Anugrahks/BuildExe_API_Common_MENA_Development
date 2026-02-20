using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BuildExeMaterialServices.Models;
using BuildExeMaterialServices.Repository;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using BuildExeMaterialServices.Library;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeMaterialServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndentController : ControllerBase
    {
        private readonly IIndentRepository _indentRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public IndentController(IIndentRepository indentRepository, IUserLogRepository userLogRepository,IMdHashValidator mdHashValidator)
        {
            _indentRepository = indentRepository;
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
                var brand = await _indentRepository.Get(CompanyId, Branchid);
                return new OkObjectResult(brand);
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
        [HttpGet("{ProjectId}/{Blockid}/{Floorid}/{UnitId}")]
        [Authorize]
        public async Task<IActionResult> Get(int ProjectId, int Blockid, int Floorid, int UnitId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var brand = await _indentRepository.Get(ProjectId, Blockid, Floorid, UnitId);
                return new OkObjectResult(brand);
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

        [HttpGet("{ProjectId}/{UnitId}/{Blockid}/{Floorid}/{supplierID}/{WorkCategoryId}/{WorkNameId}")]
        [Authorize]
        public async Task<IActionResult> Get(int ProjectId, int UnitId, int Blockid, int Floorid, int supplierID, int workCategoryId, int workNameId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var brand = await _indentRepository.GetDetailsForworkorder(ProjectId, UnitId, Blockid, Floorid, supplierID, workCategoryId, workNameId);
                return new OkObjectResult(brand);
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

        [HttpGet("ItemIntake/{ProjectId}/{UnitId}/{Blockid}/{Floorid}/{supplierID}/{WorkCategoryId}/{WorkNameId}/{date}")]
        [Authorize]
        public async Task<IActionResult> Getitem(int ProjectId, int UnitId, int Blockid, int Floorid, int supplierID, int workCategoryId, int workNameId, DateTime date, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var brand = await _indentRepository.GetDetailsForitem(ProjectId, UnitId, Blockid, Floorid, supplierID, workCategoryId, workNameId, date);
                return new OkObjectResult(brand);
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

        [HttpGet("WithOutSupplier/{ProjectId}/{UnitId}/{Blockid}/{Floorid}")]
        [Authorize]
        public async Task<IActionResult> GetWithOutSupplier(int ProjectId, int UnitId, int Blockid, int Floorid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var brand = await _indentRepository.GetWithOutSupplier(ProjectId, UnitId, Blockid, Floorid);
                return new OkObjectResult(brand);
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

        [HttpGet("{ProjectId}/{UnitId}/{Blockid}/{Floorid}/{supplierID}/{MaterialType}")]
        [Authorize]
        public async Task<IActionResult> Get(int ProjectId, int UnitId, int Blockid, int Floorid, int supplierID, int MaterialType, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var brand = await _indentRepository.GetDetailsForworkorder(ProjectId, UnitId, Blockid, Floorid, supplierID, MaterialType);
                return new OkObjectResult(brand);
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
                var brand = await _indentRepository.GetByID(id);

                return new OkObjectResult(brand);
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
                        var val = await _indentRepository.Update(indent);
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
                var bb = await _indentRepository.Delete(id, UserID);
                return new OkObjectResult(bb);
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

        [HttpPost("IndentDetails")]
        [Authorize]
        public async Task<IActionResult> PostIndentDetails([FromBody] IndentPurchase indentPurchase, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    if (indentPurchase != null)
                    {
                        var details = await _indentRepository.PostIndentDetails(indentPurchase);
                        return new OkObjectResult(details);
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
    }
}
