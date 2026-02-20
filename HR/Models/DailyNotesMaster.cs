using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BuildExeHR.Models
{
    public class DailyNotesMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }
        public string Remarks { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int UserId { get; set; }
        public int FinancialYearId { get; set; }

        public string? Document { get; set; }

        public string? DocumentName { get; set; }


        public string? Location { get; set; }


        public decimal Latitude { get; set; }


        public decimal Longitude { get; set; }
    }

}
