using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
namespace BuildExeMaterialServices.Repository
{
    public interface IFinishedGoodsRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<FinishedGoods> FinishedGoods);
        Task<IEnumerable<Validation>> Update(IEnumerable<FinishedGoods> FinishedGoods);
        Task<string> Get(int CompanyId, int BranchId);
        Task<string> GetbyID(int Id);
        //Task<IEnumerable<Validation>> PostData(IEnumerable<MaterialIssue> issue);
    }
}
