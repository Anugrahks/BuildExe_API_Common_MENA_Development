namespace BuildExeBasic.Models
{
    public class PrintableReportImageConfiguration
    {
        public int Id { get; set; }
        public int PrintableReportConfigurationId { get; set; }
        public string Text { get; set; }
        public string Image {  get; set; }
        public int Margin { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int FontSize { get; set; }
        public string Color { get; set; }
        public int ImageType { get; set; }


    }
}
