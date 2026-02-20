using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public interface IPendingClientBillsRepository
    {
        //       Task< IEnumerable<PendingClientBills >> GetPendingClientBills (int type,int projectId,int UnitId,int blockid,int FloorId);

        Task<string> GetPendingClientBills(int type, int projectId, int unitId, int blockid, int floorId, int DivisionId);
        Task<string> GetPendingClientBillsEdit(int type, int projectId, int UnitId, int blockid, int FloorId, int DivisionId,int Id);
    }
}
