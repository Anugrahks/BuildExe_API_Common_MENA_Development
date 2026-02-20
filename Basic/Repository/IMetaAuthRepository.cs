using BuildExeBasic.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace BuildExeBasic.Repository
{
    public interface IMetaAuthRepository
    {
        Task<string> ExchangeCodeForTokenAsync(string code);
        Task<JArray> GetPagesAsync(string accessToken);
        Task<JArray> GetLeadFormsAsync(string pageId, string pageAccessToken);
        Task<JArray> GetLeadsAsync(string formId, string pageAccessToken);
    }
}
