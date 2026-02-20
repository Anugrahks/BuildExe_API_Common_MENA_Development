using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeMaterialServices.Models;
using BuildExeMaterialServices.DBContexts;
using Microsoft.EntityFrameworkCore;
using BuildExeMaterialServices.Repository;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Reflection;

namespace BuildExeMaterialServices.Repository
{
    public class VoucherRepository
    {
        private readonly MaterialContext _dbContext;
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Select = 4,
            SelectAll = 5
        }

        public VoucherRepository(MaterialContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Insert(IEnumerable<Purchase> purchase)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", "0");
                var item = new SqlParameter("@item", JsonConvert.SerializeObject(purchase));
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");
                var Action = new SqlParameter("@Action", Actions.Insert);

                var data = _dbContext.Database.ExecuteSqlRaw("Stpro_Purchase @materialId,@item, @CompanyId, @BranchId, @Action", materialId, item, CompanyId, BranchId, Action);

            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public void Delete(int Id)
        {
            try { 
            var materialId = new SqlParameter("@materialId", Id);
            var item = new SqlParameter("@item", "");
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");

            var Action = new SqlParameter("@Action", Actions.Delete);
            _dbContext.Database.ExecuteSqlRaw("Stpro_Purchase @materialId,@item, @CompanyId, @BranchId, @Action", materialId, item, CompanyId, BranchId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public void Update(IEnumerable<Purchase> purchase)
        {
            try { 
            var materialId = new SqlParameter("@materialId", "0");
            var item = new SqlParameter("@item", JsonConvert.SerializeObject(purchase));
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");

            var Action = new SqlParameter("@Action", Actions.Update);
            _dbContext.Database.ExecuteSqlRaw("Stpro_Purchase @materialId,@item, @CompanyId, @BranchId, @Action", materialId, item, CompanyId, BranchId, Action);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public IEnumerable<Purchase> Get()
        {
            try { 
            var materialId = new SqlParameter("@materialId", "0");
            var item = new SqlParameter("@item", "");
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");

            var Action = new SqlParameter("@Action", Actions.SelectAll);

            var _product = _dbContext.tbl_PurchaseMaster.FromSqlRaw("Stpro_Purchase @materialId,@item, @CompanyId, @BranchId, @Action", materialId, item, CompanyId, BranchId, Action).ToList();
                return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public IEnumerable<Purchase> GetbyID(int Id)
        {
            try
            {
                var materialId = new SqlParameter("@materialId", Id);
                var item = new SqlParameter("@item", "");
                var CompanyId = new SqlParameter("@CompanyId", "0");
                var BranchId = new SqlParameter("@BranchId", "0");

                var Action = new SqlParameter("@Action", Actions.Select);

                var _product = _dbContext.tbl_PurchaseMaster.FromSqlRaw("Stpro_Purchase @materialId,@item, @CompanyId, @BranchId, @Action", materialId, item, CompanyId, BranchId, Action).ToList();
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
