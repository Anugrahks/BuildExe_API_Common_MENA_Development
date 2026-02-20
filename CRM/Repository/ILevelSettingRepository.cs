using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
   public interface ILevelSettingRepository
    {
       Task< IEnumerable<LevelSetting >> Get();
        Task<IEnumerable<LevelSetting>> GetByID(int id);
        string Getjson(int companyid, int branchid);
        Task<IEnumerable<LevelSetting>> GetByID(int id, int companyid, int branchid);
        Task<IEnumerable<Validation>> Insert(IEnumerable<LevelSetting > level);
        Task<IEnumerable<Validation>> Delete(int id, int companyid, int branchid, int userId);
        Task<IEnumerable<Validation>> Update(IEnumerable<LevelSetting> level);
       
    }
}
