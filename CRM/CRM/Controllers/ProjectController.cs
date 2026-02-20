using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BuildExeServices.Models;
using System.Transactions;
using BuildExeServices.Repository;
using System.ComponentModel.Design;
using Microsoft.AspNetCore.Authorization;
using BuildExeServices.Library;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectMasterRepository _ProjectMasterRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;
        public ProjectController(IProjectMasterRepository ProjectMasterRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _ProjectMasterRepository = ProjectMasterRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }
        // GET: api/<ProjectController>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var Project = await _ProjectMasterRepository.Getproject ();
            return new OkObjectResult(Project);
            }
            catch (Exception)
            { throw; }
            //return new string[] { "value1", "value2" };
        }

        // GET api/<ProjectController>/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetprojectByID (id);
            return new OkObjectResult(Project);
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

        [HttpGet("Department/{id}")]
        [Authorize]
        public async Task<IActionResult> GetbyDepartment(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetprojectByDepartment(id);
                return new OkObjectResult(Project);
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

        [HttpGet("{companyid}/{branchId}")]
        [Authorize]
        public async Task<IActionResult> Get(int companyid,int branchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project =await _ProjectMasterRepository.Getproject (companyid,branchId);
            return new OkObjectResult(Project);
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

        [HttpGet("siteuser/{companyid}/{branchId}/{userId}/{siteuser}")]
        [Authorize]
        public async Task<IActionResult> Get(int companyid, int branchId, int userId, int siteuser, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.Getproject(companyid, branchId, userId, siteuser);
                return new OkObjectResult(Project);
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

        [HttpGet("All/{companyid}/{branchId}")]
        //[Authorize]
        public async Task<IActionResult> GetAll(int companyid, int branchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            //if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetAllproject(companyid, branchId);
                return new OkObjectResult(Project);
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
            //else
            //{
            //    return Unauthorized("Invalid MdHash");
            //}
        }

        [HttpGet("All/{companyid}/{branchId}/{userId}/{siteuser}")]
        [Authorize]
        public async Task<IActionResult> GetAll(int companyid, int branchId, int userId, int siteuser, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetAllproject(companyid, branchId, userId, siteuser);
                return new OkObjectResult(Project);
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


        [HttpGet("List/{companyid}/{branchId}")]
        [Authorize]
        public async Task<IActionResult> GetList(int companyid, int branchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetprojectList(companyid, branchId);
                return new OkObjectResult(Project);
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
        [HttpGet("getuserList/{companyid}/{branchId}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> GetListbyid(int companyid, int branchId, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetprojectListuser(companyid, branchId,UserId);
                return new OkObjectResult(Project);
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

        [HttpGet("WithStage/{companyid}/{branchId}")]
        [Authorize]
        public async Task<IActionResult> GetWithStage(int companyid, int branchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.Getproject_withStage (companyid, branchId);
                return new OkObjectResult(Project);
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
        [HttpGet("Refund/{Type}/{companyid}/{branchId}")]
        [Authorize]
        public async Task<IActionResult> GetForRefund(int Type,int companyid, int branchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.Getproject_ForRefund (Type,companyid, branchId);
                return new OkObjectResult(Project);
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
        [HttpGet("Schedule/{companyid}/{branchId}/{scheduletype}")]
        [Authorize]
        public async Task<IActionResult> Get(int companyid, int branchId,int scheduletype, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetprojectBySchedule(companyid, branchId, scheduletype);
                return new OkObjectResult(Project);
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

        [HttpGet("getuserSchedule/{companyid}/{branchId}/{scheduletype}/{UserId}")]
        [Authorize]
        public async Task<IActionResult> Getbyid(int companyid, int branchId, int scheduletype, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetprojectByScheduleuser(companyid, branchId, scheduletype, UserId);
                return new OkObjectResult(Project);
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

        [HttpGet("BySchedule/{companyid}/{branchId}")]
        [Authorize]
        public async Task<IActionResult> BySchedule(int companyid, int branchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetBySchedule(companyid, branchId);
                return new OkObjectResult(Project);
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


        [HttpGet("PaymentMode/{companyid}/{branchId}/{PaymentMode}")]
        [Authorize]
        public async Task<IActionResult> GetbyPaymetmode(int companyid, int branchId,int PaymentMode, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.Getproject(companyid, branchId, PaymentMode);
                return new OkObjectResult(Project);
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
        [HttpGet("{Id}/{ProjectId}/{companyid}/{branchId}")]
        [Authorize]
        public async Task<IActionResult> Get(int Id, string ProjectId, int companyid,int branchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetProjectIdValidation (Id, ProjectId,companyid, branchId);
                return new OkObjectResult(Project);
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
        [HttpGet("{Id}/{ProjectId}/{ProjectName}/{companyid}/{branchId}")]
        [Authorize]
        public async Task<IActionResult> Get(int Id, string ProjectId,string ProjectName, int companyid, int branchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.Getproject_Validation(Id, ProjectId, ProjectName, companyid, branchId);
                return new OkObjectResult(Project);
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

        [HttpPost("ProjectReg")]
        [Authorize]
        public async Task<IActionResult> Getproject_Validation([FromBody] ProjectRegSearch projectRegSearch, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.Getproject_Validation(projectRegSearch.Id, projectRegSearch.ProjectId, projectRegSearch.ProjectName, projectRegSearch.CompanyId, projectRegSearch.BranchId);
                return new OkObjectResult(Project);
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

        [HttpGet("{companyid}/{branchId}/{ProjectType}")]
        [Authorize]
        public async Task<IActionResult> Get(int companyid, int branchId,string ProjectType, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.Getproject_by_type(companyid, branchId, ProjectType);
                return new OkObjectResult(Project);
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


        [HttpGet("OP/{companyid}/{branchId}/{ProjectType}/{userid}/{siteuser}")]
        [Authorize]
        public async Task<IActionResult> Getusersite(int companyid, int branchId, string ProjectType, int userid, int siteuser, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.Getproject_by_type(companyid, branchId, ProjectType,  userid,  siteuser);
                return new OkObjectResult(Project);
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

        [HttpGet("MaterialReceive/{companyid}/{branchId}/{userId}/{siteuser}")]
        [Authorize]
        public async Task<IActionResult> GetMaterialRecieve(int companyid, int branchId, int userId, int siteuser, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetMaterialRecieveProject(companyid, branchId, userId, siteuser);
                return new OkObjectResult(Project);
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

        [HttpGet("MaterialReceive/{companyid}/{branchId}")]
        [Authorize]
        public async Task<IActionResult> GetMaterialRecieve(int companyid, int branchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetMaterialRecieveProject(companyid, branchId);
                return new OkObjectResult(Project);
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


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] Project  projectmaster, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                //if (projectmaster != null)
                //{
                    //projectmaster.GovtProj.GovtProjId = 100;
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var val = await _ProjectMasterRepository.Insertproject(projectmaster);
                        scope.Complete();
                        return new OkObjectResult(val);
                        // return CreatedAtAction(nameof(Get), new { id = projectmaster.id  }, projectmaster);
                    }
                //}
                //return new NoContentResult();
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

        
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] Project projectmaster, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (projectmaster != null)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                        var val = await _ProjectMasterRepository.Updateproject(projectmaster);
                        scope.Complete();
                        return new OkObjectResult(val);
                        //var val = await _ProjectMasterRepository.Updateproject(projectmaster);
                        //scope.Complete();
                        //return new OkObjectResult(val);
                    }
                }
            return new NoContentResult();
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


        [HttpPut("Status/{ProjectId}/{DivisionId}")]
        [Authorize]
        public async Task<IActionResult> Put(int ProjectId,int DivisionId, [FromBody] Project projectmaster, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (projectmaster != null)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var val = await _ProjectMasterRepository.UpdateStatus(ProjectId, DivisionId, projectmaster);
                    scope.Complete();
                    return new OkObjectResult(val);
                }
            }
            return new NoContentResult();
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



        // DELETE api/<ProjectController>/5
        [HttpDelete("{id}/{UserId}/{DivisionId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int  id,int UserId, int DivisionId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
             var result=  await _ProjectMasterRepository.Deleteproject(id, UserId, DivisionId);
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

        [HttpGet("EditDelete/{id}/{divisionId}/{IsEdit}")]
        [Authorize]
        public async Task<IActionResult> EditDelete(int id, int divisionId ,int isEdit, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _ProjectMasterRepository.CheckEditDelete(id, divisionId, isEdit);
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


        [HttpGet("StatusName/{CompanyId}/{Branchid}/{DivisionId}")]
        [Authorize]
        public async Task<IActionResult> StatusName(int CompanyId,int BranchId, int DivisionId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _ProjectMasterRepository.StatusName(CompanyId,BranchId,DivisionId);
                    return new OkObjectResult(purchase);
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

        #region Project Filter

        [HttpGet("ProjectSpecification/{departmentId}")]
        [Authorize]
        public async Task<IActionResult> GetProjectsForProjectSpecification(int departmentId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetProjectsForProjectSpecification(departmentId);
                return new OkObjectResult(Project);
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

        [HttpGet("ProjectsinContractor/{CompanyId}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> getprojectwithcontractor(int CompanyId, int BranchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.getprojectwithcontractors(CompanyId, BranchId);
                return new OkObjectResult(Project);
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

        [HttpGet("ProjectsinContractor/{CompanyId}/{BranchId}/{userId}/{siteuser}")]
        [Authorize]
        public async Task<IActionResult> getprojectwithcontractor(int CompanyId, int BranchId, int userId, int siteuser, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.getprojectwithcontractors(CompanyId, BranchId, userId ,siteuser);
                return new OkObjectResult(Project);
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

        [HttpGet("ManualBoq/{companyid}/{branchId}")]
        [Authorize]
        public async Task<IActionResult> GetProjectsForManualBoq(int companyid, int branchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetProjectsForManualBoq(companyid, branchId);
                return new OkObjectResult(Project);
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

        [HttpGet("StageInvoice/{companyid}/{branchId}")]
        [Authorize]
        public async Task<IActionResult> GetProjectsForStageInvoice(int companyid, int branchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetProjectsForStageInvoice(companyid, branchId);
                return new OkObjectResult(Project);
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

        [HttpGet("getuserStageInvoice/{companyid}/{branchId}/{UserId}")]
        public async Task<IActionResult> GetProjectsForStageInvoicebyid(int companyid, int branchId, int UserId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetProjectsForStageInvoiceuser(companyid, branchId, UserId);
                return new OkObjectResult(Project);
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

        [HttpGet("getuserStageInvoice/{companyid}/{branchId}/{UserId}/{siteuser}")]
        public async Task<IActionResult> GetProjectsForStageInvoicebyid(int companyid, int branchId, int UserId, int siteuser, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetProjectsForStageInvoiceuser(companyid, branchId, UserId,  siteuser);
                return new OkObjectResult(Project);
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

        [HttpGet("GeneralInvoice/{companyid}/{branchId}")]
        [Authorize]
        public async Task<IActionResult> GetProjectsForGeneralInvoice(int companyid, int branchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetProjectsForGeneralInvoice(companyid, branchId);
                return new OkObjectResult(Project);
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

        [HttpGet("GeneralInvoice/{companyid}/{branchId}/{userId}/{siteuser}")]
        [Authorize]
        public async Task<IActionResult> GetProjectsForGeneralInvoice(int companyid, int branchId, int userId, int siteuser, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetProjectsForGeneralInvoice(companyid, branchId, userId, siteuser);
                return new OkObjectResult(Project);
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

        [HttpGet("StageReceipt/{companyid}/{branchId}")]
        [Authorize]
        public async Task<IActionResult> GetProjectsForStageReceipt(int companyid, int branchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetProjectsForStageReceipt(companyid, branchId);
                return new OkObjectResult(Project);
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

        [HttpGet("StageReceipt/{companyid}/{branchId}/{userId}/{siteuser}")]
        [Authorize]
        public async Task<IActionResult> GetProjectsForStageReceipt(int companyid, int branchId, int userId, int siteuser, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetProjectsForStageReceipt(companyid, branchId,userId, siteuser);
                return new OkObjectResult(Project);
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


        [HttpGet("PartBill/{companyid}/{branchId}/{PaymentMode}")]
        [Authorize]
        public async Task<IActionResult> GetProjectsForPartBill(int companyid, int branchId, int PaymentMode, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetProjectsForPartBill(companyid, branchId, PaymentMode);
                return new OkObjectResult(Project);
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

        [HttpGet("BillReceipt/{companyid}/{branchId}/{userId}/{siteuser}")]
        [Authorize]
        public async Task<IActionResult> GetProjectsForBillReceipt(int companyid, int branchId, int userId, int siteuser, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetProjectsForBillReceipt(companyid, branchId, userId, siteuser);
                return new OkObjectResult(Project);
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

        [HttpGet("PartBill/{companyid}/{branchId}/{PaymentMode}/{userId}/{siteuser}")]
        [Authorize]
        public async Task<IActionResult> GetProjectsForPartBill(int companyid, int branchId, int PaymentMode, int userId, int siteuser, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetProjectsForPartBill(companyid, branchId, PaymentMode, userId, siteuser);
                return new OkObjectResult(Project);
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

        [HttpGet("BillReceipt/{companyid}/{branchId}")]
        [Authorize]
        public async Task<IActionResult> GetProjectsForBillReceipt(int companyid, int branchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetProjectsForBillReceipt(companyid, branchId);
                return new OkObjectResult(Project);
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

        [HttpGet("OwnProjects/{companyid}/{branchId}")]
        [Authorize]
        public async Task<IActionResult> GetOwnProjects(int companyid, int branchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetOwnProjects(companyid, branchId);
                return new OkObjectResult(Project);
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


        [HttpGet("RateEvaluation/{companyid}/{branchId}")]
        [Authorize]
        public async Task<IActionResult> GetProjectsForRateEvaluation(int companyid, int branchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetProjectsForRateEvaluation(companyid, branchId);
                return new OkObjectResult(Project);
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

        [HttpGet("getuserRateEvaluation/{companyid}/{branchId}/{UserId}/{FinancialYearId}")]
        public async Task<IActionResult> GetProjectsForRateEvaluationbyid(int companyid, int branchId, int UserId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetProjectsForRateEvaluationuser(companyid, branchId, UserId, FinancialYearId);
                return new OkObjectResult(Project);
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

        [HttpGet("RateComparison/{companyid}/{branchId}")]
        [Authorize]
        public async Task<IActionResult> GetProjectsForRateComparison(int companyid, int branchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetProjectsForRateComparison(companyid, branchId);
                return new OkObjectResult(Project);
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

        [HttpGet("getuserRateComparison/{companyid}/{branchId}/{UserId}/{FinancialYearId}")]
        public async Task<IActionResult> GetProjectsForRateComparisonbyid(int companyid, int branchId, int UserId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetProjectsForRateComparisonuser(companyid, branchId, UserId, FinancialYearId);
                return new OkObjectResult(Project);
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

        [HttpGet("WeeklyBill/{companyid}/{branchId}")]
        [Authorize]
        public async Task<IActionResult> GetProjectsForWeeklyBill(int companyid, int branchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetProjectsForWeeklyBill(companyid, branchId);
                return new OkObjectResult(Project);
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

        [HttpGet("Refunding/{companyid}/{branchId}/{type}")]
        [Authorize]
        public async Task<IActionResult> GetProjectsForRefunding(int companyid, int branchId, int type, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetProjectsForRefunding(companyid, branchId, type);
                return new OkObjectResult(Project);
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

        [HttpGet("AssignBlockFloors/{companyid}/{branchId}")]
        [Authorize]
        public async Task<IActionResult> GetProjectsForAssignBlockFloors(int companyid, int branchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetProjectsForAssignBlockFloors(companyid, branchId);
                return new OkObjectResult(Project);
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

        [HttpGet("ClientAdvance/{companyid}/{branchId}")]
        [Authorize]
        public async Task<IActionResult> GetProjectsForClientAdvance(int companyid, int branchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetProjectsForClientAdvance(companyid, branchId);
                return new OkObjectResult(Project);
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

        [HttpGet("LabourInProject/{companyid}/{branchId}")]
        [Authorize]
        public async Task<IActionResult> GetProjectsForLabourInProject(int companyid, int branchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetProjectsForLabourInProject(companyid, branchId);
                return new OkObjectResult(Project);
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



        [HttpGet("LabourInProject/{companyid}/{branchId}/{UserId}/{SiteUser}")]
        [Authorize]
        public async Task<IActionResult> GetProjectsForLabourInProject(int companyid, int branchId, int UserId, int SiteUser, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetProjectsForLabourInProjectnew(companyid, branchId, UserId, SiteUser);
                return new OkObjectResult(Project);
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

        [HttpGet("LabourInProjectSitemanager/{companyid}/{branchId}/{UserId}/{SiteUser}/{SitemanagerId}")]
        [Authorize]
        public async Task<IActionResult> GetProjectsForLabourInProjectsitemanager(int companyid, int branchId, int UserId, int SiteUser,int SitemanagerId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var Project = await _ProjectMasterRepository.GetProjectsForLabourInProjectsitemanager(companyid, branchId, UserId, SiteUser, SitemanagerId);
                    return new OkObjectResult(Project);
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



        [HttpGet("LabourInProject/{companyid}/{branchId}/{UserId}/{SiteUser}/{Status}")]
        [Authorize]
        public async Task<IActionResult> GetProjectsForLabourInProject(int companyid, int branchId, int UserId, int SiteUser, int Status, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetProjectsForLabourInProjectSetting(companyid, branchId, UserId, SiteUser, Status);
                return new OkObjectResult(Project);
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

        [HttpGet("Document/{companyid}/{branchId}")]
        [Authorize]
        public async Task<IActionResult> GetProjectsForDocument(int companyid, int branchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetProjectsForDocument(companyid, branchId);
                return new OkObjectResult(Project);
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
        [HttpGet("ProjectEstimation/{companyid}/{branchId}")]
        [Authorize]
        public async Task<IActionResult> GetProjectsForProjectEstimation(int companyid, int branchId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var Project = await _ProjectMasterRepository.GetProjectsForEst(companyid, branchId);
                return new OkObjectResult(Project);
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

        #endregion Project Filter
        [HttpGet("StatusNameList/{CompanyId}/{Branchid}/{ProjectId}")]
        [Authorize]
        public async Task<IActionResult> StatusNameList(int CompanyId, int BranchId, int ProjectId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var purchase = await _ProjectMasterRepository.StatusNameList(CompanyId, BranchId, ProjectId);
                    return new OkObjectResult(purchase);
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

        [HttpGet("GetClientWiseProject/{companyid}/{branchId}/{clientName}")]
       // [Authorize]
        public async Task<IActionResult> GetClientWiseProject(int companyid, int branchId,string clientName, [FromHeader] string mdhash, [FromHeader] int User)
        {
          //  if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var Project = await _ProjectMasterRepository.GetAllClientproject(companyid, branchId, clientName);
                    return new OkObjectResult(Project);
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
            //else
            //{
            //    return Unauthorized("Invalid MdHash");
            //}
        }

    }
}
