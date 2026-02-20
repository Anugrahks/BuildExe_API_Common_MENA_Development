using BuildExeBasic.Interfaces;
using BuildExeBasic.Library;
using BuildExeBasic.Models;
using BuildExeBasic.Models.DTO;
using BuildExeBasic.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using System.Transactions;

namespace BuildExeBasic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchController : ControllerBase
    {
        private readonly IBatchRepository _batchRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public BatchController(IBatchRepository batchRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _batchRepository = batchRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody]Batch batch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    if (batch == null)
                    {
                        return BadRequest("Batch payload is null");
                    }
                    if (!ModelState.IsValid)
                        return BadRequest(ModelState);
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var result = await _batchRepository.AddBatchAsync(batch);
                    scope.Complete();
                    return new OkObjectResult(result);
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

        [HttpGet("preview-batch-number/{CompanyId}/{Branchid}/{sitemanagerId}")]
        [Authorize]
        public async Task<IActionResult> PreviewBatchNumber(int companyId, int branchid, int sitemanagerId, [FromHeader] string mdhash, [FromHeader] int User)
        {

            try
            {
                if (sitemanagerId <= 0)
                {
                    return BadRequest("Invalid SitemanagerId");
                }


                var batchNo = await _batchRepository.GenerateBatchNumberAsync(
                    companyId,
                    branchid,
                    sitemanagerId, false);

                return Ok(new { BatchNo = batchNo });
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


        [HttpGet("preview-batch-number/{CompanyId}/{Branchid}/{sitemanagerId}/{BatchValidate}")]
        [Authorize]
        public async Task<IActionResult> PreviewBatchNumber(int companyId, int branchid, int sitemanagerId,int batchValidate,[FromHeader] string mdhash, [FromHeader] int User)
        {

            try
            {
                if (sitemanagerId <= 0)
                {
                    return BadRequest("Invalid SitemanagerId");
                }

                Boolean IsValidate = Convert.ToBoolean(batchValidate);
                var batchNo = await _batchRepository.GenerateBatchNumberAsync(
                    companyId,
                    branchid,
                    sitemanagerId, IsValidate);

                return Ok(new { BatchNo = batchNo });
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

        

        [HttpGet("all/{companyId:int}/{branchId:int}")]
        [Authorize]
        public async Task<IActionResult> GetAll(int CompanyId, int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
           
                try
                {
                    var batches = await _batchRepository.GetAllBatchesAsync(CompanyId, Branchid);
                    return new OkObjectResult(batches);
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
        [HttpGet("by-id/{id:long}")]
        [Authorize]
        public async Task<IActionResult> GetById(long id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var batch = await _batchRepository.GetBatchByIdAsync(id);
                    if (batch == null)
                        return NotFound(new { Message = "Batch not found" });

                    return Ok(batch);
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

        [HttpPut("close/{id}")]
        [Authorize]
        public async Task<IActionResult> CloseBatch(long id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var result = await _batchRepository.UpdateCloseStateAsync(id);

                //if (!result)
                //    return NotFound(new { Message = "Batch not found" });

                //return Ok(new { Message = "Batch closed successfully" });

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

        //To get all open Batchno aginst sitemanager
        [HttpGet("GetBatchBySiteManager/{sitemanagerId}/{companyId:int}/{branchId:int}")]
        [Authorize]
        public async Task<IActionResult> GetBatchBySiteManager(int siteManagerId, int CompanyId, int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
           
                try
                {
                    var batches = await _batchRepository
       .GetBatchBySiteManagerAsync(siteManagerId);

                    return new OkObjectResult(batches);
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
        //To get all (open & close) Batchno aginst sitemanager
        [HttpGet("by-site-manager/{siteManagerId}")]
        public async Task<IActionResult> GetAllBatchesBySiteManager(int siteManagerId)
        {
            try
            {
                var batches = await _batchRepository.GetAllBatchesBySiteManagerAsync(siteManagerId);
                return new OkObjectResult(batches);
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
    }
}
