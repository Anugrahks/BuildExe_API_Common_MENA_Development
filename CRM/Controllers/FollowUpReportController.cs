using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Repository;
using BuildExeServices.Models;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using BuildExeServices.Library;

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowUpReportController : ControllerBase
    {
        private readonly IFollowupRepository _followupRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public FollowUpReportController(IFollowupRepository followupRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _followupRepository = followupRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;

        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] FollowupSearch followupSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (followupSearch != null)
            {
               
                    var product = await _followupRepository.GetFollowupForReport (followupSearch);

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
        public async Task<IActionResult> Put([FromBody] EnquirySearch enquirySearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (enquirySearch != null)
                {

                    var product = await _followupRepository.GetFollowupsearch(enquirySearch);

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


        [HttpGet("FollowUpByEnquiry/{EnquiryId}/{userId}")]
        [Authorize]
        public async Task<IActionResult> Get(int EnquiryId, int userId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var followup = await _followupRepository.GetFollowupbyEnquiry(EnquiryId, userId);

                return new OkObjectResult(followup);
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

        [HttpDelete("{EnquiryId}/{FollowUpId}")]
        [Authorize]
        public async Task<IActionResult> DeleteFollowupEnquiry(int EnquiryId, int FollowUpId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var followup = await _followupRepository.DeleteFollowupEnquiry(EnquiryId, FollowUpId);

                return new OkObjectResult(followup);
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
