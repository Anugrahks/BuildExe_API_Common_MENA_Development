using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
   public interface IProjectMasterRepository
    {
        Task<IEnumerable<Project>> Getproject();
        Task<IEnumerable<Project>> GetprojectByID(int projectId);
        Task<IEnumerable<Project>> GetprojectByDepartment(int Departmentid);
        Task<IEnumerable<ProjectList>> GetProjectsForGeneralInvoice(int companyid, int branchid, int userId, int siteuser);
        Task<IEnumerable<ProjectList>> GetProjectsForPartBill(int companyid, int branchid, int paymentModeId, int userId, int siteuser);

        Task<int> ExecuteStoredProc(Project project);
        Task<IEnumerable<Validation>> Insertproject(Project project);
        Task<IEnumerable<Validation>> Deleteproject(int projectId, int userId, int DivisionId);
        Task<IEnumerable<Validation>> Updateproject(Project project);
        Task<IEnumerable<Validation>> UpdateStatus(int ProjectId,int DivisionId,Project project);
        void Save();
        Task<string> GetReport(ProjectSearch projectSearch );
        Task<IEnumerable<ProjectList>> Getproject(int companyid, int branchid, int userId, int siteuser);
        Task<IEnumerable<ProjectList>> Getproject(int companyid, int branchid);
        Task<IEnumerable<ProjectList>> GetAllproject(int companyid, int branchid);
        Task<IEnumerable<ProjectList>> GetAllproject(int companyid, int branchid, int userId, int siteuser);
        Task<IEnumerable<ProjectList>> GetprojectList(int companyid, int branchid);
        Task<string> GetprojectListuser(int companyid, int branchid, int UserId);
        Task<IEnumerable<ProjectList>> Getproject_withStage(int companyid, int branchid);
        Task<IEnumerable<ProjectList>> Getproject_ForRefund(int Typee, int companyid, int branchid);
        Task<IEnumerable<ProjectList>> Getproject(int companyid, int branchid,int paymentModeId);
        Task<IEnumerable<ProjectList>> Getproject_by_type(int companyid, int branchid, string ProjetTypeid);
        Task<IEnumerable<ProjectList>> Getproject_by_type(int companyid, int branchid, string ProjetTypeid, int userid, int siteuser);
        Task<IEnumerable<ProjectCombo>> GetMaterialRecieveProject(int companyid, int branchid);
        Task<long> GetProjectIdValidation(int Id, string projectid, int companyid, int branchid);
        Task<IEnumerable<ProjectList>> GetprojectBySchedule(int companyid, int branchid, int scheduletype);
        Task<IEnumerable<ProjectList>> GetprojectByScheduleuser(int companyid, int branchid, int scheduletype, int UserId);
        Task<IEnumerable<ProjectList>> GetBySchedule(int companyid, int branchid);
        Task<IEnumerable<ProjectList>> getprojectwithcontractors(int companyid, int branchid, int userId, int siteuser);
        Task<IEnumerable<ProjectList>> GetProjectsForStageInvoiceuser(int companyid, int branchid, int UserId, int siteuser);
        Task<IEnumerable<ProjectList>> GetProjectsForBillReceipt(int companyid, int branchid, int userId, int siteuser);
        Task<IEnumerable<Validation>> Getproject_Validation(int Id, string Project_Id, string PRojectname, int companyid, int branchid);
        Task<IEnumerable<ProjectList>> GetProjectsForProjectSpecification(int departmentId);
        Task<IEnumerable<ProjectList>> GetProjectsForManualBoq(int companyid, int branchId);

        Task<IEnumerable<ProjectList>> getprojectwithcontractors(int companyid, int branchId);
        Task<IEnumerable<ProjectList>> GetProjectsForStageInvoice(int companyid, int branchId);
        Task<IEnumerable<ProjectList>> GetProjectsForStageInvoiceuser(int companyid, int branchId, int UserId);
        Task<IEnumerable<ProjectList>> GetProjectsForGeneralInvoice(int companyid, int branchId);
        Task<IEnumerable<ProjectList>> GetProjectsForStageReceipt(int companyid, int branchId);
        Task<IEnumerable<ProjectList>> GetProjectsForStageReceipt(int companyid, int branchid, int userId, int siteuser);
        Task<IEnumerable<ProjectList>> GetProjectsForPartBill(int companyid, int branchid, int paymentModeId);
        Task<IEnumerable<ProjectList>> GetProjectsForBillReceipt(int companyid, int branchid);
        Task<IEnumerable<ProjectList>> GetOwnProjects(int companyid, int branchId);
        Task<IEnumerable<ProjectList>> GetProjectsForRateEvaluation(int companyid, int branchId);
        Task<IEnumerable<ProjectList>> GetProjectsForRateEvaluationuser(int companyid, int branchId, int UserId, int FinancialYearId);
        Task<IEnumerable<ProjectList>> GetProjectsForRateComparison(int companyid, int branchId);
        Task<IEnumerable<ProjectList>> GetProjectsForRateComparisonuser(int companyid, int branchId, int UserId, int FinancialYearId);
        Task<IEnumerable<ProjectList>> GetProjectsForWeeklyBill(int companyid, int branchId);
        Task<IEnumerable<ProjectList>> GetProjectsForRefunding(int companyid, int branchId, int type);
        Task<IEnumerable<ProjectList>> GetProjectsForAssignBlockFloors(int companyid, int branchId);
        Task<IEnumerable<ProjectList>> GetProjectsForClientAdvance(int companyid, int branchId);
        Task<string> GetProjectsForLabourInProject(int companyid, int branchId);
        Task<string> GetProjectsForLabourInProjectnew(int companyid, int branchId, int UserId, int SiteUser);
        Task<IEnumerable<ProjectList>> GetProjectsForDocument(int companyid, int branchId);
        Task<IEnumerable<ProjectList>> GetProjectsForEst(int companyid, int branchId);
        Task<IEnumerable<Validation>> CheckEditDelete(int id, int divisionId, int isEdit);
        Task<IEnumerable<ProjectCombo>> GetMaterialRecieveProject(int companyid, int branchid, int userId, int siteuser);
        Task<string> GetProjectsForLabourInProjectSetting(int companyid, int branchId, int UserId, int SiteUser, int Status);
        Task<string> GetClient(int BranchId);
        Task<string> StatusName(int CompanyId, int BranchId, int DivisionId);
        Task<string> StatusNameList(int CompanyId, int BranchId, int ProjectId);

        Task<string> GetProjectsForLabourInProjectsitemanager(int companyid, int branchId, int UserId, int SiteUser, int SitemanagerId);
        Task<IEnumerable<ProjectList>> GetAllClientproject(int companyid, int branchid, string clientName);
    }
}
