using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
namespace BuildExeMaterialServices.Repository
{
    public interface ISupplierPaymentRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<SupplierPayment> material);
        Task<IEnumerable<Validation>> Update(IEnumerable<SupplierPayment> material);
        Task<IEnumerable<SupplierPayment>> GetbyID(int Id);
        Task<IEnumerable<SupplierPayment>> Get(int CompanyId, int Branchid);
        Task<IEnumerable<Validation>> Delete(int id, int UserId);
        Task<IEnumerable<SupplierPaymentList>> GetForEdit(int CompanyId, int Branchid);
        Task<IEnumerable<SupplierPaymentList>> GetForEdituser(int CompanyId, int Branchid, int UserId, int FinancialYearId);
        Task<IEnumerable<SupplierPaymentList>> GetForApproval(int CompanyId, int Branchid, int UserId, int FinancialYearId);
        Task<string> GetDetailsbyid(int Id);
        Task<string> GetforReport(MaterialSearch materialSearch);
        Task<IEnumerable<DebitNoteList>> getDebit(int SupplierId, int FinancialYearId);
        Task<IEnumerable<Validation>> Save(IEnumerable<DebitNote> debitNote);
        Task<IEnumerable<DebitNoteList>> getDebitEdit(int SupplierId, int FinancialYearId, int SupplierPaymentId);
        Task<string> GetforDebitNoteReport(MaterialSearch materialSearch);
    }
}
