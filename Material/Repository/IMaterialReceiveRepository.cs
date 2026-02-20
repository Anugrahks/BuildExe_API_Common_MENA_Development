using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
namespace BuildExeMaterialServices.Repository
{
    public interface IMaterialReceiveRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<MaterialReciept > materialReciept);
        Task<IEnumerable<Validation>> Update(IEnumerable<MaterialReciept> materialReciept);
        Task<IEnumerable<MaterialReciept>> GetbyID(int Id);

        Task<IEnumerable<MaterialReciept>> Get(int CompanyId, int Branchid);
        Task<IEnumerable<Validation>> Delete(int id,int UserId);

        Task<IEnumerable<MaterialRecieveList>> GetforEdit(int CompanyId, int Branchid);
        Task<IEnumerable<MaterialRecieveList>> GetforEdit(int companyId, int branchid, int userid, int FinancialYearId,int IsAsset);
        Task<IEnumerable<MaterialRecieveList>> GetforApproval(int CompanyId, int Branchid, int userId, int FinancialYearId,int IsAsset);
        Task<string> GetDetailsbyid(int IndentId);
        Task<string> GetDetailWithTransferQtyid(int MaterialRecieptId);
        Task<string> GetforReport(MaterialSearch materialSearch);
        Task<IEnumerable<MaterialRecieveList>> GetforView(MaterialSearch materialSearch);
    }
}
