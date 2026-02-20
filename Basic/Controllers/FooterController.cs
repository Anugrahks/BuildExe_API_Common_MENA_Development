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

namespace BuildExeBasic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FooterController : ControllerBase
    {
        private readonly IHeaderRepository _headerRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public FooterController(IHeaderRepository headerRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            
                _headerRepository = headerRepository;
                _userLogRepository = userLogRepository;
                _mdHashValidator = mdHashValidator;
            


        }

        [HttpGet("{CompanyId}/{Branchid}/{menuid}")]
        [Authorize]
        public async Task<IActionResult> Get(int CompanyId, int Branchid, int menuid, [FromHeader] string mdhash, [FromHeader] int User)
            {
                if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                {
                    try
            {
                var designation = await _headerRepository.GetFooter(CompanyId, Branchid, menuid);
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

        [HttpDelete("{CompanyId}/{Branchid}/{menuid}")]
        [Authorize]
        public async Task<IActionResult> Delete(int CompanyId, int Branchid, int menuid, [FromHeader] string mdhash, [FromHeader] int User)
            {
                if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                {
                    try
            {
                await _headerRepository.DeleteFooter(CompanyId, Branchid, menuid);
                return new OkResult();
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

        public async Task<IActionResult> Post([FromBody] IEnumerable<Footer> headers, [FromHeader] string mdhash, [FromHeader] int User)
            {
                if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                {
                    try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await _headerRepository.InsertFooter(headers);
                    scope.Complete();
                    return new OkResult();
                    //return CreatedAtAction(nameof(Get), new { Id = journalEntry.Id }, journalEntry);
                }
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
        public async Task<IActionResult> Put([FromBody] IEnumerable<Footer> headers, [FromHeader] string mdhash, [FromHeader] int User)
            {
                if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
                {
                    try
            {
                if (headers != null)
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        await _headerRepository.UpdateFooter(headers);
                        scope.Complete();
                        return new OkResult();
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
    }
}
