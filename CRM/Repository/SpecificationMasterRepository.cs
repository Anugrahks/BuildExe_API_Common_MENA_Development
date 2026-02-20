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
using System.Reflection;
using System.Data.Common;

namespace BuildExeServices.Repository
{
    public class SpecificationMasterRepository:ISpecificationMasterRepository 
    {
        private readonly ProductContext _dbContext;
        public SpecificationMasterRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            Select = 4,
            SelectAll = 5,
            Selectdetails = 6
        }
        public async Task<IEnumerable<Validation>> Insert(IEnumerable<SpecificationMaster > specificationMasters )
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
            var item = new SqlParameter("@item", JsonConvert.SerializeObject(specificationMasters));
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var userId = new SqlParameter("@userId", "0");
            var Action = new SqlParameter("@Action", Actions.Insert);

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_SpecificationMaster @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Update(IEnumerable<SpecificationMaster> specificationMasters)
        {
            try
            {
                var Id = new SqlParameter("@Id", "0");
            var item = new SqlParameter("@item", JsonConvert.SerializeObject(specificationMasters));
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var userId = new SqlParameter("@userId", "0");
            var Action = new SqlParameter("@Action", Actions.Update);

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_SpecificationMaster @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Validation>> Delete(int Idworkorder, int userid)
        {
            try
            {
                var Id = new SqlParameter("@Id", Idworkorder);
            var item = new SqlParameter("@item", "");
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var userId = new SqlParameter("@userId", userid);
            var Action = new SqlParameter("@Action", Actions.Delete);

                return await _dbContext.tbl_validation.FromSqlRaw("Stpro_SpecificationMaster @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<SpecificationMaster >> GetbyID(int Idworkorder)
        {
            try
            {
                var list =await _dbContext.tbl_SpecificationMaster .Where(x => x.Id == Idworkorder).ToListAsync();
            var detaillist =await _dbContext.tbl_SpecificationDetails .Where(x => x.SpecificationMasterId == Idworkorder).ToListAsync();
            return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<SpecificationMaster>> Get()
        {
            try
            {
                var list = await _dbContext.tbl_SpecificationMaster.ToListAsync();
            foreach (var detail in list)
            {
                var detaillist =await _dbContext.tbl_SpecificationDetails.Where(x => x.SpecificationMasterId == detail.Id).ToListAsync();
            }
            return list;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<SpecificationMaster>> Get(int companid, int branchid)
        {
            try
            {
                if (branchid == 0)
            {
                var list = await _dbContext.tbl_SpecificationMaster.Where(x => x.CompanyId == companid).OrderByDescending(x => x.Id).ToListAsync();
                foreach (var detail in list)
                {
                    var detaillist = await _dbContext.tbl_SpecificationDetails.Where(x => x.SpecificationMasterId == detail.Id).ToListAsync();
                }
                return list;
            }
            else
            {
                var list =await  _dbContext.tbl_SpecificationMaster.Where(x => x.CompanyId == companid).Where(x => x.BranchId == branchid).OrderByDescending(x => x.Id).ToListAsync();
                foreach (var detail in list)
                {
                    var detaillist = await _dbContext.tbl_SpecificationDetails.Where(x => x.SpecificationMasterId == detail.Id).ToListAsync();
                }
                return list;
            }
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }
        public async Task<string> GetforEdit(int companyId, int branchid, int departmentid)
        {
            try
            {
                var query = from a in _dbContext.tbl_SpecificationMaster
                            join b in _dbContext.tbl_Departments on a.DepartmentId equals b.DepartmentId
                            join d in _dbContext.tbl_Units on a.UnitId equals d.UnitId into ds
                            from d in ds.DefaultIfEmpty()
                            select new
                            {
                                id = a.Id,
                                specNumber = a.SpecNumber,
                                specName = a.SpecName,
                                sacCode = a.SacCode,
                                specDescription = a.SpecDescription,
                                departmentId = a.DepartmentId,
                                departmentName = b.DepartmentLongName,
                                workTypeId = a.WorkTypeId,
                                unitId = a.UnitId,
                                unit = d == null ? String.Empty : d.UnitShortName,
                                companyId = a.CompanyId,
                                branchId = a.BranchId,
                                specUnit = a.SpecUnit,
                                ratePerUnit = a.RatePerUnit,
                                deptRatePerUnit = a.DeptRatePerUnit,
                                waterElectricityCharge = a.WaterElectricityCharge,
                                labourAdditionalCharge = a.LabourAdditionalCharge,
                                subcontractAdditionalCharge = a.SubcontractAdditionalCharge,
                                waterElectricityChargePer = a.WaterElectricityChargePer,
                                labourAdditionalChargePer = a.LabourAdditionalChargePer,
                                subcontractAdditionalChargePer = a.SubcontractAdditionalChargePer,
                                contractorProfit = a.ContractorProfit,
                                contractorProfitAmt = a.ContractorProfitAmt,
                                otherExpense = a.other_expense,
                                tax = a.Tax,
                                taxAmount = a.TaxAmount,
                                userId = a.UserId,
                                marktRatePerUnit = a.RatePerUnit,
                                negotiatedRatePerUnit = a.RatePerUnit,
                                otherExpensePer = a.OtherExpensePer,
                                costIndex = a.CostIndex,
                                specSubTotal = a.SpecSubTotal,
                                imageUrl = a.ImageUrl,
                                customsDuty = a.CustomsDuty,
                                dOCharge = a.DOCharge,
                                handlingCharge = a.HandlingCharge,
                                mOFAAttestation = a.MOFAAttestation,
                                documentationCharge = a.DocumentationCharge,
                                storageCharge = a.StorageCharge
                            };

                // Apply filters conditionally
                query = query.Where(x => x.companyId == companyId && x.branchId == branchid);
                if (departmentid != 0)
                {
                    query = query.Where(x => x.departmentId == departmentid);
                }

                var data = await query.OrderByDescending(x => x.id).Distinct().ToListAsync();

                string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<string> GetforEdit(int companyId, int branchid)
        {
            try
            {
                var data = await(from a in _dbContext.tbl_SpecificationMaster 
                        join b in _dbContext.tbl_Departments  on a.DepartmentId  equals b.DepartmentId into bs from b in bs.DefaultIfEmpty()
                        join c in _dbContext.tbl_WorkType  on a.WorkTypeId  equals c.Id into cs from c in cs.DefaultIfEmpty()
                        join d in _dbContext.tbl_Units  on a.UnitId  equals d.UnitId into ds  from d in ds.DefaultIfEmpty()
                         select new
                        {
                            id = a.Id,
                            spec_Id =a.Spec_Id ,
                            specNumber=a.SpecNumber,
                            specName=a.SpecName ,
                            sacCode=a.SacCode,
                            specDescription=a.SpecDescription,
                            departmentId=a.DepartmentId,
                           // departmentName = b.DepartmentLongName ,
                             departmentName = b == null ? String.Empty : b.DepartmentLongName,
                             workTypeId =a.WorkTypeId,
                           // workTypeName = c.WorkTypeName ,
                             workTypeName = c == null ? String.Empty : c.WorkTypeName,
                             unitId =a.UnitId,
                            //unit=d.UnitShortName ,
                            unit = d == null ? String.Empty : d.UnitShortName,

                             companyId =a.CompanyId ,
                            branchId = a.BranchId,
                            specUnit = a.SpecUnit,
                            ratePerUnit = a.RatePerUnit,
                            deptRatePerUnit = a.DeptRatePerUnit,
                           

                             waterElectricityCharge = a.WaterElectricityCharge,
                            labourAdditionalCharge = a.LabourAdditionalCharge,

                            subcontractAdditionalCharge = a.SubcontractAdditionalCharge,
                            contractorProfit = a.ContractorProfit,
                            contractorProfitAmt = a.ContractorProfitAmt,
                            other_expense = a.other_expense,
                            tax = a.Tax,
                            taxAmount = a.TaxAmount,
                            userId = a.UserId,
                             otherExpensePer = a.OtherExpensePer,
                             costIndex = a.CostIndex,
                             specSubTotal = a.SpecSubTotal,
                             imageUrl = a.ImageUrl,
                             customsDuty = a.CustomsDuty,
                             dOCharge = a.DOCharge,
                             handlingCharge = a.HandlingCharge,
                             mOFAAttestation = a.MOFAAttestation,
                             documentationCharge = a.DocumentationCharge,
                             storageCharge = a.StorageCharge

                         }).Where(x => x.companyId == companyId).Where(x => x.branchId == branchid).OrderByDescending(x => x.id).ToListAsync();

            string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
           // var jsonString1 = JsonConvert.SerializeObject(data);
          
            return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<string> GetforEdituser(int companyId, int branchid, int UserId)
        {
            try
            {
                var data = await (from a in _dbContext.tbl_SpecificationMaster
                                  join b in _dbContext.tbl_Departments on a.DepartmentId equals b.DepartmentId into bs
                                  from b in bs.DefaultIfEmpty()
                                  join c in _dbContext.tbl_WorkType on a.WorkTypeId equals c.Id into cs
                                  from c in cs.DefaultIfEmpty()
                                  join d in _dbContext.tbl_Units on a.UnitId equals d.UnitId into ds
                                  from d in ds.DefaultIfEmpty()
                                  select new
                                  {
                                      id = a.Id,
                                      spec_Id = a.Spec_Id,
                                      specNumber = a.SpecNumber,
                                      specName = a.SpecName,
                                      sacCode = a.SacCode,
                                      specDescription = a.SpecDescription,
                                      departmentId = a.DepartmentId,
                                      // departmentName = b.DepartmentLongName ,
                                      departmentName = b == null ? String.Empty : b.DepartmentLongName,
                                      workTypeId = a.WorkTypeId,
                                      // workTypeName = c.WorkTypeName ,
                                      workTypeName = c == null ? String.Empty : c.WorkTypeName,
                                      unitId = a.UnitId,
                                      //unit=d.UnitShortName ,
                                      unit = d == null ? String.Empty : d.UnitShortName,

                                      companyId = a.CompanyId,
                                      branchId = a.BranchId,
                                      specUnit = a.SpecUnit,
                                      ratePerUnit = a.RatePerUnit,
                                      deptRatePerUnit = a.DeptRatePerUnit,


                                      waterElectricityCharge = a.WaterElectricityCharge,
                                      labourAdditionalCharge = a.LabourAdditionalCharge,

                                      subcontractAdditionalCharge = a.SubcontractAdditionalCharge,
                                      contractorProfit = a.ContractorProfit,
                                      contractorProfitAmt = a.ContractorProfitAmt,
                                      other_expense = a.other_expense,
                                      tax = a.Tax,
                                      taxAmount = a.TaxAmount,
                                      userId = a.UserId,
                                      otherExpensePer = a.OtherExpensePer,
                                      costIndex = a.CostIndex,
                                      specSubTotal = a.SpecSubTotal,
                                      imageUrl = a.ImageUrl,
                                      customsDuty = a.CustomsDuty,
                                      dOCharge = a.DOCharge,
                                      handlingCharge = a.HandlingCharge,
                                      mOFAAttestation = a.MOFAAttestation,
                                      documentationCharge = a.DocumentationCharge,
                                      storageCharge = a.StorageCharge

                                  }).Where(x => x.companyId == companyId).Where(x => x.branchId == branchid).OrderByDescending(x => x.id).ToListAsync();

                string jsonString = System.Text.Json.JsonSerializer.Serialize(data);
                // var jsonString1 = JsonConvert.SerializeObject(data);

                return jsonString;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
        public async Task<IEnumerable<SpecificationDetailsList>> Getspecdetails(int specid)
        {
            try
            {
                var Id = new SqlParameter("@Id", specid);
            var item = new SqlParameter("@item", "");
            var CompanyId = new SqlParameter("@CompanyId", "0");
            var BranchId = new SqlParameter("@BranchId", "0");
            var userId = new SqlParameter("@userId", "0");
            var Action = new SqlParameter("@Action", Actions.Selectdetails);

            var _product = await _dbContext.tbl_SpecificationDetailslist.FromSqlRaw("Stpro_SpecificationMaster @Id,@item,@CompanyId,@BranchId,@userId,@Action", Id, item, CompanyId, BranchId, userId, Action).ToListAsync();
            return _product;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Validation>> CheckEditDelete(int id)
        {
            try
            {
                var Id = new SqlParameter("@Id", id);
                var result = await _dbContext.tbl_validation.FromSqlRaw("Stpro_CheckSpecificationEditDelete @Id", Id).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }

        }
        public async Task<string> PostBudgetForcasting(BudgetForcasting budgetForcasting)
        {
            try
            {
                DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();

                cmd.CommandText = "dbo.Stpro_BudgetForecastingReport";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = budgetForcasting.ProjectId });
                cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = budgetForcasting.CompanyId });
                cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = budgetForcasting.BranchId });
                cmd.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.Int) { Value = 0 });
                cmd.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = "1990-01-01" });
                cmd.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = "1990-01-01" });
                cmd.Parameters.Add(new SqlParameter("@json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(budgetForcasting) });
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 1 });

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                var dataTable = new DataTable();
                dataTable.Load(reader);
                string res = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    res = res + dataTable.Rows[i][0].ToString();
                }
                if (res == "")
                {
                    res = "[]";
                }
                return res;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }
}
