
using BuildExeBasic.Library;
using BuildExeBasic.Models;
using BuildExeBasic.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Transactions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuildExeBasic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetaAuthController : ControllerBase
    {
        private readonly IMetaAuthRepository _metaRepository;
        private readonly IConfiguration _config;

        public MetaAuthController(IMetaAuthRepository metaRepository, IConfiguration config)
        {
            _metaRepository = metaRepository ?? throw new ArgumentNullException(nameof(metaRepository));
            _config = config;
        }

        [HttpGet("Callback")]
        public async Task<IActionResult> Callback([FromQuery] string code, [FromQuery] string state = null)
        {
            try
            {
                if (string.IsNullOrEmpty(code))
                    return BadRequest(new { message = "No code provided by Meta." });

                var userToken = await _metaRepository.ExchangeCodeForTokenAsync(code);
                var pages = await _metaRepository.GetPagesAsync(userToken);

                var result = new List<object>();

                foreach (var page in pages)
                {
                    string pageId = page["id"]?.ToString();
                    string pageToken = page["access_token"]?.ToString();
                    string pageName = page["name"]?.ToString();

                    var leadForms = await _metaRepository.GetLeadFormsAsync(pageId, pageToken);
                    var formsWithLeads = new List<object>();

                    foreach (var form in leadForms)
                    {
                        string formId = form["id"]?.ToString();
                        string formName = form["name"]?.ToString();
                        var leads = await _metaRepository.GetLeadsAsync(formId, pageToken);

                        formsWithLeads.Add(new
                        {
                            FormId = formId,
                            FormName = formName,
                            Leads = leads
                        });
                    }

                    result.Add(new
                    {
                        PageId = pageId,
                        PageName = pageName,
                        Forms = formsWithLeads
                    });
                }

                return Ok(new { userToken, Pages = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("LoginUrl")]
        public IActionResult LoginUrl()
        {
            var appId = _config["MetaAuth:AppId"];
            var redirectUri = _config["MetaAuth:RedirectUri"];
            var scopes = "ads_read,leads_retrieval,pages_show_list"; // adjust as needed
            var state = Guid.NewGuid().ToString(); // optional CSRF token

            var url = $"https://www.facebook.com/v20.0/dialog/oauth" +
                      $"?client_id={appId}" +
                      $"&redirect_uri={Uri.EscapeDataString(redirectUri)}" +
                      $"&scope={Uri.EscapeDataString(scopes)}" +
                      $"&response_type=code" +
                      $"&state={state}";

            return Ok(new { loginUrl = url });
        }
    }
}
