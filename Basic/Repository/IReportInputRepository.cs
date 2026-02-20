using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
namespace BuildExeBasic.Repository
{
    public interface IReportInputRepository
    {
        Task<string> Get(int CompanyId, int Branchid);
        Task<IEnumerable<Validation>> Insert(ReportInput reportInput);
        Task<IEnumerable<Validation>> Delete(int Id, int UserId);
        Task<IEnumerable<Validation>> Update(ReportInput reportInput);

        Task<string> InsertPunching(Punching reportInput);

        Task<IEnumerable<Validation>> InsertReminder(IEnumerable<Reminder> reminders);
        Task<IEnumerable<Validation>> UpdateReminder(IEnumerable<Reminder> reminders);
        Task<string> GetReminder(int CompanyId, int Branchid, int UserId);
        Task<IEnumerable<Validation>> DeleteReminder(int ID, int UserID);

    }

}
