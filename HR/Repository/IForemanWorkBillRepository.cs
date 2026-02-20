using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
   public interface IForemanWorkBillRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<ForemanWorkBill > foremanWorkBills );
        Task<IEnumerable<Validation>> Update(IEnumerable<ForemanWorkBill> foremanWorkBills);
        Task<IEnumerable<ForemanWorkBill>> GetbyID(int Id);

        Task<IEnumerable<ForemanWorkBill>> Get(int companyid,int branchid);
        Task<IEnumerable<Validation>> Delete(int id,int userid);
        Task<IEnumerable<ForemanWorkBillList >> GetforEdit(int CompanyId, int Branchid);

        Task<IEnumerable<ForemanWorkBillList>> GetforEdituser(int CompanyId, int Branchid, int UserId, int FinancialYearId);
        Task<IEnumerable<ForemanWorkBillList>> GetforApproval(int CompanyId, int Branchid, int userId, int FinancialYearId);
        Task<IEnumerable<ForemanWorkBill>> GetLastBill(int workorderid);
        Task<string> GetDetailsbyid(int IndentId);
        Task<long> GetmaxBillNo(int  Type, int workOrderId, int financialYearId);
        Task<string> Getjson(HRSearch hRSearch);
        Task<IEnumerable<ForemanWorkBillList>> Getforview(HRSearch hRSearch);
    }   
}
