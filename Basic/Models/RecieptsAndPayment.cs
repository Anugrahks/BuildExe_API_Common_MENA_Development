using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace BuildExeBasic.Models
{
    [Keyless]
    public class RecieptsAndPayment
    {
        public int? Type { get; set; }
        public String? TypeName { get; set; }
        public int? ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public int? HeadId { get; set; }
        public string? Particular { get; set; }
        public decimal? Amount { get; set; }
        public int? SlNo { get; set; }
    }
}
