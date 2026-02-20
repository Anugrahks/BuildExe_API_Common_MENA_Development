using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeBasic.Models
{
    public class JournalList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime JournalDate { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 FinancialYearId { get; set; }
        public int ProjectId { get; set; }
        public int DivisionId { get; set; }
        public string ProjectName { get; set; }
        public string DivisionShortName { get; set; }
        public int UnitId { get; set; }
        public string? UnitName { get; set; }
        public int BlockId { get; set; }
        public string? BlockName { get; set; }
        public int FloorId { get; set; }
        public string? FloorName { get; set; }

        public int VoucherTypeId { get; set; }
        public int VoucherNumber { get; set; }
        public Int16 ApprovalStatus { get; set; }
        public Int16 ApprovedBy { get; set; }
        public Int16 ApprovalLevel { get; set; }
        public string Description { get; set; }
        public Int16 IsDeleted { get; set; }
        public int CategoryId { get; set; }
        public int Maxlevel { get; set; }
        public int? WorkNameId { get; set; }
        public int EnquiryId { get; set; }
        public string ProspectName { get; set; }
        public string ApprovalRemarks { get; set; }

        public string RejectRemarks { get; set; }
    }
}
