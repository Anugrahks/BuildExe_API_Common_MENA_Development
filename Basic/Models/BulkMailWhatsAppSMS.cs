using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeBasic.Models
{
    public class BulkMailWhatsAppSMS
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }

        public int UserId { get; set; }

        public string? Remarks { get; set; }
        public int IsDeleted { get; set; }

        public string? ProspectIds { get; set; }
        public string? ClientIds { get; set; }
        public string? SupplierIds { get; set; }
        public string? LabourIds { get; set; }
        public string? GroupLabourIds { get; set; }
        public string? ForemanIds { get; set; }
        public string? SubcontractorIds { get; set; }
        public string? ContractorIds { get; set; }
        public string? EntryUserIds { get; set; }
        public string? EmployeeIds { get; set; }

        public int? EmailTemplateId { get; set; }
        public string? EmailTemplateName { get; set; }

        public int? WhatsappTemplateId { get; set; }
        public string? WhatappTemplateName { get; set; }

        public int? SmsTemplateId { get; set; }
        public string? SmsTemplateName { get; set; }

    }
}
