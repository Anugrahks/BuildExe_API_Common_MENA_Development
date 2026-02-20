using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildExeBasic.Models;
using System.Transactions;
using BuildExeBasic.Repository;
using BuildExeBasic.Helper;
using System.IO;
using System.Net.Http.Headers;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace BuildExeBasic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrintableReportConfigurationController : ControllerBase
    {
        private readonly IPrintableReportConfigurationRepository _printableReportConfigurationRepository;
        private readonly IPrintableReportFieldRepository _printableReportFieldRepository;
        private readonly IFlatDictionaryProvider _flatDictionaryProvider;
        public PrintableReportConfigurationController(
            IPrintableReportConfigurationRepository printableReportConfigurationRepository,
            IPrintableReportFieldRepository printableReportFieldRepository,
            IFlatDictionaryProvider flatDictionaryProvider)
        {
            _printableReportConfigurationRepository = printableReportConfigurationRepository;
            _printableReportFieldRepository = printableReportFieldRepository;
            _flatDictionaryProvider = flatDictionaryProvider;
        }

        [HttpGet("{id}/{CompanyId}/{BranchID}")]
        [Authorize]

        public async Task<IActionResult> Get(int id, int CompanyId, int BranchID)
        {
            try
            {
                var templates = await _printableReportConfigurationRepository.GetByMenuID(id, CompanyId, BranchID);
                return new OkObjectResult(templates);
            }
            catch (Exception)
            { throw; }
        }

        [HttpGet("{id}")]
        [Authorize]

        public async Task<IActionResult> GetByID(int id)
        {
            try
            {
                var templates = await _printableReportConfigurationRepository.GetByID(id);
                return new OkObjectResult(templates);
            }
            catch (Exception)
            { throw; }
        }

        [HttpGet()]
        [Authorize]

        public async Task<IActionResult> GetAll()
        {
            try
            {

                var templates = await _printableReportConfigurationRepository.GetPrintableReportConfigurationList();
                return new OkObjectResult(templates);
            }
            catch (Exception)
            { throw; }
        }

        [HttpGet("List/{CompanyId}/{BranchId}")]
        [Authorize]

        public async Task<IActionResult> Getlist(int CompanyId, int BranchId)
        {
            try
            {
                var templates = await _printableReportConfigurationRepository.GetPrintableReportConfigurationlistcompanybranch(CompanyId, BranchId);
                return new OkObjectResult(templates);
            }
            catch (Exception)
            { throw; }
        }


        [HttpGet("Menu/{id}")]
        [Authorize]

        public async Task<IActionResult> GetByMenuId(int id)
        {
            try
            {
                var templates = await _printableReportConfigurationRepository.GetPrintableReportConfigurationListById(id);
                return new OkObjectResult(templates);
            }
            catch (Exception)
            { throw; }
        }


        [HttpGet("Menu/{id}/{CompanyId}/{BranchId}")]
        [Authorize]

        public async Task<IActionResult> GetByMenucompanybranchId(int id, int companyid, int branchid)
        {
            try
            {
                var templates = await _printableReportConfigurationRepository.GetPrintableReportConfigurationListBycompanybranchId(id, companyid, branchid);
                return new OkObjectResult(templates);
            }
            catch (Exception)
            { throw; }
        }

        [HttpDelete("{id}")]
        [Authorize]

        public async Task DeletePrintableReportConfiguration(int id)
        {
            try
            {
                await _printableReportConfigurationRepository.DeletePrintableReportConfiguration(id);
            }
            catch (Exception)
            { throw; }
        }

        [HttpPost]
        [Authorize]

        public async Task<IActionResult> Post([FromBody] PrintableReportConfiguration configurations)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var val = await _printableReportConfigurationRepository.Insert(configurations);
                    scope.Complete();
                    return new OkObjectResult(val);
                }
            }
            catch (Exception)
            { throw; }
        }

 
        [HttpPut]
        [Authorize]


        public async Task<IActionResult> Put([FromBody] PrintableReportConfiguration configurations)
        {
            try
            {
                await _printableReportConfigurationRepository
              .UpdatePrintableReportConfiguration(configurations.Id, configurations.MenuId, configurations.TemplateName, configurations.TemplateStructure, configurations.WatermarkText, configurations.PageSize);
                return Ok();
            }
            catch (Exception)
            { throw; }
        }





        [HttpDelete("{id}/{UserID}")]
        [Authorize]

        public async Task<IActionResult> Delete(int id, int UserID)
        {
            try
            {
                var val = await _printableReportConfigurationRepository.Delete(id, UserID);
                return new OkObjectResult(val);
            }
            catch (Exception)
            { throw; }
        }

        [HttpGet("purchaseOrder/{id}")]
        [Authorize]

        public async Task<IActionResult> PurchaseOrderById(int id)
        {
            try
            {

                var purchaseOrders = await _printableReportFieldRepository.GetPurchaseOrderById(id);
                var purchaseOrderDetails = await _printableReportFieldRepository.GetPurchaseOrderDetailsByPurchaseOrderId(id);
                var flattenedPurchaseOrder = _flatDictionaryProvider.Execute(purchaseOrders);
                var purchaseOrderDetailsViewModelList = new List<ReportFieldsValuesViewModel>();

                foreach (var item in purchaseOrderDetails)
                {
                    var purchaseOrderDetailsflat = _flatDictionaryProvider.Execute(item);
                    var reportFieldsValuesViewModel = new ReportFieldsValuesViewModel()
                    {
                        ReportFields = purchaseOrderDetailsflat.ToList()
                    };

                    purchaseOrderDetailsViewModelList.Add(reportFieldsValuesViewModel);
                }
                var tabletypeReportFields = MapTableTypeReportFieldsViewModel("PurchaseOrderDetails", purchaseOrderDetailsViewModelList);
                var purchaseOrderForReportViewModel = new PrintableReportViewModel()
                {
                    ReportFields = flattenedPurchaseOrder.ToList()
                };
                purchaseOrderForReportViewModel.TableTypeReportFields.Add(tabletypeReportFields);
                return new OkObjectResult(purchaseOrderForReportViewModel);
            }
            catch (Exception)
            { throw; }


        }


        [HttpGet("partbill/{id}")]
        [Authorize]

        public async Task<IActionResult> partbillbyid(int id)

        {
            try
            {

                var partbills = await _printableReportFieldRepository.GetPartbillbyId(id);
                var partbillDetails = await _printableReportFieldRepository.GetPartbilldetailsByPartbillId(id);
                var flattenedpartbill = _flatDictionaryProvider.Execute(partbills);
                var partbillDetailsViewModelList = new List<ReportFieldsValuesViewModel>();

                foreach (var item in partbillDetails)
                {
                    var partbillDetailsflat = _flatDictionaryProvider.Execute(item);
                    var reportFieldsValuesViewModel = new ReportFieldsValuesViewModel()
                    {
                        ReportFields = partbillDetailsflat.ToList()
                    };

                    partbillDetailsViewModelList.Add(reportFieldsValuesViewModel);
                }
                var tabletypeReportFields = MapTableTypeReportFieldsViewModel("PartbillDetails", partbillDetailsViewModelList);
                var partbillForReportViewModel = new PartBillForReportViewModel()
                {
                    ReportFields = flattenedpartbill.ToList()
                };
                partbillForReportViewModel.TableTypeReportFields.Add(tabletypeReportFields);
                return new OkObjectResult(partbillForReportViewModel);
            }
            catch (Exception)
            { throw; }


        }


        [HttpGet("printable/{id}/{Menuid}")]
        [Authorize]


        public async Task<IActionResult> Getbyid(int id, int Menuid)
        {
            try
            {
                PrintableReportViewModel printableReportViewModel = new PrintableReportViewModel();
                var printable = await _printableReportFieldRepository.GetPrintableById(id, Menuid);
                if (!printable.Equals("[]"))
                {
                    printableReportViewModel = JsonConvert.DeserializeObject<List<PrintableReportViewModel>>(printable).FirstOrDefault();
                }
                return new OkObjectResult(printableReportViewModel);
            }
            catch (Exception ex)
            { return StatusCode(500, $"Internal Server Error: {ex}"); }
        }


        [HttpGet("printable/{id}/{DivisionId}/{Menuid}")]
        [Authorize]


        public async Task<IActionResult> Getbyidwithdivision(int id,int DivisionId, int Menuid)
        {
            try
            {
                PrintableReportViewModel printableReportViewModel = new PrintableReportViewModel();
                var printable = await _printableReportFieldRepository.GetPrintableByIdDivision(id, DivisionId, Menuid);
                if (!printable.Equals("[]"))
                {
                    printableReportViewModel = JsonConvert.DeserializeObject<List<PrintableReportViewModel>>(printable).FirstOrDefault();
                }
                return new OkObjectResult(printableReportViewModel);
            }
            catch (Exception ex)
            { return StatusCode(500, $"Internal Server Error: {ex}"); }
        }


        [HttpGet("salaryslip/{EmployeeId}/{CompanyId}/{BranchId}/{MonthId}/{YearId}/{FinancialYearId}")]
        [Authorize]


        public async Task<IActionResult> Getforsalaryslip(int EmployeeId, int CompanyId, int BranchId, int MonthId, int YearId, int FinancialYearId)
        {
            try
            {
                PrintableReportSalaryModel printableReportViewModel = new PrintableReportSalaryModel();
                var printable = await _printableReportFieldRepository.GetsalaryslipById(DateTime.Now,EmployeeId, CompanyId, BranchId, MonthId, YearId, FinancialYearId);
                if (!printable.Equals("[]"))
                {
                    printableReportViewModel = JsonConvert.DeserializeObject<List<PrintableReportSalaryModel>>(printable).FirstOrDefault();
                }
                return new OkObjectResult(printableReportViewModel);
            }
            catch (Exception)
            { throw; }
        }


        [HttpGet("salaryslipDuration/{EmployeeId}/{CompanyId}/{BranchId}/{MonthId}/{YearId}/{FinancialYearId}/{DurationId}/{FromDate}/{ToDate}")]
        [Authorize]


        public async Task<IActionResult> Getforsalaryslipduration(int EmployeeId, int CompanyId, int BranchId, int MonthId, int YearId, int FinancialYearId, int DurationId, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                PrintableReportSalaryModel printableReportViewModel = new PrintableReportSalaryModel();
                var printable = await _printableReportFieldRepository.Getforsalaryslipduration(DateTime.Now,EmployeeId, CompanyId, BranchId, MonthId, YearId, FinancialYearId, DurationId, FromDate, ToDate);
                if (!printable.Equals("[]"))
                {
                    printableReportViewModel = JsonConvert.DeserializeObject<List<PrintableReportSalaryModel>>(printable).FirstOrDefault();
                }
                return new OkObjectResult(printableReportViewModel);
            }
            catch (Exception)
            { throw; }
        }


        [HttpGet("salaryslip/{SalaryBillDate}/{EmployeeId}/{CompanyId}/{BranchId}/{MonthId}/{YearId}/{FinancialYearId}")]
        [Authorize]


        public async Task<IActionResult> Getforsalaryslip(DateTime SalaryBillDate,int EmployeeId, int CompanyId, int BranchId, int MonthId, int YearId, int FinancialYearId)
        {
            try
            {
                PrintableReportSalaryModel printableReportViewModel = new PrintableReportSalaryModel();
                var printable = await _printableReportFieldRepository.GetsalaryslipById(SalaryBillDate,EmployeeId, CompanyId, BranchId, MonthId, YearId, FinancialYearId);
                if (!printable.Equals("[]"))
                {
                    printableReportViewModel = JsonConvert.DeserializeObject<List<PrintableReportSalaryModel>>(printable).FirstOrDefault();
                }
                return new OkObjectResult(printableReportViewModel);
            }
            catch (Exception)
            { throw; }
        }


        [HttpGet("salaryslipDuration/{SalaryBillDate}/{EmployeeId}/{CompanyId}/{BranchId}/{MonthId}/{YearId}/{FinancialYearId}/{DurationId}/{FromDate}/{ToDate}")]
        [Authorize]


        public async Task<IActionResult> Getforsalaryslipduration(DateTime SalaryBillDate, int EmployeeId, int CompanyId, int BranchId, int MonthId, int YearId, int FinancialYearId, int DurationId, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                PrintableReportSalaryModel printableReportViewModel = new PrintableReportSalaryModel();
                var printable = await _printableReportFieldRepository.Getforsalaryslipduration(SalaryBillDate,EmployeeId, CompanyId, BranchId, MonthId, YearId, FinancialYearId, DurationId, FromDate, ToDate);
                if (!printable.Equals("[]"))
                {
                    printableReportViewModel = JsonConvert.DeserializeObject<List<PrintableReportSalaryModel>>(printable).FirstOrDefault();
                }
                return new OkObjectResult(printableReportViewModel);
            }
            catch (Exception)
            { throw; }
        }

        [HttpPost("upload")]
        [Authorize]

        public async Task<IActionResult> PrintableReportConfigurationUpload(
    [FromForm] PrintableReportConfigurationImage configurations
)
        {
            try
            {
                if (configurations.Image.Length > 0)
                {
                    var pathToSave = string.Empty;
                    string fullPath = string.Empty;

                    if (configurations.ImageType == 1)
                    {
                        var folderName = Path.Combine("Upload", "Headers");
                        pathToSave = GetAbsolutePath(folderName);
                        fullPath = await saveToPath(pathToSave, configurations.Image);
                    }

                    if (configurations.ImageType == 2)
                    {
                        var folderName = Path.Combine("Upload", "Footers");
                        pathToSave = GetAbsolutePath(folderName);
                        fullPath = await saveToPath(pathToSave, configurations.Image);
                    }

                    configurations.fileName = fullPath;
                    await _printableReportConfigurationRepository.PrintableReportConfigurationUpload(configurations);
                    return Ok();
                }
                else
                {
                    return BadRequest("No file uploaded.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex}");
            }
        }

        public async Task<string> saveToPath(string pathToSave, IFormFile file)
        {
            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            fileName = AppendTimeStamp(fileName);
            var fullPath = Path.Combine(pathToSave, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fullPath;
        }



        public string GetAbsolutePath(string folderName)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), folderName);

        }


        [HttpGet("upload/{PrintableReportConfigurationId}/{ImageType}")]
        [Authorize]

        public async Task<IActionResult> GetPrintableReportConfigurationImage(int PrintableReportConfigurationId, int ImageType)
        {
            try
            {
                var printableReportConfigurationImage = await _printableReportConfigurationRepository.GetPrintableReportConfigurationImage(PrintableReportConfigurationId, ImageType);
                if (System.IO.File.Exists(printableReportConfigurationImage.Image))
                {
                    var fileStream = System.IO.File.OpenRead(printableReportConfigurationImage.Image);
                    return File(fileStream, "image/jpeg");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex}");
            }
        }

        [HttpGet("purchaseOrder/reprint")]
        [Authorize]
        public async Task<IActionResult> PurchaseOrderReprint([FromQuery] int ProjectId, int SupplierId, int ApprovedStatus, string ToDate, string FromDate)
        {
            try
            {
                DateTime? toDate = null;
                DateTime? fromDate = null;

                if (!string.IsNullOrEmpty(ToDate) && DateTime.TryParse(ToDate, out DateTime parsedToDate))
                {
                    toDate = parsedToDate;
                }

                if (!string.IsNullOrEmpty(FromDate) && DateTime.TryParse(FromDate, out DateTime parsedFromDate))
                {
                    fromDate = parsedFromDate;
                }


                var purchaseOrder = await _printableReportConfigurationRepository.PurchaseOrderReprint(ProjectId, SupplierId, ApprovedStatus, toDate, fromDate);
                return new OkObjectResult(purchaseOrder);

            }
            catch (Exception ex) { throw ex; }
        }





        //     public async Task<IActionResult> Getreprint(int Menuid)
        //   {
        //     try
        //   {
        //     var printable = await _printableReportConfigurationRepository.Getreprintmenu(Menuid);
        //   return new OkObjectResult(printable);
        //           }
        //     catch (Exception)
        //     { throw; }
        //    }/



        public string AppendTimeStamp(string fileName)
        {
            return Path.Combine(Path.GetDirectoryName(fileName), string.Concat(Path.GetFileNameWithoutExtension(fileName),
                                                                               DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss_fff"),
                                                                               Path.GetExtension(fileName))
                );
        }
        private TableTypeReportFieldsViewModel MapTableTypeReportFieldsViewModel(string tableName, List<ReportFieldsValuesViewModel> reportFieldsValues)
        {
            return new TableTypeReportFieldsViewModel()
            {
                TableTypeReportFieldValues = reportFieldsValues,
                TableName = tableName
            };
        }

        [HttpGet("Menu/dynamic/{id}/{CompanyId}/{BranchId}")]
        [Authorize]

        public async Task<IActionResult> GetByMenucompanybranchIddynamic(int id, int companyid, int branchid)
        {
            try
            {
                var templates = await _printableReportConfigurationRepository.GetPrintableReportConfigurationListBycompanybranchIddynamic(id, companyid, branchid);
                return new OkObjectResult(templates);
            }
            catch (Exception)
            { throw; }
        }


        [HttpPost("PrintableStyle")]
        [Authorize]
        public async Task<IActionResult> SetPrintableReportStyle([FromBody] IEnumerable<PrintableReportCSS> myStyles)
        {
            try
            {
                var res = await _printableReportConfigurationRepository.SetPrintableReportStyle(myStyles);
                return new OkObjectResult(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = $"An error occurred: {ex.Message}",
                    statusCode = 0
                });
            }
        }


        [HttpGet("PrintableStyle/{id}")]
        [Authorize]
        public async Task<IActionResult> GetPrintableReportStyle(int id)
        {
            try
            {
                var templates = await _printableReportConfigurationRepository.GetPrintableReportStyle(id);
                return new OkObjectResult(templates);
            }
            catch (Exception)
            { throw; }
        }

        [HttpGet("StaticPrintable/{BranchId}/{ReportId}/{RecordId}")]
        [Authorize]
        public async Task<IActionResult> GetStaticReportData(int BranchId, int ReportId, int RecordId)
        {
            try
            {
                var validation = await _printableReportConfigurationRepository.GetStaticReportData(BranchId, ReportId, RecordId);

                return new OkObjectResult(validation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = $"An error occurred: {ex.Message}",
                    statusCode = 0
                });
            }
        }


        [HttpGet("StaticPrintablePurchaseOrder/{BranchId}/{ReportId}/{RecordId}")]
        [Authorize]
        public async Task<IActionResult> StaticPrintablePurchaseOrder(int BranchId, int ReportId, int RecordId)
        {
            try
            {
                var validation = await _printableReportConfigurationRepository.StaticPrintablePurchaseOrder(BranchId, ReportId, RecordId);

                return new OkObjectResult(validation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = $"An error occurred: {ex.Message}",
                    statusCode = 0
                });
            }
        }


    }
}
