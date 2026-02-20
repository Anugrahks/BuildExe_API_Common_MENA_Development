using BuildExeBasic.Models;
using BuildExeBasic.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
namespace BuildExeBasic.DBContexts
{
    public class BasicContext : DbContext
    {
        public BasicContext(DbContextOptions<BasicContext> options) : base(options)
        {
        }
        public DbSet<AccountHead> tbl_AccountHead { get; set; }
        public DbSet<Bank> tbl_Banks { get; set; }
        public DbSet<WorkCategory> tbl_WorkCategoryMaster { get; set; }
        public DbSet<JournalList> tbl_Journallist { get; set; }
        public DbSet<ChequeClearence> tbl_PendingChequeMaster { get; set; }
        public DbSet<Sitemanager> tbl_SitemanagersTransactions { get; set; }
        public DbSet<SitemanagerList> tbl_SitemanagersTransactionsList { get; set; }
        public DbSet<DocumentGroup> tbl_DocumentGroup { get; set; }
        public DbSet<DocumentType> tbl_DocumentType { get; set; }
        public DbSet<DocumentManagement> tbl_DocumentManagement { get; set; }
        public DbSet<UserLogs> tbl_UserLogs { get; set; }
        public DbSet<account_type_> tbl_AccountType { get; set; }
        public DbSet<account_group_> tbl_AccountGroup { get; set; }
        public DbSet<AccountSubGroup> tbl_AccountSubGroup { get; set; }
        public DbSet<FinancialYear> tbl_FinancialYear { get; set; }
        public DbSet<Company> tbl_Companies { get; set; }
        public DbSet<Godown> tbl_GodownMaster { get; set; }
        public DbSet<Alert> tbl_Alerts { get; set; }
        public DbSet<Alertfew> tbl_Alertsfew { get; set; }
        public DbSet<AlertCount> tbl_Alertscount { get; set; }
        public DbSet<AlertCountNew> tbl_Alertscountnew { get; set; }
        public DbSet<ReportField> tbl_ReportFields { get; set; }
        public DbSet<ReportFilter> tbl_ReportFilter { get; set; }
        public DbSet<ReportConfiguration> tbl_ReportConfiguration { get; set; }
        public DbSet<ConfigurationFieldDetails> tbl_ReportConfigurationFieldDetails { get; set; }
        public DbSet<ConfigurationFilterDetails> tbl_ReportConfigurationFilterDetails { get; set; }
        public DbSet<DayBook> tbl_Daybook { get; set; }
        public DbSet<DayBookSummary> tbl_DaybookSummary { get; set; }
        public DbSet<GeneralLedger> tbl_GeneralLedger { get; set; }
        public DbSet<RecieptsAndPayment> tbl_RecieptsAndPayment { get; set; }
        public DbSet<TrialBalance> tbl_TrialBalance { get; set; }
        public DbSet<Expense> tbl_Expense { get; set; }
        public DbSet<ExpenseDetail> tbl_Expensedetail { get; set; }
        public DbSet<ProjectAnalysis> tbl_ProjectAnalysis { get; set; }
        public DbSet<ProjectAnalysisDetail> tbl_ProjectAnalysisdetail { get; set; }
        public DbSet<ProjectAnalysisDetail_Datewise> tbl_ProjectAnalysisdetail_Datewise { get; set; }
        public DbSet<Validation> tbl_validation { get; set; }
        public DbSet<Header> tbl_Header { get; set; }
        public DbSet<Content> tbl_Content { get; set; }
        public DbSet<Footer> tbl_Footer { get; set; }
        public DbSet<ApprovalRemarks> tbl_Remarks { get; set; }
        public DbSet<DocumentFiles> tbl_DocumentFiles { get; set; }
        public DbSet<PrintableReportConfiguration> tbl_PrintableReportConfiguration { get; set; }
        public DbSet<PrintableReportConfigurationList> tbl_PrintableReportConfigurationList { get; set; }
        public DbSet<PrintableReportFields> tbl_PrintableReportFields { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrder { get; set; }
        public DbSet<PurchaseOrderDetails> PurchaseOrderDetails { get; set; }
        //public DbSet<PrintableReportConfigurationImage> tbl_PrintableReportConfigurationImage { get; set; }
        public DbSet<Partbill> Partbill{ get; set; }
        public DbSet<PartbillDetails> PartbillDetails { get; set; }
        public DbSet<PrintableReportImageConfiguration> tbl_PrintableReportConfigurationImage { get; set; }
        public DbSet<TermsAndConditons> tbl_TermsAndConditionMaster { get; set; }
        public DbSet<TermsAndConditonDetails> tbl_TermsAndConditionDetails { get; set; }

        public DbSet<DynamicContent> tbl_DynamicContentMaster { get; set; }

        public DbSet<DynamicContentDetails> tbl_DynamicContentDetails { get; set; }

        public DbSet<Signature> tbl_SignatureMaster { get; set; }

        public DbSet<SignatureDetails> tbl_SignatureDetails { get; set; }

        
        public DbSet<PurchaseOrderReprintModelView> tbl_PurchaseOrderReprintModelView { get; set; }
        public DbSet<ListReportName> tbl_Menu { get; set; }
        public DbSet<PrintableReportFilter> tbl_printableReportFilter { get; set; }
        public DbSet<EmailConfiguration> tbl_EmailConfiguration { get; set; }

        public DbSet<BulkMailWhatsAppSMS> tbl_BulkMailWhatsAppSMS { get; set; }
        public DbSet<EmailRecieverModel> tbl_EmailReciever { get; set; }
        public DbSet<GeneralAlert> tbl_GeneralAlerts { get; set; }
        public DbSet<Generalalertdoc> tbl_Generalalertdoc { get; set; }
        public DbSet<FundFlowSummary> tbl_FundFlowSummary { get; set; }
        public DbSet<EmailSmsWhatsappList> tbl_EmailSmsWhatsappList { get; set; }
        public DbSet<AccountsPayable> tbl_AccountsPayable { get; set; }
        public DbSet<AccountsReceivable> tbl_AccountsReceivable { get; set; }
        public DbSet<ProfitandLoss> tbl_ProjectAndLoss { get; set; }
        public DbSet<BalanceSheet> tbl_BalanceSheet { get; set; }
        public DbSet<ProjectExpenseDetail> tbl_ProjectExpenseDetail { get; set; }
        public DbSet<ProjectDetailExpense> tbl_ProjectDetailExpense { get; set; }
        public DbSet<PrintableReportCSS> tbl_PrintableReportStyle { get; set; }
        public DbSet<Batch> tbl_Batch { get; set; }

        public DbSet<EmployeeMaster> tbl_EmployeeMaster { get; set; }
    }
}