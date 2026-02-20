using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static BuildExeBasic.Models.Reminder;
namespace BuildExeBasic.Models
{
    public class Reminder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int UserId { get; set; }

        public int FinancialYearId { get; set; }

        public string ReminderName { get; set; }

        public string? FromEmail { get; set; }


        public string? AppPassword { get; set; }

        public List<SoftwareReminders> SoftwareReminders { get; set; }


        public List<MailReminders> MailReminders { get; set; }


        public List<WhatsAppReminders> WhatsAppReminders { get; set; }

        public List<GsmReminders> GsmReminders { get; set; }


    }

        public class SoftwareReminders
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int DetailId { get; set; }
            public int MasterId { get; set; }

            public decimal InteruptTime { get; set; }

            public int TypeId { get; set; }
            public string Data { get; set; }

            public int OrderId { get; set; }

        }

        public class MailReminders
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int DetailId { get; set; }
            public int MasterId { get; set; }

            public decimal InteruptTime { get; set; }

            public int TypeId { get; set; }
            public string Data { get; set; }

            public int OrderId { get; set; }



        }

        public class WhatsAppReminders
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int DetailId { get; set; }
            public int MasterId { get; set; }

            public decimal InteruptTime { get; set; }

            public int TypeId { get; set; }
            public string Data { get; set; }

            public int OrderId { get; set; }



        }

        public class GsmReminders
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int DetailId { get; set; }
            public int MasterId { get; set; }

            public decimal InteruptTime { get; set; }

            public int TypeId { get; set; }
            public string Data { get; set; }

            public int OrderId { get; set; }

        }


}
