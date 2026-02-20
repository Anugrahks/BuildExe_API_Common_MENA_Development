using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public interface IBlockRepository
    {
       Task < IEnumerable<Block >> Get();
        Task<IEnumerable<Block>> Get(int companyid, int Branchid);
        Task<IEnumerable<Block>> Getuser(int companyid, int Branchid, int UserId);
        Task<Block> GetByID(int id);
        Task<IEnumerable<Validation>> Insert(Block block );
        Task<IEnumerable<Validation>> Delete(int id,int userid);
        Task<IEnumerable<Validation>> Update(Block block);
        void Save();
    }
}
