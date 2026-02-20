using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeMaterialServices.Models
{
    public class FinishedGoods
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FinishedGoodsId { get; set; }
        public int MaterialId { get; set; }
        public decimal OtherExpenses { get; set; }
        public decimal OtherexpensesPer { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public decimal NetCost { get; set; }
        public List<RawMaterial> RawMaterial { get; set; }
        public List<LabourExpenses> LabourExpenses { get; set; }
    }

    public class RawMaterial
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int MaterialId { get; set; }
        public int RawMaterialId { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? UnitRate { get; set; }

    }

    public class LabourExpenses
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public int MaterialId { get; set; }
        public int LabourId { get; set; }
        public int Quantity { get; set; }
        public decimal? Rate { get; set; }


    }
}
