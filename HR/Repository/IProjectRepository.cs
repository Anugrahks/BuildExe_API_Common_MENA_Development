using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface IProjectRepository
    {
       Task <IEnumerable<Project >> Get(int companyid, int Branchid,int type);
        Task<IEnumerable<Project>> Get(int Employeeid);
        Task<string> GetProjectsForSiteExpense(int companyid, int branchId);
        Task<string> GetProjectsForSiteExpenses(int companyid, int branchId, int SiteManager);

    }
}
