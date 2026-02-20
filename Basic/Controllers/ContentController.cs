using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
using System.Transactions;
using BuildExeBasic.Repository;
using System.Text.Json;
using Newtonsoft.Json;
using System.Security.Principal;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using BuildExeBasic.Library;
using System.Security.Policy;

namespace BuildExeBasic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly IHeaderRepository _headerRepository;
        private readonly ITermsAndConditionRepository _termsAndConditionRepository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMdHashValidator _mdHashValidator;


        public ContentController(IHeaderRepository headerRepository, ITermsAndConditionRepository termsAndConditionRepository, IUserLogRepository userLogRepository, IMdHashValidator mdHashValidator)
        {
            _headerRepository = headerRepository;
            _termsAndConditionRepository = termsAndConditionRepository;
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
                var designation = await _headerRepository.GetContent(CompanyId, Branchid, menuid);
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
                await _headerRepository.DeleteContent(CompanyId, Branchid, menuid);
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
        public async Task<IActionResult> Post([FromBody] IEnumerable<Content> headers, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await _headerRepository.InsertContent(headers);
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
        public async Task<IActionResult> Put([FromBody] IEnumerable<Content> headers, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                if (headers != null)
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        await _headerRepository.UpdateContent(headers);
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

        [HttpPost("termsandcondition/upload")]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] TermsAndConditons TermsAndCondition, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                await _termsAndConditionRepository.Post(TermsAndCondition);
                return Ok();
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




        [HttpGet("termsandcondition/{Id}")]
        [Authorize]
        public async Task<IActionResult> GetTermsAndConditionById(int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var entity = await _termsAndConditionRepository.GetTermsAndConditonsById(Id);
                string json = JsonConvert.SerializeObject(entity, Formatting.Indented,
                new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });
                return new OkObjectResult(json);
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





        [HttpGet("termsandconditions/printableConfigurations/{Id}")]
        [Authorize]
        public async Task<IActionResult> GetTermsAndConditionListById(int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var entity = await _termsAndConditionRepository.GetTermsAndConditonsListByPrintableConfigurationId(Id);
                string json = JsonConvert.SerializeObject(entity, Formatting.Indented,
                new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });
                return new OkObjectResult(json);
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

        [HttpGet("termsandcondition/PrintableConfiguration/{Id}")]
        [Authorize]
        public async Task<IActionResult> GetTermsAndConditionByPrintableConfigurationId(int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var entity = await _termsAndConditionRepository.GetTermsAndConditonsByPrintableConfigurationId(Id);
                var termsAndCondition = new TermsAndConditionViewModel();
                if(entity != null)
                {
                    termsAndCondition.Id = entity.Id;
                    termsAndCondition.PrintableConfigurationId = entity.PrintableConfigurationId;
                    termsAndCondition.TemplateName = entity.PrintableReportConfiguration?.TemplateName;
                }

                return new OkObjectResult(termsAndCondition);
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



        [HttpGet("termsandcondition/{CompanyId}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> GetTermsAndConditions(int CompanyId, int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var entity = await _termsAndConditionRepository.GetTermsAndConditionsListByCompanyIdAndBranchId(
                    CompanyId, Branchid);
                var termsConditionList = new List<TermsAndConditionViewModel>();
                foreach (var item in entity)
                {
                    termsConditionList.Add(new TermsAndConditionViewModel()
                    {
                        Id = item.Id,
                        PrintableConfigurationId = item.PrintableConfigurationId,
                        TemplateName = item.PrintableReportConfiguration.TemplateName
                    });
                }

                return new OkObjectResult(termsConditionList);
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






        [HttpPut("termsandcondition")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] TermsAndConditons termsAndConditons, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                await _termsAndConditionRepository.Update(termsAndConditons);
                return Ok();

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



        [HttpDelete("termsandcondition/{Id}")]
        [Authorize]
        public async Task<IActionResult> DeleteTermsAndCondition(int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                await _termsAndConditionRepository.DeleteTermsAndCondition(Id);
                return Ok();
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


//------------------------------------------------------DynamicContent-------------------------------------------------------------------------//

        [HttpPost("dynamiccontent/upload")]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] DynamicContent dynamicContent, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                await _termsAndConditionRepository.Post(dynamicContent);
                return Ok();
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

        [HttpGet("dynamiccontent/{Id}")]
        [Authorize]
        public async Task<IActionResult> GetdynamicContentById(int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var entity = await _termsAndConditionRepository.GetdynamicContentById(Id);
                string json = JsonConvert.SerializeObject(entity, Formatting.Indented,
                new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });
                return new OkObjectResult(json);
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

        [HttpGet("dynamiccontent/PrintableConfiguration/{Id}")]
        [Authorize]
        public async Task<IActionResult> GetDynamicContentByPrintableConfigurationId(int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var entity = await _termsAndConditionRepository.GetDynamicContentByPrintableConfigurationId(Id);
                var termsAndCondition = new TermsAndConditionViewModel();
                if (entity != null)
                {
                    termsAndCondition.Id = entity.Id;
                    termsAndCondition.PrintableConfigurationId = entity.PrintableConfigurationId;
                    termsAndCondition.TemplateName = entity.PrintableReportConfiguration?.TemplateName;
                }

                return new OkObjectResult(termsAndCondition);
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

        [HttpGet("dynamiccontentList/printableConfigurations/{Id}")]
        [Authorize]
        public async Task<IActionResult> GetdynamicContentListById(int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var entity = await _termsAndConditionRepository.GetdynamicContentListById(Id);
                string json = JsonConvert.SerializeObject(entity, Formatting.Indented,
                new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });
                return new OkObjectResult(json);
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


        [HttpGet("dynamiccontent/{CompanyId}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> GetDynamicContent(int CompanyId, int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                var entity = await _termsAndConditionRepository.GetDynamicContentListByCompanyIdAndBranchId(
                    CompanyId, Branchid);
                var termsConditionList = new List<TermsAndConditionViewModel>();
                foreach (var item in entity)
                {
                    termsConditionList.Add(new TermsAndConditionViewModel()
                    {
                        Id = item.Id,
                        PrintableConfigurationId = item.PrintableConfigurationId,
                        TemplateName = item.PrintableReportConfiguration.TemplateName
                    });
                }

                return new OkObjectResult(termsConditionList);
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

        [HttpPut("dynamiccontent")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] DynamicContent dynamicContent, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                await _termsAndConditionRepository.Update(dynamicContent);
                return Ok();

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

        [HttpDelete("dynamiccontent/{Id}")]
        [Authorize]
        public async Task<IActionResult> Deletedynamiccontent(int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
            {
                await _termsAndConditionRepository.Deletedynamiccontent(Id);
                return Ok();
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

        //------------------------------------------------------Signature-------------------------------------------------------------------------//

        [HttpPost("Signature/upload")]
        [Authorize]
        public async Task<IActionResult> PostSignature([FromBody] Signature dynamicContent, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    await _termsAndConditionRepository.PostSignature(dynamicContent);
                    return Ok();
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

        [HttpGet("Signature/{Id}")]
        [Authorize]
        public async Task<IActionResult> GetSignatureById(int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var entity = await _termsAndConditionRepository.GetSignatureById(Id);
                    string json = JsonConvert.SerializeObject(entity, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    });
                    return new OkObjectResult(json);
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

        [HttpGet("Signature/PrintableConfiguration/{Id}")]
        [Authorize]
        public async Task<IActionResult> GetSignatureByPrintableConfigurationId(int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var entity = await _termsAndConditionRepository.GetSignatureByPrintableConfigurationId(Id);
                    var termsAndCondition = new TermsAndConditionViewModel();
                    if (entity != null)
                    {
                        termsAndCondition.Id = entity.Id;
                        termsAndCondition.PrintableConfigurationId = entity.PrintableConfigurationId;
                        termsAndCondition.TemplateName = entity.PrintableReportConfiguration?.TemplateName;
                    }

                    return new OkObjectResult(termsAndCondition);
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

        [HttpGet("SignatureList/printableConfigurations/{Id}")]
        [Authorize]
        public async Task<IActionResult> GetSignatureListById(int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var entity = await _termsAndConditionRepository.GetSignatureListById(Id);
                    string json = JsonConvert.SerializeObject(entity, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    });
                    return new OkObjectResult(json);
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


        [HttpGet("Signature/{CompanyId}/{BranchId}")]
        [Authorize]
        public async Task<IActionResult> GetSignature(int CompanyId, int Branchid, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    var entity = await _termsAndConditionRepository.GetSignatureListByCompanyIdAndBranchId(
                        CompanyId, Branchid);
                    var termsConditionList = new List<TermsAndConditionViewModel>();
                    foreach (var item in entity)
                    {
                        termsConditionList.Add(new TermsAndConditionViewModel()
                        {
                            Id = item.Id,
                            PrintableConfigurationId = item.PrintableConfigurationId,
                            TemplateName = item.PrintableReportConfiguration.TemplateName
                        });
                    }

                    return new OkObjectResult(termsConditionList);
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

        [HttpPut("Signature")]
        [Authorize]
        public async Task<IActionResult> UpdateSignature([FromBody] Signature dynamicContent, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    await _termsAndConditionRepository.UpdateSignature(dynamicContent);
                    return Ok();

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

        [HttpDelete("Signature/{Id}")]
        [Authorize]
        public async Task<IActionResult> DeleteSignature(int Id, [FromHeader] string mdhash, [FromHeader] int User)
        {
            if (await _mdHashValidator.ValidateMdHashAsync(mdhash, User))
            {
                try
                {
                    await _termsAndConditionRepository.DeleteSignature(Id);
                    return Ok();
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
