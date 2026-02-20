using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
namespace BuildExeMaterialServices.Repository
{
    public interface IAdvanceBalanceRepository
    {
       Task <IEnumerable< AdvanceBalance >> GetDetail(int CompanyId, int Branchid, int SupplierId ,int Projectid);
        Task<string>  Get(int CompanyId,int Branchid,  int SupplierId, int ProjectId);
        Task<string> WithProject(int CompanyId, int Branchid, int SupplierId, int ProjectId);

    }
}
