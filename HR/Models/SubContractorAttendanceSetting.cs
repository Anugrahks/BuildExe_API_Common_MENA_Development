using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeHR.Models
{
    public class SubContractorAttendanceSetting
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string WorkName { get; set; }
        public DateTime DateOrdered { get; set; }
        public int SubContractorId { get; set; }
        public int WorkTypeId { get; set; }
        public int ProjectId { get; set; }
        public int DivisionId { get; set; }

        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }
        public string Description { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 WorkStatus { get; set; }
        public Int16 UserId { get; set; }

        public int ContractorId { get; set; }
        public List<AttendanceSettingDetails> AttendanceSettingDetails { get; set; }
    }
    public class AttendanceSettingDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AttendanceSettingDetailsId { get; set; }
        public int SubContractorAttendanceSettingId { get; set; }
        public int LabourWorkId { get; set; }
        public decimal WorkRate { get; set; }
        public decimal OTRate { get; set; }
    }
}
