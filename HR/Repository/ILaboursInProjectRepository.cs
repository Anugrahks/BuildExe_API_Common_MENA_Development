using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models; 
namespace BuildExeHR.Repository
{
   public  interface ILaboursInProjectRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<LaboursInProject > holiday);
        Task<IEnumerable<Validation>> Update(IEnumerable<LaboursInProject> holiday);
        Task<IEnumerable<LaboursInProject>> GetbyID(int Id);
        Task<IEnumerable<LaboursInProject>> Get();
        Task <string> Get(int CompanyId,int BranchId);
        Task<string> Getuser(int CompanyId, int BranchId, int UserId);
        Task<IEnumerable<Validation>> Delete(int Id, int UserId);

        Task< IEnumerable<EmployeeInProject>> GetEmplyeeInProject(LaboursInProject laboursInProject);
        Task<IEnumerable<Validation>> CheckEditDelete(int projectId, int blockId, int floorId, int unitId);

        Task<string> EmployeeWise(HRSearch laboursInProject);


    }
}
