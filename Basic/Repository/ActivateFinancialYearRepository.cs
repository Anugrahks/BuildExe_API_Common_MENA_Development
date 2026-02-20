using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.DBContexts;
using BuildExeBasic.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.ComponentModel.Design;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Reflection;

namespace BuildExeBasic.Repository
{
    public class ActivateFinancialYearRepository : IActivateFinancialYearRepository
    {

        private int CompanyId;
        private int BranchId;
        private FinancialYear FinancialYear;
        private int NewFinancialYearId;

        private readonly BasicContext _dbContext;
        public ActivateFinancialYearRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }



        public enum LedgerActions
        {
            OpeningHeads = 1

        }

        public async Task<FinancialYear> GetFinancilaYearByID(int financialYearId)
        {
            try
            {
                return await _dbContext.tbl_FinancialYear.FindAsync(financialYearId);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

        public async Task<IEnumerable<FinancialYear>> GetActiveFinancialYear(int CompanyId, int BranchId)
        {
            try
            {
                return await _dbContext.tbl_FinancialYear.Where(x => x.CompanyId == CompanyId).Where(x => x.BranchId == BranchId).Where(x => x.Active == "Y").Where(x => x.Status == "Active").ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }

            public Task ActivateFYI(int companyId, int branchId)
        {
            #region Collect Values

            CompanyId = companyId;
            BranchId = branchId;

            var currentYear = _dbContext.tbl_FinancialYear.Where(x => x.CompanyId == CompanyId).Where(x => x.BranchId == BranchId).Where(x => x.Active == "Y").Where(x => x.Status == "Active").FirstOrDefault();

            //var currentYear = await GetActiveFinancialYear(companyId, branchId);

            FinancialYear = currentYear;
            var financialYears = _dbContext.tbl_FinancialYear.Where(e => e.CompanyId == companyId && BranchId == branchId).
                                                                Where(f => f.FinancialYearId != FinancialYear.FinancialYearId).
                                                                Where(f => f.FinancialYearId > FinancialYear.FinancialYearId).
                                                                Where(f => f.Active != "N").
                                                                Where(f => f.Status != "Closed");

            if (financialYears.FirstOrDefault() is null)
            {
                NewFinancialYearId = _dbContext.tbl_FinancialYear.Max(f => f.FinancialYearId) + 1;
            }
            else
            {
                NewFinancialYearId = financialYears.FirstOrDefault().FinancialYearId;
            }

            StartProcess();


            return Task.CompletedTask;



            #endregion
        }

        private void StartProcess()
        {


            GeneralAccountHead();
            Suppliers();
            //Client();//todo-- need to check with Nidheesh
            //OutStandings();
            //SiteManager();
            //OpeningAdvances();
            //Stock();

        }

        private void OpeningAdvances()
        {
            DataTable dtOpeningAdvance, dtTotAdvance;
            DateTime fromDate, toDate;
            int openingTableCount;
            string clientId, projectId, clientName, openingType = "D", openingId;
            decimal openingAdvance = 0, totalAdvance = 0, totalRecoveredAmount = 0, partBillRecovery = 0, percentagewiseRecovery = 0, openingAdvanceNewYear;
            DataTable dtClient = GetClients(CompanyId, BranchId, NewFinancialYearId);
            if (dtClient.Rows.Count > 0)
            {
                fromDate = FinancialYear.start_date;
                toDate = FinancialYear.end_date;
                for (int i = 0; i < dtClient.Rows.Count; i++)
                {
                    clientId = dtClient.Rows[i]["ClientId"].ToString();
                    projectId = dtClient.Rows[i]["ProjectId"].ToString();
                    clientName = dtClient.Rows[i]["FirstName"].ToString();
                    //Get opening Advance if exist
                    dtOpeningAdvance = null; //GetClient(CompanyId, BranchId, NewFinancialYearId, clientId);
                    openingTableCount = dtOpeningAdvance.Rows.Count;
                    if (openingTableCount > 0)
                    {
                        DataTable dtPreOpening = null; //GetClient(CompanyId, BranchId, FinancialYear.FinancialYearId, clientId);
                        if (dtPreOpening.Rows.Count > 0)
                        {
                            openingAdvance = Convert.ToDecimal(dtPreOpening.Rows[0]["OpeningBalance"]);
                        }
                        else
                        {
                            openingAdvance = 0;
                        }
                    }
                    else
                    {
                        openingAdvance = 0;
                    }

                    //Total Advance given by the Client
                    totalAdvance = GetClientAdvanceAmtTotal(projectId, fromDate.ToString(), toDate.ToString());
                    //Advance recovry from partbill and percentagewise bill
                    partBillRecovery = GetTotalRecoveryProjectWise(projectId, fromDate.ToString(), toDate.ToString());
                    percentagewiseRecovery = GetPercentageWiseTotAdvanceRecovery(projectId, fromDate.ToString(), toDate.ToString());
                    //Total Recovery
                    totalRecoveredAmount = partBillRecovery + percentagewiseRecovery;
                    //Net Opening Advance in new year
                    openingAdvanceNewYear = openingAdvance + totalAdvance - totalRecoveredAmount;
                    if (openingAdvanceNewYear >= 0)
                    {
                        openingType = "D";
                    }
                    else
                    {
                        openingType = "C";
                    }
                    if (openingTableCount > 0)
                    {
                        //Update code
                        openingId = dtOpeningAdvance.Rows[0]["OpeningAdvanceId"].ToString();
                        //UpdateSqlStr(openingId, openingAdvanceNewYear, openingType, NewFinancialYearId);
                    }
                    else
                    {
                        //Insert into the table
                        //SaveSqlStr("0", "0", "", clientId, "0", clientName, openingAdvanceNewYear, openingAdvanceNewYear, openingType, NewFinancialYearId);

                    }


                }

            }

        }

        private decimal GetPercentageWiseTotAdvanceRecovery(string projectId, string fromDate, string toDate)
        {
            decimal totAmt = 0;
            string sqlQuery = "select SUM(AdvanceAmount) from tbl_PercentageBill where ProjectId ='" + projectId + "' and  " +
                "BillDate >= '" + Convert.ToDateTime(fromDate).ToString("yyyy/MM/dd") + "' and BillDate <= '" + Convert.ToDateTime(toDate).ToString("yyyy/MM/dd") + "' and IsDeleted = 0 and CompanyId = " + CompanyId + " and BranchId = " + BranchId + "";
            var dt = ExecuteQuery(sqlQuery);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0] != DBNull.Value)
                {
                    totAmt = Convert.ToDecimal(dt.Rows[0][0]);
                }
            }
            return totAmt;
        }

        private decimal GetTotalRecoveryProjectWise(string projectId, string fromDate, string toDate)
        {
            decimal totAmt = 0;
            string sqlQuery = "select SUM(AdvanceAmount) from tbl_PartbillMaster where ProjectId ='" + projectId + "' and " +
                "ApprovedDate >= '" + Convert.ToDateTime(fromDate).ToString("yyyy/MM/dd") + "' and ApprovedDate <= '" + Convert.ToDateTime(toDate).ToString("yyyy/MM/dd") + "' and IsDeleted = 0 and CompanyId = " + CompanyId + " and BranchId = " + BranchId + "";
            var dt = ExecuteQuery(sqlQuery);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0] != DBNull.Value)
                {
                    totAmt = Convert.ToDecimal(dt.Rows[0][0]);
                }
            }
            return totAmt;
        }

        private decimal GetClientAdvanceAmtTotal(string projectId, string fromDate, string toDate)
        {
            decimal totAmt = 0;
            string sqlQuery = "select SUM(AdvanceAmount) as Amount from  tbl_ClientAdvanceMaster where ProjectId ='" + projectId + "' and Date >='" + Convert.ToDateTime(fromDate).ToString("yyyy/MM/dd") + "' and Date<= '" + Convert.ToDateTime(toDate).ToString("yyyy/MM/dd") + "' " +
                "and IsDeleted = 0 and CompanyId = " + CompanyId + " and BranchId = " + BranchId + "";
            var dt = ExecuteQuery(sqlQuery);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0] != DBNull.Value)
                {
                    totAmt = Convert.ToDecimal(dt.Rows[0][0]);
                }
            }
            return totAmt;
        }

        private DataTable GetClients(int companyId, int branchId, int newFinancialYearId)
        {
            string sqlQuery = "Select Id, ProjectId, FirstName, LastName, Sex, DateOfBirth, Address, Post, Pin, PhoneNumber, MobileNumber, EmailId, CompanyId, ClientId " +
                "from tbl_ClientMaster with(nolock) where  CompanyId = " + companyId + " and BranchId = " + branchId + "";
            var dTable = ExecuteQuery(sqlQuery);
            return dTable;
        }

        private void OutStandings()
        {
            string accountHeadId, accountHeadName;
            //Salary OutStanding Calculation
            string employeeId, employeeName, employeeCategoryId, employeeCategoryName, openingId;
            decimal totNetSalary = 0, netSalary, basicSalary, totBasicsalary = 0, advanceEarnings, daEarnings, totDaEarnings = 0, hraEarnings, totHraEarnings = 0, incentiveEarnings, totIncentiveEarnings = 0, otherEarnings, totOtherEarnings = 0, bonusEarnings, totBonusEranings = 0, pfDeductions, totPfDeduction = 0, esiDeduction, totEsiDeduction = 0, loanDeduction, advanceDeduction, leaveDeduction, totLeaveDeduction = 0;
            DataTable dtOpenOutStanding;
            int openingTableCount = 0;
            decimal openingAdvance = 0, opNetSalary = 0, opBasicSalary = 0, opAdvanceEarnings = 0, opDaEarnings = 0, opHraEarnings = 0, opIncentiveEarnings = 0, opOtherEarnings = 0, opBonusEarnings = 0, opPfDeduction = 0, opEsiDeduction = 0, opLoandeductions = 0, opAdvanceDeductions = 0, opLeaveDeductions = 0;
            accountHeadName = "SALARY OUTSTANDING";
            accountHeadId = GetHeadId(accountHeadName);


            #region Salaried Employees
            DataTable dtEmployee = SalariedEmployees();
            if (dtEmployee.Rows.Count > 0)
            {

                for (int i = 0; i < dtEmployee.Rows.Count; i++)
                {
                    openingAdvance = 0; opNetSalary = 0; opBasicSalary = 0; opAdvanceEarnings = 0; opDaEarnings = 0; opHraEarnings = 0; opIncentiveEarnings = 0; opOtherEarnings = 0; opBonusEarnings = 0; opPfDeduction = 0; opEsiDeduction = 0; opLoandeductions = 0; opAdvanceDeductions = 0; opLeaveDeductions = 0;
                    totNetSalary = 0; netSalary = 0; basicSalary = 0; totBasicsalary = 0; advanceEarnings = 0; daEarnings = 0; totDaEarnings = 0; hraEarnings = 0; totHraEarnings = 0; incentiveEarnings = 0; totIncentiveEarnings = 0; otherEarnings = 0; totOtherEarnings = 0; bonusEarnings = 0; totBonusEranings = 0; pfDeductions = 0; totPfDeduction = 0; esiDeduction = 0; totEsiDeduction = 0; loanDeduction = 0; advanceDeduction = 0; leaveDeduction = 0; totLeaveDeduction = 0;

                    decimal TotalBenifit = 0, BenefitAmount = 0, DeductionAmount = 0, TaxAmount = 0;

                    employeeId = dtEmployee.Rows[i]["EmployeeId"].ToString();
                    employeeName = dtEmployee.Rows[i]["FullName"].ToString();
                    employeeCategoryId = dtEmployee.Rows[i]["EmployeeCategoryId"].ToString();
                    employeeCategoryName = dtEmployee.Rows[i]["EmployeeCategoryName"].ToString();
                    //Get Pending Salary of employee
                    DataTable dtPendingSalary = null; //PendingSalary(employeeId);
                    if (dtPendingSalary.Rows.Count > 0)
                    {
                    }
                }
            }
            #endregion Salaried Employees

            #region Labour
            decimal prvBalance = 0, totPrvBalance = 0, opOutStanding = 0, netOpOutStanding = 0;
            //Labour Wage Outstanding
            accountHeadName = "LABOUR ACCOUNT";
            accountHeadId = GetHeadId(accountHeadName);
            DataTable dtLabour = GetEmployeeCategoryWise("LABOUR");
            employeeCategoryId = dtEmployee.Rows[0]["EmployeeCategoryId"].ToString();
            if (dtLabour.Rows.Count > 0)
            {
                DataTable dtProject, dtPrevoiusbalance;
                string projectId, unitId;
                employeeCategoryName = "Labour";
                for (int i = 0; i < dtLabour.Rows.Count; i++)
                {
                    totPrvBalance = 0; opOutStanding = 0; netOpOutStanding = 0;
                    employeeId = dtLabour.Rows[i]["EmployeeId"].ToString();
                    employeeName = dtLabour.Rows[i]["FullName"].ToString();

                    dtProject = GetProjectId(employeeId, FinancialYear.FinancialYearId, employeeCategoryName);
                    if (dtProject.Rows.Count > 0)
                    {
                        for (int pjct = 0; pjct < dtProject.Rows.Count; pjct++)
                        {
                            prvBalance = 0;
                            projectId = dtProject.Rows[pjct]["ProjectId"].ToString();
                            unitId = dtProject.Rows[pjct]["UnitId"].ToString();
                            string getpreviouslbr = "Select BalanceAmount from tbl_LabourWagePaymentDetails where LabourWagePaymentId = (select MAX(LabourWagePaymentId) " +
                                "from tbl_LabourWagePayment A join tbl_LabourWagePaymentDetails B on A.Id = B.LabourWagePaymentId where EmployeeId = " + employeeId + " and ProjectId = " + projectId + " and " +
                                "UnitId = " + unitId + " and CompanyId = " + CompanyId + " and BranchId = " + BranchId + " and FinancialYearid = " + FinancialYear.FinancialYearId + "  and IsDeleted = 0)";
                            dtPrevoiusbalance = ExecuteQuery(getpreviouslbr);
                            if (dtPrevoiusbalance.Rows.Count > 0)
                            {
                                prvBalance = Convert.ToDecimal(dtPrevoiusbalance.Rows[0][0]);
                            }
                            totPrvBalance = totPrvBalance + prvBalance;
                        }
                    }
                    //Save or Update opening wagwe outstanding for individual employee

                    //todo
                }
            }
            #endregion Labour

            #region Group Labour
            //groupLabour wage payment outstanding
            employeeCategoryName = "Group Labour";

            accountHeadName = "GROUP LABOUR ACCOUNT";
            accountHeadId = GetHeadId(accountHeadName);
            //GetEmployeeCategory name and Id
            DataTable dtGrpLabour = GetEmployeeCategoryWise(employeeCategoryName);
            employeeCategoryId = dtEmployee.Rows[0]["EmployeeCategoryId"].ToString();
            if (dtGrpLabour.Rows.Count > 0)
            {
                DataTable dtProject, dtPrevoiusbalance;
                string projectId, unitId;
                for (int i = 0; i < dtGrpLabour.Rows.Count; i++)
                {
                    totPrvBalance = 0; opOutStanding = 0; netOpOutStanding = 0;
                    employeeId = dtGrpLabour.Rows[i]["EmployeeId"].ToString();
                    employeeName = dtGrpLabour.Rows[i]["FullName"].ToString();
                    //Get pending salary for each labour from labour wage payment.Get balance amount of max paymentid of employee 
                    dtProject = GetProjectId(employeeId, FinancialYear.FinancialYearId, employeeCategoryName);
                    if (dtProject.Rows.Count > 0)
                    {
                        for (int pjct = 0; pjct < dtProject.Rows.Count; pjct++)
                        {
                            prvBalance = 0;
                            projectId = dtProject.Rows[pjct]["ProjectId"].ToString();
                            unitId = dtProject.Rows[pjct]["UnitId"].ToString();
                            string getpreviouslbr = "select PreviousBalance from tbl_GroupLabourWagePaymentDetails where GroupLabourWagePaymentId =(select MAX (GroupLabourWagePaymentId) " +
                                "from tbl_GroupLabourWagePayment A join tbl_GroupLabourWagePaymentDetails B on A.Id = B.GroupLabourWagePaymentId where EmployeeId = " + employeeId + " and ProjectId = " + projectId + " and " +
                                "UnitId = " + unitId + " and CompanyId = " + CompanyId + " and BranchId = " + BranchId + " and FinancialYearid = " + FinancialYear.FinancialYearId + "  and IsDeleted = 0)";
                            dtPrevoiusbalance = ExecuteQuery(getpreviouslbr);
                            if (dtPrevoiusbalance.Rows.Count > 0)
                            {
                                prvBalance = Convert.ToDecimal(dtPrevoiusbalance.Rows[0][0]);
                            }
                            totPrvBalance = totPrvBalance + prvBalance;
                        }
                    }
                }

                //Save or Update opening wage outstanding for individual employee

                //todo
            }
            #endregion Group Labour

            #region Foreman
            //Foreman payment outstanding
            employeeCategoryName = "Foreman";
            accountHeadName = "FOREMAN ACCOUNT";
            accountHeadId = GetHeadId(accountHeadName);
            //GetEmployeeCategory name and Id
            DataTable dtForeman = GetEmployeeCategoryWise(employeeCategoryName);
            employeeCategoryId = dtEmployee.Rows[0]["EmployeeCategoryId"].ToString();
            if (dtForeman.Rows.Count > 0)
            {
                DataTable dtProject, dtPrevoiusbalance;
                string projectId, unitId, WorkOrderMasterId;
                for (int i = 0; i < dtForeman.Rows.Count; i++)
                {

                    totPrvBalance = 0; opOutStanding = 0; netOpOutStanding = 0;
                    employeeId = dtForeman.Rows[i]["EmployeeId"].ToString();
                    employeeName = dtForeman.Rows[i]["FullName"].ToString();
                    //todo -- query
                    dtProject = GetProjectId(employeeId, FinancialYear.FinancialYearId, employeeCategoryName);
                    //string getproject = "select distinct ProjectId ,UnitId,WorkOrderMasterId from tbl_ForemanWagePayment where EmployeeId = '" + employeeId + "' and CompanyId ='" + LogIn.compid + "' and HistoryStatus='IsActive'";
                    if (dtProject.Rows.Count > 0)
                    {
                        for (int pjct = 0; pjct < dtProject.Rows.Count; pjct++)
                        {
                            prvBalance = 0;
                            projectId = dtProject.Rows[pjct]["ProjectId"].ToString();
                            unitId = dtProject.Rows[pjct]["UnitId"].ToString();
                            WorkOrderMasterId = dtProject.Rows[pjct]["WorkOrderMasterId"].ToString();
                            //todo - query WorkOrdermasterId
                            string getpreviouslbr = "select PaymentAmount from tbl_ForemanPaymentDetails where ForemanPaymentId =(select MAX(ForemanPaymentId) " +
                                "from tbl_ForemanPayment A join tbl_ForemanPaymentDetails B on A.Id = B.ForemanPaymentId where EmployeeId = " + employeeId + " and ProjectId = " + projectId + " and " +
                                "UnitId = " + unitId + " and CompanyId = " + CompanyId + " and BranchId = " + BranchId + " and FinancialYearid = " + FinancialYear.FinancialYearId + "  and IsDeleted = 0)";
                            dtPrevoiusbalance = ExecuteQuery(getpreviouslbr);
                            if (dtPrevoiusbalance.Rows.Count > 0)
                            {
                                prvBalance = Convert.ToDecimal(dtPrevoiusbalance.Rows[0][0]);
                            }
                            totPrvBalance = totPrvBalance + prvBalance;
                        }
                    }


                    //Save or Update opening wagwe outstanding for individual employee
                    //todo
                }
            }
            #endregion

            #region Sub Contractor

            //Subcontractor payment outstanding
            employeeCategoryName = "SubContractor";
            accountHeadName = "SUB CONTRACTORS ACCOUNT";
            accountHeadId = GetHeadId(accountHeadName);
            //GetEmployeeCategory name and Id
            DataTable dtSubContractor = GetEmployeeCategoryWise(employeeCategoryName);
            employeeCategoryId = dtEmployee.Rows[0]["EmployeeCategoryId"].ToString();
            if (dtSubContractor.Rows.Count > 0)
            {
                DataTable dtPrevoiusbalance;
               
                for (int i = 0; i < dtSubContractor.Rows.Count; i++)
                {
                    prvBalance = 0; totPrvBalance = 0; opOutStanding = 0; netOpOutStanding = 0;
                    employeeId = dtSubContractor.Rows[i]["EmployeeId"].ToString();
                    employeeName = dtSubContractor.Rows[i]["FullName"].ToString();
                    //Get total billbalance amount for each subcontractor  

                    //string getpreviouslbr = "select isnull (SUM(BillAmt-(Paid+BalanceAmt)),0) from temp_SupplierBalance with(nolock)  where supplierid='" + employeeId + "'";

                    string getpreviouslbr = "select isnull(SUM(AmountAsPerBillBalance),0) from tbl_SubContractorBillMaster where  CompanyId=" + CompanyId + " and BranchId = "+BranchId+"  and ApprovalStatus =2 and SubContractorId ='" + employeeId + "' and FinancialYearId<=" + FinancialYear.FinancialYearId + " ";
                    dtPrevoiusbalance = ExecuteQuery(getpreviouslbr);
                    if (dtPrevoiusbalance.Rows.Count > 0)
                    {
                        totPrvBalance = Convert.ToDecimal(dtPrevoiusbalance.Rows[0][0]);
                    }

                    //Save or Update opening wagwe outstanding for individual employee
                    //todo
                }
            }
            #endregion

            #region Contractor

            //Otstanding details of Contractor
            employeeCategoryName = "Contractor";

            accountHeadName = "CONTRACTORS ACCOUNT";
            accountHeadId = GetHeadId(accountHeadName);
            //GetEmployeeCategory name and Id
            DataTable dtContractor = GetEmployeeCategoryWise(employeeCategoryName);
            if (dtContractor.Rows.Count > 0)
            {
                DataTable dtPrevoiusbalance;
                for (int i = 0; i < dtContractor.Rows.Count; i++)
                {
                    prvBalance = 0; totPrvBalance = 0; opOutStanding = 0; netOpOutStanding = 0;
                    employeeId = dtContractor.Rows[i]["EmployeeId"].ToString();
                    employeeName = dtContractor.Rows[i]["FullName"].ToString();
                    //Get total billbalance amount for each subcontractor  
                    //13-05-2020// womc = new WorkOrderMasterContractor(LogIn.compid, Library.FinancialYearId.ToString());
                    //13-05-2020// totPrvBalance = womc.SumBillBalance(employeeId);
                    //string getpreviouslbr = "select isnull (SUM(BillAmt-(Paid+BalanceAmt)),0) from temp_SupplierBalance with(nolock) where supplierid='" + employeeId + "'";

                    string getpreviouslbr = "select isnull(SUM(BillAmountBalance),0) from tbl_ContractorWorkOrderMaster where  CompanyId=" + CompanyId + " and BranchId = "+BranchId+"  and ApprovalStatus =2 and ContractorId ='" + employeeId + "' and FinancialYearId<=" + FinancialYear.FinancialYearId + " and IsDeleted = 0";
                    dtPrevoiusbalance = ExecuteQuery(getpreviouslbr);
                    if (dtPrevoiusbalance.Rows.Count > 0)
                    {
                        totPrvBalance = Convert.ToDecimal(dtPrevoiusbalance.Rows[0][0]);
                    }

                    //Save or Update opening balance contractor
                    //todo
                }
            }
            #endregion
        }

        private DataTable GetProjectId(string employeeId, int fId, string employeeCategoryName)
        {
            string sqlQuery = string.Empty;
            if (employeeCategoryName == "Labour")
            {
                sqlQuery = "Select distinct ProjectId ,UnitId from tbl_LabourWagePayment A join tbl_LabourWagePaymentDetails B on A.Id = B.LabourWagePaymentId " +
                    "where EmployeeId = '" + employeeId + "' and FinancialYearid = " + fId + "  and IsDeleted = 0 and CompanyId = " + CompanyId + " and BranchId = " + BranchId + " ";
            }
            else if (employeeCategoryName == "Group Labour")
            {
                sqlQuery = "select distinct ProjectId , UnitId from tbl_GroupLabourWagePayment A join tbl_GroupLabourWagePaymentDetails B on A.Id = B.GroupLabourWagePaymentId where" +
                    "EmployeeId = '" + employeeId + "' and FinancialYearid = " + fId + "  and IsDeleted = 0 and CompanyId = " + CompanyId + " and BranchId = " + BranchId + " ";
            }
            else if (employeeCategoryName == "Foreman")
            {
                sqlQuery = "select distinct ProjectId , UnitId from tbl_GroupLabourWagePayment A join tbl_GroupLabourWagePaymentDetails B on A.Id = B.GroupLabourWagePaymentId where" +
                    "EmployeeId = '" + employeeId + "' and FinancialYearid = " + fId + "  and IsDeleted = 0 and CompanyId = " + CompanyId + " and BranchId = " + BranchId + " ";
            }
            var dTable = ExecuteQuery(sqlQuery);
            return dTable;
        }

        private DataTable SalariedEmployees()
        {
            string sqlQuery = " Select A.Id, A.FullName, A.EmployeeCategoryId, EmployeeCategoryName  from tbl_EmployeeMaster A join tbl_EmployeeCategory B on A.EmployeeCategoryId = B.EmployeeCategoryId" +
                " where EmployeeCategoryName in ('OfficeStaff','Supervisor' ,'Site Manager' ,'MONTHLY SALARIED') and CompanyId = " + CompanyId + " and BranchId = " + BranchId + " ";
            var dTable = ExecuteQuery(sqlQuery);
            return dTable;
        }

        private void SiteManager()
        {
            DataTable dtSiteOpening;
            string openingType = "C";
            DataTable dtSiteManager = GetEmployeeCategoryWise("SiteManager");
            if (dtSiteManager.Rows.Count > 0)
            {
                foreach (DataRow dtRow in dtSiteManager.Rows)
                {
                    int siteManagerId = Convert.ToInt32(dtRow["EmployeeId"]);
                    // For Switching
                    dtSiteOpening = GetSiteManagerOpeningById(CompanyId, BranchId, NewFinancialYearId, siteManagerId);
                    if (dtSiteOpening.Rows.Count > 0)
                    {
                        //Update

                        decimal opAmt = SiteManagerOpening(siteManagerId);
                        if (opAmt > 0)
                            openingType = "D";
                        UpdateSiteManager(CompanyId, BranchId, NewFinancialYearId, siteManagerId, opAmt, opAmt, openingType);
                    }
                    else
                    {
                        //Insert new opening balance
                        decimal opAmt = SiteManagerOpening(siteManagerId);
                        if (opAmt > 0)
                            openingType = "D";
                        InsertSiteManager(CompanyId, BranchId, NewFinancialYearId, siteManagerId, dtRow["FullName"].ToString(), opAmt, openingType);
                    }
                }
            }
        }



        private decimal SiteManagerOpening(int siteManagerId)
        {
            decimal openingAmt = 0, debitAmt = 0, creditAmt = 0;
            DataTable dtSiteOpening;
            dtSiteOpening = GetSiteManagerOpeningById(CompanyId, BranchId, FinancialYear.FinancialYearId, siteManagerId);
            if (dtSiteOpening.Rows.Count > 0)
                openingAmt = Convert.ToDecimal(dtSiteOpening.Rows[0]["OpeningBalance"]);
            else
                openingAmt = 0;

            DataTable dt = GetSiteManagerBalance(CompanyId, BranchId, FinancialYear.FinancialYearId, siteManagerId);
            string cashSubGroup = "107";//cash subgroup id
            string bankSubGroup = "106";//bank subgroup id
            string bankODSubGroup = "112";//bank OD subgroup id
            for (int i = 0; dt.Rows.Count > i; i++)
            {
                int accountHead = Convert.ToInt32(dt.Rows[i]["DebitHeadId"]);
                DataTable dt1 = GetAccountHead(CompanyId, BranchId, FinancialYear.FinancialYearId, accountHead);
                if (dt1.Rows.Count > 0) //Except SiteMangerHeadId(Cash,Bank,OtherExpenseHead)
                {

                    string accountSubGroupId = dt1.Rows[i]["AccountSubGroupId"].ToString();
                    if ((accountSubGroupId == cashSubGroup) || (accountSubGroupId == bankSubGroup) || (accountSubGroupId == bankODSubGroup))
                    {
                        debitAmt += Convert.ToDecimal(dt.Rows[i][9]);//Money in hand (Money TO SiteManager)
                    }
                    else
                    {
                        creditAmt += Convert.ToDecimal(dt.Rows[i][9]);//Site Expenses
                    }
                }
                else
                {
                    creditAmt += Convert.ToDecimal(dt.Rows[i][9]);//Money Return From SiteManager
                }
            }
            if (openingAmt >= 0)
                debitAmt = debitAmt + openingAmt;
            else
                creditAmt = creditAmt - openingAmt;
            openingAmt = debitAmt - creditAmt;
            return openingAmt;
        }


        private void Stock()
        {

            StockUpdate(CompanyId, BranchId, FinancialYear.FinancialYearId, NewFinancialYearId);
            //DataTable dtIsStockExist = GetAllStock(NewFinancialYearId);
            //if (dtIsStockExist.Rows.Count > 0)
            //{
            //    //Update
            //    DataTable dtIsPrvStockExist = GetAllStock(FinancialYear.FinancialYearId);
            //    foreach (DataRow dtRow in dtIsStockExist.Rows)
            //    {
            //        string ProjectId = dtRow["ProjectId"].ToString();
            //        string MatId = dtRow["MaterialId"].ToString();
            //        int stockId = Convert.ToInt32(dtRow["StockId"].ToString());
            //        foreach (DataRow dtRow1 in dtIsPrvStockExist.Rows)
            //        {
            //            if ((dtRow1["ProjectId"].ToString() == ProjectId) && (dtRow1["MaterialId"].ToString() == MatId))
            //            {
            //                decimal stock = Convert.ToDecimal(dtRow1["StockIn"]);
            //                IEnumerable<Stock> stockDetails = new List<Stock>()
            //                {
            //                    new Stock()
            //                    {
            //                        FinancialYearId = NewFinancialYearId,
            //                        StockIn = stock,
            //                        Id = stockId
            //                    }
            //                };
            //                UpdateStock(stockDetails);
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    //Insert Stock
            //    IEnumerable<Stock> stockDetails = new List<Stock>()
            //        {
            //            new Stock()
            //            {
            //                FinancialYearId = NewFinancialYearId,
            //            }
            //        };

            //    InsertStock(stockDetails);
            //}

        }

        #region GeneralAccountHead
        private void GeneralAccountHead()
        {
            string openingType = "C";
            DataTable dsHeads = GetOpeningLedgerHeads();
            if (dsHeads.Rows.Count > 0)
            {
                foreach (DataRow drRow in dsHeads.Rows)
                {
                    int headId = Convert.ToInt32(drRow["AccountHeadId"]);


                    DataTable dtIsOpeningEist = GetOpeningByHeadId(headId, NewFinancialYearId, 0, 0, string.Empty, 1);
                    if (dtIsOpeningEist.Rows.Count > 0)
                    {
                        //Update
                        decimal opAmt = GeneralHeadOpening(headId);
                        if (opAmt > 0)
                            openingType = "D";
                        GetOpeningByHeadId(headId, NewFinancialYearId, opAmt, opAmt, openingType, 3);

                    }
                    else
                    {
                        //Insert
                        decimal opAmt = GeneralHeadOpening(headId);
                        if (opAmt > 0)
                            openingType = "D";
                        GetOpeningByHeadId(headId, NewFinancialYearId, opAmt, opAmt, openingType, 2);
                    }
                }
            }
        }
        #endregion

        #region Suppliers
        private void Suppliers()
        {
            string openingType = "C";
            DataTable dsSupplierHeads = SupplierHead();
            if (dsSupplierHeads.Rows.Count > 0)
            {
                foreach (DataRow drRow in dsSupplierHeads.Rows)
                {
                    // For Switching
                    int supplierHeadId = Convert.ToInt32(drRow["SupplierId"].ToString());
                    DataTable dtSupplierOpeningExist = GetOpeningByHeadId(supplierHeadId, NewFinancialYearId, 0, 0, string.Empty, 1);
                    if (dtSupplierOpeningExist.Rows.Count > 0)
                    {
                        //Update
                        decimal newOpening = Convert.ToDecimal(dtSupplierOpeningExist.Rows[0]["OpeningBalance"]);
                        decimal opAmt = SupplierOpening(supplierHeadId);
                        decimal balanceAdjusted = Convert.ToDecimal(dtSupplierOpeningExist.Rows[0]["BalanceAdjusted"]);

                        decimal adjustedAmt = 0;
                        if (opAmt == 0)
                        {
                            opAmt = newOpening;
                        }
                        else
                        {
                            if (newOpening >= opAmt)
                            {
                                adjustedAmt = newOpening - opAmt;
                                balanceAdjusted = balanceAdjusted - adjustedAmt;
                            }
                            else
                            {
                                adjustedAmt = opAmt - newOpening;
                                balanceAdjusted = balanceAdjusted + adjustedAmt;
                            }
                        }
                        if (opAmt > 0)
                            openingType = "D";
                        GetOpeningByHeadId(supplierHeadId, NewFinancialYearId, opAmt, balanceAdjusted, openingType, 3);

                    }
                    else
                    {
                        //Insert
                        decimal opAmt = SupplierOpening(supplierHeadId);
                        if (opAmt > 0)
                            openingType = "D";
                        //DataTable dtHeadDetails = GetHeadDetails(supplierHeadId);
                        //if (dtHeadDetails.Rows[0]["AccountHeadId"].ToString() == "2204")
                        //{
                        //    MessageBox.Show("select");
                        //}
                        GetOpeningByHeadId(supplierHeadId, NewFinancialYearId, opAmt, opAmt, openingType, 2);
                    }
                }
            }
        }
        #endregion

        #region Client
        private void Client()
        {
            decimal openingAmt = 0;
            DataTable dsClientHeads = GetAllClientDetails();
            if (dsClientHeads.Rows.Count > 0)
            {
                foreach (DataRow drRow in dsClientHeads.Rows)
                {
                    int clientHeadId = Convert.ToInt32(drRow["ClientId"]);
                    string openingType = "";
                    // For Switching
                    DataTable dtClientOpening = GetOpeningByHeadId(clientHeadId, NewFinancialYearId, 0, 0, string.Empty, 1);
                    if (dtClientOpening.Rows.Count > 0)
                    {
                        //Update
                        openingAmt = GeneralHeadOpening(clientHeadId);
                        if (openingAmt > 0)
                            openingType = "D";
                        GetOpeningByHeadId(clientHeadId, NewFinancialYearId, openingAmt, openingAmt, openingType, 3);
                    }
                    else
                    {
                        DataTable dtStageEntry = GetOpeningByHeadId(0, NewFinancialYearId, 0, 0, string.Empty, 4);
                        if (dtStageEntry.Rows.Count <= 0)
                        {
                            DataTable dtWorkStageMaster = GetAllStage();
                            if (dtWorkStageMaster.Rows.Count > 0)
                            {
                                var currentYear = _dbContext.tbl_FinancialYear.Where(x => x.CompanyId == CompanyId).
                                    Where(x => x.BranchId == BranchId).Where(f => f.FinancialYearId == NewFinancialYearId).Where(x => x.Active == "Y").Where(x => x.Status == "Active").FirstOrDefault();
                                if (currentYear != null)
                                {
                                    string strStart = currentYear.start_date.ToString();
                                    string strEnd = currentYear.end_date.ToString();
                                    DateTime dtStart = Convert.ToDateTime(strStart);
                                    DateTime dtEnd = Convert.ToDateTime(strEnd);
                                    foreach (DataRow dtRow in dtWorkStageMaster.Rows)
                                    {
                                        DateTime dtDue = Convert.ToDateTime(dtRow["DateDue"].ToString());
                                        if (dtDue >= dtStart && dtDue <= dtEnd)
                                        {
                                            int voucherNo = 0, voucherTypeId = 0;
                                            string stageId = dtRow["Id"].ToString();
                                            string creditHeadId = GetHeadId("CLIENT BILL");
                                            string clientId = dtRow["ClientId"].ToString();
                                            string debitHeadId = clientId;
                                            string projectId = dtRow["ProjectId"].ToString();
                                            string unitId = dtRow["OwnProjectDetailsiId"].ToString();
                                            decimal Amount = Convert.ToDecimal(dtRow["NetAmount"].ToString());
                                            //Update VoucherNumber
                                            var dtVoucher = GetVoucherSqlStr("RCPTB", NewFinancialYearId);
                                            if (dtVoucher.Rows.Count > 0)
                                            {

                                                voucherTypeId = Convert.ToInt32(dtVoucher.Rows[0]["VoucherTypeId"].ToString());
                                                voucherNo = Convert.ToInt32(dtVoucher.Rows[0]["VoucherNumber"]) + 1;
                                                UpdateVoucherSqlStr(voucherTypeId.ToString(), voucherNo, NewFinancialYearId);

                                                //JournalEntry
                                                //SaveJournalMasterString(dtDue, "0", "WORK STATUS", clientId, "0", voucherTypeId.ToString(), voucherNo.ToString(), "", "", "", LogIn.UserName, "Client Bill-Own Project Datewise");
                                                //string lastId = GetLastIdSqlStr("JournalMasterId", "tbl_JournalMaster", "NoHistory").ToString();
                                                //SaveJournalDetailsString(lastId, debitHeadId, creditHeadId, Amount.ToString(), projectId, unitId, "Client Bill-Own Project Datewise");

                                                ////VoucherEntry
                                                //SaveVoucherMasterString(dtDue, "0", "WORK STATUS", clientId.ToString(), "0", voucherTypeId.ToString(), voucherNo.ToString(), "", "", "", LogIn.UserName, "Client Bill-Own Project Datewise");
                                                //string vouchrId = GetLastIdSqlStr("VocuherMasterId", "tbl_VoucherMaster", "NoHistory").ToString();
                                                //SaveVoucherDetailsString(vouchrId, debitHeadId, Amount.ToString(), "0", projectId, unitId, "Client Bill-Own Project Datewise");
                                                //SaveVoucherDetailsString(vouchrId, creditHeadId, "0", Amount.ToString(), projectId, unitId, "Client Bill-Own Project Datewise");
                                                ////Update WorkStage VoucherNumber 
                                                //Update(stageId, voucherTypeId.ToString(), voucherNo.ToString());
                                            }
                                        }
                                    }
                                }
                            }

                        }

                    }

                    //Insert new opening Balance
                    openingAmt = GeneralHeadOpening(clientHeadId);
                    if (openingAmt > 0)
                        openingType = "D";
                    GetOpeningByHeadId(clientHeadId, NewFinancialYearId, openingAmt, openingAmt, openingType, 2);
                }
            }
        }

        #endregion



        public decimal GeneralHeadOpening(int headId)
        {
            decimal openingAmt = 0, debitAmt = 0, creditAmt = 0;


            var dtOpening = GetOpeningByHeadId(headId, FinancialYear.FinancialYearId, 0, 0, string.Empty, 1);
            if (dtOpening.Rows.Count > 0)
                openingAmt = Convert.ToDecimal(dtOpening.Rows[0]["OpeningBalance"]);
            else
                openingAmt = 0;

            DataTable dtTradingDebit = GetTradingDebitAndCreditDetailsStr(headId, FinancialYear.start_date, FinancialYear.end_date, 1);
            if (dtTradingDebit.Rows.Count > 0)
            {
                foreach (DataRow dtRow in dtTradingDebit.Rows)
                {
                    debitAmt += Convert.ToDecimal(dtRow["DebitAmount"].ToString());
                }
            }

            DataTable dtTradingCredit = GetTradingDebitAndCreditDetailsStr(headId, FinancialYear.start_date, FinancialYear.end_date, 2);
            if (dtTradingCredit.Rows.Count > 0)
            {
                foreach (DataRow dtRow in dtTradingCredit.Rows)
                {
                    creditAmt += Convert.ToDecimal(dtRow["CreditAmount"].ToString());
                }
            }
            if (openingAmt >= 0)
                debitAmt = debitAmt + openingAmt;
            else
                creditAmt = creditAmt - openingAmt;
            openingAmt = debitAmt - creditAmt;
            //string sqlQuery = "EXEC dbo.Stpro_GeneralHeadOpeningByHeadId " + CompanyId + "," + BranchId + ","+ accHeadId + ","+ FinancialYear.FinancialYearId + "";
            //var dtOpening = ExecuteQuery(sqlQuery);
            //openingAmt = Convert.ToDecimal(dtOpening.Rows[0]["OpeningBalance"]);
            return openingAmt;
        }

        public decimal SupplierOpening(int supplierId)
        {
            decimal openingAmt = 0, debitAmt = 0, creditAmt = 0, FirstFinYrOpAmt = 0;
            string query = "select OpeningBalanceRecover from tbl_Suppliers where SupplierId='" + supplierId + "' ";
            var dtFrstOpng = ExecuteQuery(query);
            if (dtFrstOpng.Rows.Count > 0)
            {
                FirstFinYrOpAmt = Convert.ToDecimal(dtFrstOpng.Rows[0]["OpeningBalanceRecover"]);
            }

            var dtOpening = GetOpeningByHeadId(supplierId, FinancialYear.FinancialYearId, 0, 0, string.Empty, 1);
            if (dtOpening.Rows.Count > 0)
                openingAmt = Convert.ToDecimal(dtOpening.Rows[0]["OpeningBalance"]);
            else
                openingAmt = 0;

            DataTable dtTradingDebit = GetTradingDebitAndCreditDetailsStr(supplierId, FinancialYear.start_date, FinancialYear.end_date, 1);
            if (dtTradingDebit.Rows.Count > 0)
            {
                foreach (DataRow dtRow in dtTradingDebit.Rows)
                {
                    debitAmt += Convert.ToDecimal(dtRow["DebitAmount"].ToString());
                }
            }

            DataTable dtTradingCredit = GetTradingDebitAndCreditDetailsStr(supplierId, FinancialYear.start_date, FinancialYear.end_date, 2);
            if (dtTradingCredit.Rows.Count > 0)
            {
                foreach (DataRow dtRow in dtTradingCredit.Rows)
                {
                    creditAmt += Convert.ToDecimal(dtRow["CreditAmount"].ToString());
                }
            }
            if (openingAmt >= 0)
                debitAmt = debitAmt + openingAmt;
            else
                creditAmt = creditAmt - openingAmt;
            openingAmt = debitAmt - creditAmt;
            //string sqlQuery = "EXEC dbo.Stpro_GeneralHeadOpeningByHeadId " + CompanyId + "," + BranchId + ","+ accHeadId + ","+ FinancialYear.FinancialYearId + "";
            //var dtOpening = ExecuteQuery(sqlQuery);
            //openingAmt = Convert.ToDecimal(dtOpening.Rows[0]["OpeningBalance"]);
            return openingAmt;
        }

        #region QueryBuilder
        public DataTable GetTradingDebitAndCreditDetailsStr(int accountHeadId, DateTime startDate, DateTime endDate, int action)
        {
            string sqlQuery = "exec stpro_Accounts " + CompanyId + "," + BranchId + "," + accountHeadId + ", " + FinancialYear.FinancialYearId + ",'" + Convert.ToDateTime(startDate).ToString("yyyy/MM/dd") + "','" + Convert.ToDateTime(endDate).ToString("yyyy/MM/dd") + "'," + action + "";
            var dTable = ExecuteQuery(sqlQuery);
            return dTable;
        }

        public DataTable GetOpeningLedgerHeads()
        {
            string sqlQuery = "EXEC dbo.stpro_ledgerDetails " + CompanyId + "," + BranchId + "," + 1 + "";
            var dTable = ExecuteQuery(sqlQuery);
            return dTable;
        }


        public DataTable GetOpeningByHeadId(int headId, int fyi, decimal opening, decimal balance, string openingType, int action)
        {
            string sqlQuery = "EXEC stpro_OpeningHeads " + headId + "," + CompanyId + "," + BranchId + "," + fyi + "," + opening + "," + balance + ",'" + openingType + "'," + action + "";
            var dTable = ExecuteQuery(sqlQuery);
            return dTable;
        }

        public DataTable SupplierHead()
        {
            string sqlQuery = " exec stpro_Suppliers " + CompanyId + "," + BranchId + ", 1 ";
            var dTable = ExecuteQuery(sqlQuery);
            return dTable;
        }

        public DataTable GetHeadDetails(int headId)
        {
            string sqlQuery = "exec stpro_AccountHead " + headId + "," + CompanyId + "," + BranchId + ",1";
            var dTable = ExecuteQuery(sqlQuery);
            return dTable;
        }

        public DataTable GetAllClientDetails()
        {
            string sqlQuery = "exec stpro_ClientMaster " + CompanyId + "," + BranchId + ",1";
            var dTable = ExecuteQuery(sqlQuery);
            return dTable;
        }

        public DataTable GetAllStage()
        {
            string sqlQuery = "exec stpro_ProjectStages  " + CompanyId + "," + BranchId + ",1";
            var dTable = ExecuteQuery(sqlQuery);
            return dTable;
        }
        public string GetHeadId(string headName)
        {
            string sqlQuery = "select AccountHeadId,AccountTypeId,AccountGroupId,AccountSubGroupId,CompanyId,OpeningAmount,OpeningType,description," +
                "AccountHeadName from tbl_AccountHead with(nolock) where AccountHeadName='" + headName + "' and CompanyId=" + CompanyId + " and BranchId = " + BranchId + "";
            var dTable = ExecuteQuery(sqlQuery);
            return dTable.Rows[0]["AccountHeadId"].ToString();
        }

        public DataTable GetAllStock(int financialYearId)
        {
            string sqlQuery = " select * from tbl_Stock " +
                "where CompanyId = " + CompanyId + " and BranchId = " + BranchId + " and FinancialYearId = " + financialYearId + "  ";
            var dTable = ExecuteQuery(sqlQuery);
            return dTable;
        }

        //public void UpdateStock(IEnumerable<Stock> stockDetails)
        //{
        //    try
        //    {
        //        var materialId = new SqlParameter("@materialId", "0");
        //        var item = new SqlParameter("@item", JsonConvert.SerializeObject(stockDetails));
        //        var companyId = new SqlParameter("@CompanyId", CompanyId);
        //        var branchId = new SqlParameter("@BranchId", BranchId);
        //        var FinancialYearId = new SqlParameter("@FinancialYearId", "0");
        //        var Action = new SqlParameter("@Action", 7);
        //        _dbContext.Database.ExecuteSqlRawAsync("Stpro_Stock @materialId,@item, @CompanyId, @BranchId,@FinancialYearId, @Action",
        //            materialId, item, companyId, branchId, FinancialYearId, Action);
        //    }
        //    catch (Exception)
        //    { throw; }

        //    //var item = new SqlParameter("@item", JsonConvert.SerializeObject(stockDetails));

        //    //string sqlQuery = " update tbl_Stock set StockIn = " + stock + " where StockId= " + stockId + "  " +
        //    //    "where CompanyId = " + CompanyId + " and BranchId = " + BranchId + " and FinancialYearId = " + financialYearId + "  ";
        //    //var dTable = ExecuteQuery(sqlQuery);
        //}

        //public void InsertStock(IEnumerable<Stock> stockDetails)
        //{
        //    try
        //    {
        //        var materialId = new SqlParameter("@materialId", "0");
        //        var item = new SqlParameter("@item", JsonConvert.SerializeObject(stockDetails));
        //        var companyId = new SqlParameter("@CompanyId", CompanyId);
        //        var branchId = new SqlParameter("@BranchId", BranchId);
        //        var FinancialYearId = new SqlParameter("@FinancialYearId", FinancialYear.FinancialYearId);
        //        var Action = new SqlParameter("@Action", 8);
        //        _dbContext.Database.ExecuteSqlRawAsync("Stpro_Stock @materialId,@item, @CompanyId, @BranchId,@FinancialYearId, @Action",
        //            materialId, item, companyId, branchId, FinancialYearId, Action);
        //    }
        //    catch (Exception)
        //    { throw; }
        //}

        public DataTable StockUpdate(int compId, int branId, int fyid, int newFyid)
        {
            string sqlQuery = "EXEC dbo.Stpro_FinStock " + compId + "," + branId + "," + fyid + ", " + newFyid + "";
            var dTable = ExecuteQuery(sqlQuery);
            return dTable;
        }

        public void UpdateVoucherSqlStr(string voucherTypeId, int voucherNumber, int fyiId)
        {
            string sqlQuery = "exec stpro_Voucher '" + voucherTypeId + "','',''," + voucherNumber + ",'" + CompanyId + "','" + fyiId + "','U'";
            ExecuteQuery(sqlQuery);
        }

        public DataTable GetVoucherSqlStr(string voucherTypeName, int fyiId)
        {
            string sqlQuery = "exec stpro_Voucher '','" + voucherTypeName + "','','','" + CompanyId + "','" + fyiId + "','Select'";
            var dTable = ExecuteQuery(sqlQuery);
            return dTable;
        }

        public DataTable GetEmployeeCategoryWise(string categoryName)
        {
            string sqlQuery = "Select Id as EmployeeId, FullName, EmployeeCategoryId from tbl_EmployeeMaster where EmployeeCategoryId  in (select EmployeeCategoryId from tbl_EmployeeCategory where EmployeeCategoryName='" + categoryName + "')";
            //dt = ExecuteQuery(sqlQuery);
            //int id = 0;
            //if (dt.Rows.Count > 0)
            //{
            //    id = Convert.ToInt32(dt.Rows[0][0].ToString());
            //}
            //return id;
            var dTable = ExecuteQuery(sqlQuery);
            return dTable;
        }
        private DataTable GetSiteManagerOpeningById(int companyId, int branchId, int financialYearId, int siteManagerId)
        {
            string sqlQuery = "select Amount as OpeningBalance, DebitHeadId  from tbl_SitemanagersTransactions where CompanyId=" + companyId + " and BranchId = " + branchId + " and FinancialYearId = " + financialYearId + " and EmployeeId = " + siteManagerId + " ";
            var dTable = ExecuteQuery(sqlQuery);
            return dTable;
        }

        private DataTable GetSiteManagerBalance(int companyId, int branchId, int financialYearId, int siteManagerId)
        {
            string sqlQuery = "select Id,TransactionDate,ProjectId,UnitId,CompanyId,DebitHeadId,CreditHeadId,Narration,EmployeeId,Amount  from tbl_SitemanagersTransactions " +
                "where CompanyId=" + companyId + " and BranchId = " + branchId + " and FinancialYearId = " + financialYearId + " and EmployeeId = " + siteManagerId + " and IsDeleted =0";
            var dTable = ExecuteQuery(sqlQuery);
            return dTable;
        }

        private DataTable GetAccountHead(int companyId, int branchId, int financialYearId, int AccountHeadId)
        {
            string sqlQuery = "select AccountTypeId,AccountGroupId,AccountSubGroupId,CompanyId,OpeningAmount,OpeningType,description,AccountHeadName,AccountSubGroupName" +
                " from View_AccountHead with(nolock) where AccountHeadId = " + AccountHeadId + " and CompanyId = " + companyId + " and BranchId = " + branchId + " and FinancialYearId = " + financialYearId + " ";
            var dTable = ExecuteQuery(sqlQuery);
            return dTable;
        }

        private void UpdateSiteManager(int companyId, int branchId, int financialYearId, int siteManagerId, decimal opAmt, decimal balanceAdjusted, string openingType)
        {
            string sqlQuery = "update tbl_SitemanagersTransactions set Amount=" + opAmt + ",BalanceAdjusted=" + balanceAdjusted + ", OpeningType =" + openingType + " where EmployeeId = " + siteManagerId + " " +
                "and CompanyId=" + companyId + " and BranchId = " + branchId + " and FinancialYearId = " + financialYearId + " ";
            ExecuteQuery(sqlQuery);
        }
        private void InsertSiteManager(int companyId, int branchId, int financialYearId, int siteManagerId, string fullName, decimal opAmt, string openingType)
        {
            string sqlQuery = "Insert into tbl_SitemanagersTransactions (EmployeeId, Amount, CompanyId, BranchId, FinancialYearId, ApprovalStatus, IsDeleted) values" +
               "(" + siteManagerId + ", " + opAmt + ", " + companyId + ", " + branchId + ", " + financialYearId + ", 1, 0)";
            ExecuteQuery(sqlQuery);
        }

        #endregion

        #region Data

        private DataTable ExecuteQuery(string sqlQuery)
        {
            DbCommand cmd = _dbContext.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = sqlQuery;
            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = CompanyId });
            //cmd.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int) { Value = BranchId });
            //cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.Int) { Value = 1 });
            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            DbDataReader reader = cmd.ExecuteReader();
            var dataTable = new DataTable();
            dataTable.Load(reader);
            return dataTable;
        }

        #endregion


    }
}
