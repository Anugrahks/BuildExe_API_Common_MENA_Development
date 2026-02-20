using BuildExeBasic.DBContexts;
using BuildExeBasic.Helper;
using BuildExeBasic.Interfaces;
using BuildExeBasic.Library;
using BuildExeBasic.Models;
using BuildExeBasic.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using System.Text;

namespace BuildExeBasic
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
            services.AddDbContext<BasicContext>(o => o.UseSqlServer(Configuration.GetConnectionString("DbConnection")));
            services.Configure<MetaAuthSettings>(Configuration.GetSection("MetaAuth"));
            services.AddTransient<IAccountHeadRepository, AccountHeadRepository >();
            services.AddTransient<IBankRepository, BankRepository >();
            services.AddTransient<IWorkCategoryRepository, WorkCategoryRepository >();
            services.AddTransient<IJournalEntryRepository, JournalEntryRepository >();
            services.AddTransient<IChequeClearenceRepository, ChequeClearenceRepository >();
            services.AddTransient<ISitemanagerRepository, SitemanagerRepository >();
            services.AddTransient<IDocumentGroupRepository, DocumentGroupRepository >();
            services.AddTransient<IDocumentTypeRepository, DocumentTypeRepository >();
            services.AddTransient<IDocumentManagementRepository, DocumentManagementRepository >();
            services.AddTransient<IUserLogRepository, UserLogRepository >();
            services.AddTransient<IAccountTypeRepository , AccountTypeRepository >();
            services.AddTransient<IAccountGroupRepository, AccountGroupRepository >();
            services.AddTransient<IAccountSubGroupRepository, AccountSubGroupRepository >();
            services.AddTransient<IFinancialYearRepository, FinancialYearRepository>();
            services.AddTransient<IGodownRepository, GodownRepository >();
            services.AddTransient<IAlertRepository, AlertRepository>();
            services.AddTransient<IReportFilterRepository , ReportFilterRepository >();
            services.AddTransient<IReportFieldRepository, ReportFieldRepository>();
            services.AddTransient<IReportConfigurationRepository, ReportConfigurationRepository >();
            services.AddTransient<IDayBookRepository, DayBookRepository >();
            services.AddTransient<IGeneralLedgerRepository, GeneralLedgerRepository >();
            services.AddTransient<IRecieptsAndPaymentRepository , RecieptsAndPaymentRepository >();
            services.AddTransient<ITrialBalanceRepository, TrialBalanceRepository>();
            services.AddTransient<IExpenseIncomeRepository, ExpenseIncomeRepository >();
            services.AddTransient<IProjectAnalysisRepository, ProjectAnalysisRepository >();
            services.AddTransient<IHeaderRepository , HeaderRepository>();
            services.AddTransient<IActivateFinancialYearRepository, ActivateFinancialYearRepository>();
            services.AddTransient<IPrintableReportConfigurationRepository, PrintableReportConfigurationRepository>();
            services.AddTransient<IPrintableReportFieldRepository, PrintableReportFieldRepository>();
            services.AddTransient<IFlatDictionaryProvider, FlatDictionaryProvider>();
            services.AddTransient<ITermsAndConditionRepository, TermsAndConditionRepository>();
            services.AddTransient<IPrintableReportFilter, PrintableReportFilterRepository>();
            services.AddTransient<IEmailConfigurationRepository, EmailConfigurationRepository>();
            services.AddTransient<IGeneralAlertRepository, GeneralAlertRepository>();
            services.AddTransient<IAccountsPayableRepository, AccountsPayableRepository>();
            services.AddTransient<IAccountsReceivableRepository, AccountsReceivableRepository>();
            services.AddTransient<IEmailSmsWhatsappRepository, EmailSmsWhatsappRepository>();
            services.AddTransient<IProjectDetailExpenseRepository, ProjectDetailExpenseRepository>();
            services.AddTransient<IReleaseNotesRepository, ReleaseNotesRepository>();
            services.AddTransient<ISuperAdminRepository, SuperAdminRepository>();
            services.AddTransient<IVirtualVoucherRepository, VirtualVoucherRepository>();
            services.AddTransient<IEmailSmsWhatsappActivationRepository, EmailSmsWhatsappActivationRepository>();
            services.AddTransient<IReportInputRepository, ReportInputRepository>();
            services.AddHttpClient<IMetaAuthRepository, MetaAuthRepository>();
            services.AddTransient<ISmsRepository, SmsRepository>();
            services.AddTransient<ICurrencyMasterRepository, CurrencyMasterRepository>();
            services.AddTransient<IExchangeRateRepository, ExchangeRateRepository>();
            services.AddTransient<IBatchRepository, BatchRepository>();
            services.AddScoped<IMdHashValidator, MdHashValidator>();
            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });


            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
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

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
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


        }
    }
}
