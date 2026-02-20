using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BuildExeServices.Models
{
    public class EnquiryBulkInsert
{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime EnquiryDate { get; set; }

        public string AttendedStaff { get; set; }
        public string ModeOfEnquiry { get; set; }
        public string EnquiryFor { get; set; }

        public string EnquiryID { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }

        public string MarketingTeam { get; set; }
        public string NextAssignedStaff { get; set; }
        public string EnquiryStatus { get; set; }

        public int CompanyId { get; set; }

        public int BranchId { get; set; }

        public int FinancialYearId  { get; set; }
        public int UserId { get; set; }

        public string? CustomerRequirement { get; set; }

        public string? UnqProspectName {get; set; }
    }
}