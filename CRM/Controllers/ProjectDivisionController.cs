using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BuildExeServices.Models;
using BuildExeServices.Repository;

using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using BuildExeServices.Library;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectDivisionController : ControllerBase
    {
        private readonly IProjectDivisionRepository _projectDivisionRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public ProjectDivisionController(IProjectDivisionRepository projectDivisionRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _projectDivisionRepository = projectDivisionRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;

        }
        [HttpGet("{CompanyId}/{Branchid}")]
        [Authorize]
        public async Task<IActionResult> Get(int CompanyId, int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var products = await _projectDivisionRepository.Getproject(CompanyId, Branchid);
            return new OkObjectResult(products);
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
        [HttpGet("{ProjectId}")]
        [Authorize]
        public async Task<IActionResult> Get(int ProjectId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var products = await _projectDivisionRepository.GetBlock(ProjectId);
            return new OkObjectResult(products);
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
        [HttpPost("{ProjectId}/{BlockId}")]
        [Authorize]
        public async Task<IActionResult> Post(int ProjectId, int BlockId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var products = await _projectDivisionRepository.GetFloor(ProjectId, BlockId);
            return new OkObjectResult(products);
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
        [HttpPut("{ProjectId}")]
        [Authorize]
        public async Task<IActionResult> Put(int ProjectId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var products = await _projectDivisionRepository.Gettype(ProjectId );
            return new OkObjectResult(products);
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

        [HttpGet("{ProjectId}/{BlockId}/{FloorId}")]
        [Authorize]
        public async Task<IActionResult> Get(int ProjectId, int BlockId, int FloorId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var products = await _projectDivisionRepository.GetUnit(ProjectId, BlockId, FloorId);
                return new OkObjectResult(products);
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

        #region Project, Block, Floor, Unit

        [HttpGet("Project/{CompanyId}/{Branchid}")]
        [Authorize]
        public async Task<IActionResult> GetProject(int CompanyId, int Branchid)
        {
            try
            {
                var products = await _projectDivisionRepository.GetProj(CompanyId, Branchid);
                return new OkObjectResult(products);
            }
            catch (Exception)
            { throw; }
        }
        [HttpGet("Block/{ProjectId}")]
        [Authorize]
        public async Task<IActionResult> GetBlock(int ProjectId)
        {
            try
            {
                var products = await _projectDivisionRepository.GetBlockByProj(ProjectId);
                return new OkObjectResult(products);
            }
            catch (Exception)
            { throw; }
        }
        [HttpGet("Floor/{ProjectId}/{BlockId}")]
        [Authorize]
        public async Task<IActionResult> GetFloor(int ProjectId, int BlockId)
        {
            try
            {
                var products = await _projectDivisionRepository.GetFloorByProjBlock(ProjectId, BlockId);
                return new OkObjectResult(products);
            }
            catch (Exception)
            { throw; }
        }


        [HttpGet("Unit/{ProjectId}/{BlockId}/{FloorId}")]
        [Authorize]
        public async Task<IActionResult> GetUnit(int ProjectId, int BlockId, int FloorId)
        {
            try
            {
                var products = await _projectDivisionRepository.GetUnitByProjBlockFloor(ProjectId, BlockId, FloorId);
                return new OkObjectResult(products);
            }
            catch (Exception)
            { throw; }
        }

        [HttpGet("UnitByProject/{ProjectId}")]
        [Authorize]
        public async Task<IActionResult> GetUnitByProj(int ProjectId)
        {
            try
            {
                var products = await _projectDivisionRepository.GetUnitByProj(ProjectId);
                return new OkObjectResult(products);
            }
            catch (Exception)
            { throw; }
        }
        #endregion
    }
}
