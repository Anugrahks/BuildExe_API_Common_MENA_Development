using BuildExeServices.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuildExeServices.Repository
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Validation>> InsertProperty(string jsonData);
        Task<IEnumerable<Validation>> UpdateProperty(string jsonData);
        Task<string> GetPropertyRentalCategories(int BranchId);
        Task<string> GetPropertyUnitDetails(int ProjectId);
        Task<string> GetActiveTenants(int CompanyId, int BranchId);
        Task<string> GetProperties(int CompanyId, int BranchId);
        Task<string> GetVacantUnits(int CompanyId, int BranchId);

    }
}
