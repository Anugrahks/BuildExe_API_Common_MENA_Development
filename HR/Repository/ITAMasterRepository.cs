using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface ITAMasterRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<TAMaster> attendances);
        Task Delete(int Id, int UserID);
        Task<IEnumerable<Validation>> Update(IEnumerable<TAMaster> attendances);
       
        Task<string> Get(int CompanyId, int BranchId);

        Task<IEnumerable<Validation>> InsertDailyNotes(IEnumerable<DailyNotesMaster> attendances);
        Task DeleteDailyNotes(int Id, int UserID);
        Task<IEnumerable<Validation>> UpdateDailyNotes(IEnumerable<DailyNotesMaster> attendances);

        Task<string> GetDailyNotes(int CompanyId, int BranchId, int UserId, int FinancialYearId);

        Task<string> GetById(int Id);
        Task<string> GetforApproval(int CompanyId, int BranchId, int UserId, int FinancialYearId);
        Task<string> GetforEdit(int CompanyId, int BranchId, int UserId, int FinancialYearId);

        Task<IEnumerable<Validation>> UpdateStaffTA(IEnumerable<StaffTA> attendances);

        Task DeleteStaffTA(int Id, int userId);

        Task<IEnumerable<Validation>> InsertStaffTA(IEnumerable<StaffTA> attendances);

        
        Task<string> StaffTaReport(HRSearch hRSearch);

        Task<string> GetforApprovalTAPayment(int CompanyId, int BranchId, int UserId, int FinancialYearId);
        Task<string> IndividualReport(HRSearch hRSearch);

       
        Task<string> GetforEditTAPayment(int CompanyId, int BranchId, int UserId, int FinancialYearId);

        Task<string> GetShowPayment(int Id, int EmployeeId,int BranchId, int FinancialYearId, DateTime FromDate, DateTime ToDate);

        Task<IEnumerable<Validation>> UpdateTAPayment(IEnumerable<TAPayment> attendances);

        Task DeleteTAPayment(int Id, int userId);

        Task<IEnumerable<Validation>> InsertTAPayment(IEnumerable<TAPayment> attendances);
    }
}
