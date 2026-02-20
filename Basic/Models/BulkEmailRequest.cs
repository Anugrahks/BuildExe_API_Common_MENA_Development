namespace BuildExeBasic.Models
{
    public class BulkEmailRequest
    {
        public int CompanyId { get; set; }
        public int BranchId { get; set; }

        public string Subject { get; set; }
        public string Body { get; set; }

        // Comma separated emails
        public string ToEmails { get; set; }

        // Base64 HTML -> PDF (optional)
        public string? AttachmentBase64Html { get; set; }
    }

}
