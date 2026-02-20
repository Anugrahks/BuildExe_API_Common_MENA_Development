using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Repository;
using BuildExeMaterialServices.Models;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using BuildExeMaterialServices.Library;

namespace BuildExeMaterialServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinishedGoodsController : ControllerBase
    {
        private readonly IFinishedGoodsRepository _finishedGoodsRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public FinishedGoodsController(IFinishedGoodsRepository finishedGoodsRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _finishedGoodsRepository = finishedGoodsRepository;
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
                var purchase = await _finishedGoodsRepository.Get(CompanyId, Branchid);
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



        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var material = await _finishedGoodsRepository.GetbyID(id);

                return new OkObjectResult(material);
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
        public async Task<IActionResult> Post([FromBody] IEnumerable<FinishedGoods> finishedGoods, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var val = await _finishedGoodsRepository.Insert(finishedGoods);
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
        public async Task<IActionResult> Put([FromBody] IEnumerable<FinishedGoods> finishedGoods, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (finishedGoods != null)
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var supplir = await _finishedGoodsRepository.Update(finishedGoods);
                        scope.Complete();
                        return new OkObjectResult(supplir);
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

        //[HttpPost(" Aswin")]
        //[Authorize]
        //public async Task<IActionResult> PostData([FromBody] IEnumerable<MaterialIssue> issue, [FromHeader] string mdhash, [FromHeader] int User)
        //{
        //    if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
        //    {
        //        try
        //        {
        //            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        //            {
        //                var val = await _finishedGoodsRepository.PostData(issue);
        //                scope.Complete();

        //                return new OkObjectResult(val);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return StatusCode(500, new
        //            {
        //                message = $"An error occurred: {ex.Message}",
        //                statusCode = 0
        //            });
        //        }
        //    }
        //    else
        //    {
        //        return Unauthorized("Invalid MdHash");
        //    }
        //}

    }
}
