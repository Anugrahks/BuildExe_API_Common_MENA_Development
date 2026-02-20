using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeMaterialServices.Models
{
    public class ItemQuotation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string QuotationNumber { get; set; }
        public string QuotationType { get; set; }
        public DateTime  ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public int ProjectId { get; set; }
        public int MaterialId { get; set; }
        public decimal Quantity { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public int IndentId { get; set; }
     
        public Int16  IsDeleted { get; set; }
        public Int16 UserId { get; set; }
        
    }
}
