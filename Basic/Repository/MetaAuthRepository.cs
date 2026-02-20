using BuildExeBasic.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BuildExeBasic.Repository
{
    public class MetaAuthRepository : IMetaAuthRepository
    {
        private readonly HttpClient _http;
        private readonly MetaAuthSettings _settings;

        public MetaAuthRepository(HttpClient httpClient, IOptions<MetaAuthSettings> settings)
        {
            _http = httpClient;
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
        }

        public async Task<string> ExchangeCodeForTokenAsync(string code)
        {
            var url = $"https://graph.facebook.com/v20.0/oauth/access_token" +
                      $"?client_id={_settings.AppId}" +
                      $"&redirect_uri={Uri.EscapeDataString(_settings.RedirectUri)}" +
                      $"&client_secret={_settings.AppSecret}" +
                      $"&code={code}";

            var response = await _http.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Meta token exchange failed: {content}");
            }

            var json = JObject.Parse(content);
            return json["access_token"]?.ToString();
        }


        public async Task<JArray> GetPagesAsync(string accessToken)
        {
            var url = $"https://graph.facebook.com/v20.0/me/accounts?access_token={accessToken}";
            var response = await _http.GetStringAsync(url);
            var json = JObject.Parse(response);
            return (JArray)json["data"];
        }

        public async Task<JArray> GetLeadFormsAsync(string pageId, string pageAccessToken)
        {
            var url = $"https://graph.facebook.com/v20.0/{pageId}/leadgen_forms?access_token={pageAccessToken}";
            var response = await _http.GetStringAsync(url);
            var json = JObject.Parse(response);
            return (JArray)json["data"];
        }

        public async Task<JArray> GetLeadsAsync(string formId, string pageAccessToken)
        {
            var url = $"https://graph.facebook.com/v20.0/{formId}/leads?access_token={pageAccessToken}";
            var response = await _http.GetStringAsync(url);
            var json = JObject.Parse(response);
            return (JArray)json["data"];
        }
    }
}
