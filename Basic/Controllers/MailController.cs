using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
using System.Transactions;
using BuildExeBasic.Repository;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeBasic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private IMailRepository _mailRepository;
        public MailController(IMailRepository mailRepository)
        {
            _mailRepository = mailRepository;
        }

        [HttpGet("Mail")]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var value = _mailRepository.SendMailStudentwithCC();
                return new OkObjectResult(value);
            }
            catch (Exception)
            { throw; }
        }
    }

}
