using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{
    public class Menu
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string Component { get; set; }
        public int Rootlevel { get; set; }
        public int RootMenuId { get; set; }
        public int ModuleId { get; set; }
        public int HaveApprovalLevel { get; set; }
        public int IsActive { get; set; }
        public int HasPrint { get; set; }
        public int HasView { get; set; }
        public int HasDelete { get; set; }
        public string route { get; set; }


    }
}
