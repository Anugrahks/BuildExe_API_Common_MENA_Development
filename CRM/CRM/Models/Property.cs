using System.Collections.Generic;

namespace BuildExeServices.Models
{
    public class PropertyRentalDTO
    {
        public int Id { get; set; }
        public string ProjectId { get; set; }
        public string ProjectTypeId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectArea { get; set; }
        public string ProjectAddress { get; set; }
        public string ProjectDescription { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }
        public int ProjectStatus { get; set; }
        public int BranchId { get; set; }
        public int UserId { get; set; }

        public List<UnitDetailDTO> UnitDetails { get; set; }
    }

    public class UnitDetailDTO
    {
        public int Id { get; set; }
        public string UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }
        public int ProjectCategoryId { get; set; }
        public int RentalTypeId { get; set; }
        public decimal BuildUpArea { get; set; }
        public decimal RatePerArea { get; set; }
        public decimal TotalAmount { get; set; }
        public int CarParking { get; set; }
        public string CarParkingSlots { get; set; }
        public int ClientId { get; set; }

    }

}
