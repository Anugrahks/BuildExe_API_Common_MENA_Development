using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
using BuildExeServices.Repository;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using BuildExeServices.Library;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartBillController : ControllerBase
    {
        private readonly IPartBillRepository _partBillRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public PartBillController(IPartBillRepository partBillRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _partBillRepository = partBillRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;

        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var products = await _partBillRepository.Get();
            return new OkObjectResult(products);
            }
            catch (Exception)
            { throw; }

        }
        [HttpGet("{companyid}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> Get(int companyid, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product = await _partBillRepository.Get(companyid, BranchId);
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

        [HttpGet("LastBill/{ProjectId}/{UnitId}/{BlockId}/{FloorId}")]
        [Authorize]
        public async Task<IActionResult> GetLastBill(int ProjectId, int UnitId, int BlockId, int FloorId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product = await _partBillRepository.GetLastBill(ProjectId, UnitId, BlockId, FloorId);
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

        [HttpGet("{ProjectId}/{UnitId}/{BlockId}/{FloorId}")]
        [Authorize]
        public async Task<IActionResult> Get(int ProjectId, int UnitId, int BlockId, int FloorId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product =await _partBillRepository.GetBillDetailsBasedOnProject (ProjectId, UnitId, BlockId, FloorId);
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
        [HttpGet("{ProjectId}/{UnitId}/{BlockId}/{FloorId}/{Id}")]
        [Authorize]
        public async Task<IActionResult> Get(int ProjectId, int UnitId, int BlockId, int FloorId,int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product = await _partBillRepository.GetBillDetailsBasedOnProject(ProjectId, UnitId, BlockId, FloorId, Id);
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

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product = await _partBillRepository.GetbyID(id);
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
        //        public async Task<IActionResult> Post( [FromBody] PartBillMaster partBillMaster,   [FromHeader] string mdhash,   [FromHeader] int User)
        public async Task<IActionResult> Post([FromBody] List<PartBillMaster> partBillMasters, [FromHeader] string mdhash, [FromHeader] int User)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var results = new List<object>();

                    foreach (var partBillMaster in partBillMasters)
                    {
                        // Call your repository for each bill
                        var val = await _partBillRepository.Insert(partBillMaster);
                        //results.Add(val);
                        results.AddRange(val);
                    }

                    scope.Complete();
                    return new OkObjectResult(results);
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

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] IEnumerable<PartBillMaster> partBillMasters, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (partBillMasters != null)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                   var val =await _partBillRepository.Update(partBillMasters);
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
               var val= await _partBillRepository.Delete(id, UserId);

                return new OkObjectResult (val);
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

        [HttpGet("PartBillValidation/{ProjectId}")]
        [Authorize]
        public async Task<IActionResult> GetStatus(int ProjectId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var val = await _partBillRepository.GetByProjectId(ProjectId);
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



            
        [HttpGet("get_bill_history/{companyid}/{BranchId}/{ProjectId}/{DivisionId}/{UserId}/{FinancialYearId}/{Id}")]
       // [Authorize]
        public async Task<IActionResult> GetbyInvoicedItem(int companyid, int BranchId,int ProjectId, int DivisionId, int UserId, int FinancialYearId,int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
           // if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var product = await _partBillRepository.GetbyInvoicedItem(companyid, BranchId, ProjectId,DivisionId, UserId, FinancialYearId, Id);
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
            //else
            //{
            //    return Unauthorized("Invalid MdHash");
            //}
        }
    }
}
