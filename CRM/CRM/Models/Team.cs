using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{
    public class Team
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      
        public int Id { get; set; }
        public string TeamName { get; set; }
        public Int16  CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 EntryUserId { get; set; }

        public int Restricted { get; set; }
        public List<TeamDetails> TeamDetails { get; set; } 

    }
    public class TeamDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeamDetailsId { get; set; }
        public int TeamId { get; set; }
        public Int16 UserId { get; set; }
    }


}
