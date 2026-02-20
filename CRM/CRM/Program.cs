using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BuildExeServices
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        
        // attach Serilog as a logging service 
            // while building the host
        //    .ConfigureLogging(logger =>
        //    {
        //    logger.AddSerilog(_serilogLogger);
        //});
    }
}
