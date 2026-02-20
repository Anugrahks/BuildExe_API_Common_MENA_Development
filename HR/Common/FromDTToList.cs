using BuildExeHR.Models;
using System;
using System.Data;
using System.Linq;

namespace BuildExeHR.Common
{
    public class FromDTToList
    {
        public DataTable ConvertToList(LeaveApplication leaveapplication)
        {

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[9] { new DataColumn("EmpId", typeof(int)),
                            new DataColumn("HeadName", typeof(string)),
                            new DataColumn("HeadId",typeof(int)),
                             new DataColumn("YearId",typeof(int)),

                               new DataColumn("MonthId",typeof(int)),
                                new DataColumn("CompanyId",typeof(int)),
                                  new DataColumn("BranchId",typeof(int)),
                                  new DataColumn("Amount",typeof(decimal)),
                                    new DataColumn("LeaveApplicationId",typeof(int)),




                });
            //
            DateTime startdate = leaveapplication.RequiredFrom;
            DateTime enddate2 = leaveapplication.RequiredTo;
            DateTime enddate3 = leaveapplication.RequiredTo;
//////////////// This are just random values I gave to reduce an error. The necessitty of this function is not understood at this stage of development. Might change later.
            decimal leavetotal = 15;
            decimal penaltytotal = 15;
            int startmonth = startdate.Month;
            int endmonth = enddate2.Month;
            int startyear = startdate.Year;
            int endyear = enddate2.Year;
            int cntr = 1;
            int initialcntr = 1;
            int finalcntr = 0;
            int totaldays = 0;
            int balanceday = 0;
            decimal onedayleaveamount = 0.00M;
            decimal onedaypenaltyamount = 0.00M;
            decimal totalleaveamount = 0.00M;
            decimal totalpenaltyamount = 0.00M;
            if (startmonth == endmonth && startyear == endyear)
            {
                totaldays = (enddate2 - startdate).Days;
                totaldays = totaldays + 1;
                if (leavetotal > 0)
                {
                   
                    

                    dt.Rows.Add(leaveapplication.EmployeeId, "Reimbursement", 1, startdate.Year, startdate.Month, leaveapplication.CompanyId, leaveapplication.BranchId, leavetotal, 0);
                }

                if (penaltytotal > 0)
                {

                   
                    dt.Rows.Add(leaveapplication.EmployeeId, "Penalty", 2, startdate.Year, startdate.Month, leaveapplication.CompanyId, leaveapplication.BranchId, penaltytotal, 0);

                }



            }

            else
            {
                totaldays = (enddate2 - startdate).Days;
                totaldays = totaldays + 1;
                if (leavetotal > 0)
                {
                    onedayleaveamount = leavetotal / totaldays;
                    onedayleaveamount = Decimal.Round(onedayleaveamount, 2);

                }

                if (penaltytotal > 0)
                {

                    onedaypenaltyamount = penaltytotal / totaldays;
                    onedaypenaltyamount = Decimal.Round(onedaypenaltyamount, 2);
                }



                // set end-date to end of month
                enddate2 = new DateTime(enddate2.Year, enddate2.Month, DateTime.DaysInMonth(enddate2.Year, enddate2.Month));

                var diff = Enumerable.Range(0, Int32.MaxValue)
                                     .Select(e => startdate.AddMonths(e))
                                     .TakeWhile(e => e <= enddate2)
                                     .Select(e => e.Month + "_" + e.Year);
                finalcntr = diff.Count();
                foreach (var item in diff)
                {
                    if (cntr == initialcntr)
                    {
                        int days = DateTime.DaysInMonth(@startdate.Year, @startdate.Month);
                        balanceday = (days - startdate.Day) + 1;
                        if (leavetotal > 0)
                        {
                            totalleaveamount = balanceday * onedayleaveamount;
                            totalleaveamount = Decimal.Round(totalleaveamount, 2);
                            dt.Rows.Add(leaveapplication.EmployeeId, "REIMBURSEMENT", 1, startdate.Year, startdate.Month, leaveapplication.CompanyId, leaveapplication.BranchId, totalleaveamount, 0);

                        }

                        if (penaltytotal > 0)
                        {

                            totalpenaltyamount = balanceday * onedaypenaltyamount;
                            totalpenaltyamount = Decimal.Round(totalpenaltyamount, 2);
                            dt.Rows.Add(leaveapplication.EmployeeId, "PENALTY", 2, startdate.Year, startdate.Month, leaveapplication.CompanyId, leaveapplication.BranchId, totalpenaltyamount, 0);
                        }



                    }
                    else if (cntr == finalcntr)
                    {
                        balanceday = enddate3.Day;
                        if (leavetotal > 0)
                        {
                            totalleaveamount = balanceday * onedayleaveamount;
                            totalleaveamount = Decimal.Round(totalleaveamount, 2);
                            dt.Rows.Add(leaveapplication.EmployeeId, "Reimbursement", 1, enddate2.Year, enddate2.Month, leaveapplication.CompanyId, leaveapplication.BranchId, totalleaveamount, 0);

                        }

                        if (penaltytotal > 0)
                        {

                            totalpenaltyamount = balanceday * onedaypenaltyamount;
                            totalpenaltyamount = Decimal.Round(totalpenaltyamount, 2);
                            dt.Rows.Add(leaveapplication.EmployeeId, "Penalty", 2, enddate2.Year, enddate2.Month, leaveapplication.CompanyId, leaveapplication.BranchId, totalpenaltyamount, 0);
                        }
                        // adding to datatable


                    }

                    else

                    {
                        string[] data = item.Split("_");

                        balanceday = DateTime.DaysInMonth(Convert.ToInt32(data[1]), Convert.ToInt32(data[0]));

                        if (leavetotal > 0)
                        {
                            totalleaveamount = balanceday * onedayleaveamount;
                            totalleaveamount = Decimal.Round(totalleaveamount, 2);
                            dt.Rows.Add(leaveapplication.EmployeeId, "Reimbursement", 1, data[1], data[0], leaveapplication.CompanyId, leaveapplication.BranchId, totalleaveamount, 0);

                        }

                        if (penaltytotal > 0)
                        {

                            totalpenaltyamount = balanceday * onedaypenaltyamount;
                            totalpenaltyamount = Decimal.Round(totalpenaltyamount, 2);
                            dt.Rows.Add(leaveapplication.EmployeeId, "Penalty", 2, data[1], data[0], leaveapplication.CompanyId, leaveapplication.BranchId, totalpenaltyamount, 0);
                        }


                    }

                    cntr = cntr + 1;

                }




            }

            return dt;

        }
    }
}
