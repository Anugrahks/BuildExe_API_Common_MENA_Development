using BuildExeBasic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuildExeBasic.Repository
{
    public interface IEmailConfigurationRepository
    {

        Task<EmailConfiguration> Post(EmailConfiguration configuration);
        Task<BulkMailWhatsAppSMS> PostBulk(BulkMailWhatsAppSMS configuration);
        Task<BulkMailWhatsAppSMS> EditBulk(BulkMailWhatsAppSMS model);

        Task DeleteBulk(int id);

        Task<IEnumerable<BulkMailWhatsAppSMS>> GetListBulk(int CompanyId, int BranchId);

        Task<string> getDataForEmail(string FormType, int BranchId, string Ids);
        Task<String> SendEmailAsync(int CompanyId, int BranchId, int MenuId, int Id, string file, string Content);

        Task<string> SendBulkEmailAsync(BulkEmailRequest request);
        Task<String> GetContent(int MenuId, int CompanyId, int BranchId);
        Task<IEnumerable<EmailConfiguration>> GetList(int CompanyId, int BranchId);

        Task<string> WhatsappSmsFetch(int Id, int MenuId);
        Task<String> ChangeStatus(int id, bool status);
        Task<Boolean> CheckStatus(int MenuId, int CompanyId, int BranchId);
        Task<EmailConfiguration> EditEmailConfiguration(EmailConfiguration emailConfiguration);
        Task<String> GetTemplate(int MenuId, int CompanyId, int BranchId);
    }
}
