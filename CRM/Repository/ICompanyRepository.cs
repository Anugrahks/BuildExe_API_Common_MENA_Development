using BuildExeServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildExeServices.Repository
{
   public  interface ICompanyRepository
    {
        Task <IEnumerable<Company>> Getcompany();
        Task<IEnumerable<Company>> Getlogincompany();

        Task<IEnumerable<Company>> Getlogincompanynothidden();
        Task<IEnumerable<Company>> GetloginBranch(int companyid);
        Task<Company> GetCompanyByID(int CompanyId);
         Task<IEnumerable<Company>> GetBranchBycompanyid(int companyid);
        Task<IEnumerable<Validation>> InsertCompany(Company Company);
        Task<IEnumerable<Validation>> DeleteCompany(int CompanyId);
        Task  UpdateCompany(Company Company);

        Task<Dictionary<string, object>> SiteLimit(int branchid);
        void Save();
        Task<IEnumerable<Validation>> CheckEditDelete(int CompanyId);
        Task<Dictionary<string, string>> CurrencyName(int companyId);
        Task<Dictionary<string, int>> attendancetype(int companyId);
        Task <IEnumerable<Company>> GetBranchByCompany(int companyId);

        Task<Dictionary<string, int>> BatchEnable(int companyId);
    }
}
