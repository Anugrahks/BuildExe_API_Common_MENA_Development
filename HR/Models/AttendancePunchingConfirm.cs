using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeHR.Models
{
    public class AttendancePunchingConfirm
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime LogoutTime { get; set; }
        public DateTime? Delay { get; set; }
	    public decimal OtHrs { get; set; }
	    public DateTime DateWorked { get; set; }
	    public string LoginLocation { get; set; }
	    public string LogoutLocation { get; set; }
	    public string LoginPictures { get; set; }
	    public string LogoutPictures { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }

        public int ApprovalStatus { get; set; }

        public decimal CurrentSalary { get; set; }
        public decimal CurrentOT { get; set; }
        public decimal TotalWork { get; set; }
        public decimal OTAmount { get; set; }
        public decimal Wage { get; set; }

    }
}
