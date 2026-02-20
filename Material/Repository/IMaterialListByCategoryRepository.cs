using BuildExeMaterialServices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuildExeMaterialServices.Repository
{
    public interface IMaterialListByCategoryRepository
    {


        Task<IEnumerable<MaterialListByCategory>> Get(int companyId, int branchid, int materialtypeid, int materialcategoryid);

        Task<IEnumerable<MaterialListByCategory>> Get(int companyId, int branchid, int materialtypeid, int materialcategoryid, int materialbrandid);

    }
}
