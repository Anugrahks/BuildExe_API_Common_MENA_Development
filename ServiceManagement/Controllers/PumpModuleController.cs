using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BuildExeServiceManagement.Models;
using BuildExeServiceManagement.Repository;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using BuildExeServiceManagement.Library;
using System.Security.Cryptography;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeServiceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PumpModuleController : ControllerBase
    {
        private readonly IPumpModuleRepository _salesOrderRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public PumpModuleController(IPumpModuleRepository salesOrderRepository, IMdHashValidator mdHashValidator)
        {
            _salesOrderRepository = salesOrderRepository;
            _mdHashValidator = mdHashValidator;
        }


        [HttpGet("getEdit/{CompanyId}/{BranchId}/{UserId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> Getedit(int CompanyId, int Branchid, int UserId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var brand = await _salesOrderRepository.Getedit(CompanyId, Branchid, UserId, FinancialYearId,0);
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

        [HttpGet("getEdit/{CompanyId}/{BranchId}/{UserId}/{FinancialYearId}/{Type}")]
        [Authorize]
        public async Task<IActionResult> Getedit(int CompanyId, int Branchid, int UserId, int FinancialYearId, int Type, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var brand = await _salesOrderRepository.Getedit(CompanyId, Branchid, UserId, FinancialYearId, Type);
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


        [HttpGet("getElectricalTest/{Id}")]
        [Authorize]
        public async Task<IActionResult> getElectricalTest(int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var brand = await _salesOrderRepository.getElectricalTest(Id);
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


        [HttpGet("getAutoFetch/{BranchId}/{TypeId}")]
        [Authorize]
        public async Task<IActionResult> getAutoFetch(int BranchId, int TypeId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var brand = await _salesOrderRepository.getAutoFetch(BranchId, TypeId);
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
                    var brand = await _salesOrderRepository.GetApproval(CompanyId, Branchid, UserId, FinancialYearId,0);
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


        [HttpGet("getApproval/{CompanyId}/{BranchId}/{UserId}/{FinancialYearId}/{Type}")]
        [Authorize]
        public async Task<IActionResult> getApproval(int CompanyId, int Branchid, int UserId, int FinancialYearId, int Type, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var brand = await _salesOrderRepository.GetApproval(CompanyId, Branchid, UserId, FinancialYearId, Type);
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


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] IEnumerable<PumpModuleRequest> mat, [FromHeader] string mdhash, [FromHeader] int User)
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


        [HttpPost("ElectricalTest")]
        [Authorize]
        public async Task<IActionResult> PostElectricalTest([FromBody] IEnumerable<ElectricalTestRequest> mat, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var val = await _salesOrderRepository.InsertElectricalTest(mat);
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
        public async Task<IActionResult> Put([FromBody] IEnumerable<PumpModuleRequest> mat, [FromHeader] string mdhash, [FromHeader] int User)
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

        //[HttpPost("Report")]
        //[Authorize]
        //public async Task<IActionResult> DeliveryOrderReportReport([FromBody] MaterialSearch materialSearch, [FromHeader] string mdhash, [FromHeader] int User)
        //{
        //    if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
        //    {
        //        try
        //        {
        //            if (materialSearch != null)
        //            {
        //                var deliveryOrder = await _salesOrderRepository.GetDeliveryOrderReport(materialSearch);
        //                return new OkObjectResult(deliveryOrder);
        //            }

        //            return new NoContentResult();
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
