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
using System.Security.Cryptography;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeMaterialServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryOrderController : ControllerBase
    {
        private readonly IDeliveryOrderRepository _salesOrderRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;

        public DeliveryOrderController(IDeliveryOrderRepository salesOrderRepository, IPurchaseOrderRepository purchaseOrderRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _salesOrderRepository = salesOrderRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
            _purchaseOrderRepository = purchaseOrderRepository;
        }



        [HttpGet("getEdit/{CompanyId}/{BranchId}/{UserId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> Getedit(int CompanyId, int Branchid, int UserId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var brand = await _salesOrderRepository.Getedit(CompanyId, Branchid, UserId, FinancialYearId);
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





        //[HttpGet("getEdit/{CompanyId}/{BranchId}/{UserId}/{FinancialYearId}/{IsAsset}")]
        //[Authorize]
        //public async Task<IActionResult> Getedit(int CompanyId, int Branchid, int UserId, int FinancialYearId,int IsAsset, [FromHeader] string mdhash, [FromHeader] int User)
        //{
        //    if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
        //    {
        //        try
        //        {
        //            var brand = await _salesOrderRepository.Getedit(CompanyId, Branchid, UserId, FinancialYearId, IsAsset);
        //            return new OkObjectResult(brand);
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

        [HttpGet("getbyId/{Id}")]
        [Authorize]
        public async Task<IActionResult> getbyId(int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var brand = await _salesOrderRepository.GetById(Id);
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



        [HttpGet("getApproval/{CompanyId}/{BranchId}/{UserId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> getApproval(int CompanyId, int Branchid, int UserId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var brand = await _salesOrderRepository.GetApproval(CompanyId, Branchid, UserId, FinancialYearId);
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

        //[HttpGet("getApproval/{CompanyId}/{BranchId}/{UserId}/{FinancialYearId}/{IsAsset}")]
        //[Authorize]
        //public async Task<IActionResult> getApproval(int CompanyId, int Branchid, int UserId, int FinancialYearId, int IsAsset, [FromHeader] string mdhash, [FromHeader] int User)
        //{
        //    if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
        //    {
        //        try
        //        {
        //            var brand = await _salesOrderRepository.GetApproval(CompanyId, Branchid, UserId, FinancialYearId, IsAsset);
        //            return new OkObjectResult(brand);
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

        [HttpGet("DeliveryOrderForPurchase/{ProjectId}/{UnitId}/{BlockId}/{FloorId}/{SupplierId}/{OrderCategoryId}/{WorkNameId}/{PurchaseDate}/{MaterialTypeId}/{Id}")]
        [Authorize]
        public async Task<IActionResult> DeliveryOrder(int ProjectId, int UnitId, int BlockId, int FloorId,
        int SupplierId, int OrderCategoryId, int WorkNameId, DateTime PurchaseDate, int MaterialTypeId, int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var material = await _purchaseOrderRepository.DeliveryOrderForPurchase(ProjectId, UnitId, BlockId, FloorId, SupplierId, OrderCategoryId, WorkNameId, PurchaseDate, MaterialTypeId, Id);
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
       public async Task<IActionResult> Post([FromBody] IEnumerable<DeliveryOrderMaster> mat, [FromHeader] string mdhash, [FromHeader] int User)
       {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var val = await _salesOrderRepository.Insert(mat);
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

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] IEnumerable<DeliveryOrderMaster> mat, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var val = await _salesOrderRepository.Update(mat);
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

        [HttpPost("DeliveryOrderReport")]
        [Authorize]
        public async Task<IActionResult> DeliveryOrderReportReport([FromBody] MaterialSearch materialSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    if (materialSearch != null)
                    {
                        var deliveryOrder = await _salesOrderRepository.GetDeliveryOrderReport(materialSearch);
                        return new OkObjectResult(deliveryOrder);
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
                    var bb = await _salesOrderRepository.Delete(id, UserID);
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
    }
}
