using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
namespace BuildExeBasic.Repository
{
    public interface ITrialBalanceRepository
    {
        Task<IEnumerable<TrialBalance>> TrialBalance(BasicSearch basicSearch);

        Task<IEnumerable<ProfitandLoss>> ProfitAndLoss(BasicSearch basicSearch);
        Task < IEnumerable<BalanceSheet>> BalanceSheet(BasicSearch basicSearch);
    }
}
