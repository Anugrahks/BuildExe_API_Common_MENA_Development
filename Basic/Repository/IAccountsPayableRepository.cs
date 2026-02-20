using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
namespace BuildExeBasic.Repository
{
    public interface IAccountsPayableRepository
    {
        Task<IEnumerable<AccountsPayable>> GetForReport(BasicSearch basicSearch);
    }
}
