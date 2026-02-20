using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface IEmployeeListRepository
    {
       Task <IEnumerable<EmployeeListPersonalLedger>> Get(int companid, int branchid, int categoryId);
        Task<IEnumerable<EmployeeList>> GetByProj(int companid, int branchid, int projectId, int unitId, int blockId, int floorId, int sitemanager, int sitemanagerid);
        Task<IEnumerable<EmployeeList>> Getsiteuser(int companid, int branchid, int categoryId, int sitemanager, int sitemanagerid);
        Task<IEnumerable<EmployeeList>> Getsiteuser(int companid, int branchid);
        Task<IEnumerable<EmployeeList>> GetByProj(int companid, int branchid, int projectId, int unitId, int blockId, int floorId);
        Task<IEnumerable<EmployeeList>> Get(int DepartmentId, int DesinationId);
        Task<IEnumerable<EmployeeList>> GetEmpByProject(int companid, int branchid, int categoryId, int projectId, int sitemanager, int sitemanagerid);
        Task<IEnumerable<ListEmployeeByCategory>> Getemployeebylabourheadid(int id);
        Task<string> NumberofDaysInMonth(int CompanyId, int BranchId, int DesignationId);
        Task<string> GetReport(HRSearch hRSearch );
        Task<IEnumerable<EmployeeList>> GetByLabourGroup(int companid, int branchid, int LabourGroupId);
        Task<IEnumerable<EmployeeList>> Get(int companid, int branchid, int projectId, int unitId, int blockId, int floorId);
        Task<IEnumerable<EmployeeList>> GetEmpByProject(int companid, int branchid, int categoryId, int projectId);

        Task<IEnumerable<ListEmployeeByCategory>> GetEmployeeListById(int employeeCategoryId, int EmployeeLabourGroupId, int CompanyId, int BranchId);
        Task<IEnumerable<EmployeeList>> GetAdvance(int companid, int branchid, int categoryId, int ProjectId);
    }
}
