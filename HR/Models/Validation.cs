using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace BuildExeHR.Models
{
    [Keyless]
    public class Validation
    {

        public int? StatusCode { get; set; }
        public int? Id { get; set; }
        public String? Status { get; set; }
        public String? ErrorMessage { get; set; }
        public String? ErrorType { get; set; }
    }
}
