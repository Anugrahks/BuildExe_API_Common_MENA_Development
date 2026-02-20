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
    public class MaterialListController : ControllerBase
    {

        private readonly IMaterialListRepository _materialListRepository  ;
        private readonly IMaterialRepository _materialRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        private readonly IMaterialListByCategoryRepository _materialListByCategoryRepository;
        public MaterialListController(IMaterialListRepository materialListRepository, IMaterialRepository materialRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator, IMaterialListByCategoryRepository materialListByCategoryRepository)
        {
            _materialListRepository = materialListRepository;
            _materialRepository = materialRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
            _materialListByCategoryRepository = materialListByCategoryRepository;
        }

        [HttpGet("{CompanyId}/{Branchid}")]
        [Authorize]
        public async Task<IActionResult> Get(int CompanyId, int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var material = await _materialListRepository.Get(CompanyId, Branchid);
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

        [HttpGet("getstock/{CompanyId}/{Branchid}")]
        [Authorize]
        public async Task<IActionResult> GetStock(int CompanyId, int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var material = await _materialListRepository.Getstock(CompanyId, Branchid);
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
        [HttpGet("GetUser/{CompanyId}/{Branchid}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> GetByUser(int CompanyId, int Branchid, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var det = await _materialListRepository.GetByUser(CompanyId, Branchid, UserId);
                return new OkObjectResult(det);
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


        [HttpGet("{MaterialId}")]
        [Authorize]
        public async Task<IActionResult> Get(int MaterialId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var material =await _materialListRepository.GetMaterial (MaterialId);
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

        [HttpGet("{CompanyId}/{Branchid}/{Materialtypeid}")]
        [Authorize]
        public async Task<IActionResult> Get(int CompanyId, int Branchid, int Materialtypeid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var material =await _materialListRepository.Get(CompanyId, Branchid, Materialtypeid);
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

        [HttpGet("ByProject/{CompanyId}/{Branchid}/{Materialtypeid}/{ProjectId}/{UnitId}/{Blockid}/{Floorid}")]
        [Authorize]
        public async Task<IActionResult> GetByProjectId(int CompanyId, int Branchid, int Materialtypeid, int ProjectId, int UnitId, int Blockid, int Floorid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var material = await _materialListRepository.GetByProjectId(CompanyId, Branchid, Materialtypeid, ProjectId, UnitId, Blockid, Floorid);
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


        [HttpGet("ByProjectWithStock/{CompanyId}/{Branchid}/{Materialtypeid}/{ProjectId}/{UnitId}/{Blockid}/{Floorid}/{DivisionId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> ByProjectWithStock(int CompanyId, int Branchid, int Materialtypeid, int ProjectId, int UnitId, int Blockid, int Floorid,int DivisionId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var material = await _materialListRepository.ByProjectWithStock(CompanyId, Branchid, Materialtypeid, ProjectId, UnitId, Blockid, Floorid, DivisionId, FinancialYearId);
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


        [HttpGet("ByProjectWithBrand/{CompanyId}/{Branchid}/{Materialtypeid}/{MaterialBrandId}/{ProjectId}/{UnitId}/{Blockid}/{Floorid}")]
        [Authorize]
        public async Task<IActionResult> GetByProjectIdWithBrand(int CompanyId, int Branchid, int Materialtypeid,int MaterialBrandId, int ProjectId, int UnitId, int Blockid, int Floorid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var material = await _materialListRepository.GetByProjectIdWithBrand(CompanyId, Branchid, Materialtypeid, MaterialBrandId, ProjectId, UnitId, Blockid, Floorid);
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

        [HttpGet("Quotation/{CompanyId}/{Branchid}/{ProjectId}")]
        [Authorize]
        public async Task<IActionResult> GetQuotedRate(int CompanyId, int Branchid, int ProjectId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var material = await _materialListRepository.GetWithQuotationBrand(CompanyId, Branchid, ProjectId);
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

        [HttpGet("{CompanyId}/{Branchid}/{Materialtypeid}/{ProjectId}/{UnitId}/{Blockid}/{Floorid}")]
        [Authorize]
        public async Task<IActionResult> Get(int CompanyId, int Branchid, int Materialtypeid, int ProjectId, int UnitId, int Blockid, int Floorid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var material = await _materialListRepository.Get_Schedulerate(CompanyId, Branchid, Materialtypeid, ProjectId,  UnitId, Blockid, Floorid);
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
                        var product = await _materialRepository.GetReport(materialSearch);
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
                        var product = await _materialRepository.GetStockReport(materialSearch);
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

        [HttpGet("EditDelete/{id}")]
        [Authorize]
        public async Task<IActionResult> EditDelete(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _materialRepository.CheckEditDelete(id);
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


        [HttpGet("Transfer/{BranchId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> TransferIds(int BranchId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _materialRepository.TransferId(BranchId, FinancialYearId);
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

        [HttpGet("Receive/{BranchId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> ReceiveIds(int BranchId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _materialRepository.ReceiveId(BranchId, FinancialYearId);
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

        [HttpGet("Consumption/{BranchId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> ConsumptionIds(int BranchId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _materialRepository.ConsumptionId(BranchId, FinancialYearId);
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

        [HttpGet("Quotation/{BranchId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> QuotationIds(int BranchId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _materialRepository.QuotationId(BranchId, FinancialYearId);
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

        [HttpGet("MaterialsByCategory/{CompanyId}/{Branchid}/{Materialtypeid}/{MaterialCategoryId}")]
        [Authorize]
        public async Task<IActionResult> GetMaterialsByCategory(int CompanyId, int Branchid, int Materialtypeid, int MaterialCategoryId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var material = await _materialListByCategoryRepository.Get(CompanyId, Branchid, Materialtypeid, MaterialCategoryId);
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

        [HttpGet("MaterialsByCategory/{CompanyId}/{Branchid}/{Materialtypeid}/{MaterialCategoryId}/{MaterialBrandId}")]
        [Authorize]
        public async Task<IActionResult> GetMaterialsByCategory(int CompanyId, int Branchid, int Materialtypeid, int MaterialCategoryId, int MaterialBrandId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var material = await _materialListByCategoryRepository.Get(CompanyId, Branchid, Materialtypeid, MaterialCategoryId, MaterialBrandId);
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
    }
}
