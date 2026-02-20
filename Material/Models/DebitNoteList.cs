using System.ComponentModel.DataAnnotations.Schema;
using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

public class DebitNoteList
{
    [Key]
    public int Id { get; set; }
    public Int32 CompanyId { get; set; }
    public Int32 BranchId { get; set; }
    public int? SupplierId { get; set; }
    public int? FinancialYearId { get; set; }
    public decimal? Amount { get; set; }
    public decimal? RecoveryAmount { get; set; }
    public decimal? BalanceAmount { get; set; }
    public decimal? RecoveredAmount { get; set; }
    public int DebitNoteNo { get; set; }
    public string? DebitNoteName { get; set; }
    public int IsSelect { get; set; }
    public int PurchaseReturnId { get; set; }
    public DateTime? TransactionTime { get; set; }
    public int? SupplierPaymentId { get; set; }
}