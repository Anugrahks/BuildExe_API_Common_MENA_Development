namespace BuildExeBasic.Models
{
    public class PurchaseOrderDetails
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string MaterialName { get; set; }
        public decimal ItemRate { get; set; }
        public decimal Tax { get; set; }
        public decimal DiscPer { get; set; }
        public decimal QuantityOrdered { get; set; }
        public string ApprovalRemarks { get; set; }
    }
}
