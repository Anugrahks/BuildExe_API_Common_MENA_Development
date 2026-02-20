using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BuildExeServices.Models;

namespace BuildExeServices.DBContexts
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }
        public DbSet<Unit> tbl_Units { get; set; }
        public DbSet<Product> Products { get; set; }
        //  public DbSet<Category> Categories { get; set; }
        public DbSet<Company> tbl_Companies { get; set; }
        public DbSet<EmployeeDesignation> tbl_EmployeeDesignation { get; set; }
        public DbSet<EnquiryMode> tbl_EnquiryMode { get; set; }
        public DbSet<EmployeeDepartment> tbl_EmployeeDepartment { get; set; }
        public DbSet<Branch> tbl_Branch { get; set; }
        public DbSet<Employee> tbl_EmployeeMaster { get; set; }
        public DbSet<Enquiry> tbl_Enquiry1 { get; set; }
        public DbSet<EnquiryForMobile> tbl_Enquiry { get; set; }
        public DbSet<EnquiryList> tbl_Enquirylist { get; set; }
        public DbSet<EnquiryReport> tbl_EnquiryReport { get; set; }

        public DbSet<LoginLog> tbl_LoginLog { get; set; }
        
        public DbSet<Followup> tbl_Followup { get; set; }
        public DbSet<FollowUpList> tbl_FollowupList { get; set; }
        public DbSet<Department> tbl_Departments { get; set; }
        public DbSet<Project> tbl_ProjectMaster { get; set; }
        public DbSet<ProjectList> tbl_ProjectMasterlist { get; set; }
        public DbSet<ProjectCombo> tbl_ProjectMastercombo { get; set; }
        public DbSet<ProjectReport> tbl_ProjectMasterReport { get; set; }
        public DbSet<FinancialYear> tbl_FinancialYear { get; set; }
        public DbSet<UserGroup> tbl_UserGroup { get; set; }
        public DbSet<Users> tbl_Users { get; set; }
        public DbSet<EnquiryBulkInsert> tbl_EnquiryBulkInsert { get; set; }
        
        public DbSet<UserAssignedProject> tbl_UserAssignedProject { get; set; }
        public DbSet<Level> tbl_Level { get; set; }
        public DbSet<LevelSetting> tbl_LevelSetting { get; set; }
        public DbSet<EnquiryFor> tbl_EnquiryFor { get; set; }
        public DbSet<EnquiryStatus> tbl_EnquiryStatus { get; set; }
        public DbSet<Team> tbl_TeamMaster { get; set; }
        public DbSet<TeamDetails> tbl_TeamDetails { get; set; }

        public DbSet<Floor> tbl_Floors { get; set; }
        public DbSet<Block> tbl_Block { get; set; }

        public DbSet<ProjectBlockFloorAssign> tbl_Project_BlockFloorAssign { get; set; }
        public DbSet<OwnProject> tbl_OwnProjectDetails { get; set; }
        public DbSet<OwnProjectList> tbl_OwnProjectDetailsList { get; set; }
        public DbSet<ConsultancyWork> tbl_ConsultancyWorkMaster { get; set; }
        public DbSet<ProjectConsultancy> tbl_ProjectConsultancyDetails { get; set; }
        public DbSet<ProjectStage> tbl_ProjectWorkStage { get; set; }
        public DbSet<ProjectBooking> tbl_ClientMaster { get; set; }
        public DbSet<UserLogs> tbl_UserLogs { get; set; }
        public DbSet<OwnProjectType> tbl_OwnProjectType { get; set; }
        public DbSet<ProjectPaymentMode> tbl_ProjectPaymentMode { get; set; }
        public DbSet<ProjectDivision> tbl_ProjectDivision { get; set; }

        public DbSet<WorkType> tbl_WorkType { get; set; }
        public DbSet<WorkCategory> tbl_WorkCategoryMaster { get; set; }
        public DbSet<SpecificationMaster> tbl_SpecificationMaster { get; set; }
        public DbSet<SpecificationDetails> tbl_SpecificationDetails { get; set; }
        public DbSet<SpecificationDetailsList> tbl_SpecificationDetailslist { get; set; }
        public DbSet<ProjectSpecificationMaster> tbl_ProjectSpecificationMaster { get; set; }
        public DbSet<ProjSpecificationDetails> tbl_ProjectSpecificationDetails { get; set; }
        public DbSet<Template> tbl_TemplateMaster { get; set; }
        public DbSet<TemplateDetail> tbl_TemplateDetails { get; set; }
        public DbSet<TemplateDetailList> tbl_TemplateDetailsList { get; set; }

        public DbSet<ClientAdvance> tbl_ClientAdvanceMaster { get; set; }
        public DbSet<ClientAdvanceList> tbl_ClientAdvanceMasterList { get; set; }
        public DbSet<ClientAdvanceReport> tbl_ClientAdvanceMasterReport { get; set; }
        public DbSet<AdditionalBill> tbl_AdditionalBillMaster { get; set; }
        public DbSet<AdditionalBillList> tbl_AdditionalBillMasterList { get; set; }
        public DbSet<AdditionalBillReport> tbl_AdditionalBillReport { get; set; }
        public DbSet<AdditionalBillDetails> tbl_AdditionalBillDetails { get; set; }
        public DbSet<PartBillMaster> tbl_PartBillMaster { get; set; }
        public DbSet<PartBillList> tbl_PartBillMasterList { get; set; }
        public DbSet<PartBillReport> tbl_PartBillMasterReport { get; set; }
        public DbSet<PartBillDetails> tbl_PartBillDetails { get; set; }
        public DbSet<PartBillDetailsList> tbl_PartBillDetailsList { get; set; }
        public DbSet<PercentageBill> tbl_PercentageBill { get; set; }
        public DbSet<PercentageBillReport> tbl_PercentageBillReport { get; set; }
        public DbSet<Reciept> tbl_Reciepts { get; set; }
        public DbSet<RecieptsList> tbl_RecieptsList { get; set; }
        public DbSet<RecieptDetail> tbl_RecieptDetails { get; set; }
        public DbSet<PendingClientBills> tbl_PendingClientBills { get; set; }
        public DbSet<ClientMaster> tbl_getClientMaster { get; set; }
        public DbSet<UserProject> tbl_UserProject { get; set; }
        public DbSet<TendorSubmitted> tbl_TendorSubmitted { get; set; }
        public DbSet<TendorAnalysis> tbl_TenderAnalysis { get; set; }
        public DbSet<TendorNegotiation> tbl_TendorNegotiation { get; set; }
        public DbSet<Refund> tbl_Refund { get; set; }
        public DbSet<RefundList> tbl_RefundList { get; set; }
        public DbSet<TendorWorkOrder> tbl_TendorWorkOrder { get; set; }
        public DbSet<CoApplicant> tbl_CoApplicant { get; set; }
        public DbSet<GovtProject> tbl_TenderMaster { get; set; }
        public DbSet<Menu> tbl_Menu { get; set; }
        public DbSet<UserPrevilege> tbl_UserPrivilege { get; set; }

        public DbSet<RateEvaluation> tbl_Rate_evaluation { get; set; }
        public DbSet<RateEvaluationDetails> tbl_Rate_evaluationdetails { get; set; }

        public DbSet<BoqMaster> tbl_BoqMaster { get; set; }
        public DbSet<BoqDetails> tbl_BoqDetails { get; set; }
        public DbSet<BoqMasterList> tbl_BoqMasterList { get; set; }
        public DbSet<BoqDetailsList> tbl_BoqDetailsList { get; set; }
        public DbSet<WeeklyBill> tbl_WeeklyBill { get; set; }
        public DbSet<WeeklyBillDetails> tbl_WeeklyBillDetails { get; set; }
        public DbSet<WeeklyBillDetailsList> tbl_WeeklyBillDetailsList { get; set; }
        public DbSet<WeeklyBillList> tbl_WeeklyBillList { get; set; }
        public DbSet<ProjectBookingCancellation> tbl_ProjectBookingCancellation { get; set; }
        public DbSet<Validation> tbl_validation { get; set; }
        public DbSet<ProjectBlockFloorAssignList> tbl_Project_BlockFloorAssignList { get; set; }
        public DbSet<AdvanceBalance> tbl_advanceBalance { get; set; }
        public DbSet<SpecificationNegotiation> tbl_SpecificationNegotiation { get; set; }
        public DbSet<RefundBalance> tbl_RefundBalance { get; set; }
        public DbSet<WorkName> tbl_WorkName { get; set; }
        public DbSet<TeamsUsers> tbl_TeamUsers { get; set; }
        public DbSet<ReportHeaderSettings> tbl_ReportHeaderSettings { get; set; }

        public DbSet<WaterMarkSetting> tbl_WaterMarkSettings { get; set; }
        public DbSet<Division> tbl_Division { get; set; }

        public DbSet<Batch> tbl_Batch { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<Category>().HasData(
            //    new Category
            //    {
            //        Id = 1,
            //        Name = "Electronics",
            //        Description = "Electronic Items",
            //    },
            //    new Category
            //    {
            //        Id = 2,
            //        Name = "Clothes",
            //        Description = "Deresses",
            //    },
            //    new Category
            //    {
            //        Id = 3,
            //        Name = "Grocery",
            //        Description = "Grocery Items",
            //    }
            //    );
        }
    }
}
