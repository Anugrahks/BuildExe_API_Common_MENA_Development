using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeHR.Models
{
    public class LabourWorkRate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Int16 SpecItemTypeId { get; set; }
        public string  LabourWorkName { get; set; }
        public int UnitId { get; set; }
        public decimal  Rate { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 UserId { get; set; }
    }
}
