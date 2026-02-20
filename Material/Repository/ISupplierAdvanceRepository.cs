using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
namespace BuildExeMaterialServices.Repository
{
   public interface ISupplierAdvanceRepository
    {
       Task< IEnumerable<SupplierAdvance >> Get(int CompanyId, int Branchid);
        Task<IEnumerable<SupplierAdvance>> GetByID(int id);
        Task<IEnumerable<Validation>> Insert(SupplierAdvance supplierAdvance);
        Task<IEnumerable<Validation>> Delete(int id,int Userid);
        Task<IEnumerable<Validation>> Update(SupplierAdvance supplierAdvance);

        Task<IEnumerable<SupplierAdvanceList>> GetForEdit(int CompanyId, int Branchid);
        Task<IEnumerable<SupplierAdvanceList>> GetForEdituser(int CompanyId, int Branchid, int UserId, int FinancialYearId);
        Task<IEnumerable<SupplierAdvanceList>> GetForApproval(int CompanyId, int Branchid,int UserId, int FinancialYearId);
        Task<string> GetforReport(MaterialSearch materialSearch);
        // void Save();
        Task<IEnumerable<Validation>> getvalidation(SupplierAdvance supplieradvance);
    }
}
