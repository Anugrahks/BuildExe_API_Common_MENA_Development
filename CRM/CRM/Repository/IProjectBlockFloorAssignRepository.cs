using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public interface IProjectBlockFloorAssignRepository
    { 
       Task<IEnumerable<ProjectBlockFloorAssign>> Get();
        Task<IEnumerable<ProjectBlockFloorAssign>> GetByID(int id);
        Task<string> Getbycompany(int Companyid, int Branchid);
        Task<string> Getbycompanyuser(int Companyid, int Branchid, int UserId);
        Task<IEnumerable<ProjectBlockFloorAssignList>> get(int Companyid, int Branchid);
        Task<IEnumerable<ProjectBlockFloorAssignList>> getuser(int Companyid, int Branchid, int UserId);
        Task<IEnumerable<ProjectBlockFloorAssign>> getFloor(int projectid, int blockid);
        Task<IEnumerable<Validation>> Insert(IEnumerable<ProjectBlockFloorAssign> projectBlockFloorAssign);
        Task<IEnumerable<Validation>> Delete(int id,int Blockid, int UserId);
        Task<IEnumerable<Validation>> Update(IEnumerable<ProjectBlockFloorAssign> projectBlockFloorAssign);
        Task<IEnumerable<Validation>> CheckEditDelete(int id, int type);
    }
}
