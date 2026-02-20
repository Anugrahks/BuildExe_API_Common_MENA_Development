using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeServices.Models
{
    public class Company
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }

        public string CompanyCode { get; set; }
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
        public int AttendanceType { get; set; }
        public int islabourhourly {  get; set; }
        public int ismonthlyhourly { get; set; }
        public int IsDivision { get; set; }
        public int IsHide { get; set; }

        public string? WhatsappUserName { get; set; }
        public string? WhatsappPassword { get; set; }
        public int SalaryBillChange    { get; set; }

        public int LeaveForwardOption { get; set; }
        public int AutoFetchLatePenalty { get; set; }
        public int AutoFetchOverTime { get; set; }
        public decimal FetchLatePenaltyAfter { get; set; }
        public string CompanyIdName { get; set; }

        public int TAAmountoption { get; set; }

        public int ProfitSeparation { get; set; }


        public int leavesurrender { get; set; }


        public int EarlyPunchIn { get; set; }


        public int LatePenaltyCustomization { get; set; }

        public int RemoveValidationEnquiry { get; set; }

        public int MultiDayPunchIn { get; set;  }


        public  int Threshold { get; set; }

        public int PunchInOnly { get; set; }

        public int SiteLimit { get; set; }


        public decimal LimitAmount { get; set; }

        public int? BatchEnable { get; set; }

    }
}
