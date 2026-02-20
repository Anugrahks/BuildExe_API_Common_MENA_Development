using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface IIndentRepository
    {
        Task<IEnumerable<Validation>> Insert(IEnumerable<Indent> indent);
        Task<IEnumerable<Validation>> Delete(int Id, int UserID);
        Task<IEnumerable<Validation>> Update(IEnumerable<Indent> indent);
        Task< IEnumerable<IndentList>> GetforEdit(int CompanyId, int Branchid);

        Task<IEnumerable<IndentList>> GetforEdituser(int CompanyId, int Branchid, int userId, int FinancialYearId);
        Task<IEnumerable<IndentList>> GetforApproval(int CompanyId, int Branchid, int userId, int FinancialYearId);
        Task<IEnumerable<IndentList>> Getforview(HRSearch hRSearch);
        Task<string> GetDetailsbyid(int IndentId);
        Task<IEnumerable<IndentDetailsList>> GetDetailsForworkorder(int projectId,int Unitid,int blockid,int floorid,int subcontractoId,int Workorderid);
        Task<string> Getjson(HRSearch hRSearch);
        Task<IEnumerable<IndentDetailsList>> GetDetails(int projectId, int unitid, int blockid, int floorid, int subcontractoId, int workcategoryId, int workNameId);
    }
}
