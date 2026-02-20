using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
namespace BuildExeBasic.Repository
{
    public interface IChequeClearenceRepository
    {
        Task<IEnumerable<ChequeClearence > >Get(int CompanyId, int Branchid);
        Task<IEnumerable<ChequeClearence>> Getuser(int CompanyId, int Branchid, int UserId, int FinancialYearId);
        Task<IEnumerable<ChequeClearence>> GetByID(int Id);
        Task  Delete(int Id ,int UserId);
        Task<IEnumerable<Validation>> Update(ChequeClearence chequeClearence );
        Task<IEnumerable<ChequeClearence>> GetByType(int companyId, int branchid, string chequeType);
        Task<IEnumerable<ChequeClearence>> GetByClearenceStatus(int companyId, int branchid, int ClearenceStatus, DateTime FromDate, DateTime ToDate, string ChequeType);
    }
}
