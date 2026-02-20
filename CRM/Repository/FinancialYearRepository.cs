using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeServices.DBContexts;
using BuildExeServices.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

using System.Data.SqlClient;
using System.Data.Common;
namespace BuildExeServices.Repository
{
    public class FinancialYearRepository:IFinancialYearRepository 
    {
        private readonly ProductContext _dbContext;
        public FinancialYearRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1
        }

        public void DeleteFinancilaYear(int financialYearId)
        {
            var FinancialYear = _dbContext.tbl_FinancialYear.Find(financialYearId);

            _dbContext.tbl_FinancialYear.Remove(FinancialYear);
            Save();
        }

        public FinancialYear GetFinancilaYearByID(int financialYearId)
        {
            return _dbContext.tbl_FinancialYear.Find(financialYearId);
        }

        public IEnumerable<FinancialYear> GetFinancilaYear()
        {
            return _dbContext.tbl_FinancialYear .ToList();
        }
        public IEnumerable<FinancialYear> GetFinancialYear(int CompanyId, int BranchId)
        {
            return _dbContext.tbl_FinancialYear.Where(x => x.CompanyId == CompanyId).Where(x => x.BranchId == BranchId).ToList();
        }
        public IEnumerable<FinancialYear> GetActiveFinancialYear(int CompanyId,int BranchId)
        {
            return _dbContext.tbl_FinancialYear.Where(x => x.CompanyId == CompanyId).Where(x => x.BranchId  == BranchId ).Where(x => x.Active  == "Y").Where(x => x.Status == "Active").ToList();
        }

        public void InsertFinancilaYear(FinancialYear financialYear)
        {

            var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
            var Financial_Year = new SqlParameter("@Financial_Year", financialYear.Financial_Year );
            var CompanyId = new SqlParameter("@CompanyId", financialYear.CompanyId );
            var BranchId = new SqlParameter("@BranchId", financialYear.BranchId );
            var Active = new SqlParameter("@Active", financialYear.Active );
            var Status = new SqlParameter("@Status", financialYear.Status );
            var start_date = new SqlParameter("@start_date", financialYear.start_date );
            var end_date = new SqlParameter("@end_date", financialYear.end_date );
            var Action = new SqlParameter("@Action", Actions.Insert);
            _dbContext.Database.ExecuteSqlRaw("Stpro_FinancialYear @FinancialYearId,@Financial_Year,@CompanyId,@BranchId,@Active,@Status,@start_date,@end_date,@Action", FinancialYearId, Financial_Year, CompanyId, BranchId, Active, Status, start_date, end_date, Action);


            // _dbContext.Add(financialYear);
            // Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateFinancilaYear(FinancialYear financialYear)
        {
            _dbContext.Entry(financialYear).State = EntityState.Modified;
            Save();
        }
    }
}
