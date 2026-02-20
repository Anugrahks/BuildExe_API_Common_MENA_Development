using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public interface IProjectSpecificationRepository
    {

        /*--------------------------------------------------------Rohith--------------------------------------------------------------------------------------------------------*/
        Task<string> GetEdit(int Id);

        Task<string> GetSpecDetailsById(string Ids);
        Task<IEnumerable<Validation>> Insert(IEnumerable<ProjectSpecificationMaster> projectSpecificationMaster);
        Task<IEnumerable<Validation>> Update(IEnumerable<ProjectSpecificationMaster> projectSpecificationMaster);

        Task<IEnumerable<Validation>> Delete(int id, int userid);

        Task<string> GetEstimationId(SpecificationFilters specificationFilters);
        Task<string> GetEstimationIdList(SpecificationFilters specificationFilters);

        Task<string> EstimationApproval(int ProjectId, int DivisionId, int BranchId, int UserId, int EnquiryId);

        Task<string> EstimationOrdering(int ProjectId, int DivisionId, int BranchId, int UserId, int EnquiryId);
        Task<string> EstimationApprovalPost(SpecificationFilters specificationFilters);



        Task<IEnumerable<Validation>> EstimationOrderingUpdate(IEnumerable<EstimationOrdering> specificationMasters);

        Task<IEnumerable<Validation>> approve(IEnumerable<ProjectSpecificationMaster> specificationMasters);

        Task<string> GetEstimationSubId(SpecificationFilters specificationFilters);

        Task<string> EstimationOrderingForPrint(SpecificationFilters specificationFilters);
        /*--------------------------------------------------------Rohith--------------------------------------------------------------------------------------------------------*/
        Task UpdateOneProjectSpec(IEnumerable<ProjectSpecificationMaster> projectSpecificationMaster);
        Task< IEnumerable<ProjectSpecificationMaster>> GetbyID(int Id);

        Task<IEnumerable<ProjectSpecificationMaster>> GetforPartRate(int Id);
        Task<string> Getbyproject(SpecificationFilters specificationFilters);
        Task<IEnumerable<ProjectSpecificationMaster>> Get();
        Task<IEnumerable<ProjectSpecificationMaster>> Get(int companyid, int branchid);
        Task Delete(int projectid, int UnitId, int Blockid, int floorid, int userid);
        Task<IEnumerable<Validation>> Validation(int projectid, int UnitId, int Blockid, int floorid, string estimationid);
        Task<IEnumerable<ProjectSpecificationMaster>> GetForApproval(int projectid, int UnitId, int Blockid, int floorid,int userid);
        Task<string> GetbyprojectList(int projectid, int UnitId, int Blockid, int floorid, int divisionid);
        Task<string> GetbyprojectSpec(int companyid, int branchid);
        Task<string> GetbyprojectSpecGrid(int companyid, int branchid);
        Task<string> GetbyprojectSpecGriduser(int companyid, int branchid, int UserId, int FinancialYearId);
        Task<string> getforApproval(int companyid, int branchid, int UserId, int FinancialYearId);
        Task<decimal> GetQuotedAmt(int projectId);
        Task<IEnumerable<Validation>> CheckEditDelete(int id, int unit, int block, int floor, int DivisionId, int EnquiryId, int EstimationId);
        Task<IEnumerable<Validation>> CheckProject(int projectid, int UnitId, int Blockid, int floorid);
        Task<string> GetbyprojectComp(int projectid, int UnitId, int Blockid, int floorid);
        Task<string> GetReport(SpecSearch specSearch);


        Task<string> EstimationReport(SpecSearch specSearch);

    }
}
