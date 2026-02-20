using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;

namespace BuildExeMaterialServices.Repository
{
    public interface IMaterialTransferRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<MaterialTransfer > materialTransfer);
        Task<IEnumerable<Validation>> Update(IEnumerable<MaterialTransfer> materialTransfer);
        Task< IEnumerable<MaterialTransfer>> GetbyID(int Id);

        Task<IEnumerable<MaterialTransfer>> Get(int CompanyId, int Branchid);
      //  Task Delete(int id,int UserId);
        Task<IEnumerable<Validation>> Delete(int Id, int Userid);
        Task<IEnumerable<MaterialTransferList>> GetforRecieve(int CompanyId, int Branchid, int ProjectId, DateTime ReceiveDate);
        Task<IEnumerable<MaterialTransferList >> GetforEdit(int CompanyId, int Branchid);
        Task<IEnumerable<MaterialTransferList>> GetforEdit(int companyId, int branchid, int userid, int FinancialYearId,int IsAsset);
        Task<IEnumerable<MaterialTransferList>> GetforView(MaterialSearch materialSearch);
        Task<IEnumerable<MaterialTransferList>> GetforApproval(int CompanyId, int Branchid, int userId, int FinancialYearId,int IsAsset);
        Task<string> GetDetailsbyid(int IndentId);
        Task<string> GetforReport(MaterialSearch materialSearch);
    }
}
