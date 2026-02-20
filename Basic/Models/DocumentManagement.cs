using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace BuildExeBasic.Models
{
    public class DocumentManagement
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public string DocumentName { get; set; }
        public string Description { get; set; }
        public string DocumentStatus { get; set; }
        public int ProjectId { get; set; }
        public int UnitId { get; set; }
        public int BlockId { get; set; }
        public int FloorId { get; set; }
        public DateTime EnteredDate { get; set; }
        public string FileNo { get; set; }
        public string RackNo { get; set; }
        public int DocumentGroupId { get; set; }
        public int DocumentTypeId { get; set;}
        public string Category { get; set; }
        public string DocumentPath { get; set; }
        public Int16 UserId { get; set; }
        public string FormName { get; set; }
        public Int32 MasterId { get; set; }
        public int DivisionId { get; set; }

        public int EntryId { get; set; } = 0;
        public List<DocumentFiles> DocumentFiles { get; set; }

    }

    public class DocumentFiles
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ManagementId { get; set; }
        public string DocumentName { get; set; }
        public string Document { get; set; }
        public Int16 CompanyId { get; set; }
        public Int16 BranchId { get; set; }
        public Int16 UserId { get; set; }

        public int Confidential { get; set; }
    }


    }
