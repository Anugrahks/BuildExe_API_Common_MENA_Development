using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BuildExeMaterialServices.DBContexts;
using BuildExeMaterialServices.Models;
using BuildExeMaterialServices.Repository;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;
using BuildExeMaterialServices.Library;
/*Rohith Change*/
namespace BuildExeMaterialServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskMastersController : ControllerBase
    {
        private readonly ITaskMasterRepository _taskMasterRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public TaskMastersController(ITaskMasterRepository taskMasterRepository)
        {
            _taskMasterRepository = taskMasterRepository;
        }

        [HttpGet("getEdit/{BranchId}/{UserId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> Getedit(int BranchId, int UserId, int FinancialYearId)
        {
            try
            {
                var brand = await _taskMasterRepository.Getedit(BranchId, UserId, FinancialYearId);
                return new OkObjectResult(brand);
            }
            catch (Exception)
            { throw; }

        }

        [HttpGet("getbyId/{Id}")]
        [Authorize]
        public async Task<IActionResult> getbyId(int Id)
        {
            try
            {
                var brand = await _taskMasterRepository.GetById(Id);
                return new OkObjectResult(brand);
            }
            catch (Exception)
            { throw; }

        }



        [HttpGet("getApproval/{BranchId}/{UserId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> getApproval(int BranchId, int UserId, int FinancialYearId)
        {
            try
            {
                var brand = await _taskMasterRepository.GetApproval(BranchId, UserId, FinancialYearId);
                return new OkObjectResult(brand);
            }
            catch (Exception)
            { throw; }

        }

        [HttpGet("getTaskId/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> getTaskId(int BranchId)
        {
            try
            {
                var brand = await _taskMasterRepository.getTaskId(BranchId);
                return new OkObjectResult(brand);
            }
            catch (Exception)
            { throw; }

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] IEnumerable<TaskMaster> mat)
        {
            try
            {
                var val = await _taskMasterRepository.Insert(mat);
                return new OkObjectResult(val);
            }
            catch (Exception)
            { throw; }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] IEnumerable<TaskMaster> mat)
        {
            try
            {
                var val = await _taskMasterRepository.Update(mat);
                return new OkObjectResult(val);
            }
            catch (Exception)
            { throw; }
        }


        [HttpDelete("{id}/{UserID}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id, int UserID)
        {
            try
            {
                var bb = await _taskMasterRepository.Delete(id, UserID);
                return new OkObjectResult(bb);
            }
            catch (Exception)
            { throw; }
        }


    }
}
