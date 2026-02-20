using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
namespace BuildExeBasic.Repository
{
    public interface IGeneralLedgerRepository
    {
        Task<IEnumerable<GeneralLedger>> GeneralLedger(BasicSearch basicSearch);
        Task<IEnumerable<GeneralLedger>> PersonalLedger(BasicSearch basicSearch);
        Task<string> LedgerMeerging(BasicSearch basicSearch);
        Task<IEnumerable<GeneralLedger>> GroupSubGroupLedger(BasicSearch basicSearch);
    }
}
