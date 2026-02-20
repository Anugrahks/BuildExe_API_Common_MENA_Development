using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace BuildExeBasic.Models
{
    public class EmailSmsWhatsappActivation
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public string? MenuName { get; set; }
        public int EmailSave { get; set; }
        public int EmailApproval { get; set; }
        public int WhatsAppSave { get; set; }
        public int WhatsAppApproval { get; set; }
        public int SmsSave { get; set; }
        public int SmsApproval { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public string? Content { get; set; }
        public int IsActive { get; set; }
        public string? TemplateId { get; set; }
        public int UserId { get; set; }
        public int ReportTemplateId { get; set; }


    }
}
