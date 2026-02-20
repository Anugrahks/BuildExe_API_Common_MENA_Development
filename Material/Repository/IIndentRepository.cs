using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
namespace BuildExeMaterialServices.Repository
{
 public   interface IIndentRepository
    {
       Task< IEnumerable<Indent >> Get(int CompanyId, int Branchid);
        Task<IEnumerable<Indent>> Get(int ProjectId, int Blockid, int Floorid, int UnitId);
        Task<IEnumerable<Indent>>  GetByID(int id);
        Task<IEnumerable<Validation>> Insert(IEnumerable<Indent> indent);
        //Task Delete(int id,int userId);
        Task<IEnumerable<Validation>> Delete(int Id, int UserID);
        Task<IEnumerable<Validation>> Update(IEnumerable<Indent> indent);
        void Save();

        Task<IEnumerable<IndentList>> GetforEdit(int CompanyId, int Branchid);
        Task<IEnumerable<IndentList>> GetforEdit(int CompanyId, int Branchid, int UserId, int FinancialYearId, int IsAsset);
        Task<IEnumerable<IndentList>> GetforApproval(int CompanyId, int Branchid,int userId, int FinancialYearId, int IsAsset);
        Task<IEnumerable<IndentList>> Getforview(MaterialSearch materialSearch);
        Task<string> GetDetailsbyid(int IndentId);
        Task<string> GetforReport(MaterialSearch materialSearch );
        Task<string> Getjson(MaterialSearch hRSearch);
        Task<IEnumerable<IndentDetailsList>> GetDetailsForworkorder(int projectId, int Unitid, int blockid, int floorid, int supplierid, int workCategoryId, int workNameId);
        Task<IEnumerable<IndentDetailsList>> GetDetailsForworkorder(int projectId, int Unitid, int blockid, int floorid, int supplierid, int MaterialType);
        Task<IEnumerable<IndentDetailsList>> GetWithOutSupplier(int projectId, int Unitid, int blockid, int floorid);
        Task<string> GetDetailsForitem(int projectId, int Unitid, int blockid, int floorid, int supplierid, int workCategoryId, int workNameId, DateTime date);
        Task<string> PostIndentDetails(IndentPurchase indentPurchase);
    }
}
