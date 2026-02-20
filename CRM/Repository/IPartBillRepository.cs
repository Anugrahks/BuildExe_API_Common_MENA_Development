using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public interface IPartBillRepository
    {
        //Task<IEnumerable<Validation>> Insert(IEnumerable<PartBillMaster > partBillMasters );
        Task<IEnumerable<Validation>> Insert(PartBillMaster partBillMaster);
        Task<IEnumerable<Validation>> Update(IEnumerable<PartBillMaster> partBillMasters);
        Task<IEnumerable<PartBillMaster>> GetbyID(int Id);

        Task<IEnumerable<PartBillMaster>> Get();
        Task<IEnumerable<PartBillMaster>> Get(int companyid, int branchid);
        Task<string> GetReport(BillSearch billSearch);
        Task<IEnumerable<PartBillList>> Getforview(BillSearch billSearch);
        Task<IEnumerable<Validation>> Delete(int Idworkorder, int userid);

        Task<IEnumerable<PartBillList >> Getforapproval(int companyId, int branchid, int UserID,int FinancialYearId);
        Task<IEnumerable<PartBillList>> GetforEdit(int companyId, int branchid);
        Task<IEnumerable<PartBillList>> GetforEdituser(int companyId, int branchid, int UserId, int FinancialYearId);
        Task<string> GetDetailsbyid(int PurchaseMasterid);

        Task<IEnumerable<PartBillMaster>> GetLastBill(int projectId, int unitId, int blockId, int floorId);
        Task<string> GetBillDetailsBasedOnProject(int projectId, int unitId, int blockId, int floorId);
        Task<string> GetBillDetailsBasedOnProject(int projectId, int unitId, int blockId, int floorId, int id);
        Task<IEnumerable<Validation>> GetByProjectId(int ProjectId);
        Task<string> GetbyInvoicedItem(int companyId, int branchid,int projectId,int divisionid, int UserId, int FinancialYearId,int Id);
    }
}
