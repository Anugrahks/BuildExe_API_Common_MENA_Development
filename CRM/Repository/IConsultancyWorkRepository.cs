using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
   public  interface IConsultancyWorkRepository
    {
       Task < IEnumerable<ConsultancyWork >> Get();
        Task<IEnumerable<ConsultancyWork>> Get(int companyid, int branchid);
        Task<string> Getdetails(int companyid, int branchid);
        Task<string> Getdetailsuser(int companyid, int branchid, int UserId);
        Task<ConsultancyWork> GetByID(int id);
        Task<IEnumerable<Validation>> Insert(ConsultancyWork consultancyWork);
        Task<IEnumerable<Validation>> Delete(int id, int userid);
        Task<IEnumerable<Validation>> Update(ConsultancyWork consultancyWork);
        void Save();
    }
}
