using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Repository;
using BuildExeServices.Models;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApprovalMenuController : ControllerBase
    {
        private readonly IMenuRepository _menuRepository;
        public ApprovalMenuController(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;

        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try { 
            var products =  await _menuRepository.GetMenuforApprovallevel();
            return new OkObjectResult(products);
            }
            catch (Exception)
            { throw; }

        }
    }
}
