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

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuReportController : ControllerBase
    {
        private readonly IMenuRepository _menuRepository;
        public MenuReportController(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;

        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var products = await _menuRepository.GetMenuforReport();
            return new OkObjectResult(products);
            }
            catch (Exception)
            { throw; }
        }

        [HttpGet("reprint")]
        [Authorize]
        public async Task<IActionResult> Getforreprint()
        {
            try
            {
                var products = await _menuRepository.GetMenuforReprint();
                return new OkObjectResult(products);
            }
            catch (Exception)
            { throw; }
        }

        [HttpGet("automail")]
        [Authorize]
        public async Task<IActionResult> Getforautomail()
        {
            try
            {
                var products = await _menuRepository.GetMenuforautomail();
                return new OkObjectResult(products);
            }
            catch (Exception)
            { throw; }
        }

    }
}
