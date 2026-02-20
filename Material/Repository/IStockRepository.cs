using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
namespace BuildExeMaterialServices.Repository
{
    public interface IStockRepository
    {
        Task Insert(IEnumerable<Stock > stock);
        Task Update(IEnumerable<Stock> stock);
        Task<IEnumerable<Stock>> GetbyID(int Id);
        Task<IEnumerable<Stock>> Get();
        Task Delete(int id);
        Task<string> StockReport(StockSearch stockSearch);
    }
}
