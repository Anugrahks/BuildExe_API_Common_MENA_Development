using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public interface IRecieptsRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<Reciept > reciepts );
        Task<IEnumerable<Validation>> Update(IEnumerable<Reciept> reciepts);
        Task< IEnumerable<Reciept>> GetbyID(int Id);

        Task<IEnumerable<Reciept>> Get();
        Task<IEnumerable<Reciept>> Get(int companyid, int branchid);
        Task<IEnumerable<Validation>> Delete(int id, int userid);

        Task<IEnumerable<RecieptsList >> Getforapproval(int companyId, int branchid, int UserID,int menuid, int FinancialYearId);
        Task<IEnumerable<RecieptsList>> GetforEdit(int companyId, int branchid, int menuid);
        Task<IEnumerable<RecieptsList>> GetforEdituser(int companyId, int branchid, int menuid, int UserId, int FinancialYearId);
        Task<IEnumerable<RecieptsList>> GetforView(BillSearch billSearch);
        Task<IEnumerable<RecieptDetail >> Getdetails(int Id);
        Task<string> getRecieptDetails(int Id);
    }
}
