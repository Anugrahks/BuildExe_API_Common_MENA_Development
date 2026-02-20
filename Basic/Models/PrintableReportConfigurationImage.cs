using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace BuildExeBasic.Models
{
    public class PrintableReportConfigurationImage
    {
        public IFormFile Image { get; set; }
        public int Id { get; set; }
        public int PrintableReportConfigurationId { get; set; }
        public string Text { get; set; }
        public int Margin { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int FontSize { get; set; }
        public string Color { get; set; }
        public int ImageType { get; set; }
        public string fileName { get; set; }
        // public PrintableReportConfigurationImageContent imageContent { get; set; }
    }
    public class PrintableReportConfigurationImageContent
    {
        public int Id { get; set; }
        public int PrintableReportConfigurationId { get; set; }
        public string Text { get; set; }
        public int Margin { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int FontSize { get; set; }
        public string Color { get; set; }
        public int ImageType { get; set; }
        public string fileName { get; set; }
    }
}
