using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
   public interface ITemplateRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<Template > templates );
        Task<IEnumerable<Validation>> Update(IEnumerable<Template> templates);
        Task<IEnumerable<Template>> GetbyID(int Id);
        Task<IEnumerable<Template>> GetbyWorkname(int Id);
        Task<IEnumerable<Template>> Get();
        Task<IEnumerable<Template>> Get(int companyid, int branchid);
        Task<string> getTemplateforEdit(int companyid, int branchid);
        Task<string> getTemplateforEdituser(int companyid, int branchid, int UserId, int FinancialYearId);
        Task<IEnumerable<Validation>> Delete(int id, int userid);
        Task<IEnumerable<TemplateDetailList>> GetTemplateDetails(IEnumerable<Template> templates);
        Task<string> getTemplate(int workid, int CategoryId);
        Task<IEnumerable<Validation>> CheckEditDelete(int id);
    }
}
