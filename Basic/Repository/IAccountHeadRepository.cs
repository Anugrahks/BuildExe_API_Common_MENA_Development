using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
namespace BuildExeBasic.Repository
{
   public  interface IAccountHeadRepository
    {
       Task<IEnumerable<AccountHead >> Get(int CompanyId, int Branchid);
        Task<IEnumerable<AccountHead>> Getuser(int CompanyId, int Branchid, int UserId);
        Task<IEnumerable<AccountHead>> Getjournal(int CompanyId, int Branchid);
        Task<IEnumerable<AccountHead>> Get(int CompanyId, int Branchid,int accountTypeId);
        Task<IEnumerable<AccountHead>> GetByID(int departmentId);
        Task<IEnumerable<Validation>> Insert(AccountHead accountHead );
        Task  Delete(int Id,int UserId);
        Task<IEnumerable<Validation>> Update(AccountHead accountHead );

        Task<string> GetaccountBalance(string Type, int HeadId, int companyId, int Branchid, int FinancialYearId);

        Task<IEnumerable<Validation>> CheckEditDelete(int id);
        Task<IEnumerable<AccountHead>> GetAll (int CompanyId, int Branchid);

        Task<IEnumerable<AccountHead>> GetWithSuppliers(int companyId, int branchid);

        Task<string> GetdebitCredit(int creditid, int debitid, int financialyearid);

        Task<IEnumerable<AccountHead>> GetDetails(int AccountHeadId, int CompanyId, int BranchId);
        Task<string> GetAdvancepayment(int AccountHeadId, int SupplierId, int Type, int CategoryId, int JournalType, int ProjectId, int financialyearid, int Id);
        Task<string> Getledgermerge(int Branchid, int AccountTypeId);

        Task<IEnumerable<AccountHead>> GetByBranch(int BranchId);
    }
}
