using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public interface ILevelRepository
    {
       Task< IEnumerable<Level >> Get();
        Task<Level> GetByID(int id);
       Task<IEnumerable<Level>> Getforcompany(int Companyid,int branchid);
        int Selectmaxlevel(int menuid, int companyid, int branchid);
        Task<string> Getmenuwithlevel(int companyid, int branchid);
        Task<IEnumerable<Validation>> Insert(IEnumerable<Level> level );
        Task Delete(int Companyid,int BranchId);
        Task<IEnumerable<Validation>> Update(IEnumerable<Level> level);
        void Save();
    }
}
