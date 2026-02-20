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
    public class LabourWorkRateController : ControllerBase
    {
        private readonly ILabourWorkRateRepository _labourWorkRateRepository  ;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public LabourWorkRateController(ILabourWorkRateRepository labourWorkRateRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _labourWorkRateRepository = labourWorkRateRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpGet("{companyid}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> Get(int Companyid,int branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var designation = await _labourWorkRateRepository.Getworkrate(Companyid, branchid);
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


        [HttpGet("getuser/{companyid}/{BranchId}/{UserId}")]
        [Authorize]

        public async Task<IActionResult> Getbyuser(int Companyid, int branchid, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var designation = await _labourWorkRateRepository.Getworkratebyuser(Companyid, branchid, UserId);
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

        [HttpGet("{companyid}/{BranchId}/{specitemTypeid}")]
        [Authorize]
        public async Task<IActionResult> Get(int Companyid, int branchid,int specitemTypeid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var designation =await _labourWorkRateRepository.Get(Companyid, branchid, specitemTypeid);
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
                var department =await _labourWorkRateRepository.GetByID(id);
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
        public async Task<IActionResult> Post([FromBody] LabourWorkRate  labourWorkRate, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
               var val = await _labourWorkRateRepository.Insert(labourWorkRate);
               // _userLogRepository.Insert(labourWorkRate.UserId, labourWorkRate.Id, "LABOUR WORK RATE", 1);
                scope.Complete();
                    return new OkObjectResult(val); //CreatedAtAction(nameof(Get), new { id = labourWorkRate.Id }, labourWorkRate);
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
        public async Task<IActionResult> Put([FromBody] LabourWorkRate labourWorkRate, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (labourWorkRate != null)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                   var val = await _labourWorkRateRepository.Update(labourWorkRate);
                   // _userLogRepository.Insert(labourWorkRate.UserId, labourWorkRate.Id, "LABOUR WORK RATE",2);
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
                var vaal = await _labourWorkRateRepository.Delete(id, UserId);
                return new OkObjectResult(vaal);
                // _userLogRepository.Insert(UserId, id, "LABOUR WORK RATE", 3);
               // return new OkResult();
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
                var result = await _labourWorkRateRepository.CheckEditDelete(id);
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
