using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;

namespace BuildExeMaterialServices.Repository
{
  public  interface IPurchaseReturnRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<PurchaseReturn > purchasereturn);
        Task<IEnumerable<Validation>> Update(IEnumerable<PurchaseReturn> purchasereturn);
        Task<IEnumerable<PurchaseReturn>> GetbyID(int Id);
        Task<IEnumerable<PurchaseReturn>> Get(int CompanyId, int Branchid);
        Task Delete(int id,int userid);
        Task<IEnumerable<PurchaseReturnList >> GetforEdit(int CompanyId, int Branchid);
        Task<IEnumerable<PurchaseReturnList>> GetforEdit(int companyId, int branchid, int userId, int FinancialYearId, int json);
        Task<IEnumerable<PurchaseReturnList>> GetforApproval(int CompanyId, int Branchid, int userId, int FinancialYearId, int json);
        Task<string> GetDetailsbyid(int IndentId);
        Task<string> GetforReport(MaterialSearch materialSearch);
        Task<IEnumerable<PurchaseReturnList>> Getforview(MaterialSearch materialSearch);
        int GenerateNextDebitNoteNo(int BranchId, int FinancialYearId);

        Task<string> PurchaseBillDetails(int Id);
        Task<string> PurchaseReturnBillDetails(int Id);
        Task<string> PurchaseBillInPurchase(int SupplierId, int FinancialYearId);

        Task<string> PurchaseBillInPurchaseReturn(int SupplierId, int FinancialYearId);

        Task<string> PurchaseBillInPurchaseReturn(int SupplierId, int FinancialYearId, int ProjectId);
    }
}
