using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildExeBasic.Models
{
    [Keyless]
    public class Alert
    {
        public int AlertType { get; set; }
        public int MasterId { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string AlertMesssage { get; set; }
        public string State { get; set; }

        public int ProjectId { get; set; }


        public int IndentId { get; set; }

        public int DivisionId { get; set; }
        public int SubcontractorIndentId { get; set; }


        public int EnquiryId { get; set; }
        public int SupplierId { get; set; }

        public int FormType { get; set; }
    }
}
