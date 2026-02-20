using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface IGroupLabourWagePaymentRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<GroupLabourWagePayment> labourWagePayments);
        Task<IEnumerable<Validation>> Update(IEnumerable<GroupLabourWagePayment> labourWagePayments);
        Task<IEnumerable<GroupLabourWagePayment>> GetbyID(int Id);

        Task<IEnumerable<GroupLabourWagePayment>> Get(int companyid, int Branchid);
        Task<IEnumerable<Validation>> Delete(int id, int userid);
        Task<IEnumerable<LabourWageForPayment>> GetForPayment(int CompanyId, int BranchId, int EmployeeId, int sitemanagerId, int FinancialyearId, DateTime dateto);

        Task<IEnumerable<LabourWageForPayment>> GetForPaymentList(LabourForPayment labourForPayment);

        Task<IEnumerable<GroupLabourWagePaymentList>> GetForEdit(int companyid, int Branchid);
        Task<IEnumerable<GroupLabourWagePaymentList>> GetForEdituser(int companyid, int Branchid, int UserId, int FinancialYearId);
        Task<IEnumerable<GroupLabourWagePaymentList>> GetForApproval(int companyid, int Branchid, int UserId, int FinancialYearId);
        Task<IEnumerable<GroupLabourWagePaymentList>> GetForView(HRSearch hRSearch);
        Task<string> GetDetailsbyid(int Id);
        Task<string> Getjson(HRSearch hRSearch);
        Task<IEnumerable<Validation>> ValidationCheck(int EmployeeId, int FinancialyearId, DateTime dateto);
        Task<IEnumerable<LabourWageForPayment>> PendingBillsClear(int CompanyId, int BranchId, int EmployeeId, int SitemanagerId, int FinancialyearId, DateTime Dateto);
        Task<string> GetLabel(int CompanyId, int BranchId);

    }
}
