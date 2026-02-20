
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace BuildExeServices.Models
{
    [Keyless]
    public class GetUniqueId
    {
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
        public int SupplierPreffered { get; set; }
        public int SupplierId { get; set; }
        public int ProjectId { get; set; }

        public int EnquiryId { get; set; }
        public int ActionButton { get; set; }

        public int EntryId { get; set; }

        public string EnquiryFor { get; set; }

    }
}
