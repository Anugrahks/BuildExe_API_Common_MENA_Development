using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BuildExeServices.Repository;
using BuildExeServices.Models;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using BuildExeServices.Library;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FloorController : ControllerBase
    {
        private readonly IFloorRepository _FloorRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public FloorController(IFloorRepository floorRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _FloorRepository = floorRepository;
            
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpGet]
        [Authorize]
        public async Task <IActionResult> Get()
        {
            try
            {
                var floor =  await _FloorRepository.Get ();
            return new OkObjectResult(floor);
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
                var floor  = await _FloorRepository.GetByID(id);

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

        [HttpGet("{companyid}/{Branchid}")]
        [Authorize]
        public async Task<IActionResult> Get(int companyid,int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var floor = await _FloorRepository.Get(companyid,Branchid);

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
                var floor = await _FloorRepository.Getuser(companyid, Branchid, UserId);

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
        public async Task<IActionResult> Post([FromBody] Floor  floor, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                    var val = await _FloorRepository.Insert(floor);
               // _userLogRepository.Insert(floor.UserId, floor.FloorId , "FLOOR", 1);
                scope.Complete();
                    return new OkObjectResult(val); //CreatedAtAction(nameof(Get), new { id = floor.FloorId }, floor);
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
        public async Task<IActionResult> Put([FromBody] Floor floor, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (floor != null)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled ))
                {
                   var val = await _FloorRepository.Update(floor);
                   // _userLogRepository.Insert(floor.UserId, floor.FloorId, "FLOOR", 2);
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
                var val =await _FloorRepository.Delete(id, UserId);
                // _userLogRepository.Insert(UserId,id, "FLOOR", 3);
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

        [HttpGet("EditDelete/{id}/{projectId}/{blockId}/{floorId}")]
        [Authorize]

        public async Task<IActionResult> EditDelete(int id, int projectId, int blockId, int floorId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _FloorRepository.CheckEditDelete(id, projectId, blockId, floorId);
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
