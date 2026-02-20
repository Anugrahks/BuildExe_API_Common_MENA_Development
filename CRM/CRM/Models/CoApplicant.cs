using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildExeServices.Models
{
    public class CoApplicant
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int UnitId { get; set; }
        public string CoApplicantName { get; set; }
        public string? CoApplicantAddress { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? CoApplicantSex { get; set; }
        public DateTime? coapplicantDateOfBirth { get; set; }
        public string? Pin { get; set; }
        public string? Sonof { get; set; }
        public string? SpouseName { get; set; }

        public string? PanNO { get; set; }
        public string? AdharNo { get; set; }
        public string? MaritalStatus { get; set; }
        public DateTime? WeddingAnniversary { get; set; }

        public string? Nationality { get; set; }
        public string? State { get; set; }
        public string? District { get; set; }
        public string? HouseName { get; set; }


        public string? Permanentaddress { get; set; }
        public string? Permanentdistrict { get; set; }
        public string? PermanentState { get; set; }
        public string? PermanentPin { get; set; }
        public string? villege { get; set; }
        public string? Amsom { get; set; }
        public string? desom { get; set; }
        public string? Taluk { get; set; }


        public string? NameOfPowerofattorny { get; set; }
        public string? ReidentialStatus { get; set; }

        public string? NameOfOrganization { get; set; }
        public string? Addressoforganization { get; set; }

        public string? OrganizationType { get; set; }
        public string? ClientImage { get; set; }
    }
}
