using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeBasic.Models
{
    public class Company
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int CompanyTypeID { get; set; }
        public string CompanyAddress1 { get; set; }
        public string CompanyAddress2 { get; set; }
        public string Post { get; set; }
        public string Pin { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public string TINNo { get; set; }
        public string CSTNo { get; set; }
        public int IsBranch { get; set; }
        public int ParentCompanyid { get; set; }

        public int Status { get; set; }
        public int Curr_prec { get; set; }
        public string TaxType { get; set; }
        public int GrossSalary { get; set; }
        public int bulkwagepayment { get; set; }
        public int sitemanagerbalance { get; set; }
        public string? CurrencyName { get; set; }
        public string? CurrencyShortName { get; set; }
        public string? UnitName { get; set; }
        public string? Symbol { get; set; }
    }
}
