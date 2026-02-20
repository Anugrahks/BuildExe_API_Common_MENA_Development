using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeMaterialServices.Models
{
    public class Supplier
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public string SupId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddres1 { get; set; }
        public string SupplierAddres2 { get; set; }
        public string Post { get; set; }
        public string Pin { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public string TINNo { get; set; }
        public string GSTNo { get; set; }
       
       
        public Int16  CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 FinancialYearId { get; set; }

        public string OpeningType { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal OpeningBalanceRecover { get; set; }
        public Int16 UserId { get; set; }


        public int BlackListed { get; set; }

        public int IsSupplierCreditors { get; set; }

        public int? CurrencyId { get; set; }
    }
}
