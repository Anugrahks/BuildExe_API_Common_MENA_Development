using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeBasic.Models
{
    [Table("tbl_Batch")]
    public class Batch
    {
        public long Id { get; set; }
        public Guid GuId { get; set; }
        public int SitemanagerId { get; set; }
        public string BatchNo { get; set; }
        public bool Status { get; set; }
        public bool CloseState { get; set; }
        public short CompanyId { get; set; }
        public short BranchId { get; set; }
        public short FinancialYearId { get; set; }
        public short UserId { get; set; }

        [NotMapped]
        public bool BatchValidate { get; set; }
        public EmployeeMaster SiteManager { get; set; }
    }

}

