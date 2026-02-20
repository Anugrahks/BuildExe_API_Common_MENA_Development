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
using BuildExeMaterialServices.DBContexts;
using Microsoft.EntityFrameworkCore;
using BuildExeMaterialServices.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BuildExeMaterialServices.Library;

namespace BuildExeMaterialServices
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
            services.AddDbContext<MaterialContext>(o => o.UseSqlServer(Configuration.GetConnectionString("DbConnection")));
            services.AddTransient<IUnitRepository, UnitRepository >();
            services.AddTransient<IBrandRepository, BrandRepository >();
            services.AddTransient<IMaterialCategoryRepository , MaterialCategoryRepository >();
            services.AddTransient<IMaterialRepository, MaterialRepository >();
            services.AddTransient<ISupplierRepository , SupplierRepository >();
            services.AddTransient<IIndentRepository, IndentRepository>();
            services.AddTransient<IPurchaseOrderRepository, PurchaseOrderRepository>();
            services.AddTransient<IStockRepository, StockRepository>();
            services.AddTransient<IPurchaseRepository , PurchaseRepository>();
            services.AddTransient<IDamageStockRepository, DamageStockRepository>();
            services.AddTransient<IPurchaseReturnRepository , PurchaseReturnRepository >();
            services.AddTransient <IMaterialUsageRepository, MaterialUsageRepository>();
            services.AddTransient<IMaterialTransferRepository, MaterialTransferRepository>();
            services.AddTransient<IMaterialReceiveRepository , MaterialReceiveRepository>();
            services.AddTransient<ISupplierAdvanceRepository , SupplierAdvanceRepository>();
            services.AddTransient<IQuotationRepository, QuotationRepository >();
          
            services.AddTransient<IUserLogRepository, UserLogRepository >();
            services.AddTransient<ISupplierPaymentRepository , SupplierPaymentRepository >();
            services.AddTransient<IMaterialListRepository , MaterialListRepository>();
            services.AddTransient<IPurchaseForPaymentRepository , PurchaseForPaymentRepository >();
            services.AddTransient<IAdvanceBalanceRepository, AdvanceBalanceRepository>();
            services.AddTransient<IItemIntakeRepository, ItemIntakeRepository>();
            services.AddTransient<IItemReturnTransferRepository, ItemReturnTransferRepository>();
            services.AddTransient<ITaskMasterRepository, TaskMasterRepository>();
            services.AddTransient<IFinishedGoodsRepository, FinishedGoodsRepository>();
            services.AddTransient<IMaterialProductionRepository, MaterialProductionRepository>();
            services.AddTransient<ISalesOrderRepository, SalesOrderRepository>();
            services.AddTransient<IMaterialSalesRepository, MaterialSalesRepository>();
            services.AddTransient<ICustomerRegistrationRepository, CustomerRegistrationRepository>();
            services.AddTransient<IIssueReturnRepository, IssueReturnRepository>();
            services.AddTransient<IAssetDetailRepository, AssetDetailRepository>();
            services.AddTransient<IOverHeadRepository, OverHeadRepository>();
            services.AddTransient<IAssetAppreciationRepository, AssetAppreciationRepository>();
            services.AddTransient<IDeliveryOrderRepository, DeliveryOrderRepository>();
            services.AddTransient<IMaterialListByCategoryRepository, MaterialListByCategoryRepository>();
            services.AddTransient<IStockAdjustmentRepository, StockAdjustmentRepository>();
            services.AddTransient<IMaterialSalesReturnRepository, MaterialSalesReturnRepository>();
            services.AddTransient<IMaterialDeliveryOrderRepository, MaterialDeliveryOrderRepository>();
            services.AddScoped<IMdHashValidator, MdHashValidator>();

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

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
