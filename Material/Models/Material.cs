using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeMaterialServices.Models
{
    public class Material
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string MaterialID { get; set; }
        public string MaterialName { get; set; }
        public int MaterialTypeId { get; set; }
        public int MaterialBrandId { get; set; }
        public int MaterialCategoryId { get; set; }
        public int UnitId { get; set; }
        public decimal MaterialUnitRate { get; set; }
        public decimal OpenigStock { get; set; }
        public decimal TaxPer { get; set; }
        public decimal KFCPer { get; set; }
        public decimal LandingCost { get; set; }
        public string HsnCode { get; set; }
        public string Remarks { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 UserId { get; set; }

        public string MaterialBrandNameMaster { get; set; }

        public string MaterialTypeNameMaster { get; set; }

        public string MaterialCategoryNameMaster { get; set; }

        public string UnitShortNameMaster { get; set; }

        public decimal CoefficientFactor { get; set; }

        public int CoefficientUnitId { get; set; }

        public string CoefficientUnitName { get; set; }
        public List<OpeningStock> OpeningStock { get; set; }
        public List<OpeningRent> OpeningRent { get; set; }


    }
    public class OpeningStock
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OpeningStockId { get; set; }
        public int MaterialId { get; set; }
        public int ProjectId { get; set; }
        public int Unit_Id { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }
        public decimal Stock { get; set; }
        public decimal Rate { get; set; }
        public int FinancialYearId { get; set; }
        public Int16 FirstOpening { get; set; }
        public int? DivisionId { get; set; }

        public string ChildDescription { get; set; }
    }

    public class OpeningRent
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OpeningRentId { get; set; }
        public int MaterialId { get; set; }
        public int ProjectId { get; set; }
        public int Unit_Id { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }
        public decimal Stock { get; set; }
        public decimal Rate { get; set; }
        public int FinancialYearId { get; set; }
        public Int16 FirstOpening { get; set; }
        public int SupplierId { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string TaxArea { get; set; }
        public int? DivisionId { get; set; }
    }

}
