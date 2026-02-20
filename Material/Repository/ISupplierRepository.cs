using BuildExeMaterialServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildExeMaterialServices.Repository
{
  public  interface ISupplierRepository
    {
        Task<string> GetSuppliers(int CompanyId, int Branchid, int IsServiceCreditors);

        Task<string> Getwithfinancial(int CompanyId, int Branchid, int FinancialYearId);
        Task<string> Get(int CompanyId, int Branchid, int UserId);
        Task<string> GetReport(int CompanyId, int Branchid, int Reportid);
        Task<Supplier> GetByID(int id);
        Task<IEnumerable<Validation>> Insert(Supplier supplier);
        Task<IEnumerable<Validation>> Delete(int id,int UserId);
        Task<IEnumerable<Validation>> Update(Supplier supplier);
        void Save();
        Task<IEnumerable<Validation>> CheckTransactonExists(int supplierid);
        Task<IEnumerable<Validation>> CheckEditDelete(int supplierid);
        Task<string> GetSuppplierQuotation(int ProjectId, int CompanyId, int BranchId);

    }
}
