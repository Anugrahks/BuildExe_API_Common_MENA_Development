using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
    public class BlockController : ControllerBase
    {
        private readonly IBlockRepository _BlockRepository;
        private readonly IUserLogRepository _userLogRepository ;
        private readonly IMdHashValidator _mdHashValidator;
        public BlockController(IBlockRepository BlockRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _BlockRepository = BlockRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var products = await _BlockRepository.Get();
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
                var product = await _BlockRepository.GetByID(id);
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
        public async Task<IActionResult> Get(int companyid, int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
                if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                {
                    try
            {
                var floor = await _BlockRepository.Get(companyid, Branchid);

                return new OkObjectResult(floor);
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
                var floor = await _BlockRepository.Getuser(companyid, Branchid, UserId);

                return new OkObjectResult(floor);
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
        public async Task<IActionResult> Post([FromBody] Block block, [FromHeader] string mdhash, [FromHeader] int User)
        {
                        if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                        {
                            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var val = await _BlockRepository.Insert(block);
                //_userLogRepository.Insert(block.UserId, block.BlockId, "BLOCK", 1);
                scope.Complete();
                    return new OkObjectResult(val); //CreatedAtAction(nameof(Get), new { id = block.BlockId }, block);
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
        public async Task<IActionResult> Put([FromBody] Block block, [FromHeader] string mdhash, [FromHeader] int User)
        {
                            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                            {
                                try
            {
                if (block != null)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled ))
                {
                    var val = await _BlockRepository.Update(block);
                    //_userLogRepository.Insert(block.UserId, block.BlockId, "BLOCK", 2);
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
                    var val = await _BlockRepository.Delete(id, UserId);
                    // _userLogRepository.Insert(UserId, id, "BLOCK", 3);
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
