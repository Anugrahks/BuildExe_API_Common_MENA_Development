using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
   public interface IAdditionalBillRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<AdditionalBill > additionalBills );
        Task<IEnumerable<Validation>> Update(IEnumerable<AdditionalBill> additionalBills);
        Task<IEnumerable<AdditionalBill>> GetbyID(int Id);


        Task<IEnumerable<AdditionalBill>> Get();
        
        Task<IEnumerable<AdditionalBill>> Get(int companyid, int branchid);
        Task<IEnumerable<Validation>> Delete(int Idworkorder, int userid) ;
        Task<IEnumerable<AdditionalBillList >> Getforapproval(int companyId, int branchid, int UserID, int FinancialYearId);
        Task<IEnumerable<AdditionalBillList>> GetforEdit(int companyId, int branchid);
        Task<IEnumerable<AdditionalBillList>> GetforEdituser(int companyId, int branchid, int UserId, int FinancialYearId);
        Task<IEnumerable<AdditionalBillDetails >> Getdetailst(int Id);
        Task<string> GetReport(BillSearch billSearch );
        Task<IEnumerable<AdditionalBillList>> Getforview(BillSearch billSearch);
        Task<string> GetRetention(int Id, int TypeId);
        Task<string> GetLabel(int BranchId);
        Task<string> GetRetPer(int BranchId);


    }
}
