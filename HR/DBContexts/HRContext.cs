using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BuildExeHR.Models;
namespace BuildExeHR.DBContexts
{
    public class HRContext : DbContext
    {
        public HRContext(DbContextOptions<HRContext> options) : base(options)
        {

        }
        public DbSet<EmployeeDepartment> tbl_EmployeeDepartment { get; set; }
        public DbSet<EmployeeDesignation> tbl_EmployeeDesignation { get; set; }
        public DbSet<WorkingHours> tbl_Workinghours { get; set; }


        public DbSet<LatePenaltyCustomization> tbl_LatePenaltyCustomization { get; set; }
        public DbSet<Holiday> tbl_Holiday { get; set; }
        public DbSet<EmployeeMaster> tbl_EmployeeMaster { get; set; }
        public DbSet<EmployeeJoiningModel> tbl_EmployeeJoiningModel { get; set; }
        public DbSet<SalaryItemHead> tbl_SalaryItemHead { get; set; }
        public DbSet<EmployeeSalaryHead> tbl_EmployeeSalaryHead { get; set; }
        public DbSet<LaboursInProject> tbl_LaboursInProjects { get; set; }
        public DbSet<AttendancePunching> tbl_AttendancePunching { get; set; }
        public DbSet<LaboursInProjectDetail> tbl_LaboursInProjectDetails { get; set; }
        public DbSet<Attendance> tbl_AttendanceMaster { get; set; }
        public DbSet<AttendanceList> tbl_AttendanceMasterList { get; set; }
        public DbSet<AttendanceDetail> tbl_AttendanceDetail { get; set; }
        public DbSet<LabourWorkRate> tbl_LabourWorkRate { get; set; }
        public DbSet<ForemanWorkOrder> tbl_ForemanWorkOrderMaster { get; set; }
        public DbSet<ForemanWorkOrderDetails> tbl_ForemanWorkOrderDetails { get; set; }
        public DbSet<ForemanWorkBill> tbl_ForemanBillMaster { get; set; }
        public DbSet<ForemanWorkBillList> tbl_ForemanBillMasterList { get; set; }
        public DbSet<ForemanWorkBillDetails> tbl_ForemanBillDetails { get; set; }
        public DbSet<SubContractorAttendanceSetting> tbl_SubContractorAttendanceSettingMaster { get; set; }
        public DbSet<AttendanceSettingDetails> tbl_SubContractorAttendanceSettingDetails { get; set; }
        public DbSet<SubContractorAttendance> tbl_SubContractorAttendanceMaster { get; set; }
        public DbSet<SubContractorAttendanceList> tbl_SubContractorAttendanceMasterList { get; set; }
        public DbSet<AttendanceDetails> tbl_SubContractorAttendanceDetails { get; set; }
        public DbSet<SubContractorWorkOrder> tbl_SubContractorWorkOrderMaster { get; set; }
        public DbSet<SubContractorWorkOrderList> tbl_SubContractorWorkOrderMasterList { get; set; }
        public DbSet<SubContractorWorkOrderDetails> tbl_SubContractorWorkOrderDetails { get; set; }
        public DbSet<SubContractorBill> tbl_SubContractorBillMaster { get; set; }
        public DbSet<SubContractorPreviousSubBill> tbl_SubContractorPreviousSubBill { get; set; }
        public DbSet<SubContractorBillList> tbl_SubContractorBillMasterList { get; set; }
        public DbSet<SubContractorBillDetails> tbl_SubContractorBillDetails { get; set; }
        public DbSet<SubContractorAdditionalBillDetails> tbl_SubContractorAdditionalBillDetails { get; set; }
        public DbSet<ContractorAdditionalBillDetails> tbl_ContractorAdditionalBillDetails { get; set; }
        public DbSet<ContractorWorkOrder> tbl_ContractorWorkOrderMaster { get; set; }
        public DbSet<ContractorWorkOrderList> tbl_ContractorWorkOrderMasterList { get; set; }
        public DbSet<ContractorWorkOrderDetails> tbl_ContractorWorkOrderDetails { get; set; }
        public DbSet<ContractorStageSetting> tbl_ContractorStageSetting { get; set; }
        public DbSet<LabourAdvanceMaster> tbl_LaboursAdvanceMaster { get; set; }
        public DbSet<LabourAdvanceMasterList> tbl_LaboursAdvanceMasterList { get; set; }
        public DbSet<UserLogs> tbl_UserLogs { get; set; }
        public DbSet<LeaveMaster> tbl_LeaveMaster { get; set; }
        public DbSet<ForemanPayment> tbl_ForemanPayment { get; set; }
        public DbSet<ForemanPaymentList> tbl_ForemanPaymentList { get; set; }
        public DbSet<ForemanForPayment> tbl_ForemanForPayment { get; set; }
        public DbSet<ForemanForPaymentAbstract> tbl_ForemanForPaymentAbstract { get; set; }
        public DbSet<ForemanPaymentDetails> tbl_ForemanPaymentDetails { get; set; }
        public DbSet<SubContractorPayment> tbl_SubcontractorPayment { get; set; }
        public DbSet<SubContractorForPayment> tbl_SubcontractorForPayment { get; set; }
        public DbSet<SubContractorPaymentList> tbl_SubcontractorPaymentList { get; set; }
        public DbSet<SubContractorPaymentDetails> tbl_SubContractorPaymentDetails { get; set; }
        public DbSet<ContractorPayment> tbl_ContractorPayment { get; set; }
        public DbSet<ContractorPaymentList> tbl_ContractorPaymentList { get; set; }
        public DbSet<ContractorForPayment> tbl_ContractorForPayment { get; set; }
        public DbSet<ContractorPaymentDetails> tbl_ContractorPaymentDetails { get; set; }
        public DbSet<EmployeeList> tbl_EmployeeMasterlist { get; set; }
        public DbSet<EmployeeListPersonalLedger> tbl_EmployeeMasterlistPersonal { get; set; }
        public DbSet<Employee> tbl_Employee { get; set; }
        public DbSet<EmployeeCategory> tbl_EmployeeCategory { get; set; }
        public DbSet<SalaryPayment> tbl_SalaryPayment { get; set; }
        public DbSet<LabourWagePayment> tbl_LabourWagePayment { get; set; }
        public DbSet<LabourWagePaymentList> tbl_LabourWagePaymentList { get; set; }
        public DbSet<LabourWageForPayment> tbl_LabourWageForPayment { get; set; }
        public DbSet<LabourWagePaymentDetails> tbl_LabourWagePaymentDetails { get; set; }
        public DbSet<AttendanceMonthly> tbl_AttendanceMaster_Monthly { get; set; }
        public DbSet<AttendanceMonthlyList> tbl_AttendanceMaster_MonthlyList { get; set; }
        public DbSet<AttendanceMonthlyDetails> tbl_AttendanceMaster_Monthly_Details { get; set; }

        public DbSet<AttendanceMonthlyEmployeeDetails> tbl_AttendanceMaster_Monthly_Employee { get; set; }
        public DbSet<IndentList> tbl_IndentMasterList { get; set; }
        public DbSet<Indent> tbl_IndentMaster { get; set; }
        public DbSet<IndentDetails> tbl_IndentDetails { get; set; }
        public DbSet<Division> tbl_Division { get; set; }
        public DbSet<IndentDetailsList> tbl_IndentDetailsList { get; set; }
        public DbSet<Unit> tbl_Units { get; set; }
        public DbSet<WorkType> tbl_WorkType { get; set; }
        public DbSet<Project> tbl_ProjectMaster { get; set; }
        public DbSet<Block> tbl_Block { get; set; }
        public DbSet<Floor> tbl_Floors { get; set; }
        public DbSet<OwnProject> tbl_OwnProjectDetails { get; set; }
        public DbSet<AttendanceForApproval> tbl_AttendanceMasterForApproval { get; set; }
        public DbSet<PayrollAttendanceForApproval> tbl_PayrollAttendanceMasterForApproval { get; set; }
        public DbSet<GroupLabourWagePayment> tbl_GroupLabourWagePayment { get; set; }
        public DbSet<GroupLabourWagePaymentList> tbl_GroupLabourWagePaymentlist { get; set; }
        public DbSet<GroupLabourWagePaymentDetails> tbl_GroupLabourWagePaymentDetails { get; set; }
        public DbSet<EmployeeLabourGroup> tbl_EmployeeLabourGroup { get; set; }
        public DbSet<ForemanWorkOrderList> tbl_ForemanWorkOrderMasterList { get; set; }
        public DbSet<AdvanceBalance> tbl_advanceBalance { get; set; }
        public DbSet<EmployeeReport> tbl_EmployeeReport { get; set; }
        public DbSet<EmployeeInProject> tbl_EmployeeInProject { get; set; }
        public DbSet<FinancialYear> tbl_FinancialYear { get; set; }
        public DbSet<LoanBalance> tbl_LoanBalance { get; set; }
        public DbSet<Validation> tbl_validation { get; set; }
        public DbSet<ValidationAttendance> tbl_validationAttendance { get; set; }
        public DbSet<HolidayLeave> tbl_HolidayLeave { get; set; }
        public DbSet<HolidayLeaveList> tbl_HolidayLeavelist { get; set; }
        public DbSet<EmployeeJoining> tbl_EmployeeJoining { get; set; }

        public DbSet<EmployeeJoiningDocumentAndAlert> tbl_EmployeeJoiningDocumentAndAlerts { get; set; }

        public DbSet<EmployeeJoiningBankDetail> tbl_EmployeeJoiningBankDetails { get; set; }

        public DbSet<EmployeeJoiningFacilityDetail> tbl_EmployeeJoiningFacilityDetails { get; set; }

        public DbSet<EmployeeJoiningExpensePerDay> tbl_EmployeeJoiningExpensePerDay { get; set; }

        public DbSet<EmployeeJoiningIssueDetail> tbl_EmployeeJoiningIssueDetails { get; set; }

        public DbSet<TerminationResignPromotion> tbl_TerminationResignPromotion { get; set; }

        public DbSet<TerminationResignPromotionDocumentDetails> tbl_TerminationResignPromotionDocumentDetails { get; set; }

        public DbSet<SalaryVaryingHeadResponse> tbl_SalaryVaryingHeadResponse { get; set; }
        public DbSet<SalaryItemHeadDetails> tbl_SalaryItemHeadDetails { get; set; }
        public DbSet<SalaryHeadResponse> tbl_SalaryHeadResponse { get; set; }
        public DbSet<EmployeeJoiningList> tbl_EmployeeJoininglist { get; set; }
        public DbSet<LeaveApplication> tbl_LeaveApplication { get; set; }
        public DbSet<SalaryVaryingHeadDetails> tbl_SalaryVaryingHead { get; set; }
        public DbSet<LeaveApplicationList> tbl_LeaveApplicationList { get; set; }
        public DbSet<LeaveAccountClearence> tbl_LeaveAccountClearence { get; set; }
        public DbSet<LeaveAccountClearenceList> tbl_LeaveAccountClearenceList { get; set; }
        public DbSet<LeaveApplicationDocument> tbl_LeaveApplicationDocument { get; set; }
        public DbSet<ListEmployeeByCategory> tbl_ListEmployeeByCategory { get; set; }
        public DbSet<ListLabour> tbl_ListEmployee { get; set; }
        public DbSet<EmployeeDetail> tbl_EmployeeDetail { get; set; }
        public DbSet<AttendaceDetails> tbl_AttendaceDetails { get; set; }
        public DbSet<DefaultDate> tbl_DefaultDate { get; set; }
        public DbSet<MonthlyVaryingHeadSettingsMaster> tbl_MonthlyVaryingHeadSettingsMaster { get; set; }
        public DbSet<MonthlyVaryingHeadSettingsDetails> tbl_MonthlyVaryingHeadSettingsDetails { get; set; }

        public DbSet<TableAttendance> tbl_TableAttendance { get; set; }
        public DbSet<TableAttendanceGet> tbl_TableAttendanceget { get; set; }
        public DbSet<SubContractorRateRevision> tbl_SubContractorRateRevision { get; set; }
        public DbSet<Batch> tbl_Batch { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        base.OnModelCreating(modelBuilder);

        // Stored procedure result mapping
        modelBuilder.Entity<EmployeeListPersonalLedger>().HasNoKey();
}
    }
}
