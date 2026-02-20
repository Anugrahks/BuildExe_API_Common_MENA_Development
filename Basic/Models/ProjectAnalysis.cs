using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace BuildExeBasic.Models
{
    [Keyless]
    public class ProjectAnalysis
    {
        public int SlNo { get; set; }
        public int Project_Id { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public decimal Income { get; set; }
        public decimal Receipt { get; set; }
        public decimal BalanceReceivable { get; set; }

        public decimal Expense { get; set; }
        public decimal Payment { get; set; }
        public decimal BalancePayment { get; set; }
        public decimal BudgetedCost { get; set; }

        public decimal OtherIncome { get; set; }

        public decimal OtherReceipt { get; set; }

        public decimal OtherBill { get; set; }

        public int InProfit { get; set; }

        public int InLoss { get; set; }

        public decimal Profit { get; set; }

    }
}
