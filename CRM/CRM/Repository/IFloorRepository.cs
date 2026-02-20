using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public interface IFloorRepository
    {
        Task<IEnumerable<Floor >> Get();
        Task<Floor> GetByID(int id);
        Task<IEnumerable<Floor>> Get(int companyid,int Branchid);
        Task<IEnumerable<Floor>> Getuser(int companyid, int Branchid, int UserId);
        Task<IEnumerable<Validation>> Insert(Floor floor);
        Task<IEnumerable<Validation>> Delete(int id, int userid);
        Task<IEnumerable<Validation>> Update(Floor floor);
        void Save();
        Task<IEnumerable<Validation>> CheckEditDelete(int id, int projectId, int blockId, int floorId);
    }
}
