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
    public class PurchaseOrderListController : ControllerBase
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository  ;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public PurchaseOrderListController(IPurchaseOrderRepository purchaseOrderRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
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
                var purchase =await _purchaseOrderRepository.GetDetailsbyid(IndentId);
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
        [HttpGet("ForPurchase/{Id}")]
        [Authorize]
        public async Task<IActionResult> GetpendingOrder(int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _purchaseOrderRepository.GetpendingpurchaseOrderdetails(Id);
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
                var purchase =await _purchaseOrderRepository.GetforEdit(CompanyId, Branchid);
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

        [HttpGet("GetUser/{CompanyId}/{Branchid}/{UserId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> GetByUser(int CompanyId, int Branchid, int UserId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var purchase = await _purchaseOrderRepository.GetforEdit(CompanyId, Branchid, UserId, FinancialYearId,0);
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


        [HttpGet("GetUser/{CompanyId}/{Branchid}/{UserId}/{FinancialYearId}/{IsAsset}")]
        [Authorize]
        public async Task<IActionResult> GetByUser(int CompanyId, int Branchid, int UserId, int FinancialYearId, int IsAsset, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _purchaseOrderRepository.GetforEdit(CompanyId, Branchid, UserId, FinancialYearId, IsAsset);
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
                var purchase = await _purchaseOrderRepository.GetforApproval(CompanyId, Branchid, UserID, FinancialYearId,0);
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


        [HttpGet("GetApproval/{CompanyId}/{Branchid}/{UserID}/{FinancialYearId}/{IsAsset}")]
        [Authorize]
        public async Task<IActionResult> Get(int CompanyId, int Branchid, int UserID, int FinancialYearId,int IsAsset, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _purchaseOrderRepository.GetforApproval(CompanyId, Branchid, UserID, FinancialYearId, IsAsset);
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

        [HttpGet("{ProjectId}/{UnitId}/{BlockId}/{FloorId}/{SupplierId}/{OrderCategoryId}/{WorkNameId}/{PurchaseDate}")]
        [Authorize]
        public async Task<IActionResult> Combo(int ProjectId, int UnitId, int BlockId, int FloorId, 
            int SupplierId, int OrderCategoryId, int WorkNameId, DateTime PurchaseDate, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var material = await _purchaseOrderRepository.GetpendingpurchaseOrder(ProjectId, UnitId, BlockId, FloorId, SupplierId, OrderCategoryId, WorkNameId, PurchaseDate);
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


        [HttpGet("{ProjectId}/{UnitId}/{BlockId}/{FloorId}/{SupplierId}/{OrderCategoryId}/{WorkNameId}/{PurchaseDate}/{MaterialTypeId}")]
        [Authorize]
        public async Task<IActionResult> Combo(int ProjectId, int UnitId, int BlockId, int FloorId,
    int SupplierId, int OrderCategoryId, int WorkNameId, DateTime PurchaseDate, int MaterialTypeId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var material = await _purchaseOrderRepository.GetpendingpurchaseOrder(ProjectId, UnitId, BlockId, FloorId, SupplierId, OrderCategoryId, WorkNameId, PurchaseDate, MaterialTypeId);
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

        [HttpGet("PurchaseOrderForDelivery/{ProjectId}/{UnitId}/{BlockId}/{FloorId}/{SupplierId}/{OrderCategoryId}/{WorkNameId}/{PurchaseDate}/{MaterialTypeId}/{Id}")]
        [Authorize]
        public async Task<IActionResult> DeliveryOrder(int ProjectId, int UnitId, int BlockId, int FloorId,
   int SupplierId, int OrderCategoryId, int WorkNameId, DateTime PurchaseDate, int MaterialTypeId,int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var material = await _purchaseOrderRepository.DeliveryOrder(ProjectId, UnitId, BlockId, FloorId, SupplierId, OrderCategoryId, WorkNameId, PurchaseDate, MaterialTypeId,Id);
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
        public async Task<IActionResult> Post([FromBody] MaterialSearch materialSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (materialSearch != null)
            {
                var product =await _purchaseOrderRepository.GetReport (materialSearch);
                return new OkObjectResult(product);
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
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] MaterialSearch materialSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (materialSearch != null)
                {
                    var product = await _purchaseOrderRepository.Getforview(materialSearch);
                    return new OkObjectResult(product);
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


        [HttpGet("GetPONo/{CompanyId}/{Branchid}")]
        [Authorize]
        public async Task<IActionResult> GetPONo(int CompanyId, int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _purchaseOrderRepository.GetPONo(CompanyId, Branchid);
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
