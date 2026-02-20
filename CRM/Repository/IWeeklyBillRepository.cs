using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
   public interface IWeeklyBillRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<WeeklyBill > weeklyBills );
        Task<IEnumerable<Validation>> Update(IEnumerable<WeeklyBill> weeklyBills);
        Task<IEnumerable<Validation>> Delete(int id, int userid);
        Task<IEnumerable<WeeklyBill>> GetbyID(int Id);
        Task<IEnumerable<WeeklyBill>> GetLastBill(int ProjectId, int UnitId, int BlockId, int FloorId);

        Task<string> GetBillDetailsBasedOnProject(int projectId, int unitId, int blockId, int floorId, int id);
        Task<IEnumerable<WeeklyBillDetailsList>> GetSpec(int projectId, int unitId, int blockId, int floorId);
        Task<IEnumerable<WeeklyBillList >> Getforapproval(int companyId, int branchid, int UserID, int FinancialYearId);
        Task<IEnumerable<WeeklyBillList>> GetforEdit(int companyId, int branchid);
        Task<IEnumerable<WeeklyBillList>> GetforEdituser(int companyId, int branchid, int UserId, int FinancialYearId);
        Task<long> GetAutoNo(int projectId, int unitId, int blockId, int floorId);
        Task<IEnumerable<Validation>> Validation(int projectId, int blockId, int floorId, int unitId);
        Task<string> GetWeeklyBillByBillNo(IEnumerable<WeeklyBillByDates>  weeklyBillByDates);
        Task<int> GetNextBillNoFrom(IEnumerable<WeeklyBillByDates> weeklyBillByDates);
    }
}
