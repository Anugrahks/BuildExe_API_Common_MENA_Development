using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuildExeBasic.Models
{
    [Keyless]
    public class GeneralAlert
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string AlertName { get; set; }
        public string Description { get; set; }
        public DateTime? RenewDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime ReminderBaseDate { get; set; }
        public int TriggeringDay { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public int IsDeleted { get; set; }

        public List<Generalalertdoc> Generalalertdoc { get; set; }
    }
    public class Generalalertdoc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int docId { get; set; }
        public int AlertId { get; set; }
        public string DocumentName { get; set; }
        public string Document { get; set; }

    }

}

