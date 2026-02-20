using BuildExeBasic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuildExeBasic.Repository
{
    public interface IEmailSmsWhatsappActivationRepository
    {
        Task<string> EmailSmsWhatsappActivation(int CompanyId, int BranchId);
        Task<IEnumerable<Validation>> Update(IEnumerable<EmailSmsWhatsappActivation> emailSmsWhatsappActivation);

        Task<IEnumerable<Validation>> Post(EmailSmsWhatsappActivation emailSmsWhatsappActivation);
        Task<string> WhatsAppConfiguration(int CompanyId, int BranchId);
        Task<string> GetByMenu(int MenuId, int CompanyId, int BranchId);
        Task<string> SavePath(string file);
        Task<IEnumerable<Validation>> PutWhatsapp(EmailSmsWhatsappActivation emailSmsWhatsappActivation);
        Task<IEnumerable<Validation>> PutStatus(EmailSmsWhatsappActivation emailSmsWhatsappActivation);
    }
}
