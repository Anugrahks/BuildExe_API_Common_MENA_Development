using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using BuildExeServices.DBContexts;
using Microsoft.EntityFrameworkCore;
using BuildExeServices.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Data.SqlClient;
using System.IO;
using BuildExeServices.Library;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace BuildExeServices
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
            services.AddHttpClient<IFacebookRepository, FacebookRepository>();
            services.AddDbContext<ProductContext>(o => o.UseSqlServer(Configuration.GetConnectionString("DbConnection")));
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IDesignationRepository, DesignationRepository>();
            services.AddTransient<IEnquiryModeRepository, EnquiryModeRepository>();
            services.AddTransient<IEmployeeDepartmentRepository, EmployeeDepartmentRepository>();
            services.AddTransient<IBranchRepository, BranchRepository>();
            services.AddTransient<IEmployeeMasterRepository, EmployeeMasterRepository>();
            services.AddTransient<IEnquiryRepository, EnquiryRepository>();
            services.AddTransient<IFollowupRepository, FollowupRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IProjectMasterRepository, ProjectMasterRepository>();
            services.AddTransient<IFinancialYearRepository, FinancialYearRepository>();
            services.AddTransient<IUserGroupRepository, UserGroupRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ILevelRepository, LevelRepository>();
            services.AddTransient<ILevelSettingRepository, LevelSettingRepository>();
            services.AddTransient<IEnquiryForRepository, EnquiryForRepository>();
            services.AddTransient<IEnquiryStatusRepository, EnquiryStatusRepository>();
            services.AddTransient<ITeamRepository, TeamRepository>();
            services.AddTransient<IBlockRepository, BlockRepository>();

            services.AddTransient<IFloorRepository, FloorRepository>();
            services.AddTransient<IOwnProjectRepository, OwnProjectRepository>();
            services.AddTransient<IConsultancyWorkRepository, ConsultancyWorkRepository>();
            services.AddTransient<IProjectConsultancyRepository, ProjectConsultancyRepository>();
            services.AddTransient<IProjectStageRepository, ProjectStageRepository>();
            services.AddTransient<IProjectBlockFloorAssignRepository, ProjectBlockFloorAssignRepository>();
            services.AddTransient<IProjectBookingRepository, ProjectBookingRepository>();
            services.AddTransient<IUserLogRepository, UserLogRepository>();
            services.AddTransient<IGovtProjectRepository, GovtProjectRepository>();
            services.AddTransient<IOwnProjectTypeRepository, OwnProjectTypeRepository>();
            services.AddTransient<IProjectPaymentModeRepository, ProjectPaymentModeRepository>();
            services.AddTransient<IProjectDivisionRepository, ProjectDivisionRepository>();

            services.AddTransient<IWorkTypeRepository, WorkTypeRepository>();
            services.AddTransient<ISpecificationMasterRepository, SpecificationMasterRepository>();
            services.AddTransient<IProjectSpecificationRepository, ProjectSpecificationRepository>();
            services.AddTransient<ITemplateRepository, TemplateRepository>();
            services.AddTransient<IRateEvaluationRepository, RateEvaluationRepository>();
            services.AddTransient<IClientAdvanceRepository, ClientAdvanceRepository>();

            services.AddTransient<IAdditionalBillRepository, AdditionalBillRepository>();
            services.AddTransient<IPartBillRepository, PartBillRepository>();
            services.AddTransient<IPercentageBillRepository, PercentageBillRepository>();
            services.AddTransient<IRecieptsRepository, RecieptsRepository>();
            services.AddTransient<IPendingClientBillsRepository, PendingClientBillsRepository>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IUserProjectRepository, UserProjectRepository>();
            services.AddTransient<ITendorSubmittedRepository, TendorSubmittedRepository>();
            services.AddTransient<ITendorAnalysisRepository, TendorAnalysisRepository>();
            services.AddTransient<ITendorNegotiationRepository, TendorNegotiationRepository>();
            services.AddTransient<ITendorWorkOrderRepository, TendorWorkOrderRepository>();
            services.AddTransient<IRefundRepository, RefundRepository>();
            services.AddTransient<ICoApplicantRepository, CoApplicantRepository>();
            services.AddTransient<IMenuRepository, MenuRepository>();
            services.AddTransient<IUserPrevilegeRepository, UserPrevilegeRepository>();
            services.AddTransient<IBoqMasterRepository, BoqMasterRepository>();
            services.AddTransient<IWeeklyBillRepository, WeeklyBillRepository>();
            services.AddTransient<IProjectBookingCancellationRepository, ProjectBookingCancellationRepository>();
            services.AddTransient<IAdvanceBalanceRepository, AdvanceBalanceRepository>();
            services.AddTransient<IRefundBalanceRepository, RefundBalanceRepository>();
            services.AddTransient<IWorkNameRepository, WorkNameRepository>();
            services.AddTransient<IReportHeaderSettingsRepository, ReportHeaderSettingsRepository>();
            services.AddTransient<IDivisionRepository, DivisionRepository>();
            services.AddTransient<IWorkEnquiryStageSettingsRepository, WorkEnquiryStageSettingsRepository>();
            services.AddTransient<IProjectWorkSettingRepository, ProjectWorkSettingRepository>();
            services.AddTransient<ISyncProjectRepository, SyncProjectRepository>();
            services.AddTransient<ICancellationRepository, CancellationRepository > ();
            services.AddTransient<ISyncTablesRepository, SyncTablesRepository>();
            services.AddTransient<IWaterMarkSettingRepository, WaterMarkSettingRepository>();
            services.AddTransient<IJobCreationRepository, JobCreationRepository>();
            services.AddTransient<IUniqueIdRepository, UniqueIdRepository>();
            services.AddTransient<ITimeSchedulerRepository, TimeSchedulerRepository>();
            services.AddScoped<IMdHashValidator, MdHashValidator>();
            services.AddTransient<IPropertyRepository, PropertyRepository>();
            services.AddTransient<IApprovalViewRepository, ApprovalViewRepository>();
            services.AddTransient<IEnquiryProfessionRepository, EnquiryProfessionRepository>();
            services.AddTransient<IQCRepository, QCRepository>();
            services.AddTransient<IReturnRepository, ReturnRepository>();
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
            //app.UseCors(options => options.WithOrigins("http://localhost:4200/").AllowAnyOrigin());

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<TokenValidationMiddleware>();
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
