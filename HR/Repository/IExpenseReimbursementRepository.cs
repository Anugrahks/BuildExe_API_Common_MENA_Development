using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface IExpenseReimbursementRepository
    {

        Task<IEnumerable<Validation>> Insert(IEnumerable<ExpenseReimbursement> items);
        Task<IEnumerable<Validation>> Update(IEnumerable<ExpenseReimbursement> items);

        Task<string> GetById(int id, int companyId, int branchId);
        Task<string> GetAll(int companyId, int branchId, int financialYearId);
       Task<IEnumerable<Validation>> Delete(int id, int userId);
    }
}