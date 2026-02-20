using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
using System.Transactions;
using BuildExeBasic.Repository;
using Microsoft.AspNetCore.Authorization;


namespace BuildExeBasic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectDetailExpenseController : ControllerBase
    {
        private readonly IProjectDetailExpenseRepository _ProjectDetailExpenseRepository;

        public ProjectDetailExpenseController(IProjectDetailExpenseRepository ProjectDetailExpenseRepository)
        {
            _ProjectDetailExpenseRepository = ProjectDetailExpenseRepository;

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] BasicSearch basicSearch)
        {
            try
            {
                if (basicSearch != null)
                {

                    if ((basicSearch.IsDetail is null) || (basicSearch.IsDetail == 0))
                    {
                        var product = await _ProjectDetailExpenseRepository.ProjectDetailExpenseReport(basicSearch);
                        return new OkObjectResult(product);
                    }
                    else
                    {
                        var product = await _ProjectDetailExpenseRepository.ProjectExpenseReport(basicSearch);
                        return new OkObjectResult(product);
                    }


                }
                return new NoContentResult();
            }
            catch (Exception)
            { throw; }
        }
    }
}
