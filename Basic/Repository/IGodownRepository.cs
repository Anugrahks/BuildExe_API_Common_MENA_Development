using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
namespace BuildExeBasic.Repository
{
    public interface IGodownRepository
    {
       Task < IEnumerable<Godown >> Get(int CompanyId, int Branchid);
        Task<IEnumerable<Godown>> GetByID(int Id);
        Task Insert(Godown godown  );
        Task Delete(int Id,int UserId);
        Task Update(Godown godown );
        
    }
}
