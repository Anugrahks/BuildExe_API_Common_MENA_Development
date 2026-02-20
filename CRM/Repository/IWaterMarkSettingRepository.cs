

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.Models;
namespace BuildExeServices.Repository
{
    public interface IWaterMarkSettingRepository
    {
        IEnumerable<WaterMarkSetting> GetHeader();
        WaterMarkSetting GetByID(int id);
        IEnumerable<WaterMarkSetting> GetByCompanyAndBranch(int companyId, int branchId);
        Task<IEnumerable<Validation>> Insert(WaterMarkSetting reportHeaderSettings);
        Task<IEnumerable<Validation>> Delete(int id);
        Task<IEnumerable<Validation>> Update(WaterMarkSetting reportHeaderSettings);
        Task<IEnumerable<Validation>> HeaderUpdate(int companyid, int branchID, int id, string status);
        Task<IEnumerable<WaterMarkSetting>> HeaderStatusByType(int companyid, int branchID, int id, int type, string status);
        Task<IEnumerable<Validation>> HeaderNameValidation(int companyid, int branchID, int id, string headerName);
    }
}

