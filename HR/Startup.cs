using BuildExeHR.DBContexts;
using BuildExeHR.Library;
using BuildExeHR.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Text;

namespace BuildExeHR
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            services.AddDataProtection();
            services.AddDbContext<HRContext>(o => o.UseSqlServer(Configuration.GetConnectionString("DbConnection")));
            services.AddTransient<IEmployeeDepartmentRepository, EmployeeDepartmentRepository>();
            services.AddTransient<IEmployeeDesignationRepository, EmployeeDesignationRepository>();
            services.AddTransient<IWorkingHoursRepository, WorkingHoursRepository>();
            services.AddTransient<IHolidayRepository, HolidayRepository>();
            services.AddTransient<IEmployeeMasterRepository, EmployeeMasterRepository>();
            services.AddTransient<ISalaryItemHeadRepository, SalaryItemHeadRepository>();
            services.AddTransient<IEmployeeSalaryHeadRepository, EmployeeSalaryHeadRepository>();
            services.AddTransient<ILaboursInProjectRepository, LaboursInProjectRepository>();
            services.AddTransient<IAttendanceRepository, AttendanceRepository>();
            services.AddTransient<ILabourWorkRateRepository, LabourWorkRateRepository>();
            services.AddTransient<IForemanWorkOrderRepository, ForemanWorkOrderRepository>();
            services.AddTransient<IForemanWorkBillRepository, ForemanWorkBillRepository>();
            services.AddTransient<ISubContractorAttendanceSettingRepository, SubContractorAttendanceSettingRepository>();
            services.AddTransient<ISubContractorAttendanceRepository, SubContractorAttendanceRepository>();
            services.AddTransient<ISubContractorWorkOrderRepository, SubContractorWorkOrderRepository>();
            services.AddTransient<ISubContractorBillRepository, SubContractorBillRepository>();
            services.AddTransient<IContractorWorkOrderRepository, ContractorWorkOrderRepository>();
            services.AddTransient<ILabourAdvanceMasterRepository, LabourAdvanceMasterRepository>();
            services.AddTransient<IUserLogRepository, UserLogRepository>();
            services.AddTransient<ILeaveMasterRepository, LeaveMasterRepository>();
            services.AddTransient<IForemanPaymentRepository, ForemanPaymentRepository>();
            services.AddTransient<ISubContractorPaymentRepository, SubContractorPaymentRepository>();
            services.AddTransient<IContractorPaymentRepository, ContractorPaymentRepository>();
            services.AddTransient<IEmployeeListRepository, EmployeeListRepository>();
            services.AddTransient<IEmployeeCategoryRepository, EmployeeCategoryRepository>();
            services.AddTransient<ISalaryBillRepository, SalaryBillRepository>();
            services.AddTransient<ISalaryPaymentRepository, SalaryPaymentRepository>();
            services.AddTransient<ILabourWagePaymentRepository, LabourWagePaymentRepository>();
            services.AddTransient<IAttendanceMonthlyRepository, AttendanceMonthlyRepository>();
            services.AddTransient<IIndentRepository, IndentRepository>();
            services.AddTransient<IForemanForPaymentRepository, ForemanForPaymentRepository>();
            services.AddTransient<IContractorForPaymentRepository, ContractorForPaymentRepository>();
            services.AddTransient<ISubContractorForPaymentRepository, SubContractorForPaymentRepository>();
            services.AddTransient<IGroupLabourWagePaymentRepository, GroupLabourWagePaymentRepository>();
            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<IEmployeeLabourGroupRepository, EmployeeLabourGroupRepository>();
            services.AddTransient<IAdvanceBalanceRepository, AdvanceBalanceRepository>();
            services.AddTransient<ILoanBalanceRepository, LoanBalanceRepository>();
            services.AddTransient<IHolidayLeaveRepository, HolidayLeaveRepository>();
            services.AddTransient<IEmployeeJoiningRepository, EmployeeJoiningRepository>();
            services.AddTransient<ILeaveApplicationRepository, LeaveApplicationRepository>();
            services.AddTransient<ILeaveAccountClearenceRepository, LeaveAccountClearenceRepository>();
            services.AddTransient<IRetensionPaymentRepository, RetensionPaymentRepository>();
            services.AddTransient<IMonthlyVaryingHeadSettingRepository, MonthlyVaryingHeadSettingRepository>();
            services.AddTransient<ITableAttendanceRepository, TableAttendanceRepository>();
            services.AddTransient<IContractorStageStatusUpdationRepository, ContractorStageStatusUpdationRepository>();
            services.AddTransient<IAttendancePayRollRepository, AttendancePayRollRepository>();
            services.AddTransient<IPayrollReportRepository, PayrollReportRepository>();
            services.AddTransient<ILoanRepaymentRepository, LoanRepaymentRepository>();
            services.AddTransient<ISalaryIncrementRepository, SalaryIncrementRepository>();
            services.AddTransient<IUnsavedChangesRepository, UnsavedChangesRepository>();
            services.AddTransient<IWeeklyReportRepository, WeeklyReportRepository>();
            services.AddTransient<IAttendancePunchingRepository, AttendancePunchingRepository>();
            services.AddTransient<IOTDetailsRepository, OTDetailsRepository>();
            services.AddTransient<ITerminationResignPromotionRepository, TerminationResignPromotionRepository>();
            services.AddTransient<ILeaveSurrenderMasterRepository, LeaveSurrenderMasterRepository>();
            services.AddTransient<ISubContractorQuotationRepository, SubContractorQuotationRepository>();
            services.AddTransient<IContractorQuotationRepository, ContractorQuotationRepository>();
            services.AddTransient<ITAMasterRepository, TAMasterRepository>();
            services.AddTransient<IDSRRepository, DSRRepository>();
            services.AddTransient<IPromotionReportRepository, PromotionReportRepository>();
            services.AddTransient<IAnnualLeaveRepository, AnnualLeaveRepository>();
            services.AddScoped<IMdHashValidator, MdHashValidator>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = "https://localhost",
                    ValidAudience = "https://localhost",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("XLTRPNZ7ZsKGr5RKOLSNsJe9rgcPLLjn"))
                };

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            var webSocketOptions = new WebSocketOptions()
            {
                KeepAliveInterval = TimeSpan.FromSeconds(120),
            };

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSwagger();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger();
            //app.UseMiddleware<ModelStateMiddleware>();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Upload")),
                RequestPath = new PathString("/Upload")
            });
            //app.Use(async (context, next) =>
            //{
            //    context.Request.EnableBuffering(); // Allows reading the body multiple times
            //    using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
            //    {
            //        string body = await reader.ReadToEndAsync();
            //        Logger.InfoLog("RequestMiddleware", "RawRequestBody", $"Received Raw Body: {body}");
            //        context.Request.Body.Position = 0; // Reset the stream position for normal processing
            //    }
            //    await next();
            //});


        }
    }
}
