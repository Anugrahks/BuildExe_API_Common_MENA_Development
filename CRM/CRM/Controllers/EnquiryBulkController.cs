using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BuildExeServices.Models;
using BuildExeServices.DBContexts;
using System.Reflection;
using BuildExeServices.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BuildExeServices.Library;

namespace BuildExeServices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnquiryBulkInsertController : ControllerBase
    {
        private readonly ProductContext _dbContext;
        private readonly IWorkEnquiryStageSettingsRepository _workEnquiryStageSettingsRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public EnquiryBulkInsertController(IWorkEnquiryStageSettingsRepository workEnquiryStageSettings, ProductContext dbContext, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _workEnquiryStageSettingsRepository = workEnquiryStageSettings;
            _dbContext = dbContext;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UploadBulkEnquiries([FromBody] List<EnquiryBulkInsert> enquiries, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                if (enquiries == null || enquiries.Count == 0)
            {
                return BadRequest(new
                {
                    message = "No data provided. Please upload valid enquiries.",
                    statusCode = 0
                });
            }

            try
            {
                
                var enquiryKeys = enquiries
                    .Select(e => new { e.BranchId, e.EnquiryID, e.MobileNo })
                    .Distinct()
                    .ToList();

                
                var existingEnquiries = await _dbContext.tbl_Enquiry
                    .Where(e => enquiryKeys.Select(eq => eq.BranchId).Contains(e.BranchId))
                    .ToListAsync();

                
                var newEnquiries = enquiries
                    .Where(e => !existingEnquiries.Any(eq =>
                        eq.BranchId == e.BranchId &&
                        (eq.EnquiryNo == e.EnquiryID || eq.Mobile == e.MobileNo)))
                    .ToList();

                if (newEnquiries.Count == 0)
                {
                    return Ok(new
                    {
                        message = "All EnquiryID's or Mobile numbers already exist.",
                        count = 0,
                        statusCode = 0
                    });
                }

                
                await _dbContext.tbl_EnquiryBulkInsert.AddRangeAsync(newEnquiries);
                await _dbContext.SaveChangesAsync();

                return Ok(new
                {
                    message = $"{newEnquiries.Count} new entries identified.",
                    count = newEnquiries.Count,
                    statusCode = 1
                });
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
        public async Task<IActionResult> Put([FromBody] GeneralMessageMaster generalMessageMaster, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {

                var result = await _workEnquiryStageSettingsRepository.UpdateEnquiryBulk(generalMessageMaster);
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
