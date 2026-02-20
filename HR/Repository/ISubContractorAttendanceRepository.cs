using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface ISubContractorAttendanceRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<SubContractorAttendance > subContractorAttendances );
        Task<IEnumerable<Validation>> Update(IEnumerable<SubContractorAttendance> subContractorAttendances);
        Task<IEnumerable<SubContractorAttendance>> GetbyID(int Id);

        Task<IEnumerable<SubContractorAttendance>> Get(int companyid,int branchid);
        Task Delete(int id,int userid);

        Task< IEnumerable<SubContractorAttendanceList >> GetforEdit(int CompanyId, int Branchid);

        Task<IEnumerable<SubContractorAttendanceList>> GetforEdituser(int CompanyId, int Branchid, int UserId, int FinancialYearId);
        Task<IEnumerable<SubContractorAttendanceList>> GetforApproval(int CompanyId, int Branchid, int userId, int FinancialYearId);
        Task<string> GetDetailsbyid(int IndentId);
        Task<decimal> getattendaceamount(SubContractorBill subContractorBill );
        Task<string> Getjson(HRSearch hRSearch);

        Task<string> Getjsongroup(HRSearch hRSearch);
        Task<DateTime> GetBillToDate(int workorderid);
        int GenerateNextBillNo(int mainWorkOrderNo, int SubcontractorId, int ContractorId);
    }
}
