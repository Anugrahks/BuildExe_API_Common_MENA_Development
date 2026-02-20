using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace BuildExeMaterialServices.Models
{
    public class IndentPurchase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }
        public string? SupplierId { get; set; }
        public int WorkCategoryId { get; set; }
        public int WorkNameId { get; set; }
        public DateTime Date { get; set; }
        public int MaterialType { get; set; }
    }
}