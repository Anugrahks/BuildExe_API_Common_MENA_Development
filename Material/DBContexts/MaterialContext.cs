using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BuildExeMaterialServices.Models;
namespace BuildExeMaterialServices.DBContexts
{
    public class MaterialContext : DbContext
    {
        public MaterialContext(DbContextOptions<MaterialContext> options) : base(options)
        {
        }
       public DbSet<Unit> tbl_Units { get; set; }
        public DbSet<Brand> tbl_MaterialBrand { get; set; }
        public DbSet<MaterialCategory > tbl_MaterialCategory { get; set; }
        public DbSet<Material > tbl_MaterialMaster { get; set; }
        public DbSet<Materials > tbl_MaterialMaster_combo { get; set; }
        public DbSet<MaterialList> tbl_MaterialMasterlist { get; set; }
        public DbSet<MaterialListByCategory> tbl_MaterialMasterlistByCategory { get; set; }
        public DbSet<MaterialReport> tbl_MaterialMasterReport{ get; set; }
        public DbSet<Supplier > tbl_Suppliers { get; set; }
        public DbSet<Indent > tbl_IndentMaster { get; set; }
        public DbSet<IndentList> tbl_IndentMasterList { get; set; }
        public DbSet<IndentDetails> tbl_IndentDetails { get; set; }
        public DbSet<PurchaseOrder> tbl_PurchaseOrderMaster { get; set; }
        public DbSet<PurchaseOrderList> tbl_PurchaseOrderMasterlist { get; set; }
        public DbSet<PurchaseOrderDetail> tbl_PurchaseOrderDetails { get; set; }
        public DbSet<Stock > tbl_Stock { get; set; }
        public DbSet<OpeningStock > tbl_OpeningStock { get; set; }
        public DbSet<Purchase > tbl_PurchaseMaster { get; set; }
        public DbSet<PurchaseForPayment> tbl_PurchaseMaster_ForPayment{ get; set; }
        public DbSet<PurchaseDetail> tbl_PurchaseDetails { get; set; }
        public DbSet<Voucher> tbl_VoucherMaster { get; set; }
        public DbSet<DamageStock > tbl_DamageStock { get; set; }
        public DbSet<DamageStockList> tbl_DamageStocklist { get; set; }
        public DbSet<PurchaseReturn> tbl_PurchaseReturnMaster { get; set; }
        public DbSet<PurchaseReturnList> tbl_PurchaseReturnMasterlist { get; set; }
        public DbSet<PurchaseReturnDetail> tbl_PurchaseReturnDetails { get; set; }
        public DbSet<MaterialUsage > tbl_MaterialUsageMaster { get; set; }
        public DbSet<MaterialusageList> tbl_MaterialUsagelist { get; set; }
        public DbSet<MaterialUsageDetails> tbl_MaterialUsageDetails { get; set; }
        public DbSet<MaterialTransfer> tbl_MaterialTransferMaster { get; set; }
        public DbSet<MaterialTransferList> tbl_MaterialTransferMasterlist { get; set; }
        public DbSet<TransferDetail> tbl_MaterialTransferDetails { get; set; }
        public DbSet<MaterialReciept > tbl_MaterialReceiveMaster { get; set; }
        public DbSet<MaterialRecieveList > tbl_MaterialReceiveMasterlist { get; set; }
        public DbSet<RecieptDetail > tbl_MaterialReceiveDetails { get; set; }
        public DbSet<SupplierAdvance > tbl_SupplierAdvanceMaster { get; set; }
        public DbSet<Quotation > tbl_QuotationMaster { get; set; }
        public DbSet<QuotationDetail> tbl_QuotationDetails { get; set; }
        public DbSet<QuotationSupplier> tbl_QuotationSupplier { get; set; }
        public DbSet<QuotationRate> tbl_QuotationRate { get; set; }
        public DbSet<QuotationList> tbl_Quotationlist { get; set; }
        public DbSet<UserLogs> tbl_UserLogs { get; set; }
        public DbSet<SupplierPayment > tbl_SupplierPaymentMaster { get; set; }
        public DbSet<SupplierPaymentDetails > tbl_SupplierPaymentDetails { get; set; }
        public DbSet<AdvanceBalance > tbl_advanceBalance { get; set; }
        public DbSet<Project > tbl_ProjectMaster { get; set; }
        public DbSet<Block > tbl_Block { get; set; }
        public DbSet<Floor > tbl_Floors { get; set; }
        public DbSet<OwnProject > tbl_OwnProjectDetails { get; set; }
        public DbSet<PurchaseList> tbl_PurchaseMasterList { get; set; }
        public DbSet<PurchaseReport> tbl_Purchasereport { get; set; }
        public DbSet<SupplierAdvanceList > tbl_SupplierAdvanceMasterList { get; set; }
        public DbSet<IndentDetailsList> tbl_IndentDetailsList { get; set; }
        public DbSet<SupplierPaymentList> tbl_SupplierPaymentMasterList { get; set; }
        public DbSet<MaterialType > tbl_MaterialType { get; set; }
        public DbSet<MasterId> tbl_Id { get; set; }
        public DbSet<MaterialStockReport> tbl_MaterialStockReport { get; set; }
        public DbSet<Validation> tbl_validations { get; set; }
        public DbSet<MaterialSchedule> tbl_materialSchedule { get; set; }
        public DbSet<Validation> tbl_validation { get; set; }
        public DbSet<ItemIntake> tbl_ItemIntakeMaster { get; set; }
        public DbSet<BuildExeMaterialServices.Models.TaskMaster> tbl_TaskMaster { get; set; }
        public DbSet<BuildExeMaterialServices.Models.TaskDetails> tbl_TaskDetails { get; set; }
        public DbSet<DebitNote> tbl_DebitNote { get; set; }
        public DbSet<DebitNoteList> tbl_DebitNoteList { get; set; }
        public DbSet<PurchaseReturnAll> tbl_PurchaseReturnAll { get; set; }

    }
    
}

