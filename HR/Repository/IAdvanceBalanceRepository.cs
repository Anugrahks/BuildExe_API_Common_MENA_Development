using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface IAdvanceBalanceRepository
    {
        Task<decimal> GetBalance(int ProjectId, int UnitId, int BlockId, int FloorId, int CompanyId,int Branchid,  int employeeId);
        Task<IEnumerable<AdvanceBalance>> Get(int ProjectId, int UnitId, int BlockId, int FloorId,int CompanyId, int Branchid, int employeeId);
        Task<string> tdsBalance(int Employeeid, int projectId, int Unitid, int BlockID, int Floorid);
        Task<string> RetensionBalance(int Employeeid, int projectId, int Unitid, int BlockID, int Floorid);
    }
}
