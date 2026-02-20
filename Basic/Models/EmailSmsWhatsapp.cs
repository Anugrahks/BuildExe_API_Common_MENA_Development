using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeBasic.Models
{
    public class EmailSmsWhatsapp
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int MenuId { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public Boolean EmailId { get; set; }
        public Boolean WhatsappNumber { get; set; }
        public Boolean Sms { get; set; }

    }


    public class EmailSmsWhatsappList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int MenuId { get; set; }
        public string Menuname { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public Boolean EmailId { get; set; }
        public Boolean WhatsappNumber { get; set; }
        public Boolean Sms { get; set; }

        public Boolean IsActive { get; set; }

    }
}
