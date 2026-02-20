using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
  public  interface ISpecificationMasterRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<SpecificationMaster > specificationMasters);
        Task<IEnumerable<Validation>> Update(IEnumerable<SpecificationMaster> specificationMasters);
        Task< IEnumerable<SpecificationMaster>> GetbyID(int Id);
        Task<IEnumerable<SpecificationMaster>> Get();
        Task<IEnumerable<SpecificationMaster>> Get(int companyid, int branchid);
        Task<IEnumerable<Validation>> Delete(int id, int userid);

        Task<string> GetforEdit(int companyId, int branchid);
        Task<string> GetforEdituser(int companyId, int branchid, int UserId);
        Task<string> GetforEdit(int companyId, int branchid,int departmentid);
        Task<IEnumerable<SpecificationDetailsList >> Getspecdetails(int specid);
        Task<IEnumerable<Validation>> CheckEditDelete(int id);
        Task<string> PostBudgetForcasting(BudgetForcasting budgetForcasting);

    }
}
