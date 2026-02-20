using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
   public interface IAdvanceBalanceRepository
    {
        Task<IEnumerable<AdvanceBalance>> GetDetail(int ProjectId, int Unitid, int BlockID,int Floorid);
        Task<string> Get(int ProjectId, int Unitid, int BlockID, int Floorid);

        Task<string> tdsBalance(int projectId, int Unitid, int BlockID, int Floorid);
    }
}
