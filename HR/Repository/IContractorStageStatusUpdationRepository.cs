using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
namespace BuildExeHR.Repository
{
    public interface IContractorStageStatusUpdationRepository
    {
        Task<IEnumerable<Validation>> Update(IEnumerable<ContractorStageSetting> ContractorStage);
        Task<IEnumerable<Validation>> ContractorAdditionalBill(IEnumerable<ContractorStageSetting> ContractorStage);

        Task<string> Getbyid(int id);
        Task<string> GetbyAdditionalBill(int Id);
        Task<IEnumerable<Validation>> Insert(IEnumerable<ContractorAdditionalbill> contractorAdditionalbill);
        Task<IEnumerable<Validation>> Additionalbill(IEnumerable<ContractorAdditionalbill> contractorAdditionalbill);
        Task<string> GetAdditionalBill(int CompanyId, int Branchid, int FinacialYearId, int UserId);
        Task<string> GetByIdAdditionalBill(int Id);
        Task<string> GetApprovalAdditionalBill(int CompanyId, int Branchid, int FinacialYearId, int UserId);
        Task<string> GetDeleteAdditionalBill(int Id, int UserId);
    }
}
