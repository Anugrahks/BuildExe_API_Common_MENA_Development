using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
   public interface ISubContractorWorkOrderRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<SubContractorWorkOrder > subContractorWorkOrder);
        Task<IEnumerable<Validation>> Update(IEnumerable<SubContractorWorkOrder> subContractorWorkOrder);
        Task< IEnumerable<SubContractorWorkOrder>> GetbyID(int Id);

        Task<IEnumerable<SubContractorWorkOrder>> Get(int companyid,int Branchid );
        Task<IEnumerable<Validation>> Delete(int id,int userid);
        Task<IEnumerable<SubContractorWorkOrder>> Getbyproject(int ProjectId, int UnitId, int BlockId, int FloorId,int SubContractorId, int DivisionId);
        Task<IEnumerable<SubContractorWorkOrder>> Getbyproject_Completed(int ProjectId, int UnitId, int BlockId, int FloorId, int SubContractorId);
        Task<IEnumerable<SubContractorWorkOrder>> GetbyattendanceType(int ProjectId, int UnitId, int BlockId, int FloorId, int SubContractorId,int AttendanceType);
        Task<IEnumerable<SubContractorWorkOrderList >> GetforEdit(int CompanyId, int Branchid);

        Task<IEnumerable<SubContractorWorkOrderList>> GetforEdituser(int CompanyId, int Branchid, int UserId, int FinancialYearId);
        Task<IEnumerable<SubContractorWorkOrderList>> GetforApproval(int CompanyId, int Branchid, int userId, int FinancialYearId);
        Task<string> GetDetailsbyid(int IndentId);
        Task<string> Getjson(HRSearch hRSearch);
        Task<IEnumerable<SubContractorWorkOrderList>> Getforview(HRSearch hRSearch);
        Task<IEnumerable<SubContractorWorkOrder>> GetDetails(int projectId, int unitId, int blockId, int floorId, int subcontractorId, int workcategoryId, int workNameId, int DivisionId);
        Task<IEnumerable<SubContractorWorkOrder>> GetDetails(int ProjectId, int UnitId, int BlockId,
    int FloorId, int SubContractorId);

        Task<IEnumerable<SubContractorWorkOrderList>> getapproved(int companyid, int branchid, int UserId, int FinancialYearId);
    }
}
