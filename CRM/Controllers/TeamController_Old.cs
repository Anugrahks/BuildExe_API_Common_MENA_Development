using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using BuildExeServices.Repository;
using BuildExeServices.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController_Old : ControllerBase
    {

        private readonly ITeamRepository _TeamRepository;
        public TeamController_Old(ITeamRepository TeamRepository)
        {
            _TeamRepository = TeamRepository;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TeamController>/5
        [HttpGet("{id}", Name = "Get")]
        //public IActionResult Get(int id)
        //{
        //    var product = _TeamRepository.GetTeambyID(id);
        //    return new OkObjectResult(product);
        //}



        [HttpPost]

        public IActionResult Post([FromBody] IEnumerable<Team> team)
        {
            using (var scope = new TransactionScope())
            {
                _TeamRepository.InsertTeam(team);
                scope.Complete();
                return new OkResult();
            }
        }


        // PUT api/<TeamController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TeamController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
