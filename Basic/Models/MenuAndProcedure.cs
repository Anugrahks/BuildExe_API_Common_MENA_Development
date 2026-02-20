using System.Collections.Generic;

namespace BuildExeBasic.Models
{
    public class MenuAndProcedure
    {
        public int MenuId { get; set; }
        public string Procedure { get; set; }

        public static List<MenuAndProcedure> MenuAndProcedures { get; } = InitializeList();

        private static List<MenuAndProcedure> InitializeList()
        {
            return new List<MenuAndProcedure>
            {
                new MenuAndProcedure { MenuId = 10103 , Procedure = "stpro_EmailPurchaseOrder"},
                new MenuAndProcedure { MenuId = 10112 , Procedure = "stpro_EmailSupplierAdvance"},
                new MenuAndProcedure { MenuId = 10113 , Procedure = "stpro_EmailSupplierPaymentVoucher"},
                new MenuAndProcedure { MenuId = 10207 , Procedure = "stpro_EmailLabourWagePaymentVoucher"},
                new MenuAndProcedure { MenuId = 10208 , Procedure = "stpro_EmailGroupLabourWagePaymentVoucher"},
                new MenuAndProcedure { MenuId = 10214 , Procedure = "stpro_EmailSubContractorWorkOrder"},
                new MenuAndProcedure { MenuId = 10216 , Procedure = "stpro_EmailSubContractorBill"},
                new MenuAndProcedure { MenuId = 10217 , Procedure = "stpro_EmailSubContractorPaymentVoucher"},
                new MenuAndProcedure { MenuId = 10210 , Procedure = "stpro_EmailForemanWorkOrder"},
                new MenuAndProcedure { MenuId = 10211 , Procedure = "stpro_EmailForemanBill"},
                new MenuAndProcedure { MenuId = 10212 , Procedure = "stpro_EmailForemanPaymentVoucher"},
                new MenuAndProcedure { MenuId = 10219 , Procedure = "stpro_EmailContractorWorkOrder"},
                new MenuAndProcedure { MenuId = 10220 , Procedure = "stpro_EmailContractorPaymentVoucher"},
                new MenuAndProcedure { MenuId = 10056 , Procedure = "stpro_EmailPartBill"},
                new MenuAndProcedure { MenuId = 10055 , Procedure = "stpro_EmailAdditionalBill"},
                new MenuAndProcedure { MenuId = 10054 , Procedure = "stpro_EmailStageInvoice"},
                new MenuAndProcedure { MenuId = 10058 , Procedure = "stpro_EmailClientAdvance"},
                new MenuAndProcedure { MenuId = 6102 , Procedure = "stpro_EmailStageReciept"},
                new MenuAndProcedure { MenuId = 6100 , Procedure = "stpro_EmailBillReciept"},
                new MenuAndProcedure { MenuId = 4114 , Procedure = "stpro_EmailLabourAdvance"},
                new MenuAndProcedure { MenuId = 4116 , Procedure = "stpro_EmailLabourRetension"},
                new MenuAndProcedure { MenuId = 4118 , Procedure = "stpro_EmailLabourLoan"},
                new MenuAndProcedure { MenuId = 2015 , Procedure = "stpro_EmailProjectRegistration"},
                new MenuAndProcedure { MenuId = 2016 , Procedure = "stpro_EmailProjectBooking"},
                new MenuAndProcedure { MenuId = 10204 , Procedure = "stpro_EmailSalaryPaymentVoucher"},
                new MenuAndProcedure { MenuId = 3005 , Procedure = "stpro_EmailQuotationMaterial"},

                new MenuAndProcedure { MenuId = 10107 , Procedure = "stpro_EmailPurchaseReturn"},
                new MenuAndProcedure { MenuId = 3042 , Procedure = "stpro_EmailItemIntake"},
                new MenuAndProcedure { MenuId = 3011 , Procedure = "stpro_EmailIndentCreation"},
                new MenuAndProcedure { MenuId = 3040 , Procedure = "stpro_EmailItemReturnTransfer"},
                new MenuAndProcedure { MenuId = 3048 , Procedure = "stpro_EmailSales"},
                new MenuAndProcedure { MenuId = 3046 , Procedure = "stpro_EmailSalesOrder"},
                new MenuAndProcedure { MenuId = 6200 , Procedure = "stpro_EmailFundTransferReturn"},
                new MenuAndProcedure { MenuId = 3046 , Procedure = "stpro_EmailSalesOrder"},
                new MenuAndProcedure { MenuId = 10319 , Procedure = "stpro_EmailSales"},
                new MenuAndProcedure { MenuId = 11111 , Procedure = "stpro_EmailSalarySlip"}
            };
        }

    }

     
}
