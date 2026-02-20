using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
using System.Transactions;
using BuildExeBasic.Repository;
using Microsoft.AspNetCore.Authorization;
using BuildExeBasic.Library;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeBasic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PunchingController : ControllerBase
    {
        private readonly IReportInputRepository _reportInputRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public PunchingController(IReportInputRepository reportInputRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _reportInputRepository = reportInputRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }


        [HttpPost]

        public async Task<IActionResult> InsertPunching([FromBody] Punching reportInput)
        {

                try
                {

                        var val = await _reportInputRepository.InsertPunching(reportInput);
                        return new OkResult();

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

    }
}
