using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeMaterialServices.Models
{
    public class Attribute
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public string InstallationType { get; set; }
        public string Model { get; set; }
        public string Version { get; set; }
        public string Drive { get; set; }
        public string Head { get; set; }
        public string Curve { get; set; }
        public string Power { get; set; }
        public string Voltage { get; set; }
        public string Frequency { get; set; }
        public int MaterialId { get; set; }
    }
}
