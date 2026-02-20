using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BuildExeServices.Models;
using BuildExeServices.Repository;

using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using BuildExeServices.Library;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnquiryListController : ControllerBase
    {
        private readonly IEnquiryRepository _enquiryRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public EnquiryListController(IEnquiryRepository enquiryRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _enquiryRepository = enquiryRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }
        [HttpGet("{Companyid}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> Get(int Companyid, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product = await _enquiryRepository.GetEnquirylist(Companyid, BranchId);
                return new OkObjectResult(product);
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

        [HttpGet("enquiry/{Companyid}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> Getenquiry(int Companyid, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product = await _enquiryRepository.GetEnquirybylist(Companyid, BranchId);
                return new OkObjectResult(product);
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

        [HttpGet("enquiryreport/{Companyid}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> Getenquiryreport(int Companyid, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product = await _enquiryRepository.GetEnquirybylistreport(Companyid, BranchId);
                return new OkObjectResult(product);
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

        //[HttpGet("getuser/{Companyid}/{BranchId}/{UserId}")]
        //[Authorize]
        //public async Task<IActionResult> Getbyid(int Companyid, int BranchId, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        //{
        //    if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
        //    {
        //        try
        //    {
        //        var product = await _enquiryRepository.GetEnquirylistuser(Companyid, BranchId, UserId);
        //        return new OkObjectResult(product);
        //    }
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

        [HttpGet("getuser/{Companyid}/{BranchId}/{UserId}/{Page}/{PageSize}")]
        [Authorize]
        public async Task<IActionResult> GetEnquirylistuser(int CompanyId, int Branchid, int UserId, int Page, int PageSize, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _enquiryRepository.GetEnquirylistuser(CompanyId, Branchid, UserId,  Page,  PageSize);
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


        [HttpGet("getuser/{Companyid}/{BranchId}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> GetEnquirylistuser(int CompanyId, int Branchid, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _enquiryRepository.GetEnquirylistuser(CompanyId, Branchid, UserId, 0, 0);
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


        [HttpGet("Project/{Companyid}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> GetByProject(int Companyid, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product = await _enquiryRepository.GetEnquiryByProj(Companyid, BranchId);
                return new OkObjectResult(product);
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
        public async Task<IActionResult> Post([FromBody] EnquirySearch enquirySearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (enquirySearch != null)
                {
                    var product = await _enquiryRepository.GetEnquirySearch(enquirySearch);
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
        public async Task<IActionResult> put([FromBody] EnquiryReportSearch enquiryReportSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (enquiryReportSearch != null)
                {
                    var product = await _enquiryRepository.GetEnquiryReport(enquiryReportSearch);
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
    }
}
