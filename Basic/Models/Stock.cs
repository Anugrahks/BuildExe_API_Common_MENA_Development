using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeBasic.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public string  Form { get; set; }
        public int MasterId { get; set; }
        public DateTime Entrydate { get; set; }
        public int MaterialId { get; set; }
        public int ProjectId { get; set; }
        public int Unit_Id { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }
        public decimal  StockIn { get; set; }
        public decimal StockOut { get; set; }
        public decimal Rate { get; set; }
        public int FinancialYearId { get; set; }
      
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        

    }
}
