using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
   public interface IRefundBalanceRepository
    {
        Task<IEnumerable<RefundBalance>> GetDetail(int type,int ProjectId, int Unitid, int BlockID, int Floorid);
        Task<decimal> Get(int type,int ProjectId, int Unitid, int BlockID, int Floorid);
    }
}
