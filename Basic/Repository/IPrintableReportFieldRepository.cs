using BuildExeBasic.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuildExeBasic.Repository
{
    public interface IPrintableReportFieldRepository
    {
        Task<IEnumerable<PrintableReportFields>> GetByID(int MenuId);
        Task<PurchaseOrder> GetPurchaseOrderById(int purchaseOrderId);
        Task<IEnumerable<PurchaseOrderDetails>> GetPurchaseOrderDetailsByPurchaseOrderId(int purchaseOrderId);
        Task<Partbill> GetPartbillbyId(int partbillid);

        Task<IEnumerable<PartbillDetails>> GetPartbilldetailsByPartbillId(int partbillid);

        Task<string> GetPrintableById(int id, int Menuid);

        Task<string> GetPrintableByIdDivision(int id,int DivisionId, int Menuid);

        
        Task<string> IndividualEntryPrint(IndividualPrintable individual);
        Task<string> GetsalaryslipById(DateTime SalaryBillDate, int EmployeeId, int CompanyId, int BranchId, int MonthId, int YearId, int FinancialYearId);
        Task<string> Getforsalaryslipduration(DateTime SalaryBillDate, int EmployeeId, int CompanyId, int BranchId, int MonthId, int YearId, int FinancialYearId, int DurationId, DateTime FromDate, DateTime ToDate);
        
    }
}
