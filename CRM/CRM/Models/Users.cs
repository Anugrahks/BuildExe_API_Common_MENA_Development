using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeServices.Models
{
    public class Users
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserGroupId { get; set; }
        public string FullName { get; set; }
        public string Active { get; set; }
        public string EmailId { get; set; }
        public string Mobile { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int SiteUser { get; set; }
        public int IsAdmin { get; set; }
        public int SuperUser { get; set; }
        public int YearClosingRights { get; set; }
        public int DashBoardPrivilege { get; set; }
        public int Sitemanager { get; set; }
        public int SitemanagerId { get; set; }

        public int CentralizedUserEnquiry { get; set; }

        public int CentralizedUserProject { get; set; }

        public int BranchAdmin { get; set; }

        public int EmployeeUser { get; set; }

        public int PersonalLedgerPermission { get; set; }

        public int EmployeeMasterPermission { get; set; }

        public string DepartmentIds { get; set; }

        public string DepartmentName { get; set; }

        public int Client { get; set; }

        public string ClientNameId { get; set; }

        public string ClientName { get; set; }

        public int NormalUser { get; set; }




        public List<UserAssignedProject> UserAssignedProject { get; set; }
    }

    public class UserAssignedProject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }


    }
}
