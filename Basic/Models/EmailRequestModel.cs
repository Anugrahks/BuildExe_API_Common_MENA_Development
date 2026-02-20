using Microsoft.AspNetCore.Http;
using System.IO;

namespace BuildExeBasic.Models
{
    public class EmailRequestModel
    {
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int MenuId { get; set; }
        public int Id { get; set; }
        public string file { get; set; }
        public string Content { get; set; }
    }
}
