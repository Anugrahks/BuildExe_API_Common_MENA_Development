using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
namespace BuildExeBasic.Repository
{
    public interface ISuperAdminRepository
    {
        Task<string> Insert(int MenuId, int BranchId, int FinancialYearId, DateTime FromDate, DateTime ToDate);
        Task<string> Delete(int MenuId, int Id);
        Task<string> Check(int MenuId, int Id);
        Task<string> approvechange(int MenuId, int Id);
    }
}
