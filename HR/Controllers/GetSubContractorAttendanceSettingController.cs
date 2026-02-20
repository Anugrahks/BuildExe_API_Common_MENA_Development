using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Repository;
using BuildExeHR.Models;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetSubContractorAttendanceSettingController : ControllerBase
    {
        private readonly ISubContractorAttendanceSettingRepository _subContractorAttendanceSettingRepository;
        public GetSubContractorAttendanceSettingController(ISubContractorAttendanceSettingRepository subContractorAttendanceSettingRepository)
        {
            _subContractorAttendanceSettingRepository = subContractorAttendanceSettingRepository;
        }

        // GET: api/<TeamController>
        [HttpGet("{companyid}/{Branchid}")]
        [Authorize]
        public IActionResult Get(int companyid, int Branchid)
        {
            try
            {
                var purchase = _subContractorAttendanceSettingRepository.GetForEdit(companyid, Branchid);
            return new OkObjectResult(purchase);
            }
            catch (Exception)
            { throw; }
        }

        // GET api/<TeamController>/5
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            try
            {
                var purchase = _subContractorAttendanceSettingRepository.GetOneDetails(id);

                return new OkObjectResult(purchase);
            }
            catch (Exception)
            { throw; }
        }
    }
}
