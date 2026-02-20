using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public interface IEnquiryModeRepository  
    {
        IEnumerable<EnquiryMode> Getenquirymode();
        IEnumerable<EnquiryMode> Getenquirymode(int CompanyId, int BranchId);
        IEnumerable<EnquiryMode> Getenquirymodeuser(int CompanyId, int BranchId, int UserId);
        EnquiryMode GetenquirymodeByID(int enquirymodeId);
        Task<IEnumerable<Validation>> Insertenquirymode(EnquiryMode enquiryMode);
        Task<IEnumerable<Validation>> Delete_enquirymode(int enquirymodeId);
        Task<IEnumerable<Validation>> Update_enquirymode(EnquiryMode enquiryMode);
        void Save();
    }
}
