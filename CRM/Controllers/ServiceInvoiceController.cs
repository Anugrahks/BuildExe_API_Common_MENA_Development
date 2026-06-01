using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BuildExeServices.Repository;
using Microsoft.AspNetCore.Authorization;
using BuildExeServices.Library;

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceInvoiceController : ControllerBase
    {
        private readonly IServiceInvoiceRepository _serviceInvoiceRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public ServiceInvoiceController(IServiceInvoiceRepository serviceInvoiceRepository, IMdHashValidator mdHashValidator)
        {
            _serviceInvoiceRepository = serviceInvoiceRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpGet("PendingInvoices/{JobId}/{CompanyId}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> GetPendingServiceInvoices(int JobId, int CompanyId, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var result = await _serviceInvoiceRepository.GetPendingServiceInvoices(JobId, CompanyId, BranchId);
                    return new OkObjectResult(result);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = $"An error occurred: {ex.Message}", statusCode = 0 });
                }
            }
            else
            {
                return Unauthorized("Invalid MdHash");
            }
        }

        [HttpGet("PendingInvoices/{JobId}/{CompanyId}/{BranchId}/{Id}")]
        [Authorize]
        public async Task<IActionResult> GetPendingServiceInvoicesEdit(int JobId, int CompanyId, int BranchId, int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var result = await _serviceInvoiceRepository.GetPendingServiceInvoicesEdit(JobId, CompanyId, BranchId, Id);
                    return new OkObjectResult(result);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = $"An error occurred: {ex.Message}", statusCode = 0 });
                }
            }
            else
            {
                return Unauthorized("Invalid MdHash");
            }
        }

        [HttpGet("Customer/{JobId}")]
        [Authorize]
        public async Task<IActionResult> GetServiceInvoiceCustomer(int JobId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var result = await _serviceInvoiceRepository.GetServiceInvoiceCustomer(JobId);
                    return new OkObjectResult(result);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = $"An error occurred: {ex.Message}", statusCode = 0 });
                }
            }
            else
            {
                return Unauthorized("Invalid MdHash");
            }
        }
    }
}