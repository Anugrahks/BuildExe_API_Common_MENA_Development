using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BuildExeServices.Repository;
using BuildExeServices.Models;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using BuildExeServices.Library;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QCController : ControllerBase
    {
        private readonly IQCRepository _projectWorkSettingRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public QCController(IQCRepository projectWorkSettingRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _projectWorkSettingRepository = projectWorkSettingRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;

        }
      

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] IEnumerable<QC> workEnquiryStageSettings, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {

                    var result = await _projectWorkSettingRepository.Update(workEnquiryStageSettings);
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

 

        [HttpPost("GetQCSetting")]
        [Authorize]
        public async Task<IActionResult> GetbyBranch([FromBody] BillSearch projectWorkSetting, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var brand = await _projectWorkSettingRepository.GetbyBranch(projectWorkSetting);
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

    }
}
