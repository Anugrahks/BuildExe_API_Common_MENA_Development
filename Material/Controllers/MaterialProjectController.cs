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
    public class MaterialProjectController : ControllerBase
    {
        private readonly IMaterialListRepository _materialListRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public MaterialProjectController(IMaterialListRepository materialListRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _materialListRepository = materialListRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpPost("{FinancialyearID}")]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] MaterialProjectSearchList  materialList , int FinancialyearID, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if ((String.IsNullOrEmpty(materialList.Id.ToString())) || materialList.Id == 0)
                {
                    var material =  await _materialListRepository.GetbasedonProject(materialList, FinancialyearID);

                    return new OkObjectResult(material);
                }
                else
                {
                    var material =await _materialListRepository.GetbasedonProjectAndMaterial(materialList, FinancialyearID);

                    return new OkObjectResult(material);
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
        [HttpGet("ForStockIndividual/{MaterialId}/{ProjectId}/{FinancialYearId}/{CompanyId}/{BranchId}/{Id}")]
        [Authorize]
        public async Task<IActionResult> ForStockIndividual(int MaterialId, int ProjectId, int FinancialYearId, int CompanyId, int BranchId, int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var material = await _materialListRepository.ForStockIndividual(MaterialId, ProjectId, FinancialYearId, CompanyId, BranchId,Id);
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

        [HttpPost("{SupplierId}/{FinancialyearID}")]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] MaterialProjectSearchList materialList, int SupplierId, int FinancialyearID, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var material =await _materialListRepository.GetbasedonProjectandSupplier(materialList, SupplierId, FinancialyearID);
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
        [HttpPost ]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] MaterialProjectSearchList materialList, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var material = await _materialListRepository.GetMaterialWithStock (materialList);
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
        [Route("WithStock")]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] MaterialProjectSearchList materialList, DateTime requiredDate, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var material = await _materialListRepository.GetMaterialWithStock2(materialList, requiredDate);
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
        [Route("PurchaseReturnEntry")]
        [Authorize]
        public async Task<IActionResult> PurchaseReturnEntry([FromBody] MaterialProjectSearchList materialList, Int16 supplierId, DateTime requiredDate, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if ((String.IsNullOrEmpty(materialList.Id.ToString())) || materialList.Id == 0)
                {
                    var material = await _materialListRepository.GetbasedonProject(materialList, (int)materialList.FinancialYearId);

                    return new OkObjectResult(material);
                }
                else
                {
                    var material = await _materialListRepository.GetbasedonProjectAndMaterial(materialList, requiredDate);

                    return new OkObjectResult(material);
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

        [HttpPost]
        [Route("PurchaseReturnAll")]
        [Authorize]
        public async Task<IActionResult> PurchaseReturnAll([FromBody] MaterialProjectSearchList materialList, DateTime requiredDate, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (materialList.IsEdit == 1)
                {
                    var material = await _materialListRepository.GetbasedonProjectAndMaterialEdit(materialList, requiredDate);

                    return new OkObjectResult(material);

                }
                else
                {

                    var material = await _materialListRepository.GetbasedonProjectAndMaterialAll(materialList, requiredDate);

                    return new OkObjectResult(material);
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



        [HttpPost]
        [Route("MaterialSale")]
        [Authorize]
        public async Task<IActionResult> GetbasedonSale([FromBody] MaterialProjectSearchList materialList, DateTime requiredDate, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {

                    var material = await _materialListRepository.GetbasedonSale(materialList, requiredDate);

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
