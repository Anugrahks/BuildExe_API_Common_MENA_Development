namespace BuildExeBasic.Models
{
    public class PrintableReportConfigurationList
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string TemplateName { get; set; }
        public string TemplateStructure { get; set; }
        public string WatermarkText { get; set; }
        public string PageSize { get; set; }

        //public int RecordId { get; set; }          // new
        //public string ContentValue { get; set; }   // new
        //public int ContentType { get; set; }       // new
    }
}
