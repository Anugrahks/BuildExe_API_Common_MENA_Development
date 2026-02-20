using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
   public interface IRetensionPaymentRepository
    {
        Task<string> GetAmounts(int companyid, int branchId, int userId, int menuID);
        Task<string> Getbyproject(int ProjectId, int UnitId, int BlockId, int FloorId,int SubContractorId);    
    }
}
