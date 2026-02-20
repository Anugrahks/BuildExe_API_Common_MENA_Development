using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeHR.Models;
using BuildExeHR.DBContexts;
using Microsoft.EntityFrameworkCore;
using BuildExeHR.Repository;
using Microsoft.Data.SqlClient;
using System.Reflection;

namespace BuildExeHR.Repository
{
    public class SubContractorForPaymentRepository:ISubContractorForPaymentRepository 
    {
        private readonly HRContext _dbContext;
        public SubContractorForPaymentRepository(HRContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {

            SelectAll = 5

        }
        public async Task <IEnumerable<SubContractorForPayment >> Get(int EmployeeId, int sitemanagerid, int financialyearId)
        {
            try
            {
                var BiilId = new SqlParameter("@BiilId", "0");
            var employeeId = new SqlParameter("@employeeId", EmployeeId);
            var Sitemanagerid = new SqlParameter("@Sitemanagerid", sitemanagerid);
            var FinancialyearId = new SqlParameter("@FinancialyearId", financialyearId);

            var Action = new SqlParameter("@Action", Actions.SelectAll);
            var _product =await _dbContext.tbl_SubcontractorForPayment.FromSqlRaw("stpro_SubContractorForPayment @BiilId, @employeeId,@Sitemanagerid,@FinancialyearId, @Action", BiilId,employeeId, Sitemanagerid, FinancialyearId, Action).ToListAsync();
            return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<SubContractorForPayment>> Get(int Id)
        {
            try
            {
                var BiilId = new SqlParameter("@BiilId", Id);
            var employeeId = new SqlParameter("@employeeId", "0");
            var Sitemanagerid = new SqlParameter("@Sitemanagerid", "0");
            var FinancialyearId = new SqlParameter("@FinancialyearId", "0");

            var Action = new SqlParameter("@Action", Actions.SelectAll);
            var _product =await _dbContext.tbl_SubcontractorForPayment.FromSqlRaw("stpro_SubContractorForPayment  @BiilId,@employeeId,@Sitemanagerid,@FinancialyearId, @Action", BiilId,employeeId, Sitemanagerid, FinancialyearId, Action).ToListAsync();
            return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }
}
