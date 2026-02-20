namespace BuildExeBasic.Models
{
    public class Smsmodel
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public string? MenuName { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public string? Content { get; set; }
        public int IsActive { get; set; }
        public string? TemplateId { get; set; }
        public int UserId { get; set; }

        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? EntityId { get; set; }
        public string? MobileNumber { get; set; }
        public string? Source { get; set; }
        public string? TmId { get; set; }
    }
}
