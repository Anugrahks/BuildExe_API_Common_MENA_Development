using BuildExeBasic.Models;
using System;
using System.ComponentModel.DataAnnotations; 

public class FinancialYearActive
{
    [Key]
    public int FinancialYearId { get; set; }
    public string Financial_Year { get; set; }
    public int BranchId { get; set; }
    public int CompanyId { get; set; }
    public string Status { get; set; }
    public bool Active { get; set; }
    public DateTime start_date { get; set; }
    public DateTime end_date { get; set; }
    public int YearNo { get; set; }
    public string TaxType { get; set; }
}
