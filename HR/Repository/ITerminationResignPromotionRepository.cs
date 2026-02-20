
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface ITerminationResignPromotionRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<TerminationResignPromotion> EmployeeJoining);
        Task<IEnumerable<Validation>> Update(IEnumerable<TerminationResignPromotion> EmployeeJoining);
        Task<IEnumerable<Validation>> Delete(int id, int Userid);
        Task<IEnumerable<TerminationResignPromotion>> GetbyID(int Idworkorder);

        Task<string> GetforEdit(int CompanyId, int BranchId, int UserId, int FinancialYearId);

        Task<string> GetforApproval(int CompanyId, int BranchId, int UserId, int FinancialYearId);

        Task<string> Document(int Id);


    }
}
