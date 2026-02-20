using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeServices.Models
{
    public class OwnProjectList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int ClientId { get; set; }
        public int ProjectCategoryId { get; set; }
        public int BlockId { get; set; }
        public string? BlockName { get; set; }
        public int FloorId { get; set; }
        public string? FloorName { get; set; }
        public string UnitId { get; set; }
        public string? Description { get; set; }
        public DateTime UnitStartDate { get; set; }
        public DateTime UnitEndDate { get; set; }
        public DateTime? DateCompleted { get; set; }
        public decimal BuildUpArea { get; set; }
        public int UnitStatus { get; set; }


        public decimal TotalAmount { get; set; }
        public decimal CarpetArea { get; set; }
        public decimal CommonArea { get; set; }
        public decimal BalconyArea { get; set; }
        public decimal WorkArea { get; set; }
        public decimal RatePerArea { get; set; }
        public decimal PrivateTerrace { get; set; }

        public decimal TerraceRate { get; set; }
        public decimal CarParking { get; set; }
        public decimal gst { get; set; }
        public decimal kfc { get; set; }

        public decimal LandCost { get; set; }
        public decimal LandRate { get; set; }
        public decimal landgst { get; set; }
        public decimal landkfc { get; set; }


        public decimal aggregatetax { get; set; }
        public decimal ClientAmount { get; set; }
        public decimal ClientPer { get; set; }
        public decimal BankAmount { get; set; }
        public decimal BankPer { get; set; }
        public int UserId { get; set; }
    }
}
