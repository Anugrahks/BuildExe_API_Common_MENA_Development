using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeHR.Models
{
    public class LaboursInProject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int EmployeeCategoryId { get; set; }
        public int EmployeeLabourGroupId { get; set; }
        public int ProjectId { get; set; }
        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int DivisionId { get; set; }
        public int FloorId { get; set; }
        public DateTime DateAssigned { get; set; }
        public Int16 UserId { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public int BatchID { get; set; }
        public string BatchNo { get; set; }
        public List<LaboursInProjectDetail> LaboursInProjectDetail { get; set; }

    }

    public class LaboursInProjectDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LaboursInProjectDetailId { get; set; }
        public int LaboursInProjectId { get; set; }
        public DateTime DateAssignedEmployee { get; set; }
        public int EmployeeId { get; set; }
  
    }
}
