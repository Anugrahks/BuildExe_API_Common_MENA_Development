using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BuildExeServices.Repository;
using BuildExeServices.Models;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using BuildExeServices.Library;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase

    {

        private readonly ICompanyRepository _companyRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;

        public CompanyController(ICompanyRepository companyRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _companyRepository = companyRepository;
            _userLogRepository = userLogRepository;
            _mdHashValidator = mdHashValidator;
        }
        // GET: api/<CompanyController>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var products =await  _companyRepository.Getcompany();
                return new OkObjectResult(products);
            }
            catch (Exception)
            { throw; }
            //return new string[] { "value1", "value2" };
        }

        // GET api/<CompanyController>/5
        [HttpGet("{id}")]
        
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var product = await _companyRepository.GetCompanyByID(id);
                return new OkObjectResult(product);
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

        [HttpGet("branch/byCompany/{companyid}")]
        [Authorize]
        public async Task<IActionResult> Gets(int companyid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product = await _companyRepository.GetBranchByCompany(companyid);
                return new OkObjectResult(product);
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

        [HttpGet("currencyname/shortname/{id}")]
        [Authorize]
        public async Task<IActionResult> currency(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product = await _companyRepository.CurrencyName(id);
                return new OkObjectResult(product);
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

        [HttpGet("branchreg/attendancetype/{id}")]
        [Authorize]
        public async Task<IActionResult> attendancetypes(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var product = await _companyRepository.attendancetype(id);
                return new OkObjectResult(product);
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

        // POST api/<CompanyController>
        [HttpPost]
        [Authorize]

        public async Task<IActionResult> Post([FromBody] Company company, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled ))
                {
                    var val = await _companyRepository.InsertCompany(company);
                    scope.Complete();
                    return new OkObjectResult(val);//CreatedAtAction(nameof(Get), new { id = company.CompanyId }, company);
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


        // PUT api/<CompanyController>/5
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] Company company, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (company != null)
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled ))
                    {
                       await _companyRepository.UpdateCompany(company);
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

        // DELETE api/<CompanyController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
              var val= await  _companyRepository.DeleteCompany(id);
                return new OkObjectResult(val);
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


        [HttpGet("EditDelete/{id}")]
        [Authorize]
        public async Task<IActionResult> EditDelete(int id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var result = await _companyRepository.CheckEditDelete(id);
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




        [HttpGet("SiteLimit/{branchid}")]
        [Authorize]
        public async Task<IActionResult> SiteLimit(int branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var result = await _companyRepository.SiteLimit(branchid);
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

        [HttpGet("branchreg/batchenable/{companyId}")]
        // [Authorize]
        public async Task<IActionResult> BatchEnable(int companyId, [FromHeader] string mdhash, [FromHeader] int User)
        {
            //if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            //{
            try
            {
                var result = await _companyRepository.BatchEnable(companyId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = $"An error occurred: {ex.Message}",
                    statusCode = 0
                });
            }
            //}
            //else
            //{
            //    return Unauthorized("Invalid MdHash");
            //}
        }
    }
}
