using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
   public interface IEmployeeDesignationRepository
    {
        Task<IEnumerable<EmployeeDesignation>> GetDesignation(int Companyid,int branchid,int Categoryid);
        Task<string> Get(int companyid, int BranchId);
        Task<string> Get(int companyid, int BranchId, int UserId);
        Task<EmployeeDesignation> GetDesignationByID(int DesignationId);
        Task<IEnumerable<Validation>> InsertDesignation(EmployeeDesignation designation);
        Task DeleteDesignation(int DesignationId,int userid);
        Task<IEnumerable<Validation>> UpdateDesignation(EmployeeDesignation designation);
        void Save();
        Task<IEnumerable<Validation>> DeleteValidation(int id);
        Task<IEnumerable<Validation>> CheckEditDelete(int id);
        Task<string> GetWorkHours(int companyid, int branchid, int Categoryid);
    }
}
