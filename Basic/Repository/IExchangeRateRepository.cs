using BuildExeBasic.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuildExeBasic.Repository
{
    public interface IExchangeRateRepository
    {

        Task<IEnumerable<Validation>> Insert(IEnumerable<ExchangeRate> exchange);

        Task<IEnumerable<Validation>> Update(IEnumerable<ExchangeRate> exchange);

        Task<IEnumerable<Validation>> Delete(int id, int userId);

        Task<string> Get(int CompanyId, int BranchId);

        Task<string> GetExchangeRate(int ParentCurrencyId, int TargetCurrencyId, DateTime Date);


        Task<string> GetUpdateExchangeRate(int CompanyId, int BranchId);

        Task<IEnumerable<Validation>> InsertExchangeRate(IEnumerable<ExchangeRate> exchange);


    }
}
