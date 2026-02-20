using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeMaterialServices.Models
{
    public class CustomerRegistration
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddres1 { get; set; }
        public string CustomerAddres2 { get; set; }
        public string Post { get; set; }
        public string Pin { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public string TINNo { get; set; }
        public string GSTNo { get; set; }


        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }
        public int UserId { get; set; }
    }
}
