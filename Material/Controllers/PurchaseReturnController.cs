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
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeMaterialServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseReturnController : ControllerBase
    {
        private readonly IPurchaseReturnRepository _purchaseReturnRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public PurchaseReturnController(IPurchaseReturnRepository purchaseReturnRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _purchaseReturnRepository = purchaseReturnRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        // GET: api/<TeamController>
        [HttpGet("{CompanyId}/{Branchid}")]
        [Authorize]
        public async Task<IActionResult> Get(int CompanyId, int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _purchaseReturnRepository.Get( CompanyId,Branchid);
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

        // GET api/<TeamController>/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase =await _purchaseReturnRepository.GetbyID(id);

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

        // POST api/<TeamController>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] IEnumerable<PurchaseReturn> purchaseReturn, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
              var te=  await _purchaseReturnRepository.Insert(purchaseReturn);
                scope.Complete();

                return new OkObjectResult(te);
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
        public async Task<IActionResult> Put([FromBody] IEnumerable<PurchaseReturn> purchaseReturn, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (purchaseReturn != null)
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var te = await _purchaseReturnRepository.Update(purchaseReturn);
                        scope.Complete();
                        return new OkObjectResult(te);
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


        [HttpDelete("{id}/{userid}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id,int userid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
               await _purchaseReturnRepository.Delete(id, userid);
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

        [HttpGet("GenerateNextDebitNoteNo/{BranchId}/{FinancialYearId}")]
        [Authorize]
        //[Route("GenerateNextEmpNo")]
        public IActionResult GenerateNextEmpNo(int BranchId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
           
                try
            {
                var material = _purchaseReturnRepository.GenerateNextDebitNoteNo(BranchId, FinancialYearId);
                return new OkObjectResult(material);
            }
            catch (Exception)
            { throw; }

        }




    [HttpGet("PurchaseBillInPurchaseReturn/{SupplierId}/{FinancialYearId}")]
    [Authorize]
    public async Task<IActionResult> PurchaseBillInPurchaseReturn(int SupplierId,int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
    {
        if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
        {
            try
            {
                var purchase = await _purchaseReturnRepository.PurchaseBillInPurchaseReturn(SupplierId, FinancialYearId);

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


        [HttpGet("PurchaseBillInPurchaseReturn/{SupplierId}/{FinancialYearId}/{ProjectId}")]
        [Authorize]
        public async Task<IActionResult> PurchaseBillInPurchaseReturn(int SupplierId, int FinancialYearId,int ProjectId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _purchaseReturnRepository.PurchaseBillInPurchaseReturn(SupplierId, FinancialYearId, ProjectId);

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


        [HttpGet("PurchaseBillInPurchase/{SupplierId}/{FinancialYearId}")]
    [Authorize]
    public async Task<IActionResult> PurchaseBillInPurchase(int SupplierId,int FinancialYearId,  [FromHeader] string mdhash, [FromHeader] int User)
    {
        if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
        {
            try
            {
                var purchase = await _purchaseReturnRepository.PurchaseBillInPurchase(SupplierId, FinancialYearId);

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



    [HttpGet("PurchaseReturnBillDetails/{id}")]
    [Authorize]
    public async Task<IActionResult> PurchaseReturnBillDetails(int id, [FromHeader] string mdhash, [FromHeader] int User)
    {
        if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
        {
            try
            {
                var purchase = await _purchaseReturnRepository.PurchaseReturnBillDetails(id);

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


    [HttpGet("PurchaseBillDetails/{id}")]
    [Authorize]
    public async Task<IActionResult> PurchaseBillDetails(int id, [FromHeader] string mdhash, [FromHeader] int User)
    {
        if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
        {
            try
            {
                var purchase = await _purchaseReturnRepository.PurchaseBillDetails(id);

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
    }
}
