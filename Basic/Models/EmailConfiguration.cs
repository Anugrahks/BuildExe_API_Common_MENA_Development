namespace BuildExeBasic.Models
{
    public class EmailConfiguration
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int MenuId { get; set; }
        public string  Content { get; set; }
        public string Subject { get; set; }
        public string  AppPassword { get; set; }
        public string FromEmail { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }
        public string Cc { get; set; }
        public int TemplateId { get; set; }
    }
}
