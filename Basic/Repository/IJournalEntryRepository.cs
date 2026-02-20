using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
namespace BuildExeBasic.Repository
{
   public interface IJournalEntryRepository
    {

        Task<IEnumerable<JournalList>> GetForEdit(int CompanyId, int Branchid);
        Task<IEnumerable<JournalList>> GetForEdituser(int CompanyId, int Branchid, int UserId, int FinancialYearId);
        Task<IEnumerable<JournalList>> GetForApproval(int CompanyId, int Branchid,int UserID, int FinancialYearId);
        Task<string> Getdetails(int Id);
        Task<IEnumerable<Validation>> Insert(IEnumerable<Journal> journalEntry);
        Task<string> Getvouchers(int CompanyId, int Branchid);
        Task<IEnumerable<Validation>> Delete(int Id,int UserId);
        Task<IEnumerable<Validation>> Update(IEnumerable<Journal> journalEntry );
        Task<string> Report(BasicSearch basicSearch);
    }

}
