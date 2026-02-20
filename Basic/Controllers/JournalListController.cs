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
using BuildExeBasic.Library;
using System.Security.Policy;

namespace BuildExeBasic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalListController : ControllerBase
    {
        private readonly IJournalEntryRepository _journalEntryRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public JournalListController(IJournalEntryRepository journalEntryRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _journalEntryRepository = journalEntryRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }
        [HttpGet("{Id}")]
        [Authorize]
        public async Task<IActionResult> Get(int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
               
            try
            {
                var designation = await _journalEntryRepository.Getdetails(Id);
                return new OkObjectResult(designation);
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
        [HttpGet("{CompanyId}/{Branchid}/{userid}/{FinancialYearId}")]
        [Authorize]
        public async Task < IActionResult > Get(int CompanyId, int Branchid,int userid, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                
            try
            {
                var designation = await _journalEntryRepository.GetForApproval(CompanyId, Branchid, userid, FinancialYearId);
            return new OkObjectResult(designation);
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
        [HttpGet("{CompanyId}/{Branchid}")]
        [Authorize]
        public async Task<IActionResult> Get(int CompanyId, int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                
            try
            {
                var designation = await  _journalEntryRepository.GetForEdit(CompanyId, Branchid);
                return new OkObjectResult(designation);
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

        [HttpGet("getuser/{CompanyId}/{Branchid}/{UserId}/{FinancialYearId}")]
        [Authorize]
        public async Task<IActionResult> Getbyid(int CompanyId, int Branchid, int UserId, int FinancialYearId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                
            try
            {
                var designation = await _journalEntryRepository.GetForEdituser(CompanyId, Branchid, UserId, FinancialYearId);
                return new OkObjectResult(designation);
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
    }
}
