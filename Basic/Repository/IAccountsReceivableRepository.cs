using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
namespace BuildExeBasic.Repository
{
    public interface IAccountsReceivableRepository
    {
        Task<IEnumerable<AccountsReceivable>> GetForReport(BasicSearch basicSearch);
    }
}
