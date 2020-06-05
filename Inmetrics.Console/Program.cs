using Microsoft.Extensions.Hosting;
using CrossCutting.DI;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Inmetrics.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();

            var connectionConfigWorker = host.Services.GetService<DbConnectionConfigWorker>();
            connectionConfigWorker.ConfigureConnectionString();

            var worker = host.Services.GetService<ConsoleWorker>();
            
            worker.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
              .ConfigureServices((hostContext, services) =>
              {
                  hostContext.Configuration = DIModule.ConfigureJson(); ;
                  services.ConfigureServices(hostContext.Configuration);
                  services.AddScoped<DbConnectionConfigWorker>();
                  services.AddScoped<ConsoleWorker>();
              });
    }
}
