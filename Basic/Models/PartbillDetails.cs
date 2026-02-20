namespace BuildExeBasic.Models
{
    public class PartbillDetails
    {
        public int Id { get; set; }
        public int ScheduleNo { get; set; }
        public string SpecName { get; set; }
        public decimal PartRatePerUnit { get; set; }
        public decimal ScheduledQty { get; set; }
        public decimal PreviousQty { get; set; }
        public decimal CurrentQty { get; set; }
        public decimal Tax { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal CurrentAmount { get; set; }
        public string Description { get; set; }
    }
}
