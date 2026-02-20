using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
namespace BuildExeBasic.Repository
{
    public interface IEmailSmsWhatsappRepository
    {
        Task<IEnumerable<EmailSmsWhatsappList>> getactivated(int CompanyId, int BranchId);
        Task<IEnumerable<Validation>> Update(EmailSmsWhatsapp emailsmswhatsapp);
        Task<IEnumerable<EmailSmsWhatsappList>> CheckStatus(int MenuId, int CompanyId, int BranchId);
    }
}
